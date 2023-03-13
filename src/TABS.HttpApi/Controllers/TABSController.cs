using TABS.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace TABS.Controllers;

/* Inherit your controllers from this class.
 */
public abstract class TABSController : AbpControllerBase
{
    protected TABSController()
    {
        LocalizationResource = typeof(TABSResource);
    }
}
