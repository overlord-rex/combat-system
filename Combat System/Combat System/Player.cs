using System;

class Player
{
    public String Name { get; set; }
    public int maxHP { get; set; }
    public int maxMana { get; set; }
    public int maxUlt { get; set; }
    public int HP { get; set; }
    public int Mana { get; set; }
    public int ultMeter { get; set; }

    public string Ult { get; set; }
    public int ATK { get; set; }

    public int Damage { get; set; }

    public int Heal { get; set; }
    public string[] Spellbook { get; set; }

    public Player(string nameInput, int maxHealth, int maxManaPool, int maxUltCharge, string ultName, int ATKStat, string[] spellList)
    {
        Name = nameInput;
        maxHP = maxHealth;
        maxMana = maxManaPool;
        maxUlt = maxUltCharge;
        HP = maxHP;
        Mana = maxMana;
        ultMeter = 0;
        Ult = ultName;
        ATK = ATKStat;
        Damage = 0;
        Heal = 0;
        Spellbook = spellList;
    }
}