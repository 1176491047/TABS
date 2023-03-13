using TABS.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace TABS;

[DependsOn(
    typeof(TABSEntityFrameworkCoreTestModule)
    )]
public class TABSDomainTestModule : AbpModule
{

}
