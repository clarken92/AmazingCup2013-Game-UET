﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Library
{
    public class SawMan: Enemy
    {
        public static Texture2D TEXTURE;
        public static Animation MOVE_ANIMATION;
        public static float MOVE_SPEED = 1.7f;
        public static float MAX_HEALTH = 90.0f;
        public static int VALUE = 2;

        public static string DEATH_SOUND = "death_2";
        
        public SawMan(Vector2 center)
            : base(center, MAX_HEALTH, VALUE, MOVE_SPEED) { deathSound = DEATH_SOUND; }

        public SawMan(Animation animation, Vector2 position, Anchor a)
            : base(animation, position, a, MAX_HEALTH, VALUE, MOVE_SPEED) { deathSound = DEATH_SOUND; }

        public override void setMoveAnimation()
        {
            animation = MOVE_ANIMATION;
        }
    }
}
