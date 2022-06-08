using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace Monogame_Time_and_Sound
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        MouseState mouseState;

        Texture2D defuseTexture;
        Rectangle defuseRect;
        Texture2D explosionTexture;
        Rectangle explosionRect;
        Texture2D bombtexture;
        Rectangle bombRectangle;

        SpriteFont titleFont;
        SoundEffect explode;

        bool defuse = false;
        bool boom = false;

        float seconds;
        float startTime;
        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            this.Window.Title = "Bomb";
            _graphics.PreferredBackBufferWidth = 800;
            _graphics.PreferredBackBufferHeight = 500;
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            bombtexture = Content.Load<Texture2D>("bomb");
            bombRectangle = new Rectangle(50, 50, 700, 400);
            explosionTexture = Content.Load<Texture2D>("boom");
            explosionRect = new Rectangle(50, 50, 700, 400);
            defuseTexture = Content.Load<Texture2D>("Yippee");
            defuseRect = new Rectangle(50, 50, 700, 400);


            titleFont = Content.Load<SpriteFont>("Score");
            explode = Content.Load<SoundEffect>("explosion");

            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            mouseState = Mouse.GetState();
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            seconds = (float)gameTime.TotalGameTime.TotalSeconds - startTime;
            if (mouseState.LeftButton == ButtonState.Pressed && seconds <= 14)
            {
                startTime = (float)gameTime.TotalGameTime.TotalSeconds;
                seconds = 0;
                defuse = true;
            }

            if (defuse == true && seconds >= 10)
                Exit();
            if (seconds >= 15)
            {
                explode.Play();
                startTime = (float)gameTime.TotalGameTime.TotalSeconds;
                defuse = false;
                boom = true;
            }            
            else if (boom == true && seconds >= explode.Duration.TotalSeconds)
                Exit();
            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            _spriteBatch.Begin();
            _spriteBatch.Draw(bombtexture, bombRectangle, Color.White);
            _spriteBatch.DrawString(titleFont, seconds.ToString("0.00"), new Vector2(270, 200), Color.Black);
            if (boom == true)
                _spriteBatch.Draw(explosionTexture, explosionRect, Color.White);
            if (defuse == true)
                _spriteBatch.Draw(defuseTexture, defuseRect, Color.White);




            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }

   
}
