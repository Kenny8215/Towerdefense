using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Towerdefense.src
{
    class GameManager
    {
        /*list of tower in game*/
        List<Tower> towerList;

        /*list of enemywaves*/
        SortedList<int, List<Enemy>> waveList;

        /*actual wave*/
        int actualWave;

        public enum enemyType
        {
            Wolf
        };


        public GameManager(int waveQuantity, int[] enemyQuantityPerWave, enemyType[] enemyTypePerWave)
        {
            
            for (int i = 1 ; i <= waveQuantity ; i++)
            {
                List<Enemy> temp;
                for (int j = 0 ; j < enemyQuantityPerWave.Length ; j++)
                {
                    temp.Add( new Enemy());
                }
                waveList.Add(i, );
            }
        }

        public void draw()
        {

        }


        public void updateEnemys()
        {

        }




    }

}
