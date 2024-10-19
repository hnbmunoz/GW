namespace MLAB.PlayerEngagement.Infrastructure.Config;

public class ConnectionString
{
    public string MicroDb { get; set; }
    public  string RabbitMqRootUri { get; set; }
    public  string RabbitMqUserName { get; set; }
    public  string RabbitMqPassword { get; set; }
    public string MlabDb { get; set; }
    public string IntegrationDb { get; set; }
    public string TicketManagementDb { get; set; }

    public string UserManagementDb { get; set; }
    public string PlayerManagementDb { get; set; }
    public string StorageConnectionString { get; set; }
    public string ContainerName { get; set; }
    public string PlayerManagementDbSecondary { get; set; }
    public string MlabDbSecondary { get; set; }
}
