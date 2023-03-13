using TABS.EntityFrameworkCore;
using Volo.Abp.Autofac;
using Volo.Abp.BackgroundJobs;
using Volo.Abp.Modularity;

namespace TABS.DbMigrator;

[DependsOn(
    typeof(AbpAutofacModule),
    typeof(TABSEntityFrameworkCoreModule),
    typeof(TABSApplicationContractsModule)
)]
public class TABSDbMigratorModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpBackgroundJobOptions>(options =>
        {
            options.IsJobExecutionEnabled = false;
        });
    }
}
