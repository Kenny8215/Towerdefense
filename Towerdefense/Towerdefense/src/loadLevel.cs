using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Towerdefense.src
{
    class LoadLevel
    {
        public void load(String str)
        {

        }

        public int[,] getGrid()
        {
            int[,] ret = new int[1,1];

            return ret;
        }

        public SortedList<int,List<Enemy>> getWaves()
        {
            SortedList<int, List<Enemy>> ret = new SortedList<int, List<Enemy>>();

            return ret;

        }
    }
}
