using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Multicore.Negocio
{
    class clsMergeSort
    {

        public static string mergeSort(int _iColumna, bool _bTipo, bool _bConcurrencia) 
        {
            clsArchivo insArchivo = new clsArchivo();
            List<string> aoLineas = insArchivo.cargarArchivo();
            StringBuilder sbTexto = new StringBuilder();

            var vTiempo = Stopwatch.StartNew();

            /* ***************************************************************************************** */
            if (_bConcurrencia)
            {
                int iSegmento = (aoLineas.Count / 3) + 1;
                StringBuilder sbTextoTmp1 = new StringBuilder();
                StringBuilder sbTextoTmp2 = new StringBuilder();
                var vLista = aoLineas.Select((x, i) => new { Index = i, Value = x })
                                      .GroupBy(x => x.Index / iSegmento)
                                      .Select(x => x.Select(v => v.Value).ToList())
                                      .ToList();
                aoLineas = null;
                Parallel.Invoke(
                    () =>
                    {
                        sbTexto = mergeSort(vLista.ElementAt(0), _iColumna, _bTipo, _bConcurrencia);
                    },
                    () =>
                    {
                        sbTextoTmp1 = mergeSort(vLista.ElementAt(1), _iColumna, _bTipo, _bConcurrencia);
                    },
                    () =>
                    {
                        sbTextoTmp2 = mergeSort(vLista.ElementAt(2), _iColumna, _bTipo, _bConcurrencia);
                    }
                );
                vLista = null;
                sbTexto.Append(sbTextoTmp1.Append(sbTextoTmp2.ToString()).ToString());
            }
            else /* ------------------------------------------------------------------------------------ */
            {
                sbTexto = mergeSort(aoLineas, _iColumna, _bTipo, _bConcurrencia);
                aoLineas = null;
            }
            /* ***************************************************************************************** */

            vTiempo.Stop();

            insArchivo.guardarArchivo(sbTexto);

            return Convert.ToString(vTiempo.Elapsed);
        }


        //Método portal que llama al método recursivo inicial
        private static StringBuilder mergeSort(List<string> _aoLineas, int _iColumna, bool _bTipo, bool _bConcurrencia)
        {
            List<string[]> loLista = new List<string[]>();
            StringBuilder sbTexto = new StringBuilder();
            string[] asTmp;

            for (int i = 0; i < _aoLineas.Count; i++)
            {
                asTmp = ((string)_aoLineas[i]).Split(new char[] { ',' });
                for (int j = 0; j < asTmp.Length; j++)
                    asTmp[j] = asTmp[j].Replace("  ", string.Empty);
                loLista.Add(asTmp);
            }
            mergeSort(loLista, 0, loLista.Count - 1, _iColumna, _bTipo, _bConcurrencia);

            ///* ***************************************************************************************** */
            //if (_bConcurrencia)
            //{
            //    Parallel.Invoke(() => { });
            //}
            //else /* ------------------------------------------------------------------------------------ */
            //{

            //}
            ///* ***************************************************************************************** */

            foreach (string[] sColumna in loLista)
                sbTexto.AppendLine(string.Join(",", sColumna));

            loLista = null;

            return sbTexto;
        }

        private static void mergeSort(List<string[]> _loLista, int _iInicio, int _iFin, int _iColumna, bool _bTipo, bool _bConcurrencia)
        {
            //Condicion de parada
            if (_iInicio == _iFin)
                return;
            //Calculo la mitad del array
            int iMitad = (_iInicio + _iFin) / 2;
            //Voy a ordenar recursivamente la primera mitad
            //y luego la segunda
            //_loLista mergeSort(_loLista, _iInicio, iMitad, _iColumna);
            mergeSort(_loLista, _iInicio, iMitad, _iColumna, _bTipo, _bConcurrencia);
            mergeSort(_loLista, iMitad + 1, _iFin, _iColumna, _bTipo, _bConcurrencia);
            //Mezclo las dos mitades ordenadas
            object[] aoCadena = merge(_loLista, _iInicio, iMitad, iMitad + 1, _iFin, _iColumna, _bTipo);
            Array.Copy(aoCadena, 0, _loLista, _iInicio, aoCadena.Length);
        }

        //Método que mezcla las dos mitades ordenadas
        private static object[] merge(List<string[]> _loLista, int _iInicio1, int _iFin1, int _iInicio2, int _Fin2, int _iColumna, bool _bTipo)
        {
            //int i = _iInicio1;
            //int b = _iInicio2;
            object[] aoCadena = new object[_iFin1 - _iInicio1 + _Fin2 - _iInicio2 + 2];

            for (int i = 0; i < aoCadena.Length; i++)
            {
                if (_iInicio2 != _loLista.Length)
                {
                    if (_iInicio1 > _iFin1 && _iInicio2 <= _Fin2)
                    {
                        aoCadena[i] = _loLista[_iInicio2];
                        _iInicio2++;
                    }
                    if (_iInicio2 > _Fin2 && _iInicio1 <= _iFin1)
                    {
                        aoCadena[i] = _loLista[_iInicio1];
                        _iInicio1++;
                    }
                    if (_iInicio1 <= _iFin1 && _iInicio2 <= _Fin2)
                    {
                        // s1 = s2 : 0
                        //                // s1 > s2 : 1
                        //                // s1 < s2 : -1
                        if (_bTipo)
                        {
                            if (((string[])_loLista[_iInicio2])[_iColumna].CompareTo(((string[])_loLista[_iInicio1])[_iColumna]) <= 0)
                            {
                                aoCadena[i] = _loLista[_iInicio2];
                                _iInicio2++;
                            }
                            else
                            {
                                aoCadena[i] = _loLista[_iInicio1];
                                _iInicio1++;
                            }
                        }
                        else
                        {
                            if (((string[])_loLista[_iInicio2])[_iColumna].CompareTo(((string[])_loLista[_iInicio1])[_iColumna]) > 0)
                            {
                                aoCadena[i] = _loLista[_iInicio2];
                                _iInicio2++;
                            }
                            else
                            {
                                aoCadena[i] = _loLista[_iInicio1];
                                _iInicio1++;
                            }
                        }
                    }
                }
                else
                {
                    if (_iInicio1 <= _iFin1)
                    {
                        aoCadena[i] = _loLista[_iInicio1];
                        _iInicio1++;
                    }
                }
            }
            return aoCadena;
        }





















        

        

        private static void QuickSort(List<string[]> _loLista, int _iInicio, int _iFin, int _iColumna, bool _bTipo, bool _bConcurrencia)
        {
            int i = _iInicio, j = _iFin;
            string sPivot = ((string[])_loLista[(_iInicio + _iFin) / 2])[_iColumna];

            while (i <= j)
            {
                if (_bTipo)
                {
                    /* ***************************************************************************************** */
                    if (_bConcurrencia)
                    {
                        Parallel.Invoke(
                            () =>
                            {
                                while (((string[])_loLista[i])[_iColumna].CompareTo(sPivot) < 0)
                                    i++;
                            },
                            () =>
                            {
                                while (((string[])_loLista[j])[_iColumna].CompareTo(sPivot) > 0)
                                    j--;
                            }
                        );
                    }
                    else /* ------------------------------------------------------------------------------------ */
                    {
                        while (((string[])_loLista[i])[_iColumna].CompareTo(sPivot) < 0)
                            i++;

                        while (((string[])_loLista[j])[_iColumna].CompareTo(sPivot) > 0)
                            j--;
                    }
                    /* ***************************************************************************************** */
                }
                else
                {
                    /* ***************************************************************************************** */
                    if (_bConcurrencia)
                    {
                        Parallel.Invoke(
                            () =>
                            {
                                while (((string[])_loLista[i])[_iColumna].CompareTo(sPivot) > 0)
                                    i++;
                            },
                            () =>
                            {
                                while (((string[])_loLista[j])[_iColumna].CompareTo(sPivot) < 0)
                                    j--;
                            }
                        );
                    }
                    else /* ------------------------------------------------------------------------------------ */
                    {
                        while (((string[])_loLista[i])[_iColumna].CompareTo(sPivot) > 0)
                            i++;

                        while (((string[])_loLista[j])[_iColumna].CompareTo(sPivot) < 0)
                            j--;
                    }
                    /* ***************************************************************************************** */
                }

                if (i <= j)
                {
                    string[] asTmp = (string[])_loLista[i];
                    _loLista[i] = _loLista[j];
                    _loLista[j] = asTmp;
                    i++;
                    j--;
                }
            }
            if (_iInicio < j)
                QuickSort(_loLista, _iInicio, j, _iColumna, _bTipo, _bConcurrencia);
            if (i < _iFin)
                QuickSort(_loLista, i, _iFin, _iColumna, _bTipo, _bConcurrencia);
        }






    }
}
