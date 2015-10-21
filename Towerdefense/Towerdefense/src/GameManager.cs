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


    }
}
