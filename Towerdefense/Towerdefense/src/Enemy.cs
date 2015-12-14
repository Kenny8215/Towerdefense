using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Towerdefense
{
    class Enemy : DynamicGameObject
    {
        #region Fields
        Texture2D sprite;
        string spritePath;

        private int hitPoints;
        private int maxHitPoints;
        private Texture2D healthBar;
        private String healthBarPath;
        private Vector2 position;
        private Vector2 lastCenterPosition;

        private int dmg;
        private int walkDistance;
        private int rotation;
        private float movementSpeed;
        private string resistance;

        private bool isBoss;
        private bool isFlying;
        private bool hasTurned;

        public Enemy cloneMe()
        {
            return new Enemy(sprite, healthBar, spritePath, healthBarPath, hitPoints, position, walkDistance, rotation, movementSpeed, resistance, isBoss, isFlying);
        }

        internal void damage(int damage, GameManager g,Player player)
        {
            hitPoints -= damage;
            if (hitPoints <= 0)
            {
                player.setGold(player.getGold() + (int)(maxHitPoints * 0.1));
                g.destroyMe(this);
                
            }
        }

        internal void enemyDoesDamage(Player player,GameManager g ,Vector2[,] fieldCenterPosition,Vector2 offset,int amountOfField) {
            Vector2 outOfGrid = new Vector2(fieldCenterPosition[amountOfField-1,amountOfField-1].X+offset.X, fieldCenterPosition[amountOfField-1, amountOfField-1].Y + offset.Y);
            if (this.Position.X >= outOfGrid.X / 2 || Position.Y >= outOfGrid.Y / 2 || Position.X <= 0 || Position.Y / 2 <= 0)
            {
                player.loseHitPoints(dmg);
                g.destroyMe(this);
            }
        }

        #region Setter and Getter
        public String HealthBarPath {
            get { return healthBarPath; }
            set { healthBarPath = HealthBarPath; }
        }
        public Texture2D HealthBar
        {
            get { return healthBar; }
            set { healthBar = value; }
        }

        public int HitPoints
        {
            get
            {
                return hitPoints;
            }

            set
            {
                hitPoints = value;
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
                position = value;
            }
        }

        public int WalkDistance
        {
            get
            {
                return walkDistance;
            }

            set
            {
                walkDistance = value;
            }
        }

        public int Rotation
        {
            get
            {
                return rotation;
            }

            set
            {
                rotation = value;
            }
        }

        public float MovementSpeed
        {
            get
            {
                return movementSpeed;
            }

            set
            {
                movementSpeed = value;
            }
        }

        public string Resistance
        {
            get
            {
                return resistance;
            }

            set
            {
                resistance = value;
            }
        }

        public bool IsBoss
        {
            get
            {
                return isBoss;
            }

            set
            {
                isBoss = value;
            }
        }

        public bool IsFlying
        {
            get
            {
                return isFlying;
            }

            set
            {
                isFlying = value;
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
        #endregion

        #region Constructors
        public Enemy()
            : base()
        {
            Rotation = 0;
            lastCenterPosition = new Vector2(-1, -1);
            position = new Vector2(30, 0);
        }

        public Enemy(Texture2D sprite,Texture2D healthBar, string spritePath,string healthPath, int hitPoints, Vector2 position, int walkDistance, int rotation, float movementSpeed, string resistance, bool isBoss, bool isFlying)
            : base(sprite, hitPoints)
        {

            this.sprite = sprite;
            HitPoints = hitPoints;
            Position = position;
            WalkDistance = walkDistance;
            Rotation = rotation;
            MovementSpeed = movementSpeed;
            Resistance = resistance;
            IsBoss = isBoss;
            IsFlying = isFlying;
            this.spritePath = spritePath;
            HealthBarPath = healthPath;
            maxHitPoints = hitPoints;
            dmg = 1;
        }
        #endregion

        public void drawEnemy(SpriteBatch spriteBatch)
        {

            Vector2 textCent = new Vector2(Sprite.Bounds.Center.X, Sprite.Bounds.Center.Y);
            int tmpX = (int)Position.X ;
            int tmpY =(int)Position.Y ;
            int hpScale =(int) (HitPoints);
            Vector2 tmp = (position);
            Rectangle rec = new Rectangle(tmpX, tmpY, 200*(HitPoints) / maxHitPoints, 10);
            Vector2 center;
            center.X = rec.Center.X;
            center.Y = rec.Center.Y;
            spriteBatch.Draw(Sprite, position, null, null, textCent, rotInRad(), new Vector2(0.3F, 0.3F), Color.White, SpriteEffects.None, 1F);
            if (HitPoints > 10) { spriteBatch.Draw(HealthBar, Position, null, rec, textCent, rotInRad(), new Vector2(0.3F, 0.3F), Color.SteelBlue, SpriteEffects.None, 1F); }
            else { spriteBatch.Draw(HealthBar, Position, null, rec, textCent, rotInRad(), new Vector2(0.3F, 0.3F), Color.Red, SpriteEffects.None, 1F); }
        }
        #endregion

        #region update
        public Vector2 moveEnemy(Vector2[,] roadTypeAndRotation, Vector2 currentEnemyField, float speedFactor, int amountOfField, Vector2[,] FieldCenterPosition, Vector2 offset)
        {
            int roadType = 0; int rotation = 0;
            Vector2 centerPosition = Vector2.Zero;

            if ((currentEnemyField.X > -1 && currentEnemyField.Y > -1) && (currentEnemyField.X < amountOfField && currentEnemyField.Y < amountOfField))
            {
                roadType = (int)roadTypeAndRotation[(int)currentEnemyField.X, (int)currentEnemyField.Y].X;
                rotation = (int)roadTypeAndRotation[(int)currentEnemyField.X, (int)currentEnemyField.Y].Y;
                centerPosition = FieldCenterPosition[(int)currentEnemyField.X, (int)currentEnemyField.Y];
            }
            else { return position; }
            switch (roadType)
            {
                case 0:
                    /*roadTypeAndRotation.X= 0 is a nonroad field*/
                    break;

                case 1:
                    /*roadTypeAndRotation.X= 1 is a nonroad field*/
                    break;
                case 2:
                    moveStraight(speedFactor, rotation);
                    break;
                case 3:
                    moveCurve(speedFactor, rotation, centerPosition, offset);
                    break;
                case 4:
                    move4WayRoad(speedFactor, centerPosition);
                    break;
                case 5:
                    move3WayRoad(speedFactor, rotation, centerPosition);
                    break;
            }
            return position;
        }
        #endregion

        #region movement
        public void moveStraight(float speedFactor, int roadRotation)
        {
            float mv = speedFactor * movementSpeed;
            if (Rotation >= 360) { Rotation = (int)normalizeDegree(Rotation); }
            if (Rotation == 0) { position.Y += mv; }
            else if (Rotation == 90) { position.X -= mv; }
            else if (Rotation == 180) { position.Y -= mv; }
            else if (Rotation == 270) { position.X += mv; }

        }

        public void moveCurve(float speedFactor, int roadRotation, Vector2 centerPosition, Vector2 offset)
        {
            if (lastCenterPosition != centerPosition)
            {
                lastCenterPosition = centerPosition; hasTurned = false;
            }
            float mv = speedFactor * movementSpeed;
            int entryZeroY = 0 + roadRotation * 90;
            int outZeroX = 270 + roadRotation * 90;
            int entryZeroX = 90 + 90 * roadRotation;
            int outZeroY = 180 + 90 * roadRotation;

            if (entryZeroY >= 360 || outZeroX >= 360 || entryZeroX >= 360 || outZeroY >= 360 || Rotation >= 360)
            {
                entryZeroY = (int)normalizeDegree(entryZeroY);
                entryZeroX = (int)normalizeDegree(entryZeroX);
                outZeroY = (int)normalizeDegree(outZeroY);
                outZeroX = (int)normalizeDegree(outZeroX);
                Rotation = (int)normalizeDegree(Rotation);
            }

            if (Rotation == 0) { position.Y += mv; }
            else if (Rotation == 90) { position.X -= mv; }
            else if (Rotation == 180) { position.Y -= mv; }
            else if (Rotation == 270)
            {
                position.X += mv;
            }

            if (!hasTurned)
            {
                if (Rotation == entryZeroY && ((Rotation == 0 && Position.Y >= centerPosition.Y) || (Rotation == 90 && Position.X <= centerPosition.X) || (Rotation == 180 && Position.Y <= centerPosition.Y) || (Rotation == 270 && Position.X >= centerPosition.X)))
                {
                    Rotation = outZeroX;
                    hasTurned = true;
                }
                else if (Rotation == entryZeroX && ((Rotation == 0 && Position.Y >= centerPosition.Y) || (Rotation == 90 && Position.X <= centerPosition.X) || (Rotation == 180 && Position.Y <= centerPosition.Y) || (Rotation == 270 && Position.X >= centerPosition.X)))
                {
                    rotation = outZeroY;
                    hasTurned = true;
                }
            }
        }

        public void move4WayRoad(float speedFactor, Vector2 centerPosition)
        {

            Rotation = (int)normalizeDegree(Rotation);

            if (lastCenterPosition != centerPosition)
            {
                lastCenterPosition = centerPosition; hasTurned = false;
            }
            float mv = speedFactor * movementSpeed;

            if (Rotation == 0) { position.Y += mv; }
            else if (Rotation == 90) { position.X -= mv; }
            else if (Rotation == 180) { position.Y -= mv; }
            else if (Rotation == 270) { position.X += mv; }

            if (!hasTurned)
            {
                if ((Rotation == 0 && Position.Y >= centerPosition.Y) || (Rotation == 90 && Position.X <= centerPosition.X) || (Rotation == 180 && Position.Y <= centerPosition.Y) || (Rotation == 270 && Position.X >= centerPosition.X))
                {
                    Random x = new Random();
                    int rand = x.Next(-1, 2);
                    Rotation += 90 * rand;

                    hasTurned = true;
                }
            }
        }

        public void move3WayRoad(float speedFactor, int roadRotation, Vector2 centerPosition)
        {
            Rotation = (int)normalizeDegree(Rotation);

            if (lastCenterPosition != centerPosition)
            {
                lastCenterPosition = centerPosition; hasTurned = false;
            }
            float mv = speedFactor * movementSpeed;

            if (Rotation == 0) { position.Y += mv; }
            else if (Rotation == 90) { position.X -= mv; }
            else if (Rotation == 180) { position.Y -= mv; }
            else if (Rotation == 270) { position.X += mv; }

            if (!hasTurned)
            {
                if ((Rotation == 0 && Position.Y >= centerPosition.Y) || (Rotation == 90 && Position.X <= centerPosition.X) || (Rotation == 180 && Position.Y <= centerPosition.Y) || (Rotation == 270 && Position.X >= centerPosition.X))
                {
                    if (Rotation == roadRotation * 90)
                    {
                        Random x = new Random();
                        int rand = x.Next(-1, 1);
                        if (rand == -1) { Rotation = (int) normalizeDegree(Rotation + 270); hasTurned = true; }
                        else { Rotation = (int) normalizeDegree(Rotation + 90); hasTurned = true; }
                    }
                    else if(Rotation == (normalizeDegree(90 + roadRotation * 90))) {
                        Random x = new Random();
                        int rand = x.Next(0, 2);
                        Rotation += (int) normalizeDegree(90 * rand);
                        hasTurned = true;
                    }
                    else if (Rotation == (normalizeDegree(270 + roadRotation * 90))) {
                        Random x = new Random();
                        int rand = x.Next(0, 2);
                        Rotation = (int) normalizeDegree(Rotation + 270 * rand);
                             hasTurned = true;
                    }
                    }
                }
        }
        /*check if degree is > than 360 and resets it */
        public float normalizeDegree(float degree)
        {
            degree = degree / 360;

            if (degree > 1)
            {
                degree -= 1;
            }

            degree *= 360;
            if (degree == 360)
            {
                degree = 0;
            }
            return degree;
        }
        #endregion

        #region helpFunctions
        public float rotInRad()
        {
            switch (Rotation)
            {
                case 90:
                    return 1.5708F;
                case 180:
                    return 3.14159F;
                case 270:
                    return 4.71239F;
                default: return 0F;
            }
        }

        public Vector2 currentEnemyField(Vector2 offset)
        {
            Vector2 currentField;
            currentField.X = (int)(position.X / offset.X);
            currentField.Y = (int)(position.Y / offset.Y);
            return currentField;
        }
        #endregion

    }
}
