using Volo.Abp.Settings;

namespace TABS.Settings;

public class TABSSettingDefinitionProvider : SettingDefinitionProvider
{
    public override void Define(ISettingDefinitionContext context)
    {
        //Define your own settings here. Example:
        //context.Add(new SettingDefinition(TABSSettings.MySetting1));
    }
}
