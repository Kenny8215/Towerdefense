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

        public List<Vector2> createGrid(int maxHeight, int amountOfFields){
            List<Vector2> fieldPosition = new List<Vector2>();
           int offset= maxHeight/amountOfFields;

           for (int i = 0; i <= amountOfFields;i++ )
           {
               for (int j = 0; j <= amountOfFields; j++)
               {
                   fieldPosition.Add(new Vector2(offset / 2 * j, offset * (j + 1) - offset / 2));
                    Console.WriteLine("X Position :" + fieldPosition.ElementAt(j + i).X);
                    Console.WriteLine("Y Position :" + fieldPosition.ElementAt(j + i).Y);
               }
               }
       
                 return fieldPosition;
        }


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
