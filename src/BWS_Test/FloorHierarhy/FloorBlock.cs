using System.Collections.Generic;

namespace BWS_Plugin.FloorHierarhy
{
    internal class FloorBlock
    {
        public SortedDictionary<int, RoomZone> RoomZones { get; set; } = new SortedDictionary<int, RoomZone>();
    }
}
