using System;

class Enemy
{
    public string Name { get; set; }
    public int maxHP { get; set; }
    public int maxUlt { get; set; }
    public int HP { get; set; }
    public int ultMeter { get; set; }
    public string Move { get; set; }
    public string Ult { get; set; }
    public int ATK { get; set; }

    public int Damage { get; set; }

    public int Heal { get; set; }
    public Enemy(string enemyName, int enemyMaxHP, int enemyMaxUlt, string moveName, string enemyUltName, int enemyATKStat)
    {
        Name = enemyName;
        maxHP = enemyMaxHP;
        maxUlt = enemyMaxUlt;
        HP = maxHP;
        ultMeter = 0;
        Move = moveName;
        Ult = enemyUltName;
        ATK = enemyATKStat;
        Damage = 0;
        Heal = 0;
    }
}
