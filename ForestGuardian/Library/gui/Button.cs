﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

namespace Library
{
    public enum ButtonStatus
    {
        Normal,
        Hovering,
        Pressing,
    }

    public class  Button
    {
        protected MouseState previousState;
        protected MouseState currentState;

        // The the different state textures.
        protected Texture2D hoverTexture;
        protected Texture2D pressTexture;
        protected Texture2D normalTexture;
        protected Texture2D texture;

        protected Vector2 position;
        //protected Rectangle bounds;

        protected float layer_depth = 0.1f;
        //protected float Rotation = 0.0f;
        //protected float Scale = 1.0f;

        protected ButtonStatus state = ButtonStatus.Normal;

        public event EventHandler Clicked;
        public event EventHandler Pressed;
        public event EventHandler Hovered;

        public Button(Texture2D normalTexture, Texture2D hoverTexture, Texture2D pressTexture, Vector2 position)
        {
            this.hoverTexture = hoverTexture;
            this.pressTexture = pressTexture;
            this.normalTexture = normalTexture;
            this.texture = normalTexture;

            this.position = position;
            //this.bounds = new Rectangle((int)position.X, (int)position.Y, (int)(normalTexture.Width), (int)(normalTexture.Height));
        }
        public float PositionX
        {
            get { return position.X; }
            set { position.X = value; }
        }
        public float PositionY
        {
            get { return position.Y; }
            set { position.Y = value; }
        }
        public Vector2 Position
        {
            get { return position; }
            set { position = value; }
        }
        public Vector2 Center
        {
            get { return new Vector2(position.X + normalTexture.Bounds.Width / 2, position.Y + normalTexture.Bounds.Height / 2); }
            set
            {
                position.X = value.X - normalTexture.Bounds.Width / 2; position.Y = value.Y - normalTexture.Bounds.Height / 2;
                //bounds = new Rectangle((int)position.X, (int)position.Y, (int)(normalTexture.Width), (int)(normalTexture.Height));
            }
        }

        public bool InBound(Vector2 pos)
        {
            Rectangle bounds = new Rectangle((int)position.X, (int)position.Y, (int)(texture.Bounds.Width), (int)(texture.Bounds.Height));
            return bounds.Contains((int)pos.X, (int)pos.Y);
        }

        public bool InBound(float x,float y)
        {
            Rectangle bounds = new Rectangle((int)position.X, (int)position.Y, (int)(texture.Bounds.Width), (int)(texture.Bounds.Height));
            return bounds.Contains((int)x, (int)y);
        }

        public virtual void Update(GameTime gameTime)
        {
            currentState = Mouse.GetState();

            bool isMouseOver = InBound(currentState.X, currentState.Y);

            if (isMouseOver && state != ButtonStatus.Pressing)
            {
                //Trang thai la MouseOver
                state = ButtonStatus.Hovering;
                //Neu khong phai hover
                if (Hovered != null) { Hovered(this, EventArgs.Empty); }
            }
            else if (isMouseOver == false && state != ButtonStatus.Pressing)
            {
                state = ButtonStatus.Normal;
            }

            //Trang thai Pressed
            if (currentState.LeftButton == ButtonState.Pressed)
            {
                if (isMouseOver == true)
                {
                    state = ButtonStatus.Pressing;
                    if (Pressed != null) { Pressed(this, EventArgs.Empty); }
                }
                //NTA added
                else
                {
                    state = ButtonStatus.Normal;
                }
            }

            //Trang thai Clicked
            if (currentState.LeftButton == ButtonState.Released &&
                previousState.LeftButton == ButtonState.Pressed)
            {
                if (isMouseOver == true)
                {
                    state = ButtonStatus.Hovering;
                    if (Clicked != null) { Clicked(this, EventArgs.Empty); }
                }
                //Neu click ra ngoai thi coi nhu ko click
                else if (state == ButtonStatus.Pressing)
                {
                    state = ButtonStatus.Normal;
                }
            }

            MouseSound();

            //Cap nhat mouse state
            previousState = currentState;

        }

        public virtual void MouseSound()
        {
            if (currentState.LeftButton == ButtonState.Pressed &&
                        previousState.LeftButton != ButtonState.Pressed &&
                        state == ButtonStatus.Pressing)
                AudioManager.soundBank.PlayCue("mouse_click");
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            switch (state)
            {
                case ButtonStatus.Pressing:
                    if (pressTexture != null) { texture = pressTexture; }
                    else { texture = normalTexture; }
                    break;
                case ButtonStatus.Hovering:
                    if (hoverTexture != null) { texture = hoverTexture; }
                    else { texture = normalTexture; }
                    break;
                case ButtonStatus.Normal:
                    texture = normalTexture;break;
            }
            spriteBatch.Draw(texture, this.position, null, Color.White, 0.0f, Vector2.Zero, 1.0f, SpriteEffects.None, layer_depth);
        }
    }
}