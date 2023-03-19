using Autodesk.Revit.DB.Architecture;
using System.Collections.Generic;

namespace BWS_Plugin.FloorHierarhy
{
    internal class RoomZone
    {
        public List<Room> Rooms { get; set; } = new List<Room>();
        public string CalculatedSubzoneID { get; set; }
    }
}
