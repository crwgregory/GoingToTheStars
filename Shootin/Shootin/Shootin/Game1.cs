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
        
        Texture2D Moon;
        Texture2D Sun1Tex;
        Texture2D Ship;
        Texture2D BackgroundTexture;
        Texture2D BlackHoleIMG;
        Texture2D TestPOS;

        Vector2 MoonPosition = Vector2.Zero;
        Vector2 Sun1 = Vector2.Zero;
        Vector2 Sun2 = Vector2.Zero;
        Vector2 NewMoonPosition = Vector2.Zero;
        Vector2 ShipPos = Vector2.Zero;
        Vector2 TestPos = Vector2.Zero;

        Rectangle ShipRec;
        Rectangle blackHoleRectangle;
        
        VectorPM VectorManipulation = new VectorPM();
        VectorPM Velocity = new VectorPM();
        VectorPM SunVec1 = new VectorPM();
        VectorPM SunVec2 = new VectorPM();
        VectorPM BlackHole = new VectorPM();
        VectorPM Acceleration = new VectorPM();

        Satellite SirShip = new Satellite();
        Satellite SunSAT1 = new Satellite();
        Satellite SunSAT2 = new Satellite();

        float rotation = 0;

        SpriteFont Font;

        Random rng;

        bool arrowKeyPressed = false;
        bool isGoalHit;


        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            
            ShipRec.Height = 40;
            ShipRec.Width = 40;

            graphics.IsFullScreen = false;
            graphics.PreferredBackBufferHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height - 100;  //sets the size of the screen
            graphics.PreferredBackBufferWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width - 300;

            this.BlackHole.Mass = 500;

            IsMouseVisible = true;

            NewMoonPosition.X = (float)VectorManipulation.XAxis;

            this.rng = new Random();
        }

       
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            this.SirShip.isShip = true;
            this.SirShip.fuel = 70000;
            this.SunSAT1 = new Satellite();
            this.SunSAT2 = new Satellite();
            this.SunSAT1.Movement.Place(300, 400);
            this.SunSAT1.PMPOS.InheritVector(this.SunSAT1.Movement);
            this.SunSAT2.Movement.Place(550, 400);
            this.SunSAT2.PMPOS.InheritVector(this.SunSAT2.Movement);
            this.SunSAT1.Movement.Velocity(0, 1.5);
            this.SunSAT2.Movement.Velocity(0, -1.5);

            isGoalHit = false;

            base.Initialize();
        }

        
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            Moon = Content.Load<Texture2D>("rock_planet_sprite_by_fidgetwidget-d32erpc");
            Ship = Content.Load<Texture2D>("gravship");
            Sun1Tex = Content.Load<Texture2D>("Sun");
            BackgroundTexture = Content.Load<Texture2D>("SpaceBlueNebula");
            BlackHoleIMG = Content.Load<Texture2D>("blackhole-icon");
            TestPOS = Content.Load<Texture2D>("Ship");

            this.Font = Content.Load<SpriteFont>("SpriteFont1");

            MoonPosition = new Vector2((graphics.GraphicsDevice.Viewport.X/ 2) + (Moon.Width / 2), (graphics.GraphicsDevice.Viewport.Y/ 2));
            //ShipPos = new Vector2((graphics.GraphicsDevice.Viewport.Width / 2) - (Ship.Width / 2), (graphics.GraphicsDevice.Viewport.Height / 2) - (Ship.Height / 2));
            //SirShip.PMPOS.XAxis = ShipPos.X; SirShip.PMPOS.YAxis = ShipPos.Y;
            //Sun1 = new Vector2((graphics.GraphicsDevice.Viewport.Width / 3.5f) - (Sun1Tex.Width / 2), (graphics.GraphicsDevice.Viewport.Height / 2) - (Sun1Tex.Height / 2));
            Sun1 = new Vector2((float)this.SunSAT1.Movement.XAxis, (float)this.SunSAT1.Movement.YAxis);
            Sun2 = new Vector2((float)this.SunSAT2.Movement.XAxis, (float)this.SunSAT2.Movement.YAxis);
            //Sun2 = new Vector2((graphics.GraphicsDevice.Viewport.Width / 1.5f) - (Sun1Tex.Width / 2), (graphics.GraphicsDevice.Viewport.Height / 2) - (Sun1Tex.Height / 2));
            ShipRec.X = (int)ShipPos.X; ShipRec.Y = (int)ShipPos.Y;

            SunVec1.XAxis = Sun1.X + 32; SunVec1.YAxis = Sun1.Y + 32;
            SunVec2.XAxis = Sun2.X + 32; SunVec2.YAxis = Sun2.Y + 32;

            //SunSAT1.Movement.XAxis = SunVec1.XAxis; SunSAT1.Movement.YAxis = SunVec1.YAxis;
            //SunSAT2.Movement.XAxis = SunVec2.XAxis; SunSAT2.Movement.YAxis = SunVec2.YAxis;
            VectorManipulation.Create((graphics.GraphicsDevice.Viewport.Width / 2) - (Sun1Tex.Width / 2), (graphics.GraphicsDevice.Viewport.Height / 2) - (Sun1Tex.Height / 2), Math.PI / 9, 3, 0.1);
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
            SunVec1.Mass = 1000;
            SunVec2.Mass = 1000;
            //this.SunSAT1.Movement.GravitateTo(SunVec2);
            //this.SunSAT2.Movement.GravitateTo(SunVec1);

            //this.SunSAT1.UpDate();
            this.SunSAT2.UpDate();

            //game updates one sun before the other causing them to move off sreen eventually
            //this.Sun1.X = (float)this.SunSAT1.PMPOS.XAxis; this.Sun1.Y = (float)this.SunSAT1.PMPOS.YAxis;
            this.Sun2.X = (float)this.SunSAT2.PMPOS.XAxis; this.Sun2.Y = (float)this.SunSAT2.PMPOS.YAxis;

            //this.SunVec1.XAxis = this.SunSAT1.PMPOS.XAxis; this.SunVec1.YAxis = this.SunSAT1.PMPOS.YAxis;
            this.SunVec2.XAxis = this.SunSAT2.PMPOS.XAxis; this.SunVec2.YAxis = this.SunSAT2.PMPOS.YAxis;

            if (IsKeyPressed())
            {
                arrowKeyPressed = true;
            }
            if (arrowKeyPressed)
            {
                this.SirShip.Movement.GravitateTo(BlackHole);
                this.SirShip.Movement.GravitateTo(SunVec1);      //set sun gravity and have the ship gravitate towards it
                //this.SirShip.Movement.GravitateTo(SunVec2);
                SirShip.UpDate();
            }
            
            

            this.ShipRec.X = (int)this.SirShip.PMPOS.XAxis;     //set the ship position personvector to the rectangle that the computer will be updating
            this.ShipRec.Y = (int)this.SirShip.PMPOS.YAxis;

            if (this.ShipRec.X > graphics.PreferredBackBufferWidth) //used for putting the ship on the other side of the screen if it goes off
            {
                this.SirShip.Movement.XAxis = 0;
            }
            if (this.ShipRec.X < 0)
            {
                this.SirShip.Movement.XAxis = graphics.PreferredBackBufferWidth;
            }
            if (this.ShipRec.Y > graphics.PreferredBackBufferHeight)
            {
                this.SirShip.Movement.YAxis = 0;
            }
            if (this.ShipRec.Y < 0)
            {
                this.SirShip.Movement.YAxis = graphics.PreferredBackBufferHeight;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Back))
            {
                this.Exit();
            }
            base.Update(gameTime);
        }

        private bool IsKeyPressed()
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                return true;  
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                return true;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Up))
            {
                return true;
            }
            return false;
        }

        private void DrawScenery()
        {
            int screenWidth = 0;
            int screenHeight = 0;

            int bl = BlackHoleIMG.Bounds.Height;

            screenHeight = graphics.PreferredBackBufferHeight;
            screenWidth = graphics.PreferredBackBufferWidth;

            Rectangle screenRectangle = new Rectangle(0, 0, screenWidth, screenHeight);
            blackHoleRectangle = new Rectangle(900, 200, 100, 100);

            this.BlackHole.XAxis = blackHoleRectangle.Center.X - 76;
            this.BlackHole.YAxis = blackHoleRectangle.Center.Y -76;

            spriteBatch.Draw(BackgroundTexture, screenRectangle, Color.White);
            spriteBatch.Draw(BlackHoleIMG, blackHoleRectangle, null, Color.White, rotation = rotation + 0.01f, new Vector2(76, 76), SpriteEffects.None, 0);
        }

        private bool HitGoal()
        {
            if (this.ShipRec.Center.X == blackHoleRectangle.Center.X
                && this.ShipRec.X == blackHoleRectangle.Center.Y
                ) //used for putting the ship on the other side of the screen if it goes off
            {
                return true;
            }
            //this.TestPos.X = blackHoleRectangle.Center.X;
            //this.TestPos.Y = blackHoleRectangle.Center.Y;

            //this.TestPos.X = this.ShipRec.X;
            //this.TestPos.Y = this.ShipRec.Y;
            //if (this.ShipRec.X == blackHoleRectangle.Bottom)
            //{
            //    this.SirShip.Movement.XAxis = graphics.PreferredBackBufferWidth;
            //}
            //if (this.ShipRec.X == blackHoleRectangle.Bottom)
            //{
            //    this.SirShip.Movement.YAxis = 0;
            //}
            //if (this.ShipRec.X == blackHoleRectangle.Bottom)
            //{
            //    this.SirShip.Movement.YAxis = graphics.PreferredBackBufferHeight;
            //}
            return false;
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            // TODO: Add your drawing code here

            //Display an 'press enter to start gravity png' 

           
            spriteBatch.Begin();
            DrawScenery();

            if (!arrowKeyPressed)
            {
                spriteBatch.DrawString(Font, "Press UP, LEFT, or RIGHT to start.", new Vector2(graphics.PreferredBackBufferWidth/2.8f, 50), Color.White);
            }
            if (!isGoalHit)
            {
                isGoalHit = HitGoal(); 
            }
            if (isGoalHit)
            {
                spriteBatch.DrawString(Font, "WON", new Vector2(graphics.PreferredBackBufferWidth/2.8f, 50), Color.White);
            }

            spriteBatch.DrawString(Font, this.SirShip.fuel.ToString(), new Vector2(50, 50), Color.White);
            spriteBatch.Draw(Ship, ShipRec, null, Color.White, this.SirShip.rotation, new Vector2(64, 64), SpriteEffects.None, 0);

            spriteBatch.Draw(TestPOS, TestPos, Color.White);
            //spriteBatch.Draw(Sun1Tex, Sun2, Color.White);
            spriteBatch.Draw(Sun1Tex, Sun1, Color.White);
      
            spriteBatch.End();
            
            base.Draw(gameTime);
        }
    }
}
