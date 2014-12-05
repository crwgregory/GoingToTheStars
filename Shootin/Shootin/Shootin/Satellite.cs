using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;




namespace Shootin
{

    class Satellite 
    {
        
        public bool Thrusting { get; set; }

        public Vector2 Position { get; set; }
        public Vector2 Direction { get; set; }
        
        private Texture2D ShipPNG { get; set; }

        public VectorPM Movement { get; set; }

        public VectorPM PMPOS { get; set; }

        private double Thrust { get; set; }
        private double XDIF { get; set; }
        private double YDIF { get; set; }
        private bool ACCELERATIONDIF { get; set; }

        public float rotation { get; set; }

        public float Accel { get; set; }
        public bool isShip { get; set; }
        private bool KeyHit{get;set;}

        public int fuel { get; set; }

        public Satellite() 
        {
            this.ACCELERATIONDIF = false;
            this.Accel = 0;
            this.rotation = 0;
            this.Position = new Vector2(0, 0);
            this.Movement = new VectorPM();
            this.Movement.Place(200, 200);
            this.PMPOS = new VectorPM();
            this.PMPOS.InheritVector(Movement);
            this.isShip = false;
            this.KeyHit = false;
        }

        public void UpDate()
        {
            
            if (isShip)
            {

                //this.PMPOS.XAxis = this.Position.X;

                //this.PMPOS.VelocityX = this.Movement.VelocityX;
                //this.PMPOS.VelocityY = this.Movement.VelocityY;



            if (Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                rotation -= .07f;
                this.KeyHit = true;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                rotation += .07f;
                this.KeyHit = true;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Up))
            {
                if (this.fuel > 0)
                {
                    //thrusting
                    this.fuel = this.fuel - 1;
                    Direction = new Vector2((float)Math.Cos(rotation), (float)Math.Sin(rotation));      //setting x and y cords of a derection based vector2 based on a rotation angle
                    this.KeyHit = true;

                    if (!ACCELERATIONDIF)
                    {
                        this.Movement.Acceleration(rotation, .13);
                    } 
                }
                
            }
            else    //deceleration
            {
                Thrusting = false;
                this.Movement.Acceleration(rotation, 0);
            } 
        }
        //uses the velocity settings and sets it to the XY axis of the ship
            
                this.Movement.UseVelocity(); 
            

        //check for acceleration, if acceleration is to high then do not accelerate
        //first we need to get the higher of the axis

        //get the difrence between the X and Y
        
        
        if (this.Movement.XAxis > this.PMPOS.XAxis)
        {
            XDIF = this.Movement.XAxis - this.PMPOS.XAxis;
        }
        else if (this.PMPOS.XAxis > this.Movement.XAxis)
        {
            XDIF = this.PMPOS.XAxis - this.Movement.XAxis;
        }

        if (this.Movement.YAxis > this.PMPOS.YAxis)
        {
            YDIF = this.Movement.YAxis - this.PMPOS.YAxis;
        }
        else if (this.PMPOS.YAxis > this.Movement.YAxis)
        {
            YDIF = this.PMPOS.YAxis - this.Movement.YAxis;
        }

        //if (XDIF > 10 || YDIF > 10 && isShip)
        //{
            //save position and velocity to re-use
            //ACCELERATIONDIF = true;
            this.PMPOS.UseVelocity();
            //this.Movement.XAxis = this.PMPOS.XAxis;
            //this.Movement.YAxis = this.PMPOS.YAxis;
        //}
        //else
        //{
            //ACCELERATIONDIF = false;
            this.PMPOS = this.PMPOS.Add(this.Movement);
        //}
            
            
            
        //updates the position of the ships XY to a Position Of Ship Vector
            
        }
    }
}
