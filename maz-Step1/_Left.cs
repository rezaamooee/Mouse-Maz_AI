using System;

namespace maz_Step1
{
    class _Left
    {
        static public char SetHeadToLeft(char HeadDirection)
        {
            switch (HeadDirection)
            {
                case 'e':
                    return 'n';
                case 'w':
                    return 's';
                case 'n':
                    return 'w';
                default:
                    return 'e';
            }
        }
        static public int SetMoveToLeft(int Row, int Column, char HeadDirection)
        {
            if (Column > 0)
            {
                Column--;
                return Row * 13 + Column;
            }
            return -1;
        }
        static public Boolean CheckMoveLeftIsTrue(int Row, int Column, char[,] Map)
        {
            return _PublicFunction.IsRout(Row, --Column, Map);
        }
    }
}
