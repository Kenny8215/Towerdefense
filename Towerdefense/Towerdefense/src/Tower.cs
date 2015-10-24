﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Towerdefense.src
{
    class Tower : GameObject
    {
        /*Towerrange*/
        private int range;

        /*costs to build a tower*/
        private int cost;

        /*Towerdamage*/
        private int damage;

        /**/
        private int fireRate;

        /*Projectile Speed*/
        private int speed;

        /*Tower can be upgraded*/
        private Boolean IsUpgradeable;

        /*Constructor*/
        public Tower(Texture2D sprite,int range, int cost, int damage, int fireRate, int speed, Boolean isUpgradeable) : base(sprite) {
            
            this.range = range;
            this.cost = cost;
            this.damage = damage;
            this.fireRate = fireRate;
            this.speed = speed;
            this.IsUpgradeable = isUpgradeable;
        }
        
        public Vector2 SearchClosestEnemy(){
        
        //TO DO : Searchs the closest Enemy 
            return Vector2.Zero;

        }

        public Boolean CanShootEnemy() {
        //TO DO : Shoots at the closest Enemy
            return true;
        }



    }
}