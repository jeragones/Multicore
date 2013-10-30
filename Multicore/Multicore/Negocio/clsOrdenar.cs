using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Multicore.Negocio
{
    class clsOrdenar
    {

        private static List<object[]> loLista = new List<object[]>();

        public void setLista(List<object[]> _loLista) 
        {
            loLista = _loLista;
        }

        public LinkedList<object[]> quickSort(LinkedList<object[]> _loLista) 
        {

            return _loLista;
        }


        //Método portal que llama al método recursivo inicial
        public static void MergeSort(object[] x, int m)
        {
            MergeSort(x, 0, x.Length - 1, m);
        }

        static private void MergeSort(object[] x, int desde, int hasta, int m)
        {
            //Condicion de parada
            if (desde == hasta)
                return;
            //Calculo la mitad del array
            int mitad = (desde + hasta) / 2;
            //Voy a ordenar recursivamente la primera mitad
            //y luego la segunda
            //_loLista mergeSort(_loLista, _iInicio, iMitad, _iColumna);
            MergeSort(x, desde, mitad, m);
            MergeSort(x, mitad + 1, hasta, m);
            //Mezclo las dos mitades ordenadas
            object[] aux = Merge(x, desde, mitad, mitad + 1, hasta, m);
            Array.Copy(aux, 0, x, desde, aux.Length);
        }

        //Método que mezcla las dos mitades ordenadas
        static private object[] Merge(object[] x, int desde1, int hasta1, int desde2, int hasta2, int m)
        {
            int a = desde1;
            int b = desde2;
            object[] result = new object[hasta1 - desde1 + hasta2 - desde2 + 2];

            for (int i = 0; i < result.Length; i++)
            {
                if (b != x.Length)
                {
                    if (a > hasta1 && b <= hasta2)
                    {
                        result[i] = x[b];
                        b++;
                    }
                    if (b > hasta2 && a <= hasta1)
                    {
                        result[i] = x[a];
                        a++;
                    }
                    if (a <= hasta1 && b <= hasta2)
                    {
                        // s1 = s2 : 0
                        //                // s1 > s2 : 1
                        //                // s1 < s2 : -1
                        string[] jj = (string[])x[a];
                        if (((string[])x[b])[m].CompareTo(((string[])x[a])[m]) <= 0)
                        {
                            result[i] = x[b];
                            b++;
                        }
                        else
                        {
                            result[i] = x[a];
                            a++;
                        }
                    }
                }
                else
                {
                    if (a <= hasta1)
                    {
                        result[i] = x[a];
                        a++;
                    }
                }
            }
            return result;
        }
        //////////////////////////////////////////////////////////////////////////////////////////////////////////

        public static void quicksort(object[] x, int m)
        {
            Quicksort(x, 0, x.Length - 1, m);
        }

        public static void Quicksort(object[] elements, int left, int right, int m)
        {
            int i = left, j = right;
            string pivot = ((string[])elements[(left + right) / 2])[m];

            while (i <= j)
            {
                while (((string[])elements[i])[m].CompareTo(pivot) < 0)
                {
                    i++;
                }

                while (((string[])elements[j])[m].CompareTo(pivot) > 0)
                {
                    j--;
                }

                if (i <= j)
                {
                    // Swap
                    string[] tmp = (string[])elements[i];
                    elements[i] = elements[j];
                    elements[j] = tmp;

                    i++;
                    j--;
                }
            }

            // Recursive calls
            if (left < j)
            {
                Quicksort(elements, left, j, m);
            }

            if (i < right)
            {
                Quicksort(elements, i, right, m);
            }
        }











        
    }
}
