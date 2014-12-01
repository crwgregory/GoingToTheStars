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
    class VectorPM
    {
        
        public double XAxis { get; set; }
        public double YAxis { get; set; }

        public double VelocityX { get; set; }
        public double VelocityY { get; set; }
        private double Gravity { get; set; }

        public double Mass { get; set; }

        //Texture2D Texture;
        //Vector2 Vector;

        public VectorPM()
        {
            this.XAxis = 1;
            this.YAxis = 0;
            this.VelocityX = 0;
            this.VelocityY = 0;
            this.Gravity = 0;
        }

        public void Create(double x, double y, double Angle, double Length, double grav = 0)
        {
            //sets teh starting points of Vector
            this.XAxis = x;
            this.YAxis = y;
            //sets the head point of vector right away
            this.SetVelocity(Angle, Length);
            //sets grav
            //this.Acceleration(0, grav);
            this.Gravity = grav;
        }

        public double AngleTo(VectorPM vec)
        {
            return Math.Atan2(vec.YAxis - this.YAxis, vec.XAxis - this.XAxis);
        }

        public double DistanceTo(VectorPM vec)
        {
            double dx = vec.XAxis - this.XAxis;
            double dy = vec.YAxis - this.YAxis;

            return Math.Sqrt(dx * dx + dy * dy);
        }

        public void GravitateTo(VectorPM vec)
        {
            //VectorPM grav = new VectorPM();
            double dis = this.DistanceTo(vec);

            //grav.SetLength(vec.Mass / (dis * dis));
            //grav.SetAngle(this.AngleTo(vec));

            this.Acceleration(this.AngleTo(vec), vec.Mass / (dis * dis));
        }

        public void UseGravity()
        {
            //this.Acceleration(0, Gravity);
        }

        

        public void UseVelocity()
        {


            this.XAxis += this.VelocityX;
            this.YAxis += this.VelocityY;



            //double X = this.XAxis;
            //double VX = this.VelocityX;

            //double Y = this.YAxis;
            //double VY = this.VelocityY;



            //if (X < VX)
            //{
            //    double between = VX - X;
            //    double onePercentOf = VX * 0.01;

            //    X += between;
            //    if (X > (VX - onePercentOf))
            //    {
            //        X = VX;
            //    }
            //}
            //else if (X > VX)
            //{
            //    double between = X - VX;
            //    double onePercentOf = VX * 0.01;

            //    X -= between;
            //    if (X < (VX + onePercentOf))
            //    {
            //        X = VX;
            //    }
            //}

            //this.XAxis = X;







            //if (this.XAxis != this.VelocityX)
            //{
            //    if (this.XAxis < this.VelocityX)
            //    {
            //        //X axis is less then my velocity x axis, i need to move my x axis to the right increasing Xaxis to try and reach velocity axis
            //        //my X axis is my min, and my velocity is my max, add what I get to my X axis
            //        this.XAxis += ((this.XAxis +(this.VelocityX - this.XAxis))  //add my diffrence to the min value
            //            - this.XAxis)                                           //minus this number(the value) by the min
            //            / (this.VelocityX - this.XAxis);                        //then divide it by the diffrence
            //    }
            //    else
            //    {
            //        // X axis is greater than velocity, we need to subtract from x axis to try and reach velocity
            //        //do what we did before but subcract from
            //        this.XAxis -= ((this.XAxis - this.VelocityX) - this.XAxis) / (this.VelocityX - this.XAxis);
            //    }

            //    //now do the same for the Y axis
            //    if (this.YAxis < this.VelocityY)
            //    {
            //        this.YAxis += ((this.YAxis - this.VelocityY) - this.YAxis) / (this.VelocityY - this.YAxis);
            //    }
            //    else
            //    {
            //        this.YAxis -= ((this.YAxis - this.VelocityY) - this.YAxis) / (this.VelocityY - this.YAxis);
            //    }
            //}

            //this.XAxis = VelocityX;
            //this.YAxis = VelocityY;
        }

        public void Deceleration(double X, double Y)
        {
            if (X > 0)
            {
                this.VelocityX += X;
            }
            else
            {
                this.VelocityX -= X;
            }
            if (Y > 0)
            {
                this.VelocityY += Y;
            }
            else
            {
                this.VelocityY -= Y;
            }
        }

        //public void Acceleration(double AccelerationX, double AccelerationY)
        //{
        //    this.Add((VelocityX += AccelerationX), (VelocityY += AccelerationY));
        //}

        public void Acceleration(double angle, double length)
        {
            //this.Add((VelocityX += vec.GetLength()), (VelocityY += vec.GetLength()));

            this.VelocityX += Math.Cos(angle) * length;     //sets the length of the X axis
            this.VelocityY += Math.Sin(angle) * length;     //sets the length of the Y axis
        }

        public Vector2 AddToVector2(Vector2 Vec)
        {
            Vec.X += (float)this.XAxis;
            Vec.Y += (float)this.YAxis;

            return Vec;
        }

        public Vector2 MakesEqual(Vector2 Vec)
        {
            Vec.X = (float)this.XAxis;
            Vec.Y = (float)this.YAxis;
            return Vec;
        }

        public void InheritVector(Vector2 vec)
        {
            this.XAxis = vec.X;
            this.YAxis = vec.Y;
        }

        public void SetVelocity(double angle, double length)
        {
            this.VelocityX = Math.Cos(angle) * length;
            this.VelocityY = Math.Sin(angle) * length;
        }

        public void AddToAngleAndLength(double angle, double length)
        {
            this.XAxis += Math.Cos(angle) * length;     //sets the length of the X axis
            this.YAxis += Math.Sin(angle) * length;     //sets the length of the Y axis
        }

        public void SetAngle(double angle)
        {
            double length = this.GetLength();
            this.XAxis = (int)Math.Cos(angle) * length;
            this.YAxis = (int)Math.Sin(angle) * length;
        }

        public double GetAngle()
        {
            return Math.Atan2(this.XAxis, this.YAxis);
        }

        public void SetLength(double Length)
        {
            double angle = this.GetAngle();
            this.XAxis = (double)Math.Cos(angle) * Length;
            this.YAxis = (double)Math.Sin(angle) * Length;
        }

        public double GetLength()
        {
            return Math.Sqrt(this.XAxis * this.XAxis + this.YAxis * this.YAxis);
        }

        public void Place(double x, double y)
        {
            this.XAxis = x;
            this.YAxis = y;
        }

        public void Velocity(double x, double y)
        {
            //this.XAxis += x;
            //this.YAxis += y;
            this.VelocityX = x;
            this.VelocityY = y;
        }

        public VectorPM Add(VectorPM v2)
        {
            VectorPM vector = new VectorPM();
            vector.XAxis += v2.XAxis;
            vector.YAxis += v2.YAxis;
            return vector;
        }

        public void Add(double X, double Y)
        {
            this.XAxis += X;
            this.YAxis += Y;
        }

        public VectorPM Subcract(VectorPM v2)
        {
            VectorPM vector = new VectorPM();
            vector.XAxis -= v2.XAxis;
            vector.YAxis -= v2.YAxis;
            return vector;
        }

        public VectorPM Multiply(double val)
        {
            VectorPM vector = new VectorPM();
            double X = this.XAxis * val;
            double Y = this.YAxis * val;

            vector.XAxis = X;
            vector.YAxis = Y;
            return vector;
        }

        public VectorPM Divide(double val)
        {
            VectorPM vector = new VectorPM();
            double X = this.XAxis / val;
            double Y = this.YAxis / val;

            vector.XAxis = X;
            vector.YAxis = Y;
            return vector;
        }

        public void AddTo(VectorPM v2)
        {
            this.XAxis += v2.XAxis;
            this.YAxis += v2.YAxis;
        }

        public void SubtractFrom(VectorPM v2)
        {
            this.XAxis -= v2.XAxis;
            this.YAxis -= v2.YAxis;
        }

        public void MultiplyBy(double valX, double valY)
        {
            double X = this.XAxis * valX;
            double Y = this.YAxis * valY;
            this.XAxis = X;
            this.YAxis = Y;
        }
        public void DivideBy(double val)
        {
            double X = this.XAxis / val;
            double Y = this.YAxis / val;
            this.XAxis = X;
            this.YAxis = Y;
        }
    }
}
