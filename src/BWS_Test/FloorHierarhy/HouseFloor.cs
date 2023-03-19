using System.Collections.Generic;

namespace BWS_Plugin.FloorHierarhy
{
    internal class HouseFloor
    {
        public Dictionary<string, FloorBlock> FloorBlocks { get; set; } = new Dictionary<string, FloorBlock>();
    }
}
