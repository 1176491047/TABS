using System.Threading.Tasks;

namespace TABS.Data;

public interface ITABSDbSchemaMigrator
{
    Task MigrateAsync();
}
