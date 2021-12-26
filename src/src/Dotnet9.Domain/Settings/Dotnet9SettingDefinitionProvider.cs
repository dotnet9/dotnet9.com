using Volo.Abp.Settings;

namespace Dotnet9.Settings
{
    public class Dotnet9SettingDefinitionProvider : SettingDefinitionProvider
    {
        public override void Define(ISettingDefinitionContext context)
        {
            //Define your own settings here. Example:
            //context.Add(new SettingDefinition(Dotnet9Settings.MySetting1));
        }
    }
}
