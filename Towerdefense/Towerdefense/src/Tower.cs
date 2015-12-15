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
            get { return projectileSpritePath; }
            set { projectileSpritePath = value; }
        }
        public Texture2D ProjectileSprite {
            get { return projectileSprite; }
            set { projectileSprite = value; }
        }
        public int MaxLevel {
            get { return maxLevel; }
        }
        public string UpdatePath {
            get { return updatePath; }
            set { updatePath = value; }
        }

        public string RangeCirclePath {
            get { return rangeCirclePath; }
            set { rangeCirclePath = value; }
        }
        public int UpgradeCost{
        get{ return upgradeCost; }
            set { upgradeCost = value; }
        }
        public Texture2D Weapon {
            get { return weapon; }
            set { weapon = value; }
        }
        public Boolean IsSelected {
            get { return isSelected; }
            set { isSelected = value;}
        }

        public Vector2 TowerField {
            get { return towerField; }
            set { towerField = value; }
        }

        public int Level {
            get {
                return level;
            }
            set {
                level = value;
            }
        }
        public Texture2D RangeCircle{
            get {
                return rangeCircle;
            }

            set {
                rangeCircle = value;
            }
    }

        public int Range
        {
            get
            {
                return range;
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
                return cost;
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
                return damage;
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
                return fireRate;
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
                return speed;
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
                return isUpgradeable;
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
                return position;
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
            IsUpgradeable = isUpgradeable;
            this.rangeCircle = rangeCircle;
            towerField.X = (int)(position.X / offset.X);
            towerField.Y = (int)(position.Y / offset.Y);
            this.upgradeCost = upgradeCost;
            this.weapon = weapon;
            Upgrade = upgrade;
            ProjectileSprite = projectileSprite;
            pastFrames = 0;
            WeaponRotation = 0F;
        }

        /*Testconstructor*/
        public Tower(Texture2D sprite,Texture2D rangeCircle,Texture2D upgrade , Vector2 position, Vector2 offset) : base(sprite) {
            this.position = position;
            this.rangeCircle = rangeCircle;
            range = 175;
            level = 0;
            this.upgrade = upgrade;
            towerField.X = (int) (position.X / offset.X);
            towerField.Y = (int) (position.Y / offset.Y);
            upgradeCost = 10;
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
            if (Level == 0) { Level++; }
            else if (IsSelected && ms.LeftButton == ButtonState.Pressed && ps.LeftButton == ButtonState.Released && player.getGold() >= upgradeCost) {
                if (Level < maxLevel) {
                    Level++;
                    player.setGold(player.getGold()- upgradeCost);
                    Damage += 5;
                    Range += 20;
                    FireRate += 5;
               }
            }
        }

        public Vector2 SearchClosestEnemy(List<Enemy> enemies){

            Vector2 enemy;
            Vector2 tmp;

            if (enemies.Count != 0)
            {
                enemy = enemies[0].Position - position;
                closestEnemy = enemies[0];
                enemyVector = enemy;

                for (int i = 0; i < enemies.Count; i++)
                {
                    tmp = enemies[i].Position - position;
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
            if (enemyVector.Length() > range)
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
            if (pastFrames >= 950/fireRate * 1)
            {
                pastFrames = 0;
                g.spawnProjectile(projectileSprite, enemyVector, closestEnemy, position, speed, damage);
            }            
        }
        #endregion


    }
}
