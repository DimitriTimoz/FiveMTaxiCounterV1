using CitizenFX.Core;
using CitizenFX.Core.Native;
using System;


namespace Server
{
    public class Main : BaseScript
    {
        public Main()
        {
            EventHandlers["chatMessage"] += new Action<int, string, string>(OnChatMessage);
        }
        private void OnChatMessage(int src, string color, string msg)
        {
            Player player = new PlayerList()[src];
            string[] args = msg.Split(' ');
            if(args[0] == "!start")
            {           
                player.TriggerEvent("TaxiCounter:Client:StartCounter");
                API.CancelEvent();
            }
            else if (args[0] == "!clear")
            {
                player.TriggerEvent("TaxiCounter:Client:ClearCounter");
                API.CancelEvent();
            }
            else if (args[0] == "!stop")
            {
                player.TriggerEvent("TaxiCounter:Client:StopCounter");
                API.CancelEvent();
            }
            else if (args[0] == "!resume")
            {
                player.TriggerEvent("TaxiCounter:Client:ResumeCounter");
                API.CancelEvent();
            }
            else if (args[0] == "!day")
            {
                player.TriggerEvent("TaxiCounter:Client:ChooseDayTarif");
                API.CancelEvent();
            }
            else if (args[0] == "!night")
            {
                player.TriggerEvent("TaxiCounter:Client:ChooseNightTarif");
                API.CancelEvent();
            }
            else if (args[0] == "!pay")
            {
                player.TriggerEvent("TaxiCounter:Client:Pay");
                API.CancelEvent();
            }
        }
    }
}
