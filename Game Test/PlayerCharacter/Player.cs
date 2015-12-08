using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Game_Test
{
    class Player
    {
        //Fields
        private string name;
        private bool gender;
        private int totalHP;
        private int totalEnergy;
        private int totalStamina;
        private int gold;
        private int xP;
        private int lvl;
        private List<Perks> playerPerks;
        private List<Items> inventory;

        private int currentHP;
        private int currentEnergy;
        private int currentStamina;

        //properties
        public string Name {  get { return this.name; } }
        public bool Gender { get { return this.gender; } }

        //Constructor
        public Player(string name, bool gender, Perks startperk)
        {
            this.name = name;
            this.gender = gender;
            this.totalHP = 1; //Default Value
            this.totalEnergy = 1; //Default Value
            this.totalStamina = 1; //Default Value
            this.gold = 1; //Default Value
            this.xP = 0; //Default Value
            this.lvl = 1; //Default Value
            this.playerPerks = new List<Perks> {startperk };
            this.inventory = new List<Items> { };

            this.currentHP = this.totalHP;
            this.currentEnergy = this.totalEnergy;
            this.currentStamina = this.totalStamina;
        }

    }
}
