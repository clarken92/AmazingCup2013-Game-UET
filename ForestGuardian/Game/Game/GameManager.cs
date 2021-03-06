using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

using ProjectMercury;
using ProjectMercury.Emitters;
using ProjectMercury.Modifiers;
using ProjectMercury.Renderers;

using Library;
using Data;

namespace CustomGame
{   
    public class GameManager : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SceneManager sceneManager;
        public static CustomRenderer renderer;
        public static KeyboardDispatcher keyboard_dispatcher;

        public GameManager()
        {
            graphics = new GraphicsDeviceManager(this);
            GameSetting.InitSetting(graphics);
            Content.RootDirectory = "Content";

            renderer = new CustomRenderer
            {
                GraphicsDeviceService = graphics
            };

            sceneManager = new SceneManager(this);
            Components.Add(sceneManager);
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            AudioManager.Initialize();

            //IsMouseVisible = true;
            keyboard_dispatcher = new KeyboardDispatcher(this.Window);
            DataSerializer.Initialize();

            base.Initialize();
            sceneManager.AddScene(new MainMenuScene());
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            renderer.LoadContent(Content);

            UserData.LoadSetting();
            MediaPlayer.Volume = (float)UserData.setting.music_volume * 0.01f;
            AudioManager.SetSoundVolume(UserData.setting.sound_volume);

            base.LoadContent();
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            base.UnloadContent();
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (!IsActive)
            {
                MediaPlayer.Pause();
                AudioManager.PauseMovingSound();
            }
            if (IsActive && (MediaPlayer.State == MediaState.Paused))
            {
                MediaPlayer.Resume();
            }

            InputManager.Update();
            AudioManager.Update();
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Transparent);
            base.Draw(gameTime);
        }

        public GraphicsDeviceManager getGraphics()
        {
            return graphics;
        }
    }
}
