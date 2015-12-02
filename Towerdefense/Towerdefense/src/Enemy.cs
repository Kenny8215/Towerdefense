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
        private Vector2 position;

        private int walkDistance;
        private int rotation;
        private float movementSpeed;
        private string resistance;

        private Boolean isBoss;
        private Boolean isFlying;

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

        public Enemy()
            : base()
        {

        }

        public Enemy(Texture2D sprite, int hitPoints, Vector2 position, int walkDistance, int rotation, float movementSpeed, string resistance, Boolean isBoss, Boolean isFlying)
            : base(sprite, hitPoints)
        {

            this.sprite = sprite;
            this.HitPoints = hitPoints;
            this.Position = position;
            this.WalkDistance = walkDistance;
            this.Rotation = rotation;
            this.MovementSpeed = movementSpeed;
            this.Resistance = resistance;
            this.IsBoss = isBoss;
            this.IsFlying = isFlying;
        }

        public void drawEnemy(Texture2D enemy, SpriteBatch spriteBatch, Vector2 startPosition)
        {

            Vector2 textCent = new Vector2(enemy.Bounds.Center.X, enemy.Bounds.Center.Y);

            spriteBatch.Draw(enemy, startPosition, null, null, textCent, 0F, new Vector2(0.2F, 0.2F), Color.White, SpriteEffects.None, 1F);
        }

        public Vector2 moveEnemy(Vector2[,] roadTypeAndRotation, Vector2 currentEnemyField, float speedFactor, int amountOfField,Vector2[,] FieldCenterPosition,Vector2 offset)
        {
            int roadType = 0; int rotation = 0;
            Vector2 centerPosition = Vector2.Zero;

            if (currentEnemyField.X < amountOfField && currentEnemyField.Y < amountOfField)
            {
                roadType = (int)roadTypeAndRotation[(int)currentEnemyField.X, (int)currentEnemyField.Y].X;
                rotation = (int)roadTypeAndRotation[(int)currentEnemyField.X, (int)currentEnemyField.Y].Y;
                centerPosition = FieldCenterPosition[(int) currentEnemyField.X,(int)currentEnemyField.Y] ;
            }else{return this.position;}
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
                    moveCurve(speedFactor, rotation,centerPosition,offset);
                    break;
                case 4:
                    /*move4WayRoad*/
                    break;
                case 5:
                    /*move3WayRoad*/
                    break;
            }

            return this.position;
        }

        #region movement
        public void moveStraight(float speedFactor, int rotation)
        {
            if (rotation == 0)
            {
                this.position.Y += speedFactor * this.movementSpeed;
            }
            else if (rotation == 2) { 
                this.position.Y -= speedFactor * this.movementSpeed; }
            else if(rotation == 1) { this.position.X += speedFactor * this.movementSpeed; }
            else{this.position.X -= speedFactor * this.movementSpeed;}
        }

        public void moveCurve(float speedFactor, int roadRotation, Vector2 centerPosition,Vector2 offset)
        {

            centerPosition -= new Vector2(offset.X,0);
            switch (roadRotation)
            {

                case 0:
                    //Y+ bis midcenterposition Y+ bis Ende
                    if (this.position.Y < centerPosition.Y)
                    {
                        this.position.Y+= speedFactor * this.movementSpeed;
                    }
                    else if (this.position.Y >= centerPosition.Y) { this.position.X += speedFactor * this.movementSpeed; }
                    break;
                case 1:
                    //X- bis mcp Y+ bis ende
                    if (this.position.X > centerPosition.X)
                    {
                        this.position.X -= speedFactor * this.movementSpeed;
                    }
                    else if (this.position.Y <= centerPosition.Y) { this.position.Y += speedFactor * this.movementSpeed; }
                  
                    break;
                case 2:
                    //X+ bis mcp Y+ bis ende
                    if (this.position.X < centerPosition.X) { this.position.X += speedFactor * this.movementSpeed; } else { this.position.Y += speedFactor * this.movementSpeed; }
                    break;
                case 3:
                    //Y+ bis mcp X-- bis ende
                    if (this.position.Y <= centerPosition.Y) { this.position.Y += speedFactor * this.movementSpeed; }
                    else { this.position.X--; }
                    break;


            }
            //todo bis zum Mittelpunkt gehen, rotieren bis zum ende des Feldes gehen
        }

        public int turn90(int rotation)
        {
            switch (rotation)
            {
                case 0: return 90;
                case 1: return 90;
                case 2: return -90;
                case 3: return -90;
                default: return 0;
            }
        }
        #endregion

        public Vector2 currentEnemyField(Vector2 offset)
        {
            Vector2 currentField;
            currentField.X = (int)(this.position.X / offset.X);
            currentField.Y = (int)(this.position.Y / offset.Y);
            return currentField;
        }
    }



}
