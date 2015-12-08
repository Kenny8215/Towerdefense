﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Towerdefense
{
    class Tower : GameObject
    {
        #region Field

        Texture2D sprite;
        Texture2D rangeCircle;
        string spritePath;

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
        private Boolean isUpgradeable;

        /*Towers Level*/
        private int level;

        /*Towerposition*/
        private Vector2 position;

        private Texture2D upgrade;

        Enemy closestEnemy;
        Vector2 enemyVector;

        #endregion  

        #region Setter and Getter
        public int Level {
            get {
                return this.level;
            }
            set {
                level = value;
            }
        }
        public Texture2D RangeCircle{
            get {
                return this.rangeCircle;
            }

            set {
                rangeCircle = value;
            }
    }

        public int Range
        {
            get
            {
                return this.range;
            }

            set
            {
                range = value;
            }
        }


        public int Cost
        {
            get
            {
                return this.cost;
            }
                set
            {
                cost = value;
            }
        }

        public int Damage
        {
            get
            {
                return this.damage;
            }

            set
            {
                damage = value;
            }
        }


        public int FireRate
        {
            get
            {
                return this.fireRate;
            }

            set
            {
                fireRate = value;
            }
        }


        public int Speed
        {
            get
            {
                return this.speed;
            }

            set
            {
                speed = value;
            }
        }
            
        public bool IsUpgradeable
        {
            get
            {
                return this.isUpgradeable;
            }

            set
            {
                isUpgradeable = value;
            }
        }

        
        public Vector2 Position
        {
            get
            {
                return this.position;
            }

            set
            {
                position  = value;
            }
        }

        public string SpritePath
        {
            get
            {
                return spritePath;
            }

            set
            {
                spritePath = value;
            }
        }

        public Texture2D Sprite1
        {
            get
            {
                return sprite;
            }

            set
            {
                sprite = value;
            }
        }

        public Texture2D Upgrade {
            get { return upgrade; }
            set { upgrade = value; }
        }
        #endregion

        #region Constructors
        public Tower(Texture2D sprite,Vector2 position,int range, int cost, int damage, int fireRate, int speed, Boolean isUpgradeable, Texture2D rangeCircle) : base(sprite) {
            this.position = position;
            this.range = range;
            this.cost = cost;
            this.damage = damage;
            this.fireRate = fireRate;
            this.speed = speed;
            this.IsUpgradeable = isUpgradeable;
            this.rangeCircle = rangeCircle;
        }

        /*Testconstructor*/
        public Tower(Texture2D sprite,Texture2D rangeCircle,Texture2D upgrade , Vector2 position) : base(sprite) {
            this.position = position;
            this.rangeCircle = rangeCircle;
            this.range = 175;
            this.level = 1;
            this.upgrade = upgrade;
        }

        public Tower() { }
        #endregion

        #region AimAndShoot
        public Vector2 SearchClosestEnemy(List<Enemy> enemies){

            Vector2 enemy;
            Vector2 tmp;

            enemy = enemies[0].Position - this.position;
            closestEnemy = enemies[0];
            enemyVector = enemy;

            for(int i = 0; i < enemies.Count; i++)
            {
                tmp = enemies[i].Position - this.position;
                if (enemy.Length() > tmp.Length())
                {
                    enemy = tmp;
                    enemyVector = tmp;
                    closestEnemy = enemies[i];
                }
            }
            
            return enemy;

        }

        public Boolean CanShootEnemy() {
        //TO DO : Shoots at the closest Enemy
            return true;
        }
        #endregion


    }
}
