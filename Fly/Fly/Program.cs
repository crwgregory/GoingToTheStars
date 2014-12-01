using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fly
{
    class Program
    {
        static void Main(string[] args)
        {
            int SizeHeight = Console.LargestWindowHeight;
            int SizeWidth = Console.LargestWindowWidth;
            Console.BufferHeight = SizeHeight;
            Console.WindowHeight = SizeHeight;
            Console.BufferWidth = SizeWidth;
            Console.WindowWidth = SizeWidth;
            //Console.SetCursorPosition(SizeWidth / 2, SizeHeight / 2);
            //Console.Write("yoyo");

            //SolarSystem Game = new SolarSystem();

            VectorPM v1 = new VectorPM();
            VectorPM v2 = new VectorPM();
            VectorPM v3 = new VectorPM();
            v1.Place(10, 10);
            v2.Place(20, 20);

           
            Console.ReadKey();

            for (int i = 0; i < 10; i++)
            {
                v1.Velocity(5, 5);
                System.Threading.Thread.Sleep(500);
                
            }

            Console.ReadKey();
        }
    }

    class VectorPM
    {
        
        public double XAxis { get; set; }
        public double YAxis { get; set; }

        public int Acceleration { get; set; }

        //Texture2D Texture;
        //Vector2 Vector;

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

        public void SetLength(int Length)
        {
            double angle = this.GetAngle();
            this.XAxis = (int)Math.Cos(angle) * Length;
            this.YAxis = (int)Math.Sin(angle) * Length;
        }

        public double GetLength()
        {
            return Math.Sqrt(this.XAxis * this.XAxis + this.YAxis * this.YAxis);
        }

        public void Place(int x, int y)
        {
            this.XAxis = x;
            this.YAxis = y;
        }

        public void Velocity(int x, int y)
        {
            this.XAxis += x;
            this.YAxis += y;
        }

        public void SetAcceleration(int A)
        {
            this.Acceleration = A;
        }

        public void Create(int x, int y, int a)
        {
            this.XAxis = x;
            this.YAxis = y;
            this.Acceleration = a;
        }

        public VectorPM Add(VectorPM v2)
        {
            VectorPM vector = new VectorPM();
            vector.XAxis += v2.XAxis;
            vector.YAxis += v2.YAxis;
            return vector;
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

        public void MultiplyBy(double val)
        {
            double X = this.XAxis * val;
            double Y = this.YAxis * val;
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

    //class SolarSystem
    //{
    //    public VectorPM[] ArrayOfObjects { get; set; }


    //    public SolarSystem()
    //    {
    //        VectorPM Object = new VectorPM();
    //        Object.Place(Console.WindowWidth / 2, Console.WindowHeight / 2);
    //        Object.Draw();
    //    }
    }
}
