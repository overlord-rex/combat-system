using System;
using System.Threading;
class Program
//Initialises an object for the player and one for the enemy
{
    public static (Player, Enemy) Initialise()
    {
        Console.WriteLine("Enter Name: ");
        string nameInput = Console.ReadLine();
        int maxHealth = 20;
        int maxManaPool = 10;
        int maxUltCharge = 5;
        string ultName = "Big Bang Bash";
        int ATKStat = 5;
        string[] spellList = { "Life Vamp", "Darkstrike", "Frost Shot" };

        Player player1 = new Player(nameInput, maxHealth, maxManaPool, maxUltCharge, ultName, ATKStat, spellList);

        string enemyName = "Goon";
        int enemyMaxHP = 40;
        int enemyMaxUlt = 5;
        string moveName = "Pistol Shot";
        string enemyUltName = "Double Shot";
        int enemyATKStat = 3;
        Enemy enemy1 = new Enemy(enemyName, enemyMaxHP, enemyMaxUlt, moveName, enemyUltName, enemyATKStat);

        Console.WriteLine();
        return (player1, enemy1);
    }

    static (Player, Enemy) magic(Player player1, Enemy enemy1)  //Magic function
    {
        foreach (string i in player1.Spellbook)
        {
            Console.WriteLine(i);
        }
        bool valid = false;
        while (!valid)
        {
            valid = true;
            Console.WriteLine();
            Console.WriteLine("Enter ability: ");
            string action = Console.ReadLine().ToLower();  //Selects chosen move
            Console.WriteLine();
            int manaCost = 4;
            if (action == "frost shot" && player1.Mana >= 4)
            {
                enemy1.Damage = 5;
                manaCost = 4;
            }
            else if (action == "life vamp" && player1.Mana >= 6)
            {
                enemy1.Damage = player1.ATK;
                player1.Heal = enemy1.Damage;
                manaCost = 6;
            }
            else if (action == "darkstrike" && player1.Mana >= 8)
            {
                enemy1.Damage = 9;
                manaCost = 8;
            }
            else  //Not a valid spell, or not enough mana
            {
                valid = false;
                int counter = 0;
                bool noMana = false;
                while ((noMana == false) && (counter < player1.Spellbook.Length)) //Sees if name matches a taken spell
                {
                    if (action == player1.Spellbook[counter].ToLower())
                    {
                        noMana = true;
                    }
                    else
                    {
                        counter++;
                    }
                }
                if (noMana == false)
                {
                    Console.WriteLine("Invalid Spell");
                }
                else
                {
                    Console.WriteLine("Not enough mana");
                }
            }

            player1.Mana = player1.Mana - manaCost; //Does mana cost

        }
        return (player1, enemy1);
    }

    static (Player, Enemy) damageReport(Player player1, Enemy enemy1) //DAMAGE REPORT!!
    {
        if (enemy1.Damage > 0)
        {
            enemy1.HP = enemy1.HP - enemy1.Damage;
            Console.WriteLine($"Hit for {enemy1.Damage}!");
            enemy1.Damage = 0; //Resets damage to 0
        }
        if (player1.Damage > 0)
        {
            player1.HP = player1.HP - player1.Damage;
            Console.WriteLine($"-{player1.Damage}");
            player1.Damage = 0; //Resets damage to 0
        }
        return (player1, enemy1);
    }

    static void showStats(Player player1, Enemy enemy1)
    {
        Thread.Sleep(200);
        Console.WriteLine();
        Console.WriteLine($"{player1.Name}: {player1.HP}/{player1.maxHP}    {enemy1.Name}: {enemy1.HP}/{enemy1.maxHP}");
        Console.WriteLine($"Mana: {player1.Mana}/{player1.maxMana}");
        Console.WriteLine($"Ults: {player1.ultMeter}/{player1.maxUlt}       {enemy1.ultMeter}/{enemy1.maxUlt}");
        Console.WriteLine("------------------------------");
    }

    static (Player, Enemy) playerUltimate(Player player1, Enemy enemy1)
    {
        enemy1.Damage = 0;
        Console.WriteLine($"{player1.Name} used {player1.Ult}!");
        Thread.Sleep(500);
        if (player1.Ult == "Big Bang Bash")
        {
            Console.WriteLine($"{player1.Name} swings a hammer...");
            Console.WriteLine($"BOOM!");
            enemy1.Damage = 10;
        }
        return (player1, enemy1);
    }
    static (Player, Enemy) enemyUltimate(Player player1, Enemy enemy1)
    {
        player1.Damage = 0;
        Console.WriteLine($"{enemy1.Name} used {enemy1.Ult}!");
        Thread.Sleep(1000);
        if (enemy1.Ult == "Double Shot")   //hide this all when you upload it
        {
            for (int i = 0; i < 2; i++)
            {
                Console.WriteLine("Bang!");
                Thread.Sleep(200);
                player1.Damage = player1.Damage + enemy1.ATK;
            }
        }
        return (player1, enemy1);
    }
    static (Player, Enemy) playerMove(Player player1, Enemy enemy1)
    {
        Console.WriteLine("ATTACK   ???   MAGIC   ULTIMATE");
        Console.WriteLine();
        bool valid = false;
        enemy1.Damage = 0;
        while (!valid)
        {
            valid = true;
            Console.WriteLine("Enter move: ");
            string action = Console.ReadLine().ToLower();  //Selects chosen move
            Console.WriteLine();
            if (action == "attack")
            {
                enemy1.Damage = player1.ATK;
            }
            else if (action == "magic")
            {
                (player1, enemy1) = magic(player1, enemy1);
            }
            else if (action == "ultimate")
            {
                (player1, enemy1) = playerUltimate(player1, enemy1);
            }
            else
            {
                valid = false;  //runs while loop again with invalid input
            }
        }
        (player1, enemy1) = damageReport(player1, enemy1); //Inflicts damage, objects store a damage variable
        return (player1, enemy1);
    }

    static (Player, Enemy) enemyMove(Player player1, Enemy enemy1)
    {
        Console.WriteLine($"{enemy1.Name} used {enemy1.Move}");
        player1.Damage = enemy1.ATK;
        return (player1, enemy1);
    }

    static (Player, Enemy) yourTurn(Player player1, Enemy enemy1)
    {
        (player1, enemy1) = playerMove(player1, enemy1);
        if (player1.ultMeter < player1.maxUlt)
        {
            player1.ultMeter = player1.ultMeter + 1;
        }
        return (player1, enemy1);
    }

    static (Player, Enemy) enemyTurn(Player player1, Enemy enemy1)  //enemyMove is only basics attacks, so damage report at end of this procedure
    {
        if (enemy1.ultMeter == enemy1.maxUlt)
        {
            (player1, enemy1) = enemyUltimate(player1, enemy1);
            enemy1.ultMeter = 0;
        }
        else
        {
            (player1, enemy1) = enemyMove(player1, enemy1);
        }
        (player1, enemy1) = damageReport(player1, enemy1);
        if (enemy1.ultMeter < player1.maxUlt) //Increment enemy ult
        {
            enemy1.ultMeter = enemy1.ultMeter + 1;
        }
        player1.Mana = player1.Mana + 2;  //Mana regen
        if (player1.Mana > 10)
        {
            player1.Mana = 10;
        }
        return (player1, enemy1);
    }


    static (bool, string) getResult(Player player1, Enemy enemy1)
    {
        bool run = false;
        string winner = "none";
        if (player1.HP <= 0)
        {
            if (enemy1.HP <= 0)
            {
                winner = "draw";
            }
            else
            {
                winner = "enemy";
            }
        }
        else if (enemy1.HP <= 0)
        {
            winner = "player";
        }
        else
        {
            run = true;
        }
        return (run, winner);
    }

    static string Step(Player player1, Enemy enemy1)  //Controls turn order
    {
        bool run = true;
        int turnCount = 1;
        string winner = "none";
        while (run)   //Run set to false when the battle ends
        {
            showStats(player1, enemy1);
            Thread.Sleep(1000);
            if ((turnCount % 2) == 1)  //Deterimes who's turn it is
            {
                (player1, enemy1) = yourTurn(player1, enemy1);
                (run, winner) = getResult(player1, enemy1);
            }


            else
            {
                (player1, enemy1) = enemyTurn(player1, enemy1);
                (run, winner) = getResult(player1, enemy1);
            }
            turnCount++;
        }
        return winner; //Placeholder to return winner
    }
    public static void victoryScreen(Player player1, Enemy enemy1, string winner)
    {
        if (winner == "player")
        {
            Console.WriteLine($"{player1.Name} defeated {enemy1.Name}");
        }
        else
        {
            Console.WriteLine("GAME OVER");
        }
    }
    public static void Main(string[] args)
    {
        (Player player1, Enemy enemy1) = Initialise();
        string winner = Step(player1, enemy1);
        victoryScreen(player1, enemy1, winner);
    }
}