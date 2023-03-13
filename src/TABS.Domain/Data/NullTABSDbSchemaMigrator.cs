using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace TABS.Data;

/* This is used if database provider does't define
 * ITABSDbSchemaMigrator implementation.
 */
public class NullTABSDbSchemaMigrator : ITABSDbSchemaMigrator, ITransientDependency
{
    public Task MigrateAsync()
    {
        return Task.CompletedTask;
    }
}
