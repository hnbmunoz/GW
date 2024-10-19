using Microsoft.Extensions.DependencyInjection;
using MLAB.PlayerEngagement.Core.Communications;
using MLAB.PlayerEngagement.Core.Logging;
using MLAB.PlayerEngagement.Core.Repositories;
using MLAB.PlayerEngagement.Infrastructure.Communications;
using MLAB.PlayerEngagement.Infrastructure.Logging.Implementations;
using MLAB.PlayerEngagement.Infrastructure.Repositories;

namespace MLAB.PlayerEngagement.Infrastructure;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection RegisterDbFactoryServices(this IServiceCollection services)
    {
        services.AddTransient<IQueueFactory, QueueRepository>();
        services.AddTransient<ICacheDbRepository, CacheDbRepository>();
        services.AddTransient<IQueuePublisher, QueuePublisher>();
        services.AddTransient<ISystemFactory, SystemFactory>();
        services.AddTransient<IUserFactor, UserFactor>();
        services.AddTransient<IPlayerFactor, PlayerFactor>();
        services.AddTransient<IMainDbFactory, MainDbFactory>();
        services.AddTransient<ISegmentationFactory, SegmentationFactory>();
        services.AddTransient<ICampaignManagementFactory, CampaignManagementFactory>();
        services.AddTransient<ICampaignTaggingPointSettingFactory, CampaignTaggingPointSettingFactory>();
        services.AddTransient<ICallListValidationFactory, CallListValidationFactory>();
        services.AddTransient<IAgentMonitoringFactory, AgentMonitoringFactory>();
        services.AddTransient(typeof(ILogger<>), typeof(GraylogsLogger<>));
        services.AddTransient<ILogger, GraylogsLogger>();
        services.AddSingleton<ILoggerFactory, GraylogsLoggerFactory>();
        services.AddTransient<IAgentWorkspaceFactory, AgentWorkspaceFactory>();
        services.AddTransient<ICampaignGoalSettingFactory, CampaignGoalSettingFactory>();
        services.AddTransient<ICampaignManagementFactory, CampaignManagementFactory>();
        services.AddTransient<IAdministratorFactory, AdminstratorFactory>();
        services.AddTransient<ICampaignDashboardFactory, CampaignDashboardFactory>();
        services.AddTransient<ICampaignPerformanceFactory, CampaignPerformanceFactory>();
        services.AddTransient<IPlayerConfigurationFactory, PlayerConfigurationFactory>();
        services.AddTransient<ICampaignJourneyFactory, CampaignJourneyFactory>();
        services.AddTransient<ISurveyAgentWidgetFactory, SurveyAgentWidgetFactory>();
        services.AddTransient<ICaseManagementFactory, CaseManagementFactory>();
        services.AddTransient<IChatbotFactory, ChatbotFactory>();
        services.AddTransient<IReportsFactory, ReportsFactory>();
        services.AddTransient<IEngagementHubFactory, EngagementHubFactory>();
        services.AddTransient<ITicketManagementFactory, TicketManagementFactory>();
        services.AddTransient<ISearchLeadsFactory, SearchLeadsFactory>();
        services.AddTransient<ISecondaryServerConnectionFactory, SecondaryServerConnectionFactory>();
        return services;
    }
}
