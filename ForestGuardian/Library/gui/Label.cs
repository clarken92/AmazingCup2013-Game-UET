﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Library
{
    public class Label
    {
        protected Texture2D texture;

        protected Vector2 position;
        //protected Rectangle bounds;
        protected float layer_depth = 0.2f;
        protected float Rotation = 0.0f;
        protected float Scale = 1.0f;
        protected Color color = Color.White;

        public Label() { }
        public Label(Texture2D texture, Vector2 position)
        {
            this.texture = texture;
            this.position = position;

            //this.bounds = new Rectangle((int)position.X, (int)position.Y, (int)(texture.Width), (int)(texture.Height));
        }
        public int Width { get { return texture.Bounds.Width; } }
        
        public int  Height { get { return texture.Bounds.Height; } }

        public virtual Texture2D Texture
        {
            get { return texture; }
            set { texture = value; }
        }
        public virtual Vector2 Position
        {
            get { return position; }
            set { position = value;}
        }

        public virtual Vector2 Center
        {
            get { return new Vector2(position.X + texture.Bounds.Width / 2, position.Y + texture.Bounds.Height / 2); }
            set { position.X = value.X - texture.Bounds.Width / 2; position.Y = value.Y - texture.Bounds.Height / 2; }
            
        }
        
        public virtual float LayerDepth{
            get { return layer_depth; }
            set { layer_depth = value; }
        }

        public virtual Color Color
        {
            get { return color; }
            set { color = value; }
        }

        public bool InBound(Vector2 pos)
        {
            Rectangle bounds = new Rectangle((int)position.X, (int)position.Y, (int)(texture.Width), (int)(texture.Height));
            return bounds.Contains((int)pos.X, (int)pos.Y);
        }

        public bool InBound(float x,float y)
        {
            Rectangle bounds = new Rectangle((int)position.X, (int)position.Y, (int)(texture.Width), (int)(texture.Height));
            return bounds.Contains((int) x, (int) y);
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, this.position, null, color, Rotation, Vector2.Zero, Scale, SpriteEffects.None, layer_depth);
        }
    }
}
