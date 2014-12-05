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

        public bool speedcap(VectorPM vec)
        {
            //take the hypotonose of both velocity and current axis's and compares them if they are to far apart then they will return false 
            //as an idea of a speed cap
            double hypotonusOfV = Math.Sqrt(vec.VelocityX * vec.VelocityX + vec.VelocityY * vec.VelocityY);
            double hypotonusOfA = Math.Sqrt(vec.XAxis * vec.XAxis + vec.YAxis * vec.YAxis);

            double percent = hypotonusOfV / hypotonusOfA;

            if (percent > .01)
            {
                return false;
            }
            else
            {
                return true;
            }

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

        public bool RotationDif(double rotation)
        {
            double hypotonusOfV = Math.Sqrt(this.VelocityX * this.VelocityX + this.VelocityY * this.VelocityY);
            double hypotonusOfA = Math.Sqrt(this.XAxis * this.XAxis + this.YAxis * this.YAxis);
            double R = rotation;
            //angel of velocity
            double angelOfVelocity = Math.Sin(this.VelocityY / hypotonusOfV);

            //angel of the axis's
            double angelOfAxis = Math.Sin(this.YAxis / hypotonusOfA);

            if (angelOfVelocity  > angelOfAxis)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

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

        public void InheritVector(VectorPM vec)
        {
            this.XAxis = vec.XAxis;
            this.YAxis = vec.YAxis;
        }

        public void SetVelocity(double angle, double length)
        {
            //this.VelocityX = Math.Cos(angle) * length;
            //this.VelocityY = Math.Sin(angle) * length;

            this.VelocityX =+ Math.Cos(angle) * length;
            this.VelocityY =+ Math.Sin(angle) * length;
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
