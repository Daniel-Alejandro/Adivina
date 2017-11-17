using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adivina
{
    class Nivel
    {
        int PRUEBA;
        //Just used to have a change handler on an int

        private int _nivel;

        public delegate void ChangeHandler(Nivel nivel);
        public event ChangeHandler Changed;

        public Nivel(int nivelq = 1)
        {
            nivel = nivelq;
        }

        public int nivel
        {
            get
            {
                return _nivel;
            }
            set
            {
                _nivel = value;
                try
                {
                    Changed(this);
                }
                catch (NullReferenceException e)
                {
                    Console.WriteLine(e);
                }
            }
        }

        public void Add(int i)
        {
            _nivel += i;
            try
            {
                Changed(this);
            }
            catch (NullReferenceException e)
            {
                Console.WriteLine(e);
            }
        }

        public void Subtract(int i)
        {
            _nivel -= i;
            try
            {
                Changed(this);
            }
            catch (NullReferenceException e)
            {
                Console.WriteLine(e);
            }
        }

        public static Nivel operator +(Nivel l1, int i)
        {
            l1.Add(i);
            return l1;
        }

        public static Nivel operator ++(Nivel l1)
        {
            l1.Add(1);
            return l1;
        }

        public static Nivel operator -(Nivel l1, int i)
        {
            l1.Subtract(i);
            return l1;
        }

        public static Nivel operator --(Nivel l1)
        {
            l1.Subtract(1);
            return l1;
        }

        public static bool operator ==(Nivel l1, Nivel l2)
        {
            if (l1.nivel == l2.nivel)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool operator !=(Nivel l1, Nivel l2)
        {
            return !(l1 == l2);
        }

        public override int GetHashCode()
        {
            return _nivel.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (obj is Nivel)
            {
                return _nivel == ((Nivel)obj)._nivel;
            }
            else
            {
                return false;
            }
        }





















    }
}
