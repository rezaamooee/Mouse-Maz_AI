using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace maz_Step1
{
    class _Straight
    {
        static public char SetHeadToStraight(char HeadDirection)
        {
            switch (HeadDirection)
            {
                case 'e':
                    return 'e';
                case 'w':
                    return 'w';
                case 'n':
                    return 'n';
                default:
                    return 's';
            }
        }
        static public int SetMoveToStraight(int Row, int Column, char HeadDirection)
        {
            if (Row >0)
            {
                Row--;
                return Row * 13 + Column;
            }
            return -1;
        }
        static public Boolean CheckMoveStraightIsTrue(int Row, int Column,char[,] Map)
        {
            return _PublicFunction.IsRout(--Row, Column, Map);
        }
    }
}
