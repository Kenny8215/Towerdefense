using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Towerdefense
{
    class Tower : GameObject
    {
        #region Field

        Texture2D sprite;
        Texture2D rangeCircle;
        Texture2D weapon;
        string spritePath;
        string weaponPath;
        string rangeCirclePath;
        string updatePath;
        int pastFrames;


        /*Towerrange*/
        private int range;

        /*costs to build a tower*/
        private int cost;

        private float weaponRotation;

        private int maxLevel = 10;

        /*Towerdamage*/
        private int damage;

        /**/
        private int fireRate;

        /*Projectile Speed*/
        private int speed;

        private int upgradeCost;

        /*Tower can be upgraded*/
        private Boolean isUpgradeable;

        /*Towers Level*/
        private int level;

        /*Towerposition*/
        private Vector2 position;

        private Texture2D upgrade;

        private Texture2D projectileSprite;

        private string projectileSpritePath;

        private Enemy closestEnemy;

        private Vector2 enemyVector;

        private Vector2 towerField;

        private Boolean isSelected;

        #endregion  

        #region Setter and Getter
        public float WeaponRotation {
            get { return weaponRotation; }
            set { weaponRotation = value; }
        }

        public string ProjectileSpritePath {
            get { return this.projectileSpritePath; }
            set { this.projectileSpritePath = value; }
        }
        public Texture2D ProjectileSprite {
            get { return this.projectileSprite; }
            set { this.projectileSprite = value; }
        }
        public int MaxLevel {
            get { return this.maxLevel; }
        }
        public string UpdatePath {
            get { return this.updatePath; }
            set { this.updatePath = value; }
        }

        public string RangeCirclePath {
            get { return this.rangeCirclePath; }
            set { this.rangeCirclePath = value; }
        }
        public int UpgradeCost{
        get{ return this.upgradeCost;}
            set { this.upgradeCost = value; }
        }
        public Texture2D Weapon {
            get { return this.weapon; }
            set { this.weapon = value; }
        }
        public Boolean IsSelected {
            get { return this.isSelected; }
            set { this.isSelected = value;}
        }

        public Vector2 TowerField {
            get { return this.towerField; }
            set { this.towerField = value; }
        }

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

        public string WeaponPath
        {
            get
            {
                return weaponPath;
            }

            set
            {
                weaponPath = value;
            }
        }
        #endregion

        #region Constructors
        public Tower(Texture2D sprite,Texture2D projectileSprite,Vector2 position,int range, int cost, int damage, int fireRate, int speed, Boolean isUpgradeable, Texture2D rangeCircle,Vector2 offset, int upgradeCost,Texture2D weapon,Texture2D upgrade) : base(sprite) {
            this.position = position;
            this.range = range;
            this.cost = cost;
            this.damage = damage;
            this.fireRate = fireRate;
            this.speed = speed;
            this.IsUpgradeable = isUpgradeable;
            this.rangeCircle = rangeCircle;
            this.towerField.X = (int)(position.X / offset.X);
            this.towerField.Y = (int)(position.Y / offset.Y);
            this.upgradeCost = upgradeCost;
            this.weapon = weapon;
            this.Upgrade = upgrade;
            this.ProjectileSprite = projectileSprite;
            pastFrames = 0;
            WeaponRotation = 0F;
        }

        /*Testconstructor*/
        public Tower(Texture2D sprite,Texture2D rangeCircle,Texture2D upgrade , Vector2 position, Vector2 offset) : base(sprite) {
            this.position = position;
            this.rangeCircle = rangeCircle;
            this.range = 175;
            this.level = 0;
            this.upgrade = upgrade;
            this.towerField.X = (int) (position.X / offset.X);
            this.towerField.Y = (int) (position.Y / offset.Y);
            this.upgradeCost = 10;
        }

        public Tower() { }
        #endregion

        #region AimAndShoot

        public float TurnToFace(Vector2 position, Vector2 faceThis,
    float currentAngle, float turnSpeed)
        {
            // consider this diagram:
            //         C
            //        /|
            //      /  |
            //    /    | y
            //  / o    |
            // S--------
            //     x
            //
            // where S is the position of the spot light, C is the position of the cat,
            // and "o" is the angle that the spot light should be facing in order to
            // point at the cat. we need to know what o is. using trig, we know that
            //      tan(theta)       = opposite / adjacent
            //      tan(o)           = y / x
            // if we take the arctan of both sides of this equation...
            //      arctan( tan(o) ) = arctan( y / x )
            //      o                = arctan( y / x )
            // so, we can use x and y to find o, our "desiredAngle."
            // x and y are just the differences in position between the two objects.
            float x = faceThis.X - position.X;
            float y = faceThis.Y - position.Y;

            // we'll use the Atan2 function. Atan will calculates the arc tangent of
            // y / x for us, and has the added benefit that it will use the signs of x
            // and y to determine what cartesian quadrant to put the result in.
            // http://msdn2.microsoft.com/en-us/library/system.math.atan2.aspx
            float desiredAngle = (float)Math.Atan2(y, x);

            // so now we know where we WANT to be facing, and where we ARE facing...
            // if we weren't constrained by turnSpeed, this would be easy: we'd just
            // return desiredAngle.
            // instead, we have to calculate how much we WANT to turn, and then make
            // sure that's not more than turnSpeed.

            // first, figure out how much we want to turn, using WrapAngle to get our
            // result from -Pi to Pi ( -180 degrees to 180 degrees )
            float difference = WrapAngle(desiredAngle - currentAngle);

            // clamp that between -turnSpeed and turnSpeed.
            difference = MathHelper.Clamp(difference, -turnSpeed, turnSpeed);

            // so, the closest we can get to our target is currentAngle + difference.
            // return that, using WrapAngle again.
            return WrapAngle(currentAngle + difference);
        }

        /// <summary>
        /// Returns the angle expressed in radians between -Pi and Pi.
        /// </summary>
        private static float WrapAngle(float radians)
        {
            while (radians < -MathHelper.Pi)
            {
                radians += MathHelper.TwoPi;
            }
            while (radians > MathHelper.Pi)
            {
                radians -= MathHelper.TwoPi;
            }
            return radians;
        }
        public void upgradeTower(MouseState ms, MouseState ps,Player player) {
            if (this.Level == 0) { this.Level++; }
            else if (this.IsSelected && ms.LeftButton == ButtonState.Pressed && ps.LeftButton == ButtonState.Released && player.getGold() >= this.upgradeCost) {
                if (this.Level < this.maxLevel) { 
                    this.Level++;
                    player.setGold(player.getGold()-this.upgradeCost);
                    this.Damage += 5;
                    this.Range += 25;
                    this.FireRate += 10;
               }
            }
        }

        public Vector2 SearchClosestEnemy(List<Enemy> enemies){

            Vector2 enemy;
            Vector2 tmp;

            if (enemies.Count != 0)
            {
                enemy = enemies[0].Position - this.position;
                closestEnemy = enemies[0];
                enemyVector = enemy;

                for (int i = 0; i < enemies.Count; i++)
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
            else { return Vector2.Zero; }
        }

        public Boolean CanShootEnemy(Vector2 enemyPosition)
        {
            if (enemyVector.Length() > this.range)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public void Shoot(GameManager g) {
            pastFrames++;
            if (pastFrames >= fireRate * 1)
            {
                pastFrames = 0;
                g.spawnProjectile(this.projectileSprite,this.enemyVector, this.closestEnemy, this.position, this.speed, this.damage);
            }            
        }
        #endregion


    }
}
