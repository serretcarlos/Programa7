﻿//&p-Archivo
//&b=46
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Programa7
{
    class Archivo
    {
        private double cuadruplos = 0;//&m
        private double wk;
        private double xk;
        private double yk;
        /*private double rxy;
        private double r2;*/ //&d=2
        private double b0;
        private double b1;
        private double b2;
        private double b3;
        private double zk;

        //&i
        public Archivo()
        {
            cuadruplos = 0;
            wk = 0;
            xk = 0;
            yk = 0;
            /*rxy = 0;
            r2 = 0;*/ //&d=2
            b0 = 0;
            b1 = 0;
            b2 = 0;
            b3 = 0;
            zk = 0;
        }

        //&i
        public double Cuadruplos    //&m
        {
            set { cuadruplos = value; } //&m
            get { return cuadruplos; }  //&m
        }

        //&i
        public double Wk
        {
            set { wk = value; }
            get { return wk; }
        }

        //&i
        public double Xk
        {
            set { xk = value; }
            get { return xk; }
        }

        //&i
        public double Yk
        {
            set { yk = value; }
            get { return yk; }
        }

        /*public double Rxy
        {
            set { rxy = value; }
            get { return rxy; }
        }*/ //&d=3



        /*public double R2
        {
            set { r2 = value; }
            get { return r2; }
        }*/  //&d=3

        //&i
        public double B0
        {
            set { b0 = value; }
            get { return b0; }
        }

        //&i
        public double B1
        {
            set { b1 = value; }
            get { return b1; }
        }

        //&i
        public double B2
        {
            set { b2 = value; }
            get { return b2; }
        }

        //&i
        public double B3
        {
            set { b3 = value; }
            get { return b3; }
        }
        
        //&i
        public void toString()
        {
            Console.WriteLine("N = " + cuadruplos + "\nwk = " + wk + "\nxk = " + xk + "\nyk = " + yk + "\n------------\nb0 = " + b0.ToString("N5") + //&m
                "\nb1 = " + b1.ToString("N5") + "\nb2 = " + b2.ToString("N5") + "\nb3 = " + b3.ToString("N5") + "\n------------\nzk = " + zk.ToString("N5")); //&m
        }
    }
}
