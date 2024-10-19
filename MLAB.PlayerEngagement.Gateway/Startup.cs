using MLAB.PlayerEngagement.Infrastructure;
using MLAB.PlayerEngagement.Infrastructure.Config;
using StackExchange.Redis.Extensions.Core.Configuration;
using StackExchange.Redis.Extensions.Newtonsoft;
using MassTransit;
using MLAB.PlayerEngagement.Application;
using MLAB.PlayerEngagement.Core.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using MLAB.PlayerEngagement.Gateway.Extensions;
using MLAB.PlayerEngagement.Infrastructure.Logging;
using NLog;

namespace MLAB.PlayerEngagement.Gateway;

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container. Trigger Deployment.
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllers().AddJsonOptions(options =>
        {
            //options.JsonSerializerOptions.IgnoreNullValues = true;
        });

        services.AddSwaggerDocumentation();

        var key = Encoding.UTF8.GetBytes(Configuration["JWTKey"]);
        services.AddAuthentication(x =>
        {
            x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(x =>
        {
            x.RequireHttpsMetadata = false;
            x.SaveToken = true;
            x.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero
            };
        });

        services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        services.AddHttpContextAccessor();
        var emailConfiguration = new EmailConfig();
        Configuration.GetSection("EmailConfig").Bind(emailConfiguration);
        services.AddSingleton(emailConfiguration);
        services.Configure<ConnectionString>(Configuration.GetSection("ConnectionStrings"));
        services.Configure<AppSetting>(Configuration.GetSection("AppSetting"));
        services.Configure<IcoreEventIntegration>(Configuration.GetSection("IcoreEventIntegration"));
        services.Configure<FmboIntegration>(Configuration.GetSection("FmboIntegration"));
        services.Configure<ManualBalanceCorrectionIntegration>(Configuration.GetSection("ManualBalanceCorrectionIntegration"));
        services.Configure<HoldWithdrawalIntegration>(Configuration.GetSection("HoldWithdrawalIntegration"));
        services.RegisterDbFactoryServices();
        services.RegisterApplicationServices();
        services.AddHttpClient();
      //  services.AddHttpContextAccessor();
     //   services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
     LogManager.Configuration = new NLogConfiguration(Configuration.GetSection("NLog"));


        services.AddStackExchangeRedisExtensions<NewtonsoftSerializer>((options) =>
        {
            return Configuration.GetSection("Redis").Get<RedisConfiguration>();
        });

        services.AddMassTransit(x =>
        {
            x.AddBus(provider => Bus.Factory.CreateUsingRabbitMq(cfg =>
            {
                cfg.Host(new Uri(Configuration.GetConnectionString("RabbitMqRootUri")), h =>
                {
                    h.Username(Configuration.GetConnectionString("RabbitMqUserName"));
                    h.Password(Configuration.GetConnectionString("RabbitMqPassword"));
                });

                cfg.Publish<ExchangeQueue>(x =>
                {
                    x.Durable = true; // default: true
                    x.AutoDelete = true; // default: false
                });             
            }));
        });


        //services.AddMassTransitHostedService();


        services.AddCors(options => options.AddPolicy("CorsPolicy", builder =>
        {
            builder
               .AllowAnyMethod()
               .AllowAnyHeader()
               .WithOrigins(Configuration.GetConnectionString("FrontEndUrl"),
                            Configuration.GetConnectionString("AswUrl"),
                            Configuration.GetConnectionString("LcAswUrl"),
                            "http://localhost")
               .AllowCredentials();


        }));
    }
   

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {


        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
            app.UseSwaggerDocumentation();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "MLAB.PlayerEngagement.Gateway v1"));
        }

        app.UseHttpsRedirection();

        app.UseRouting();

        app.UseCors("CorsPolicy");

        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }
}
