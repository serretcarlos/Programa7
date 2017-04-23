//&p-Controlador
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
                        sumWiZi += (wi * zi);
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
                    double[,] temp = new double[,] { { archivo.Cuadruplos, sumWi, sumXi, sumYi, sumZi },
                                                       { sumWi, sumWi2, sumWiXi, sumWiYi, sumWiZi},
                                                        { sumXi, sumWiXi, sumXi2, sumXiYi, sumXiZi},
                                                        { sumYi, sumWiYi, sumXiYi, sumYi2, sumYiZi} };
                    double[] x = Gauss(temp);
                    archivo.B0 = x[0];
                    archivo.B1 = x[1];
                    archivo.B2 = x[2];
                    archivo.B3 = x[3];
                    archivo.ZK = archivo.B0 + (archivo.B1 * archivo.Wk) + (archivo.B2 * archivo.Xk) + (archivo.B3 * archivo.Yk);
                    archivo.toString();
                    
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }

        //&i
        public double[] Gauss(double[,] A)
        {
            int i, j, k, n = A.GetLength(0);
            double c;
            double[] x = new double[n];

            for (j = 0; j < n; j++)
            {
                for (i = 0; i < n; i++)
                {
                    if (i != j)
                    {
                        c = A[i,j] / A[j,j];
                        for (k = 0; k < n + 1; k++)
                        {
                            A[i,k] = A[i,k] - c * A[j,k];
                        }
                    }
                }
            }
            for (i = 0; i < n; i++)
            {
                x[i] = A[i,n] / A[i,i];
                /*if ((A[i,i] != A[i,i]) || (A[i,i] == 0))
                    break;*/
            }
            return x;
        }


        //&d=3

        //&d=8

        
        //&d=2

        //&d=3

        //&d=3
    }
}
