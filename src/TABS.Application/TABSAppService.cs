using TABS.Localization;
using Volo.Abp.Application.Services;

namespace TABS;

/* Inherit your application services from this class.
 */
public abstract class TABSAppService : ApplicationService
{
    protected TABSAppService()
    {
        LocalizationResource = typeof(TABSResource);
    }
}
