using System;
using System.Collections.Generic;
using System.Xml;
using System.Linq;
using System.Text;

namespace Towerdefense
{
    class LoadLevel
    {
        public void load(String str)
        {
            var uri = new System.Uri(str);
            var converted = uri.AbsoluteUri;

            XmlTextReader read = new XmlTextReader(converted);

            read.ReadToFollowing("wave");

            
        }

        public int[,] getGrid()
        {
            int[,] ret = new int[,];

            return ret;
        }

        public SortedList<int,List<Enemy>> getWaves()
        {
            SortedList<int, List<Enemy>> ret = new SortedList<int, List<Enemy>>();

            return ret;

        }
    }
}
