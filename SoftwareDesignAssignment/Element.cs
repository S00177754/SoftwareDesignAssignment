using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftwareDesignAssignment
{
    enum SIGN { Rock, Paper, Scissors, Lizard, Spock};

    class Element
    {
        Random rand = new Random();
        SIGN mySign;

        public Element()
        {
            mySign = RandomSign();
        }

        public Element(SIGN thisSign)
        {
            mySign = thisSign;
        }

        public SIGN RandomSign()
        {
            int signAssign = rand.Next(0, 5);

            if (signAssign == 0)
                return SIGN.Rock;
            else if (signAssign == 1)
                return SIGN.Paper;
            else if (signAssign == 2)
                return SIGN.Scissors;
            else if (signAssign == 3)
                return SIGN.Lizard;
            else
                return SIGN.Spock;
        }

        public double ModifyDamage(SIGN enemySign)
        {
            switch (mySign)
            {
                case SIGN.Rock:
                
                    switch (enemySign)
                    {
                        case SIGN.Rock:
                            return 1.0; //Draw
                        case SIGN.Paper:
                            return 0.5; //Loose
                        case SIGN.Scissors:
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
                        case SIGN.Scissors:
                            return 0.5; //Loose
                        case SIGN.Lizard:
                            return 0.5; //Loose
                        case SIGN.Spock:
                            return 1.5; //Draw
                        default:
                            return 0;
                    }
                case SIGN.Scissors:
                    switch (enemySign)
                    {
                        case SIGN.Rock:
                            return 0.5; //Loose
                        case SIGN.Paper:
                            return 1.5; //Win
                        case SIGN.Scissors:
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
                        case SIGN.Scissors:
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
                        case SIGN.Scissors:
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
    }
}
