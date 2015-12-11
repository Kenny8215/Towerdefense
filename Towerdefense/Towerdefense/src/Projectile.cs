using System;
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
            this.Position = position;
            this.speed = speed;
            this.damage = damage;
            this.g = g;
        }

        public bool move()
        {
            if (closestEnemy != null)
            {
                target = closestEnemy.Position - this.position;
                target.Normalize();
                position.X = position.X + (target.X * speed );
                position.Y = position.Y + (target.Y * speed );
                if (closestEnemy.Position.X + speed * 10 >= position.X && closestEnemy.Position.Y + speed * 10 >= position.Y && closestEnemy.Position.X - speed * 10 <= position.X && closestEnemy.Position.Y <= position.Y)
                {
                    closestEnemy.damage(damage, g);
                    return true;
                }
            }
            return false;
        }

        public void Draw(SpriteBatch spriteBatch) {
            spriteBatch.Draw(this.Sprite, this.Position, null, null, null, 0F, new Vector2(0.1F,0.1F), Color.White, SpriteEffects.None, 1F);
        }

        public Texture2D Sprite {
            get { return this.sprite; }
            set { this.sprite = value; }
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
