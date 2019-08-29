using System;

namespace maz_Step1
{
    static class _PublicFunction
    {
        static public Boolean IsRout(int Row, int Column,char [,] Map)
        {
            if (Row < 13 && Row >-1)
                if (Column < 13 && Column > -1)
                    if (Map[Row, Column] != 'b')
                        return true;

            return false;
        }
        static public Boolean IsBlock(int Row, int Column, char[,] Map)
        {
            Boolean RightIsBlock = false;
            Boolean LeftIsBlock = false;
            Boolean StraightIsBlock = false;
            Boolean BackIsBlock = false;

            int RightNeighbor = GetRightNeighborPosition(Row, Column);
            int LeftNeighbor = GetLeftNeighborPosition(Row, Column);
            int StraightNeighbor = GetStraightNeighborPosition(Row, Column);
            int BackNeighbor = GetBackNeighborPosition(Row, Column);

            //Check Right
            if (!IsRout(RightNeighbor / 13, RightNeighbor % 13, Map))
                RightIsBlock = true;
            //Check Left
            if (!IsRout(LeftNeighbor / 13, LeftNeighbor % 13, Map))
                LeftIsBlock = true;
            //Check Straight
            if (!IsRout(StraightNeighbor / 13, StraightNeighbor % 13, Map))
                StraightIsBlock = true;
            //Check Back
            if (!IsRout(BackNeighbor / 13, BackNeighbor % 13, Map))
                BackIsBlock = true;

            if (RightIsBlock)
                if (LeftIsBlock)
                    if (StraightIsBlock)
                        if (BackIsBlock)
                            return true;
                return false;
        }
        static public int GetRightNeighborPosition(int Row,int Column )
        {
            if ((Column + 1) > 12)
                return -1;
            return (Row * 13) + (++Column);
        }
        static public int GetLeftNeighborPosition(int Row, int Column)
        {
            if ((Column - 1) < 0)
                return -1;
            return (Row * 13) + (--Column);
        }
        static public int GetStraightNeighborPosition(int Row, int Column)
        {
            if ((Row - 1) < 0)
                return -1;
            return ((--Row) * 13) + Column;
        }
        static public int GetBackNeighborPosition(int Row, int Column)
        {
            if ((Row + 1) >12)
                return -1;
            return ((++Row) * 13) + Column;
        }

    }
}
