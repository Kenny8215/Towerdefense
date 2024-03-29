﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Towerdefense
{
    class Projectile
    {
        private Enemy closestEnemy;
        private Vector2 enemyVector;
        private Vector2 position;
        private int speed;
        private int damage;
        private GameManager g;
        private Vector2 target;
        private Texture2D sprite;

        public Projectile(Texture2D sprite,Vector2 enemyVector, Enemy closestEnemy, Vector2 position, int speed, int damage, GameManager g)
        {
            this.sprite = sprite;
            this.enemyVector = enemyVector;
            this.closestEnemy = closestEnemy;
            Position = position;
            this.speed = speed;
            this.damage = damage;
            this.g = g;
        }

        public bool move(Player player)
        {
            if (closestEnemy != null)
            {
                target = closestEnemy.Position - position;
                target.Normalize();
                position.X = position.X + (target.X * speed );
                position.Y = position.Y + (target.Y * speed );
                if (closestEnemy.Position.X + speed * 10 >= position.X && closestEnemy.Position.Y + speed * 10 >= position.Y && closestEnemy.Position.X - speed * 10 <= position.X && closestEnemy.Position.Y <= position.Y)
                {
                    closestEnemy.damage(damage, g,player);
                    return true;
                }
            }
            return false;
        }

        public void Draw(SpriteBatch spriteBatch) {
            double angle = Math.Atan(target.Y / target.X);
            //angle = this.degToRad(angle);
            spriteBatch.Draw(Sprite, Position, null, null, new Vector2(sprite.Width / 2, sprite.Height / 2), (float)angle, new Vector2(0.2F, 0.2F), Color.White, SpriteEffects.None, 1F);
        }

        private double degToRad(double angle)
        {
            return (angle * (Math.PI / 180));
        }

        public Texture2D Sprite {
            get { return sprite; }
            set { sprite = value; }
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
    }
}
