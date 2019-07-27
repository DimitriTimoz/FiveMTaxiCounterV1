using CitizenFX.Core;
using CitizenFX.Core.UI;
using System;
using System.Collections.Generic;

namespace Client
{
    public class Main : BaseScript
    {
        //Prix du kilomètre
        private double perKilometer = 10;
        //Prix minimum
        private int min = 5;

        //prix de l'heure le jour (2 mins IRL)
        private double perGTAHourDay = 3;
        //prix de l'heure la nuit (2 mins IRL)
        private double perGTAHourNight = 5;

        //Vitesse moyenne
        private double averageV = 0;
        //Adition des vitesses
        private double total = 0;
        //Prix
        private int price = 0;

        //Formule jour/nuit
        bool night = false;
        //Calcul le prix ?
        bool started = false;
        //Repètre temps 1/2 sec
        int time = 0;



        public Main()
        {
            EventHandlers["TaxiCounter:Client:StartCounter"] += new Action(Start);
            EventHandlers["TaxiCounter:Client:ClearCounter"] += new Action(Clear);
            EventHandlers["TaxiCounter:Client:ResumeCounter"] += new Action(Resume);
            EventHandlers["TaxiCounter:Client:StopCounter"] += new Action(Stop);
            EventHandlers["TaxiCounter:Client:ChooseNightTarif"] += new Action(ChooseNightTarif);
            EventHandlers["TaxiCounter:Client:ChooseDayTarif"] += new Action(ChooseDayTarif);
            EventHandlers["TaxiCounter:Client:Pay"] += new Action(Pay);
        }

        private void ChooseNightTarif()
        {
            night = true;
        }
        private void ChooseDayTarif()
        {
            night = false;
        }
        private void Start()
        { 
            Clear();
            started = true;
            Calcul();
        }

        private void Resume()
        {
            started = true;
            Calcul();
        }
        private void Clear()
        {
            total = 0;
            time = 0;
            price = 0;
        }

        private void Stop()
        {
            started = false;
        }

        private async void Calcul()
        {
            while (true)
            {
                await Delay(50);
                Player pl = new Player(1);
                Vehicle vl = pl.LastVehicle;
                if (!started)
                {
                    return;
                }

                //Gére le temps
                time++;

                //Calcul la vitesse moyenne
                total += (int)((vl.Speed) * 3.6);
                averageV = total / time;

                // V x T
                double distance = averageV * time / 3600 / 20;

                //Calcule le prix (Prix minimum + Prix du kilometre x distance + Minutes depuis le début x Prix de l'heure)
                if (night)
                {
                    price = (int)(min + perKilometer * distance + (time / 600 * perGTAHourNight));
                }
                else
                {
                    price = (int)(min + perKilometer * distance + (time / 600 * perGTAHourDay));
                }
                
                //Affiche le prix 
                Screen.ShowSubtitle("~b~Prix de la course: ~g~" + price.ToString() + " $", 51); 
            }
        }
        private void Pay()
        {
            //Fonction système de payement         
        }

        
    }
}
