﻿//&p-Controlador
//&b=70
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Programa7
{
    class Controlador
    {
        private Archivo archivo;
        static double EPS = 0.0000000001;

        private double sumWi;
        private double sumWiXi;
        private double sumWiYi;
        private double sumWiZi;
        private double sumWi2;

        private double sumXi;
        private double sumXiYi;
        private double sumXiZi;
        private double sumXi2;

        private double sumYi;
        private double sumYiZi;
        private double sumYi2;

        private double sumZi;
        
        //&i
        public Controlador()
        {
            sumWi = 0;
            sumWiXi = 0;
            sumWiYi = 0;
            sumWiZi = 0;
            sumWi2 = 0;

            sumXi = 0;
            sumXiYi = 0;
            sumXiZi = 0;
            sumXi2 = 0;

            sumYi = 0;
            sumYiZi = 0;
            sumYi2 = 0;

            sumZi = 0;
        }


        /// <summary>
        /// Procesa el archivo y lee línea por línea de este, calculando totales correspondientes
        /// </summary>
        /// <param name="nombreArchivo"></param>
        //&i
        public void ProcesarArchivo(string nombreArchivo)
        {
            archivo = new Archivo();
            StreamReader entrada = null;
            bool bAbierto = false;
            try
            {
                entrada = File.OpenText(nombreArchivo);
                bAbierto = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            if (bAbierto)
            {
                try
                {
                    string sLinea = entrada.ReadLine();
                    string[] arrNums = sLinea.Split(',');
                    archivo.Wk = double.Parse(arrNums[0]);
                    archivo.Xk = double.Parse(arrNums[1]);  //&m
                    archivo.Yk = double.Parse(arrNums[2]);
                    sLinea = entrada.ReadLine();
                    double wi = 0, xi = 0, yi = 0, zi = 0;
                    while (sLinea != null)
                    {
                        string[] arrCuadruplos = sLinea.Split(','); //&m
                        wi = double.Parse(arrCuadruplos[0]);
                        xi = double.Parse(arrCuadruplos[1]);
                        yi = double.Parse(arrCuadruplos[2]);
                        zi = double.Parse(arrCuadruplos[3]);

                        sumWi += wi;
                        sumWiXi += (wi * xi);
                        sumWiYi += (wi * yi);
                        sumWiZi += (wi + zi);
                        sumWi2 += (wi * wi);

                        sumXi += xi;    //&m
                        sumXiYi += (xi * yi);   //&m
                        sumXiZi += (xi * zi);
                        sumXi2 += (xi * xi);  //&m

                        sumYi += yi;    //&m
                        sumYiZi += (yi * zi);
                        sumYi2 += (yi * yi);  //&m

                        sumZi += zi;
                        archivo.Cuadruplos++;   //&m
                        sLinea = entrada.ReadLine();
                    }
                    //&d=5


                    //archivo.toString();
                    double[,] temp = new double[,] { { archivo.Cuadruplos, sumWi, sumXi, sumYi, sumZi },
                                                       { sumWi, sumWi2, sumWiXi, sumWiYi, sumWiZi},
                                                        { sumXi, sumWiXi, sumXi2, sumXiYi, sumXiZi}, 
                                                        { sumYi, sumWiYi, sumXiYi, sumYi2, sumYiZi} };
                    double[] x = Gauss(temp);

                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }


        public double[] Gauss(double [,] A)
        {
            int i, j, k, n = 4;
            /*for (i = 0; i < n; i++)                    //Pivotisation
                for (k = i + 1; k < n; k++)
                    if (A[i,i] < A[k,i])
                        for (j = 0; j <= n; j++)
                        {
                            double temp = A[i,j];
                            A[i,j] = A[k,j];
                            A[k,j] = temp;
                        }
            */

            for (i = 0; i < n - 1; i++)            //loop to perform the gauss elimination
                for (k = i + 1; k < n; k++)
                {
                    double t = A[k,i] / A[i,i];
                    for (j = 0; j <= n; j++)
                        A[k,j] = A[k,j] - t * A[i,j];    //make the elements below the pivot elements equal to zero or elimnate the variables
                }

            double[] x = new double[n];
            for (i = n - 1; i >= 0; i--)                //back-substitution
            {                        //x is an array whose values correspond to the values of x,y,z..
                x[i] = A[i,n];                //make the variable to be calculated equal to the rhs of the last equation
                for (j = i + 1; j < n; j++)
                {
                    if (j != i)            //then subtract all the lhs values except the coefficient of the variable whose value                                   is being calculated
                        x[i] = x[i] - A[i, j] * x[j];
                }
                x[i] = x[i] / A[i,i];            //now finally divide the rhs by the coefficient of the variable to be calculated
            }
            Console.WriteLine("\nThe values of the variables are as follows:\n");
            for (i = 0; i < n; i++)
                Console.WriteLine(x[i] + "  ");           // Print the values of x, y,z,....    
            return x;
        }


        //&d=3

        //&d=8

        /// <summary>
        /// Se calcula el valor de b0
        /// </summary>
        /// <returns>valor tipo doble b0</returns>
        //&i
        private double CalcularB0()
        {
            return sumYi / archivo.Cuadruplos - archivo.B1 * (sumXi / archivo.Cuadruplos);  //&m
        }

        /// <summary>
        /// Se calcula el valor de b1
        /// </summary>
        /// <returns>valor tipo doble b1</returns>
        //&i
        private double CalcularB1()
        {
            return (sumXiYi - (archivo.Cuadruplos * (sumXi / archivo.Cuadruplos) * (sumYi / archivo.Cuadruplos))) /   //&m
                (sumXi2 - (archivo.Cuadruplos * ((sumXi / archivo.Cuadruplos) * (sumXi / archivo.Cuadruplos))));    //&m
        }

        //&d=3
    }
}
