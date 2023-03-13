using Volo.Abp.Modularity;

namespace TABS;

[DependsOn(
    typeof(TABSApplicationModule),
    typeof(TABSDomainTestModule)
    )]
public class TABSApplicationTestModule : AbpModule
{

}
