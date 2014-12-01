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

    class SirShip 
    {
        
        public bool Thrusting { get; set; }

        public Vector2 Position { get; set; }
        public Vector2 Direction { get; set; }
        
        private Texture2D ShipPNG { get; set; }

        public VectorPM Acceleration { get; set; }

        public VectorPM PMPOS { get; set; }

        private double Thrust { get; set; }

        public float rotation { get; set; }

        public float Accel { get; set; }

        public SirShip() 
        {
            this.Accel = 0;
            this.rotation = 0;
            this.Position = new Vector2(0, 0);
            this.Acceleration = new VectorPM();
            this.Acceleration.Place(500, 500);
            this.PMPOS = new VectorPM();
            
        }

        public void UpDate()
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                rotation -= .07f;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                rotation += .07f;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Up))
            {
                Direction = new Vector2((float)Math.Cos(rotation), (float)Math.Sin(rotation));      //setting x and y cords of a derection based vector2 based on a rotation angle
                
                this.Acceleration.Acceleration(rotation, .3);

            }
            else    //deceleration
            {
                Thrusting = false;
                if (Accel != 0)
                {
                    
                }
                this.Acceleration.Acceleration(rotation, 0);
            }

            //uses the velocity settings and sets it to the XY axis of the ship
            this.Acceleration.UseVelocity();

            //Position += Direction * Accel;

            //updates the position of the ships XY to a Position Of Ship Vector
            this.PMPOS = this.PMPOS.Add(this.Acceleration);

            
        }


        //if (this.Acceleration.GetLength() > 1)      
        //{
        //    this.Acceleration.SetLength(0.1); 
        //}
        //else
        //{
        //    this.Acceleration.SetLength(0);
        //}


        //if (Thrusting == false)
        //{
        //    Thrusting = true;
        //    this.Accel += .2f;
        //}

        //if (((this.Acceleration.VelocityX * this.Acceleration.VelocityX + this.Acceleration.VelocityY * this.Acceleration.VelocityY)
        //            - this.Acceleration.XAxis) / (this.Acceleration.XAxis - 
        //            (this.Acceleration.VelocityX * this.Acceleration.VelocityX + this.Acceleration.VelocityY * this.Acceleration.VelocityY) + 10) > 1)
        //        {
        //            if (true)
        //            {
                        
        //            }
        //        }
        
    }
}
