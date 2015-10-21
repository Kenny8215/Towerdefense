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
        /*list of sprites*/
        SortedList<enemyType, Texture2D> spriteList = new SortedList<enemyType, Texture2D>();

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


        public GameManager(int waveQuantity, int[] enemyQuantityPerWave, enemyType[] enemyTypeInWave)
        {
            
            for (int i = 0 ; i < waveQuantity ; i++)
            {
                List<Enemy> temp = new List<Enemy>();
                for (int j = 0 ; j < enemyQuantityPerWave.Length ; j++)
                {
                    temp.Add(makeEnemy(enemyTypeInWave[i]));
                }
                waveList.Add(i, temp);
            }
        }

        private Enemy makeEnemy(enemyType newEnemyType)
        {
            Enemy newEnemy;
            Texture2D sprite;
            spriteList.TryGetValue(newEnemyType, out sprite);

            switch (newEnemyType)
            {
                case enemyType.Wolf:
                    newEnemy = new Enemy(sprite, 5, 5, 5.5f, "Eis", false, false);
                    break;
                default:
                    newEnemy = null;
                    break;
            }
            return newEnemy;
        }

        public void draw()
        {

        }


        public void updateEnemys()
        {

        }




    }

}
