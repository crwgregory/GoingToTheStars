using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Shootin
{

    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        SirShip _spaceShip;
        Texture2D Moon;
        Texture2D Sun1Tex;
        Texture2D Ship;

        Vector2 MoonPosition = Vector2.Zero;
        Vector2 Sun1 = Vector2.Zero;
        Vector2 NewMoonPosition = Vector2.Zero;
        Vector2 ShipPos = Vector2.Zero;
        Vector2 ShipPos2 = Vector2.Zero;

        Rectangle ShipRec;
        
        VectorPM VectorManipulation = new VectorPM();

        VectorPM Velocity = new VectorPM();

        VectorPM SunVec = new VectorPM();

        VectorPM Acceleration = new VectorPM();
        SirShip SirShip = new SirShip();
        



        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            //Velocity.SetAngleAndLength(Math.PI / 9, 5);
            //Acceleration.
            //VectorManipulation.XAxis = 100;
            //VectorManipulation.SetVelocity(0, -5);
            
            ShipRec.Height = 40;
            ShipRec.Width = 40;

            //graphics = new GraphicsDeviceManager(this);
            graphics.IsFullScreen = false;
            graphics.PreferredBackBufferHeight = 1000;
            graphics.PreferredBackBufferWidth = 1500;

            

            NewMoonPosition.X = (float)VectorManipulation.XAxis;
        }

       
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            Moon = Content.Load<Texture2D>("rock_planet_sprite_by_fidgetwidget-d32erpc");
            Ship = Content.Load<Texture2D>("Ship");
            Sun1Tex = Content.Load<Texture2D>("Sun");

            

            MoonPosition = new Vector2((graphics.GraphicsDevice.Viewport.X/ 2) + (Moon.Width / 2), (graphics.GraphicsDevice.Viewport.Y/ 2));
            ShipPos = new Vector2((graphics.GraphicsDevice.Viewport.Width / 2) - (Ship.Width / 2), (graphics.GraphicsDevice.Viewport.Height / 2) - (Ship.Height / 2));
            Sun1 = new Vector2((graphics.GraphicsDevice.Viewport.Width / 2) - (Sun1Tex.Width / 2), (graphics.GraphicsDevice.Viewport.Height / 2) - (Sun1Tex.Height / 2));
            ShipRec.X = (int)ShipPos.X; ShipRec.Y = (int)ShipPos.Y;

            SunVec.XAxis = Sun1.X + 32; SunVec.YAxis = Sun1.Y + 32;
            //CirclePos = new Vector2(0, graphics.GraphicsDevice.Viewport.Height - 10);
            //VectorManipulation.Place(0, 100);

            VectorManipulation.Create((graphics.GraphicsDevice.Viewport.Width / 2) - (Sun1Tex.Width / 2), (graphics.GraphicsDevice.Viewport.Height / 2) - (Sun1Tex.Height / 2), Math.PI / 9, 3, 0.1);

            //CirclePos = VectorManipulation.MakesEqual(CirclePos);
            
            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            // TODO: Add your update logic here
            //this.VectorManipulation.Velocity(10, 10);
            //this.CirclePos.X = (float)this.VectorManipulation.XAxis;
            //this.CirclePos.Y = (float)this.VectorManipulation.YAxis;

            //this.Velocity.AddToAngleAndLength(Math.PI / 6, .05);

            //CirclePos = this.Velocity.MakesEqual(CirclePos);

            //VectorManipulation.Acceleration(.1, .1);

            //VectorManipulation.UseVelocity();

            //CirclePos = VectorManipulation.MakesEqual(CirclePos);
            SunVec.Mass = 3000;
            this.SirShip.Acceleration.GravitateTo(SunVec);
            SirShip.UpDate();

            
            //this.SirShip.PMPOS.GravitateTo(SunVec);

            this.ShipRec.X = (int)this.SirShip.PMPOS.XAxis;
            this.ShipRec.Y = (int)this.SirShip.PMPOS.YAxis;

            if (this.ShipRec.X > graphics.PreferredBackBufferWidth)
            {
                this.SirShip.Acceleration.XAxis = 0;
            }
            if (this.ShipRec.X < 0)
            {
                this.SirShip.Acceleration.XAxis =  graphics.PreferredBackBufferWidth;
            }
            if (this.ShipRec.Y > graphics.PreferredBackBufferHeight)
            {
                this.SirShip.Acceleration.YAxis = 0;
            }
            if (this.ShipRec.Y < 0)
            {
                this.SirShip.Acceleration.YAxis = graphics.PreferredBackBufferHeight;
            }
            base.Update(gameTime);
        }





        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);

            // TODO: Add your drawing code here

            spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend);
            //spriteBatch.Draw(Moon, MoonPosition, Color.Blue);
            spriteBatch.Draw(Sun1Tex, Sun1, Color.White);
            spriteBatch.Draw(Ship, ShipRec, null, Color.White, this.SirShip.rotation, new Vector2(16, 15), SpriteEffects.None, 0);
            spriteBatch.End();
            //
            base.Draw(gameTime);
        }
    }
}
