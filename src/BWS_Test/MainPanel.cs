using VCRevitRibbonUtil;
using Autodesk.Revit.UI;
using BWS_Plugin.Properties;

namespace BWS_Plugin
{
    internal class MainPanel : IExternalApplication
    {
        public Result OnStartup(UIControlledApplication application)
        {
            Ribbon.GetApplicationRibbon(application)
                .Tab("Custom plugin")
                .Panel("Plugins")
                .CreateButton<FlatColorizer>("Сolorize", "color the floor plan", b => 
                    b.SetLargeImage(Resources.icon32)
                        .SetSmallImage(Resources.icon16)
                        .SetLongDescription("color the floor plan"));

            return Result.Succeeded;
        }

        public Result OnShutdown(UIControlledApplication application)
        {
            return Result.Succeeded;
        }
    }
}
