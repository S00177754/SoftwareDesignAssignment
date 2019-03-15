using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AttackTest
{
    class Program
    {
        public enum SIGN { Rock, Paper, Scissor, Lizard, Spock };

        static void Main(string[] args)
        {
            Player[] Players = new Player[5];
            Player[] Enemies = new Player[5];

            //Initialise Players
            Players[0] = new Player("Player1", new Element(SIGN.Rock), 100, 50, false);
            Players[1] = new Player("Player2", new Element(SIGN.Paper), 100, 50, false);
            Players[2] = new Player("Player3", new Element(SIGN.Scissor), 100, 50, false);
            Players[3] = new Player("Player4", new Element(SIGN.Lizard), 100, 50, false);
            Players[4] = new Player("Player5", new Element(SIGN.Spock), 100, 50, false);

            //Initialise Enemies
            Enemies[0] = new Player("Enemy1", new Element(SIGN.Rock), 100, 50, true);
            Enemies[1] = new Player("Enemy2", new Element(SIGN.Paper), 100, 50, true);
            Enemies[2] = new Player("Enemy3", new Element(SIGN.Scissor), 100, 50, true);
            Enemies[3] = new Player("Enemy4", new Element(SIGN.Lizard), 100, 50, true);
            Enemies[4] = new Player("Enemy5", new Element(SIGN.Spock), 100, 50, true);


            //DisplayStatus(Players[0], Enemies);
            //Console.WriteLine("");
            //Attack(Players[0], Enemies);
            //Console.WriteLine();
            //DisplayStatus(Players[0], Enemies);

            
            foreach (var player in Players)
            {
                //Confrim initialisation
                DisplayStatus(player, Enemies);
                Console.WriteLine();
                //Attack each enemy
                Attack(player, Enemies);
                Console.WriteLine();
                //Review Changes
                DisplayStatus(player, Enemies);
                Console.WriteLine("\n\n");
                Console.WriteLine("Next Player");
                ResetEnemies(Enemies);
            }
            

            Console.ReadKey();
        }

        //Method displays a table of stats
        static void DisplayStatus(Player displayMe, Player[] enemies)
        {
            Console.WriteLine("Name\t|\tSign\t|\tHP\t|\tAP");
            Console.WriteLine("-----------------------------------------------------------------");
            Console.WriteLine(displayMe.ToString());
            Console.WriteLine("-----------------------------------------------------------------");
            foreach (var enemy in enemies)
            {
                Console.WriteLine(enemy.ToString());
            }
        }
        //End of DisplayStatus


        //Method takes in a player and attacks each enemy,  
        //using modifier in Element class
        static void Attack(Player player, Player[] enemies)
        {
            foreach (var enemy in enemies)
            {
                //Checking each enemy emelent against player's sign
                enemy.HealthPoints -= (player.AttackPoints * 
                                        player.MyElement.ModifyDamage(enemy.MyElement.mySign));
                Console.WriteLine("{0} attacks {1}", player.Name, enemy.Name);
            }
        }
        //End of Attack

        //Method resets HP of all enemies after an attack
        static void ResetEnemies(Player[] enemies)
        {
            foreach (var enemy in enemies)
            {
                enemy.HealthPoints = 100;
            }
        }
        //End of reset

        public class Element
        {
            Random rand = new Random();
            public SIGN mySign;

            //Ctor for assigning a random SIGN
            public Element()
            {
                mySign = RandomSign();
            }

            //Ctor for assigning specific SIGN
            public Element(SIGN thisSign)
            {
                mySign = thisSign;
            }

            //Method to return random SIGN
            public SIGN RandomSign()
            {
                int signAssign = rand.Next(0, 5);

                if (signAssign == 0)
                    return SIGN.Rock;
                else if (signAssign == 1)
                    return SIGN.Paper;
                else if (signAssign == 2)
                    return SIGN.Scissor;
                else if (signAssign == 3)
                    return SIGN.Lizard;
                else
                    return SIGN.Spock;
            }

            /// <summary>
            ///  //Main method that returns a damage modifier 
            ///     based on each sign as in 
            ///     Rock, Paper, Scissor, Lizard, Spock
            ///     See emthod spec for full ruleset
            /// </summary>
            public double ModifyDamage(SIGN enemySign)
            {
                switch (mySign)
                {
                    case SIGN.Rock:
                        //TODO: amend code to dropthrough wins and looses
                        switch (enemySign)
                        {
                            case SIGN.Rock:
                                return 1.0; //Draw
                            case SIGN.Paper:
                                return 0.5; //Loose
                            case SIGN.Scissor:
                                return 1.5; //Win
                            case SIGN.Lizard:
                                return 1.5; //Win
                            case SIGN.Spock:
                                return 0.5; //Loose
                            default:
                                return 0;
                        }
                    case SIGN.Paper:
                        switch (enemySign)
                        {
                            case SIGN.Rock:
                                return 1.5; //Win
                            case SIGN.Paper:
                                return 1.0; //Draw
                            case SIGN.Scissor:
                                return 0.5; //Loose
                            case SIGN.Lizard:
                                return 0.5; //Loose
                            case SIGN.Spock:
                                return 1.5; //Draw
                            default:
                                return 0;
                        }
                    case SIGN.Scissor:
                        switch (enemySign)
                        {
                            case SIGN.Rock:
                                return 0.5; //Loose
                            case SIGN.Paper:
                                return 1.5; //Win
                            case SIGN.Scissor:
                                return 1.0; //Draw
                            case SIGN.Lizard:
                                return 1.5; //Win
                            case SIGN.Spock:
                                return 0.5; //Loose
                            default:
                                return 0;
                        }
                    case SIGN.Lizard:
                        switch (enemySign)
                        {
                            case SIGN.Rock:
                                return 0.5; //Loose
                            case SIGN.Paper:
                                return 1.5; //Win
                            case SIGN.Scissor:
                                return 0.5; //Loose
                            case SIGN.Lizard:
                                return 1.0; //Draw
                            case SIGN.Spock:
                                return 1.5; //Win
                            default:
                                return 0;
                        }
                    case SIGN.Spock:
                        switch (enemySign)
                        {
                            case SIGN.Rock:
                                return 1.5; //Win 
                            case SIGN.Paper:
                                return 0.5; //Loose
                            case SIGN.Scissor:
                                return 1.5; //Win
                            case SIGN.Lizard:
                                return 0.5; //Loose
                            case SIGN.Spock:
                                return 1.0; //Draw
                            default:
                                return 0;
                        }
                    default:
                        return 0;
                }
            }

            public override string ToString()
            {
                return mySign.ToString();
            }
        }

        public class Player
        {
            public string Name { get; set; }
            public Element MyElement { get; set; }
            public double HealthPoints { get; set; }
            public double AttackPoints { get; set; }
            public bool IsEnemy { get; set; }

            public Player(string name, Element myElement, double healthPoints, double attackPoints, bool isEnemy)
            {
                Name = name;
                MyElement = myElement;
                HealthPoints = healthPoints;
                AttackPoints = attackPoints;
                IsEnemy = isEnemy;
            }

            //ToString override to display needed info
            public override string ToString()
            {
                return string.Format("{0}\t|\t{1}\t|\t{2}\t|\t{3}", Name, MyElement.ToString(), HealthPoints, AttackPoints);
            }
        }
    }
}
