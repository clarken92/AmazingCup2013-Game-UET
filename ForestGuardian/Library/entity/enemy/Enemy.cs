﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace Library
{
    public class EnemyType
    {
        public const string AXE_MAN = "AxeMan";
        public const string SAW_MAN = "SawMan";
    }
    public class Enemy : Sprite
    {
        public static Texture2D HEALTH_BAR_TEXTURE;

        protected float maxHealth;
        protected float health;
        protected float mSpeed;
        protected float hit_radius;
        protected int value;

        protected bool alive;

        protected Queue<Vector2> waypoints;
        protected static float destinationLimit = 4.0f;
        protected bool at_end;

        private float mSpeedReduce;
        private float mSlowDuration;

        public float Health
        {
            get { return health; }
        }

        public float Speed
        {
            get { return mSpeed; }
        }

        public float DistanceToDestination
        {
            get { return Vector2.Distance(mCenter, waypoints.Peek()); }
        }

        public bool atDestination
        {
            get { return DistanceToDestination <= destinationLimit; }
        }

        public bool atEnd
        {
            get { return at_end; }
        }

        public bool Alive
        {
            get { return alive; }
        }

        public int Value
        {
            get { return value; }
        }

        public float SpeedReduce
        {
            get { return mSpeedReduce; }
            set { mSpeedReduce = value; }
        }

        public float SlowDuration
        {
            get { return mSlowDuration; }
            set { mSlowDuration = value; }
        }

        public Enemy(Texture2D texture, Vector2 center, float maxHealth, int value, float move_speed)
            : base(texture, center)
        {
            this.maxHealth = maxHealth;
            this.health = maxHealth;

            this.value = value;
            this.alive = true;

            this.mSpeed = move_speed;
            this.at_end = false;
            this.layer_depth = 0.6f;
        }

        public Enemy(Texture2D texture, Vector2 position, Anchor a, float maxHealth, int value, float move_speed)
            : base(texture, position,a)
        {
            this.maxHealth = maxHealth;
            this.health = maxHealth;

            this.value = value;
            this.alive = true;

            this.mSpeed = move_speed;
            this.at_end = false;
            this.layer_depth = 0.6f;
        }

        public void lostHealth(float amount)
        {
            if (amount > 0)
            {
                health = health - amount;
            }
        }

        public void setWaypoints(Queue<Vector2> waypoints)
        {
            this.waypoints = waypoints;
        }

        public Vector2 getDirection()
        {
            Vector2 direction = waypoints.Peek() - mCenter;
            direction.Normalize();
            return direction;
        }

        public void Move(GameTime gameTime)
        {
            Vector2 direction = getDirection();
            float tmpSpeed = mSpeed;

            if (mSlowDuration <= 0)
            {
                mSpeedReduce = 0;
                mSlowDuration = 0;
            }

            if (mSpeedReduce != 0 && mSlowDuration >= 0)
            {
                tmpSpeed *= 1 - mSpeedReduce;
                //Console.Write("Time: ");
                //Console.WriteLine((float)gameTime.ElapsedGameTime.TotalSeconds);
                //Console.Write("Duration: ");
                //Console.WriteLine(mSlowDuration);
                mSlowDuration -= (float)gameTime.ElapsedGameTime.TotalSeconds;
            }

            Vector2 velocity = Vector2.Multiply(direction, tmpSpeed);

            if (DistanceToDestination > velocity.Length())
            {
                mCenter += velocity;
            }
            else
            {
                mCenter = waypoints.Peek();
            }
        }

        //public void checkHit(Bullet b)
        //{
        //    if (Vector2.Distance(mCenter, b.Center) < hit_radius)
        //    {
        //        this.lostHealth(b.Damage);
        //    }
        //}

        public override void Update(GameTime gameTime)
        {
            //Neu chua di het duong
            if (waypoints.Count > 0)
            {
                //Neu da toi mot vi tri waypoint
                if (atDestination) { waypoints.Dequeue(); }
                else { Move(gameTime); }
            }
            else{
                at_end = true;
                alive = false;
            }

            if (health <= 0) {
                alive = false;
            }
            base.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (alive)
            {
                //Ve health bar
                int healthHeight = HEALTH_BAR_TEXTURE.Bounds.Height;

                spriteBatch.Draw(HEALTH_BAR_TEXTURE,new Vector2(mPosition.X,mPosition.Y - healthHeight), Color.Red);

                float healthPercentage = health/maxHealth;
                float current_healthWidth = (float)HEALTH_BAR_TEXTURE.Bounds.Width * healthPercentage;

                spriteBatch.Draw(HEALTH_BAR_TEXTURE,
                                new Rectangle((int)mPosition.X, (int)mPosition.Y - healthHeight, (int)current_healthWidth, healthHeight),
                                Color.GreenYellow
                );
            }
            base.Draw(spriteBatch);
        }
    }

}
