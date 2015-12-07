using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Towerdefense
{
    class Wave
    {
        #region Fields
        Enemy enemy;
        int count;
        #endregion

        #region setter and getter
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
        #endregion
    }
}
