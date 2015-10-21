using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Towerdefense.src
{
    class Landscape : GameObject
    {
        private Boolean isRoad;
        private int roadDirection;

        public Landscape(Texture2D sprite, Boolean isRoad, int roadDirection) : base(sprite) {

            this.isRoad = isRoad;
            this.roadDirection = roadDirection;
        
        }

    }
}
