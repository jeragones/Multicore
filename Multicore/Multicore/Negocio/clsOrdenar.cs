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
        public static void mergeSort(object[] _aoLista, int _iColumna, bool _bTipo)
        {
            mergeSort(_aoLista, 0, _aoLista.Length - 1, _iColumna, _bTipo);
        }

        static private void mergeSort(object[] _aoLista, int _iInicio, int _iFin, int _iColumna, bool _bTipo)
        {
            //Condicion de parada
            if (_iInicio == _iFin)
                return;
            //Calculo la mitad del array
            int iMitad = (_iInicio + _iFin) / 2;
            //Voy a ordenar recursivamente la primera mitad
            //y luego la segunda
            //_loLista mergeSort(_loLista, _iInicio, iMitad, _iColumna);
            mergeSort(_aoLista, _iInicio, iMitad, _iColumna, _bTipo);
            mergeSort(_aoLista, iMitad + 1, _iFin, _iColumna, _bTipo);
            //Mezclo las dos mitades ordenadas
            object[] aoCadena = merge(_aoLista, _iInicio, iMitad, iMitad + 1, _iFin, _iColumna, _bTipo);
            Array.Copy(aoCadena, 0, _aoLista, _iInicio, aoCadena.Length);
        }

        //Método que mezcla las dos mitades ordenadas
        static private object[] merge(object[] _aoLista, int _iInicio1, int _iFin1, int _iInicio2, int _Fin2, int _iColumna, bool _bTipo)
        {
            //int i = _iInicio1;
            //int b = _iInicio2;
            object[] aoCadena = new object[_iFin1 - _iInicio1 + _Fin2 - _iInicio2 + 2];

            for (int i = 0; i < aoCadena.Length; i++)
            {
                if (_iInicio2 != _aoLista.Length)
                {
                    if (_iInicio1 > _iFin1 && _iInicio2 <= _Fin2)
                    {
                        aoCadena[i] = _aoLista[_iInicio2];
                        _iInicio2++;
                    }
                    if (_iInicio2 > _Fin2 && _iInicio1 <= _iFin1)
                    {
                        aoCadena[i] = _aoLista[_iInicio1];
                        _iInicio1++;
                    }
                    if (_iInicio1 <= _iFin1 && _iInicio2 <= _Fin2)
                    {
                        // s1 = s2 : 0
                        //                // s1 > s2 : 1
                        //                // s1 < s2 : -1
                        if (_bTipo)
                        {
                            if (((string[])_aoLista[_iInicio2])[_iColumna].CompareTo(((string[])_aoLista[_iInicio1])[_iColumna]) <= 0)
                            {
                                aoCadena[i] = _aoLista[_iInicio2];
                                _iInicio2++;
                            }
                            else
                            {
                                aoCadena[i] = _aoLista[_iInicio1];
                                _iInicio1++;
                            }
                        }
                        else 
                        {
                            if (((string[])_aoLista[_iInicio2])[_iColumna].CompareTo(((string[])_aoLista[_iInicio1])[_iColumna]) > 0)
                            {
                                aoCadena[i] = _aoLista[_iInicio2];
                                _iInicio2++;
                            }
                            else
                            {
                                aoCadena[i] = _aoLista[_iInicio1];
                                _iInicio1++;
                            }
                        }
                    }
                }
                else
                {
                    if (_iInicio1 <= _iFin1)
                    {
                        aoCadena[i] = _aoLista[_iInicio1];
                        _iInicio1++;
                    }
                }
            }
            return aoCadena;
        }
        //////////////////////////////////////////////////////////////////////////////////////////////////////////

        public static void quicksort(object[] _aoLista, int _iColumna, bool _bTipo)
        {
            Quicksort(_aoLista, 0, _aoLista.Length - 1, _iColumna, _bTipo);
        }

        public static void Quicksort(object[] _aoLista, int _iInicio, int _iFin, int _iColumna, bool _bTipo)
        {
            int i = _iInicio, j = _iFin;
            string pivot = ((string[])_aoLista[(_iInicio + _iFin) / 2])[_iColumna];

            while (i <= j)
            {
                if (_bTipo)
                {
                    while (((string[])_aoLista[i])[_iColumna].CompareTo(pivot) < 0)
                    {
                        i++;
                    }

                    while (((string[])_aoLista[j])[_iColumna].CompareTo(pivot) > 0)
                    {
                        j--;
                    }
                }
                else 
                {
                    while (((string[])_aoLista[i])[_iColumna].CompareTo(pivot) > 0)
                    {
                        i++;
                    }

                    while (((string[])_aoLista[j])[_iColumna].CompareTo(pivot) < 0)
                    {
                        j--;
                    }
                }
                
                if (i <= j)
                {
                    // Swap
                    string[] tmp = (string[])_aoLista[i];
                    _aoLista[i] = _aoLista[j];
                    _aoLista[j] = tmp;

                    i++;
                    j--;
                }
            }

            // Recursive calls
            if (_iInicio < j)
            {
                Quicksort(_aoLista, _iInicio, j, _iColumna, _bTipo);
            }

            if (i < _iFin)
            {
                Quicksort(_aoLista, i, _iFin, _iColumna, _bTipo);
            }
        }
    }
}
