using System;
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

        #endregion  

        #region Setter and Getter
        public int Level {
            get {
                return this.level;
            }
            set {
                this.Level = level;
            }
        }
        public Texture2D RangeCircle{
            get {
                return this.rangeCircle;
            }

            set {
                this.RangeCircle = rangeCircle;
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
            
        public Boolean IsUpgradeable
        {
            get
            {
                return this.isUpgradeable;
            }

            set
            {
                IsUpgradeable = value;
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
        public Tower(Texture2D sprite,Texture2D rangeCircle , Vector2 position) : base(sprite) {
            this.position = position;
            this.rangeCircle = rangeCircle;
            this.range = 175;
            this.level = 1;
        }

        public Tower() { }
        #endregion

        #region AimAndShoot
        public Vector2 SearchClosestEnemy(){
        
        //TO DO : Searchs the closest Enemy 
            return Vector2.Zero;

        }

        public Boolean CanShootEnemy() {
        //TO DO : Shoots at the closest Enemy
            return true;
        }
        #endregion


    }
}
