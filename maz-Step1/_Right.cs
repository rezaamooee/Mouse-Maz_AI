using System;

namespace maz_Step1
{
    static class _Right
    {
        static public char SetHeadToRight(char HeadDirection)
        {
            switch(HeadDirection)
            {
                case 'e':
                    return 's';
                case 'w':
                    return 'n';
                case 'n':
                    return 'e';
                default:
                    return 'w';
            }
        }
        static public int SetMoveToRight(int Row , int Column)
        {
            if (Column < 12)
            {
                Column++;
                return Row * 13 + Column;
            }
            return -1;
        }
        static public Boolean CheckMoveRightIsTrue(int Row, int Column,char [,] Map)
        {
            return _PublicFunction.IsRout(Row, ++Column, Map);
        }
    }
}
