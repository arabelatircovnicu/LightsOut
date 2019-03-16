using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightsOut.Classes
{
    class LightOffMatrix
    {
        private int RowCount,ColCount;
        private Boolean[,] Matrix;
        public Boolean[,] Data { get { return Matrix; } }
        public Boolean[,] Cell { get { return Matrix; } }
        public Boolean isOFF { get { return isOff(); } }


        public LightOffMatrix(int Rows,int Columns)
        {
            RowCount = Rows;
            ColCount = Columns;
            Matrix = new Boolean[Rows, Columns];
        }

        public void Init(int[] List)
        {
            foreach (int n in List)
                Matrix[n / ColCount, n % ColCount] = true;

        }

        public void SwitchKey(int Row, int Column)
        {
            Matrix[Row, Column] = !Matrix[Row, Column];

            if((Row+1)<RowCount)    Matrix[Row + 1, Column    ] = !Matrix[Row + 1, Column   ];
            if((Row-1)>=0)          Matrix[Row - 1, Column    ] = !Matrix[Row - 1, Column   ];
            if((Column-1)>=0)       Matrix[Row    , Column - 1] = !Matrix[Row    , Column - 1];
            if((Column+1)<ColCount) Matrix[Row    , Column + 1] = !Matrix[Row    , Column + 1];

        }

        private Boolean isOff()
        {
            Boolean Result = true;
            for (int i = 0; i < RowCount; i++)
                for (int j = 0; j < ColCount; j++)
                    if (Matrix[i, j]) Result = false;
            return Result;
        }
    }
}
