using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace maz_Step1
{
    class _Memory
    {
        private char [,] GameMap = new char[13, 13];
        private char [,] MemoryMap = new char[13, 13]; // v =Visited  b = Block 
        private char[,] MemoryHeadDirection = new char[13, 13]; 
        private int  [,] PreviousLocation = new int[13, 13] ;
        private int  [,] NextLocation = new int[13, 13];
       
        public void SetGameMap(char [,] map)
        {
            GameMap = map;
            for (int i = 0; i < 13; i++)
                for (int j = 0; j < 13; j++)
                {
                    this.PreviousLocation[i, j] = -1;
                    this.NextLocation[i, j] = -1;
                    this.MemoryMap[i, j] = ' ';
                    this.MemoryHeadDirection[i, j] = ' ';
                }
        }
        public void SaveInMemory(int SourceRow , int SourceColumn ,int DesRow,int DesColumn)
        {
            this.PreviousLocation[DesRow, DesColumn] = SourceColumn + (SourceRow * 13);
            this.NextLocation[SourceRow, SourceColumn] = (DesRow * 13) + DesColumn;
        }
        public void SaveCurrentSellDetailsInMemory(int row , int column, char Detail) //Detal : (v: Visited) / (b:Block)
        {
            this.MemoryMap[row, column] = Detail;
        }
        public int GetPreviousLocation(int row , int column)
        {
            if (this.PreviousLocation[row, column] != (-1))
                return this.PreviousLocation[row, column];
            else
                return -1;
        }
        public char GetPreviousHeadDirection(int row, int column)
        {
            if (this.MemoryHeadDirection[row, column] != ' ')
                return this.MemoryHeadDirection[row, column];
            else
                return 'b';
        }
        public int GetNextLocation(int row, int column)
        {
            if (this.NextLocation[row, column] != (-1))
                return this.NextLocation[row, column];
            else
                return -1;
        }
        public Boolean IsVisited (int row , int column)
        {
            if (row > -1 && row < 13)
                if (column > -1 && column < 13)
                    if (this.MemoryMap[row, column] == 'v')
                        return true;
            return false;
        }
        public Boolean IsVisited(int MouseLocation)
        {
            int row = MouseLocation / 13;
            int column = MouseLocation % 13;
            
            if (row > -1 && row < 13)
                if (column > -1 && column < 13)
                    if (this.MemoryMap[row, column] == 'v')
                        return true;
            return false;
        }
        public Boolean IsBlock(int row, int column)
        {
            if (row > -1 && row < 13)
                if (column > -1 && column < 13)
                    if (this.GameMap[row, column] == 'b')
                        return true;
                    else
                        return false;
                else
                    return true;
            else
                return true;
        }
        public Boolean IsBlock(int MouseLocation)
        {
            int row = MouseLocation / 13;
            int column = MouseLocation % 13;

            if (row > -1 && row < 13)
                if (column > -1 && column < 13)
                    if (this.GameMap[row, column] == 'b')
                        return true;
                    else
                        return false;
                else
                    return true;
            else
                return true;
        }
        public Boolean HasNewNeighbor(int row , int column)
        {
            if (row < 12)
                if (!IsBlock(row + 1, column))
                    if (!IsVisited(row + 1, column))
                        return true;
            else if(row>0)
                if (!IsBlock(row - 1, column))
                    if (!IsVisited(row - 1, column))
                        return true;
            else if (column < 12)
                if (!IsBlock(row , column+1))
                    if (!IsVisited(row, column+1))
                        return true;
            else if (column > 0)
                if (!IsBlock(row , column - 1))
                    if (!IsVisited(row , column - 1))
                        return true;
            return false;
        }
        public List<int>GetScapePath(int row , int column)
        {
            List<int> ScapeWay = new List<int>();
            int bff;
            
            do
            {
                if (row < 0 || row > 13)
                {
                    ScapeWay.Remove(ScapeWay.Count - 1);
                    return ScapeWay;
                }
                if (column < 0 || column > 13)
                {
                    ScapeWay.RemoveAt(ScapeWay.Count - 1);
                    return ScapeWay;
                }
                bff = this.PreviousLocation[row, column];
                ScapeWay.Add(bff);
                row = bff / 13;
                column = bff % 13;
            } while ( HasNewNeighbor(row,column) == false);
             return ScapeWay;
        }
        public Boolean DoMoveToCell(int row, int column)
        {
            if (row <= 12 && row >= 0)
                if (column <= 12 && column >= 0)
                    if (this.MemoryMap [row, column] != 'b')
                        if (!IsVisited(row, column))
                            return true;
            return false;
        }
    }
}
