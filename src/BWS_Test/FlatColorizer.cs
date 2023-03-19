using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using BWS_Plugin.FloorHierarhy;
using BWS_Plugin.Helper;
using System.Collections.Generic;
using System.Linq;

namespace BWS_Plugin
{
    [Transaction(TransactionMode.Manual)]
    [Regeneration(RegenerationOption.Manual)]
    internal class FlatColorizer : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            Document doc = commandData.Application.ActiveUIDocument.Document;

            if (doc == null)
            {
                TaskDialog.Show("Отсутсвует документ", "Не удалось обнаружить активный документ");
                return Result.Cancelled;
            }

            FilteredElementCollector filteredElements = new FilteredElementCollector(doc);
            ICollection<Element> allRooms = DocumentHelper.GetAllRooms(filteredElements);

            if (allRooms.Count() == 0)
            {
                TaskDialog.Show("Квартиры не обнаружены", "В открытом файле не обнаружено разбивки на квартиры");
                return Result.Cancelled;
            }

            Dictionary<string, HouseFloor> house = DocumentHelper.GetHouseWithHierarhy(allRooms);

            using (Transaction t = new Transaction(doc))
            {
                t.Start("Красим рядомстоящие квартиры, если количество комнат совпадает");
                DocumentHelper.ColorizeByHalfTone(house);
                t.Commit();
            }

            return Result.Succeeded;
        }
    }
}
