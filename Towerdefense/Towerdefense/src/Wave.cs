using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Towerdefense
{
    class Wave
    {
        #region Fields
        List<Enemy> listEnemy;
        Enemy enemy;
        int count;
        bool done;
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

        public bool Done
        {
            get
            {
                return done;
            }

            set
            {
                done = value;
            }
        }

        public List<Enemy> getEnemys()
        {
            return listEnemy;
        }

        public void removeEnemy(int x)
        {
            listEnemy.RemoveAt(x);
        }
        public void loadDone()
        {
            listEnemy = new List<Enemy>();
            for(int i = 0; i < count; i++)
            {
                listEnemy.Add(enemy.cloneMe());
            }
        }

        #endregion
    }
}
