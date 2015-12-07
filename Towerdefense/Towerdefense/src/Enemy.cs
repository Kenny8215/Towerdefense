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
        private Vector2 lastCenterPosition;

        private int walkDistance;
        private int rotation;
        private float movementSpeed;
        private string resistance;

        private bool isBoss;
        private bool isFlying;
        private bool hasTurned;

        public Enemy cloneMe()
        {
            return new Enemy(this.sprite, this.spritePath, this.hitPoints, this.position, this.walkDistance, this.rotation, this.movementSpeed, this.resistance, this.isBoss, this.isFlying);
        }

        #region Setter and Getter
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
            this.Rotation = 0;
            this.lastCenterPosition = new Vector2(-1, -1);
            this.position = new Vector2(30, 0);
        }

        public Enemy(Texture2D sprite, string spritePath, int hitPoints, Vector2 position, int walkDistance, int rotation, float movementSpeed, string resistance, bool isBoss, bool isFlying)
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
            this.spritePath = spritePath;
        }
#endregion

        public void drawEnemy(SpriteBatch spriteBatch)
        {

            Vector2 textCent = new Vector2(this.Sprite.Bounds.Center.X, this.Sprite.Bounds.Center.Y);

            spriteBatch.Draw(this.Sprite, this.position, null, null, textCent, rotInRad(), new Vector2(0.2F, 0.2F), Color.White, SpriteEffects.None, 1F);
        }
        #endregion 

        #region update
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
#endregion 

        #region movement
        public void moveStraight(float speedFactor, int roadRotation)
        {
            float mv = speedFactor * this.movementSpeed;
            if (this.Rotation >= 360) { this.Rotation = (int) normalizeDegree(this.Rotation); }
            if (this.Rotation == 0) { this.position.Y += mv; }
            else if (this.Rotation == 90) { this.position.X -= mv; }
            else if (this.Rotation == 180) { this.position.Y -= mv; }
            else if (this.Rotation == 270) { this.position.X += mv; }

        }

        public void moveCurve(float speedFactor, int roadRotation, Vector2 centerPosition,Vector2 offset)
        {
            if(this.lastCenterPosition != centerPosition){
                this.lastCenterPosition = centerPosition; hasTurned = false;
            }
            float mv = speedFactor * this.movementSpeed;
             int entryZeroY = 0 + roadRotation * 90;
             int outZeroX = 270 + roadRotation * 90;
             int entryZeroX = 90 + 90 * roadRotation;
             int outZeroY = 180 + 90 * roadRotation;

             if (entryZeroY >= 360 || outZeroX >= 360 || entryZeroX >= 360 || outZeroY >= 360 || this.Rotation >= 360) {
                entryZeroY = (int) normalizeDegree(entryZeroY);
                entryZeroX = (int) normalizeDegree(entryZeroX);
                outZeroY = (int) normalizeDegree(outZeroY);
                outZeroX = (int) normalizeDegree(outZeroX);
                this.Rotation = (int)normalizeDegree(this.Rotation);
             }

             if (this.Rotation == 0) { this.position.Y += mv; }
             else if (this.Rotation == 90) { this.position.X -= mv; }
             else if (this.Rotation == 180) { this.position.Y -= mv; }
             else if (this.Rotation == 270)
             {
                 this.position.X += mv;
             }

            if(!hasTurned){
                 if(this.Rotation == entryZeroY && ( (this.Rotation == 0 && this.Position.Y >= centerPosition.Y) || (this.Rotation == 90 && this.Position.X <= centerPosition.X) || (this.Rotation == 180 && this.Position.Y <= centerPosition.Y) || (this.Rotation == 270 && this.Position.X >= centerPosition.X ))){
                this.Rotation = outZeroX;
                     hasTurned = true;
             }
             else if (this.Rotation == entryZeroX && ( (this.Rotation == 0 && this.Position.Y >= centerPosition.Y) || (this.Rotation == 90 && this.Position.X <= centerPosition.X) || (this.Rotation == 180 && this.Position.Y <= centerPosition.Y) || (this.Rotation == 270 && this.Position.X >= centerPosition.X))) {
                 this.rotation = outZeroY;
                 hasTurned = true;
             }
            }
                 //todo bis zum Mittelpunkt gehen, rotieren bis zum ende des Feldes gehen
             
                 
        }

    public float normalizeDegree(float degree){
        degree = degree / 360;

        if (degree > 1)
        {
            degree -= 1;
            }

        degree *= 360;
        return degree;
    }
        #endregion

        #region helpFunctions
    public float rotInRad() { 
            switch (this.Rotation) {
                case 90:
                    return 1.5708F;
                case 180:
                    return 3.14159F;
                case 270:
                    return 4.71239F;
                default: return 0F;
        } }

    public Vector2 currentEnemyField(Vector2 offset)
    {
        Vector2 currentField;
        currentField.X = (int)(this.position.X / offset.X);
        currentField.Y = (int)(this.position.Y / offset.Y);
        return currentField;
    }
    #endregion
    
    }



}
