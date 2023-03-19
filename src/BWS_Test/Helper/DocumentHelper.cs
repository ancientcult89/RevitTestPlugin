using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Architecture;
using BWS_Plugin.FloorHierarhy;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BWS_Plugin.Helper
{
    static internal class DocumentHelper
    {
        public static ICollection<Element> GetAllRooms(FilteredElementCollector elements)
        { 
            return elements.OfCategory(BuiltInCategory.OST_Rooms)
                .WhereElementIsNotElementType()
                .ToElements()
                .Where(x => (bool)(x.GetParameters("ROM_Зона")
                ?.FirstOrDefault()?.AsString()
                ?.Contains("Квартира")))
                .ToList();
        }

        public static Dictionary<string, HouseFloor> GetHouseWithHierarhy(ICollection<Element> elements)
        {
            Dictionary<string, HouseFloor> house = new Dictionary<string, HouseFloor>();
            foreach (Room room in elements)
            {
                //номер этажа
                string roomLevel = room.Level.Name;
                //номер секции
                string blockNumber = room.GetParameters("BS_Блок")?.FirstOrDefault()?.AsString();
                //тип квартиры - одно, двух, трёх и т.д. комнатная
                string subZone = room.GetParameters("ROM_Подзона")?.FirstOrDefault()?.AsString();
                //номер квартиры
                int zone = Int16.Parse(room.GetParameters("ROM_Зона")?.FirstOrDefault()?.AsString().Replace("Квартира ", ""));
                //параметр вида 1к/2к/3к и т.д.
                string calculatedSubZone = room.GetParameters("ROM_Расчетная_подзона_ID")?.FirstOrDefault()?.AsString();

                if (!house.ContainsKey(roomLevel))
                    house.Add(roomLevel, new HouseFloor());

                if (!house[roomLevel].FloorBlocks.ContainsKey(blockNumber))
                    house[roomLevel].FloorBlocks.Add(blockNumber, new FloorBlock());

                if (!house[roomLevel].FloorBlocks[blockNumber].RoomZones.ContainsKey(zone))
                    house[roomLevel].FloorBlocks[blockNumber].RoomZones.Add(zone, new RoomZone() { CalculatedSubzoneID = calculatedSubZone });

                house[roomLevel].FloorBlocks[blockNumber].RoomZones[zone].Rooms.Add(room);
            }
            return house;
        }

        public static void ColorizeByHalfTone(Dictionary<string, HouseFloor> house)
        {
            const string suff = ".Полутон";

            foreach (var houseFloor in house)
            {
                foreach (var floorBlocks in houseFloor.Value.FloorBlocks)
                {
                    string prevRoomCount = null;
                    string currentRoomCount = "";
                    string prevRoomColor = "";

                    foreach (var roomZone in floorBlocks.Value.RoomZones)
                    {
                        string possibleRoomColor = currentRoomCount + suff;
                        currentRoomCount = roomZone.Value.CalculatedSubzoneID;

                        if (currentRoomCount == prevRoomCount)
                        {
                            if (possibleRoomColor != prevRoomColor)
                            {
                                foreach (Room room in roomZone.Value.Rooms)
                                {
                                    Parameter roomParam = room.GetParameters("ROM_Подзона_Index").FirstOrDefault();
                                    room.GetParameters("ROM_Подзона_Index").FirstOrDefault().Set(possibleRoomColor);
                                }
                                prevRoomColor = possibleRoomColor;
                            }
                            else
                            {
                                prevRoomColor = "";
                            }
                        }
                        else
                        {
                            prevRoomColor = "";
                        }
                        prevRoomCount = currentRoomCount;
                    }
                }
            }
        }
    }
}
