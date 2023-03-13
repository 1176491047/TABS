using Volo.Abp.DependencyInjection;
using Volo.Abp.Ui.Branding;

namespace TABS.Blazor;

[Dependency(ReplaceServices = true)]
public class TABSBrandingProvider : DefaultBrandingProvider
{
    public override string AppName => "TABS";
}
