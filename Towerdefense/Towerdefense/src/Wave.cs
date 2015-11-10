using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Towerdefense
{
    class Wave
    {
        Enemy enemy;
        int count;

        internal Enemy Enemy
        {
            get
            {
                return enemy;
            }

            set
            {
                enemy = value;
            }
        }

        public int Count
        {
            get
            {
                return count;
            }

            set
            {
                count = value;
            }
        }
    }
}
