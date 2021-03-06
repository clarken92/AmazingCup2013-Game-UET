﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Library
{
    public class AxeMan : Enemy
    {
        public static Texture2D TEXTURE;
        public static Animation MOVE_ANIMATION;
        public static float MOVE_SPEED = 2.0f;
        public static float MAX_HEALTH = 60.0f;
        public static int VALUE = 1;

        public static string DEATH_SOUND = "death_1";

        public AxeMan(Vector2 center)
            : base(center, MAX_HEALTH, VALUE, MOVE_SPEED) { deathSound = DEATH_SOUND; }

        public AxeMan(Animation animation, Vector2 position, Anchor a)
            : base(animation, position, a, MAX_HEALTH, VALUE, MOVE_SPEED) { deathSound = DEATH_SOUND; }

        public override void setMoveAnimation()
        {
            animation = MOVE_ANIMATION;
        }
    }
}
