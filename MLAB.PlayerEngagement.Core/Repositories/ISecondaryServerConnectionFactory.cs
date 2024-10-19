using MLAB.PlayerEngagement.Core.Constants;

namespace MLAB.PlayerEngagement.Core.Repositories
{
    public interface ISecondaryServerConnectionFactory
    {
        Task<DatabaseFactories> GetDatabaseToUseAsync(string appConfigSettingKey, DatabaseFactories primaryDb);
    }
}