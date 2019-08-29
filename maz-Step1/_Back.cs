using System;

namespace maz_Step1
{
    class _Back
    {
        static public char SetHeadToBack(char HeadDirection)
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
        static public int SetMoveToBack(int Row, int Column, char HeadDirection)
        {
            if (Row < 12)
            {
                Row++;
                return Row * 13 + Column;
            }
            return -1;
        }
        static public Boolean CheckMoveToBackIsTrue(int Row, int Column, char[,] Map)
        {
            return _PublicFunction.IsRout(++Row, Column,  Map);
        }
    }
}
