FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build

WORKDIR /

COPY ["MLAB.PlayerEngagement.Application/MLAB.PlayerEngagement.Application.csproj", "MLAB.PlayerEngagement.Application/"]
COPY ["MLAB.PlayerEngagement.Core/MLAB.PlayerEngagement.Core.csproj", "MLAB.PlayerEngagement.Core/"]
COPY ["MLAB.PlayerEngagement.Infrastructure/MLAB.PlayerEngagement.Infrastructure.csproj", "MLAB.PlayerEngagement.Infrastructure/"]
COPY ["MLAB.PlayerEngagement.Gateway/MLAB.PlayerEngagement.Gateway.csproj", "MLAB.PlayerEngagement.Gateway/"]

RUN dotnet restore "MLAB.PlayerEngagement.Gateway/MLAB.PlayerEngagement.Gateway.csproj"

COPY / .
WORKDIR "/MLAB.PlayerEngagement.Gateway/"

RUN dotnet build "MLAB.PlayerEngagement.Gateway.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "MLAB.PlayerEngagement.Gateway.csproj" -c Release -o /app

FROM build AS final
    WORKDIR /app
    
RUN addgroup --system --gid 1000 customgroup \
    && adduser --system --uid 1000 --ingroup customgroup --shell /bin/sh customuser

COPY --from=publish /app .

RUN chown -R customuser /app
RUN chown -R customuser /root

USER customuser

ENTRYPOINT [ "dotnet", "MLAB.PlayerEngagement.Gateway.dll" ]
