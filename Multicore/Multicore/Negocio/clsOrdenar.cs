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

        public List<object[]> mergeSort(List<object[]> _loLista)
        {
            mergeSort(_loLista, 0, _loLista.Count - 1);
            return null;
        }

        private void mergeSort(List<object[]> _loLista, int _iInicio, int _iFin) 
        {
            //Condicion de parada
            if (_iInicio == _iFin)
                return;
            //Calculo la mitad del array
            int iMitad = (_iInicio + _iFin) / 2;
            //Voy a ordenar recursivamente la primera mitad
            //y luego la segunda
            mergeSort(_loLista, _iInicio, iMitad);
            mergeSort(_loLista, iMitad + 1, _iFin);
            //Mezclo las dos mitades ordenadas
            List<object[]> liAux = merge(_loLista, _iInicio, iMitad, iMitad + 1, _iFin);
            
            //Array.Copy(liAux, 0, _loLista, _iInicio, liAux.Count);
        }

        private List<object[]> merge(List<object[]> _loLista, int _iInicio1, int _iFin1, int _iInicio2, int _iFin2)
        {
            int a_ = _iInicio1;
            int b_ = _iInicio2;
            int iTmp = _iFin1 - _iInicio1 + _iFin2 - _iInicio2 + 2;
            List<object[]> loResult = new List<object[]>();

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
                        if (((string)_loLista[_iInicio2][0]).CompareTo((string)_loLista[_iInicio1][0]) == -1) 
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
