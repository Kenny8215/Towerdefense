using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

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

        public Projectile(Vector2 enemyVector, Enemy closestEnemy, Vector2 position, int speed, int damage, GameManager g)
        {
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
                position.X = position.X + (target.X * speed * 10);
                position.Y = position.Y + (target.Y * speed * 10);
                if (closestEnemy.Position.X + speed * 10 >= position.X && closestEnemy.Position.Y + speed * 10 >= position.Y && closestEnemy.Position.X - speed * 10 <= position.X && closestEnemy.Position.Y <= position.Y)
                {
                    closestEnemy.damage(damage, g);
                    return true;
                }
            }
            return false;
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
