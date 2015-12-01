using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Game_Test
{
    class Player
    {
        //Fields
        private string Name;
        private bool Gender;
        private int TotalHP;
        private int TotalEnergy;
        private int TotalStamina;
        private int Gold;
        private int XP;
        private int Lvl;
        private List<Perks> PlayerPerks;
        private List<Items> Inventory;

        private int CurrentHP;
        private int CurrentEnergy;
        private int CurrentStamina;

        //Constructor
        public Player(string name, bool gender, Perks startperk)
        {
            this.Name = name;
            this.Gender = gender;
            this.TotalHP = 1; //Default Value
            this.TotalEnergy = 1; //Default Value
            this.TotalStamina = 1; //Default Value
            this.Gold = 1; //Default Value
            this.XP = 0; //Default Value
            this.Lvl = 1; //Default Value
            this.PlayerPerks = new List<Perks> {startperk };
            this.Inventory = new List<Items> { };

            this.CurrentHP = this.TotalHP;
            this.CurrentEnergy = this.TotalEnergy;
            this.CurrentStamina = this.TotalStamina;
        }

    }
}
