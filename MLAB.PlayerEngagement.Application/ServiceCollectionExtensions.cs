using MediatR;
using Microsoft.Extensions.DependencyInjection;
using MLAB.PlayerEngagement.Application.Handlers;
using MLAB.PlayerEngagement.Application.Services;
using MLAB.PlayerEngagement.Core.Repositories;
using MLAB.PlayerEngagement.Core.Services;
using MLAB.PlayerEngagement.Infrastructure.Repositories;
using System.Reflection;

namespace MLAB.PlayerEngagement.Application;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection RegisterApplicationServices(this IServiceCollection services)
    {
        services.AddMediatR(typeof(CreateQueueHandler).GetTypeInfo().Assembly);
        services.AddMediatR(typeof(CreateMemoryCacheHandler).GetTypeInfo().Assembly);
        services.AddMediatR(typeof(CreateExchangeQueueHandler).GetTypeInfo().Assembly);
        services.AddMediatR(typeof(GetAllCurrencyHandler).GetTypeInfo().Assembly);

        services.AddTransient<IMessagePublisherService, MessagePublisherService > ();
        services.AddTransient<IPlayerConfigurationService, PlayerConfigurationService>();
        services.AddTransient<ISystemService, SystemService>();
        services.AddTransient<IAuthenticationService, AuthenticationService>();
        services.AddTransient<IUserManagementService, UserManagementService>();
        services.AddTransient<IPlayerManagementService, PlayerManagementService>();
        services.AddTransient<ISegmentationService, SegmentationService>();
        services.AddTransient<ICampaignManagementService, CampaignManagementService>();
        services.AddTransient<IAgentWorkspaceService, AgentWorkspaceService>();
        services.AddTransient<ICallListValidationService, CallListValidationService>();
        services.AddTransient<IAgentMonitoringService, AgentMonitoringService>();
        services.AddTransient<ICampaignGoalSettingService, CampaignGoalSettingService>();
        services.AddTransient<ICampaignTaggingPointSettingService, CampaignTaggingPointSettingService>();
        services.AddTransient<IAdminstratorService, AdministratorService>();
        services.AddTransient<ICampaignDashboardService, CampaignDashboardService>();
        services.AddTransient<ICampaignPerformanceService, CampaignPerformanceService>();
        services.AddTransient<ICampaignCustomEventSettingService, CampaignCustomEventSettingService>();
        services.AddTransient<IRelationshipManagementService, RelationshipManagementService>();
        services.AddTransient<ICampaignJourneyService, CampaignJourneyService>();
        services.AddTransient<IRemIntegrationPublisherService, RemIntegrationPublisherService>();
        services.AddTransient<ISurveyAgentWidgetService, SurveyAgentWidgetService>();
        services.AddTransient<ICaseManagementService, CaseManagementService>();
        services.AddTransient<IChatbotService, ChatbotService>();
        services.AddTransient<IReportsService, ReportsService>();
        services.AddTransient<ITicketManagementService, TicketManagementService>();
        services.AddTransient<IEngagementHubService, EngagementHubService>();
        services.AddTransient<ISearchLeadsService, SearchLeadsService>();
        return services;
    }
}
