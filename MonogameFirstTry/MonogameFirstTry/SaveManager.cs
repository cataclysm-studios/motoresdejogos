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
        
        public static List<Ship> activeShips;
        public static List<Ship> inactiveShips;
        protected string json;
        protected Message debugJsonMessage = new Message(MessageType.Console, "");

        public void Initialize()
        {
            activeShips = new List<Ship>();
            inactiveShips = new List<Ship>();
        }
        
        public void SaveShipStates(Ship[] ships)
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
        }

        public void ConvertToJson()
        {
            json = JsonConvert.SerializeObject(activeShips) + JsonConvert.SerializeObject(inactiveShips);
            debugJsonMessage.MessageText = json;
            MessageBus.Instance.AddMessage(debugJsonMessage);
            File.WriteAllText(@"C:\Users\Pedro\Desktop\ships.json", json);
        }

        public void JsonBackToLists()
        {

        }
    }
}
