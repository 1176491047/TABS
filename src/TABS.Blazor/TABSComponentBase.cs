using TABS.Localization;
using Volo.Abp.AspNetCore.Components;

namespace TABS.Blazor;

public abstract class TABSComponentBase : AbpComponentBase
{
    protected TABSComponentBase()
    {
        LocalizationResource = typeof(TABSResource);
    }
}
