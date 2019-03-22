using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonogameFirstTry
{
    public class SaveManager
    {
        #region Singleton
        private static SaveManager _saveManager;
        public static SaveManager Instance
        {
            get
            {
                if (_saveManager == null)
                {
                    _saveManager = new SaveManager();
                }
                return _saveManager;
            }

        }
        #endregion
        
        //public static List<Ship> activeShips;
        //public static List<Ship> inactiveShips;
        public static List<Ship> allShips;
        protected Message debugJsonMessage = new Message(MessageType.Console, "");

        public void Initialize()
        {
            allShips = new List<Ship>();
            //activeShips = new List<Ship>();
            //inactiveShips = new List<Ship>();
        }
        
        /*public void SaveShipStates(Ship[] ships)
        {
            for (int i = 0; i < ships.Length; i++)
            {
                if(ships[i].ShipActive)
                {
                    activeShips.Add(ships[i]);
                }
                else
                {
                    inactiveShips.Add(ships[i]);
                }
            }
        }*/
        public void UpdateShipList(List<Ship> ships)
        {
            allShips.Clear();
            foreach (Ship ship in ships)
            {
                allShips.Add(ship);
            }
        }

        public void SaveShipStates()
        {
            //json = JsonConvert.SerializeObject(activeShips) + JsonConvert.SerializeObject(inactiveShips);
            string json = JsonConvert.SerializeObject(allShips);
            File.WriteAllText(@"" + Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "/ships.json", json);
            //debugJsonMessage.MessageText = json;
            //MessageBus.Instance.AddMessage(debugJsonMessage);
            debugJsonMessage.MessageText = "Saved All Ships info at: " + Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "/ships.json";
            MessageBus.Instance.AddMessage(debugJsonMessage);
        }

        public void LoadShipStates(List<Ship> ships, ResourceManager resourceManager)
        {
            allShips.Clear();
            string json = File.ReadAllText(@"" + Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "/ships.json");
            allShips = JsonConvert.DeserializeObject<List<Ship>>(json);
            ships.Clear();
            debugJsonMessage.MessageText = "Loaded All Ships info from: " + Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "/ships.json";
            foreach (Ship loadedShip in allShips)
            {
                ships.Add(loadedShip);
                loadedShip.LoadModel(resourceManager);
                //loadedShip.SetPosition()
                /*foreach (Ship ship in ships)
                {

                }*/
            }
            MessageBus.Instance.AddMessage(debugJsonMessage);
        }
    }
}
