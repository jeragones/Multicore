using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Multicore.Negocio
{
    class clsOrdenar
    {

        public LinkedList<object[]> quickSort(LinkedList<object[]> _loLista) 
        {

            return _loLista;
        }

        public List<object[]> mergeSort(List<object[]> _loLista, int _iColumna)
        {
            mergeSort(_loLista, 0, _loLista.Count - 1, _iColumna);
            return null;
        }

        private static void mergeSort(List<object[]> _loLista, int _iInicio, int _iFin, int _iColumna) 
        {
            //Condicion de parada
            if (_iInicio == _iFin)
                return;
            //Calculo la mitad del array
            int iMitad = (_iInicio + _iFin) / 2;
            //Voy a ordenar recursivamente la primera mitad
            //y luego la segunda
            _loLista mergeSort(_loLista, _iInicio, iMitad, _iColumna);
            mergeSort(_loLista, iMitad + 1, _iFin, _iColumna);
            //Mezclo las dos mitades ordenadas
            object[] liAux = merge(_loLista, _iInicio, iMitad, iMitad + 1, _iFin, _iColumna);
            //Array.Copy(liAux, 0, _loLista, _iInicio, liAux.Count);
        }

        private static object[] merge(List<object[]> _loLista, int _iInicio1, int _iFin1, int _iInicio2, int _iFin2, int _iColumna)
        {
            int iTmp = _iFin1 - _iInicio1 + _iFin2 - _iInicio2 + 2;
            object[] loResult = new object[iTmp];

            for (int i = 0; i < iTmp; i++)
            {
                if (_iInicio2 != _loLista.Count)
                {
                    if (_iInicio1 > _iFin1 && _iInicio2 <= _iFin2)
                    {
                        loResult[i] = _loLista[_iInicio2];
                        _iInicio2++;
                    }
                    if (_iInicio2 > _iFin2 && _iInicio1 <= _iFin1)
                    {
                        loResult[i] = _loLista[_iInicio1];
                        _iInicio1++;
                    }
                    if (_iInicio1 <= _iFin1 && _iInicio2 <= _iFin2)
                    {
                        // s1 = s2 : 0
                        // s1 > s2 : 1
                        // s1 < s2 : -1
                        if (((string)_loLista[_iInicio2][1]).CompareTo((string)_loLista[_iInicio1][1]) == -1) 
                        //if (_loLista[_iInicio2] <= _loLista[_iInicio1])
                        {
                            loResult[i] = _loLista[_iInicio2];
                            _iInicio2++;
                        }
                        else
                        {
                            loResult[i] = _loLista[_iInicio1];
                            _iInicio1++;
                        }
                    }
                }
                else
                {
                    if (_iInicio1 <= _iFin1)
                    {
                        loResult[i] = _loLista[_iInicio1];
                        _iInicio1++;
                    }
                }
            }
            return loResult;
        }
    }
}
