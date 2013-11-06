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
        //static public string mergeSort(int _iColumna, bool _bTipo, bool _bConcurrencia)
        //{
        //    clsArchivo insArchivo = new clsArchivo();
        //    List<string> aoLineas = insArchivo.cargarArchivo();
        //    StringBuilder sbTexto = new StringBuilder();
        //    var vTiempo = Stopwatch.StartNew();


        //    object[] words = new object[] { new string[] { "209002", "ALAJUELA" }, 
        //                                    new string[] { "101001", "SAN JOSE" },
        //                                    new string[] { "301031", "CARTAGO" },
        //                                    new string[] { "301031", "HEREDIA" },
        //                                    new string[] { "301031", "GUANACASTE" },
        //                                    new string[] { "301031", "PUNTARENAS" },
        //                                    new string[] { "301031", "LIMON" },
        //                                    new string[] { "301031", "CONSULADO" }};


        //    sbTexto = mergeSort(words, _iColumna, _bTipo);
        //    aoLineas = null;
        //    vTiempo.Stop();

        //    insArchivo.guardarArchivo(sbTexto);

        //    return Convert.ToString(vTiempo.Elapsed);
        //}



        ////Método portal que llama al método recursivo inicial
        //static private StringBuilder mergeSort(object[] _aoLista, int _iColumna, bool _bTipo)
        //{
        //    List<string[]> loLista = new List<string[]>();
        //    StringBuilder sbTexto = new StringBuilder();
        //    string[] asTmp;

        //    for (int i = 0; i < _aoLista.Length; i++)
        //    {
        //        asTmp = (string[])_aoLista[i];
        //        //asTmp = ((string)_aoLista[i]).Split(new char[] { ',' });
        //        for (int j = 0; j < asTmp.Length; j++)
        //            asTmp[j] = asTmp[j].Replace("  ", string.Empty);
        //        loLista.Add(asTmp);
        //    }

        //    mergeSort(loLista.ToArray(), 0, loLista.Count - 1, _iColumna, _bTipo);

        //    foreach (string[] sColumna in loLista)
        //        sbTexto.AppendLine(string.Join(",", sColumna));

        //    loLista = null;
        //    return sbTexto;
        //}

        //static private void mergeSort(object[] _aoLista, int _iInicio, int _iFin, int _iColumna, bool _bTipo)
        //{
        //    //Condicion de parada
        //    if (_iInicio == _iFin)
        //        return;
        //    //Calculo la mitad del array
        //    int iMitad = (_iInicio + _iFin) / 2;
        //    //Voy a ordenar recursivamente la primera mitad
        //    //y luego la segunda
        //    //_loLista mergeSort(_loLista, _iInicio, iMitad, _iColumna);
        //    mergeSort(_aoLista, _iInicio, iMitad, _iColumna, _bTipo);
        //    mergeSort(_aoLista, iMitad + 1, _iFin, _iColumna, _bTipo);
        //    //Mezclo las dos mitades ordenadas
        //    object[] aoCadena = merge(_aoLista, _iInicio, iMitad, iMitad + 1, _iFin, _iColumna, _bTipo);
        //    Array.Copy(aoCadena, 0, _aoLista, _iInicio, aoCadena.Length);
        //}

        ////Método que mezcla las dos mitades ordenadas
        //static private object[] merge(object[] _aoLista, int _iInicio1, int _iFin1, int _iInicio2, int _Fin2, int _iColumna, bool _bTipo)
        //{
        //    //int a = _iInicio1;
        //    //int b = _iInicio2;
        //    object[] aoCadena = new object[_iFin1 - _iInicio1 + _Fin2 - _iInicio2 + 2];

        //    for (int i = 0; i < aoCadena.Length; i++)
        //    {
        //        if (_iInicio2 != _aoLista.Length)
        //        {
        //            if (_iInicio1 > _iFin1 && _iInicio2 <= _Fin2)
        //            {
        //                aoCadena[i] = _aoLista[_iInicio2];
        //                _iInicio2++;
        //            }
        //            if (_iInicio2 > _Fin2 && _iInicio1 <= _iFin1)
        //            {
        //                aoCadena[i] = _aoLista[_iInicio1];
        //                _iInicio1++;
        //            }
        //            if (_iInicio1 <= _iFin1 && _iInicio2 <= _Fin2)
        //            {
        //                // s1 = s2 : 0
        //                //                // s1 > s2 : 1
        //                //                // s1 < s2 : -1
        //                if (_bTipo)
        //                {
        //                    if (((string[])_aoLista[_iInicio2])[_iColumna].CompareTo(((string[])_aoLista[_iInicio1])[_iColumna]) <= 0)
        //                    {
        //                        aoCadena[i] = _aoLista[_iInicio2];
        //                        _iInicio2++;
        //                    }
        //                    else
        //                    {
        //                        aoCadena[i] = _aoLista[_iInicio1];
        //                        _iInicio1++;
        //                    }
        //                }
        //                else
        //                {
        //                    if (((string[])_aoLista[_iInicio2])[_iColumna].CompareTo(((string[])_aoLista[_iInicio1])[_iColumna]) >= 0)
        //                    {
        //                        aoCadena[i] = _aoLista[_iInicio2];
        //                        _iInicio2++;
        //                    }
        //                    else
        //                    {
        //                        aoCadena[i] = _aoLista[_iInicio1];
        //                        _iInicio1++;
        //                    }
        //                }
        //            }
        //        }
        //        else
        //        {
        //            if (_iInicio1 <= _iFin1)
        //            {
        //                aoCadena[i] = _aoLista[_iInicio1];
        //                _iInicio1++;
        //            }
        //        }
        //    }
        //    return aoCadena;
        //}






        public static string mergeSort(int _iColumna, bool _bTipo, bool _bConcurrencia)
        {
            clsArchivo insArchivo = new clsArchivo();
            List<string> aoLineas = insArchivo.cargarArchivo();
            int iSegmento = (aoLineas.Count / 3) + 1;
            StringBuilder sbTexto = new StringBuilder();

            var vTiempo = Stopwatch.StartNew();
            sbTexto = mergeSort(aoLineas, _iColumna, _bTipo, _bConcurrencia);
            aoLineas = null;
            vTiempo.Stop();

            insArchivo.guardarArchivo(sbTexto);

            return Convert.ToString(vTiempo.Elapsed);
        }

        private static List<string[]> unirLista(List<string> _asLineas) 
        {
            List<string[]> lsLista = new List<string[]>();
            string[] asTmp;
            for (int i = 0; i < _asLineas.Count; i++)
            {
                asTmp = ((string)_asLineas[i]).Split(new char[] { ',' });
                for (int j = 0; j < asTmp.Length; j++)
                    asTmp[j] = asTmp[j].Replace("  ", string.Empty);
                lsLista.Add(asTmp);
            }
            return lsLista;
        }

        private static StringBuilder mergeSort(List<string> _lsLineas, int _iColumna, bool _bTipo, bool _bConcurrencia)
        {
            List<string[]> lsLista = null;
            StringBuilder sbTexto = new StringBuilder();

            /* ***************************************************************************************** */
            if (_bConcurrencia)
            {
                List<string[]> lsTmpLista1 = null;
                List<string[]> lsTmpLista2 = null;
                int iSegmento = (_lsLineas.Count / 3) + 1;
                var vLista = _lsLineas.Select((x, i) => new { Index = i, Value = x })
                                      .GroupBy(x => x.Index / iSegmento)
                                      .Select(x => x.Select(v => v.Value).ToList())
                                      .ToList();
                Parallel.Invoke(
                    () => 
                    { 
                        lsLista = unirLista(vLista.ElementAt(0)); 
                    },
                    () => 
                    { 
                        lsTmpLista1 = unirLista(vLista.ElementAt(1)); 
                    },
                    () => 
                    { 
                        lsTmpLista2 = unirLista(vLista.ElementAt(2)); 
                    }
                );
                lsLista = lsLista.Concat(lsTmpLista1.Concat(lsTmpLista2).ToList()).ToList();
                lsTmpLista1 = null;
                lsTmpLista2 = null;
            }
            else /* ------------------------------------------------------------------------------------ */
                lsLista = unirLista(_lsLineas);
            /* ***************************************************************************************** */

            foreach (string[] sColumna in MergeSort(lsLista.ToArray(), _iColumna, _bTipo, _bConcurrencia))
                sbTexto.AppendLine(string.Join(",", sColumna));
            lsLista = null;
            return sbTexto;
        }
        
        private static object[] MergeSort(object[] _aoLista, int _iColumna, bool _bTipo, bool _bConcurrencia)
        {
            MergeSort(_aoLista, 0, _aoLista.Length - 1, _iColumna,_bTipo,_bConcurrencia);
            return _aoLista;
        }

        static private void MergeSort(object[] _aoLista, int _iInicio, int _iFin, int _iColumna, bool _bTipo, bool _bConcurrencia)
        {
            if (_iInicio == _iFin)
                return;
            int iPivote = (_iInicio + _iFin) / 2;

            /* ***************************************************************************************** */
            if (_bConcurrencia)
            {
                Parallel.Invoke(
                    () => 
                    {
                        MergeSort(_aoLista, _iInicio, iPivote, _iColumna, _bTipo, _bConcurrencia); 
                    },
                    () => 
                    {
                        MergeSort(_aoLista, iPivote + 1, _iFin, _iColumna, _bTipo, _bConcurrencia);
                    }
                );
            }
            else /* ------------------------------------------------------------------------------------ */
            {
                MergeSort(_aoLista, _iInicio, iPivote, _iColumna, _bTipo, _bConcurrencia);
                MergeSort(_aoLista, iPivote + 1, _iFin, _iColumna, _bTipo, _bConcurrencia);
            }
            /* ***************************************************************************************** */

            object[] aoTmp = Merge(_aoLista, _iInicio, iPivote, iPivote + 1, _iFin, _iColumna, _bTipo, _bConcurrencia);
            Array.Copy(aoTmp, 0, _aoLista, _iInicio, aoTmp.Length);
        }

        static private object[] Merge(object[] _aoLista, int _iInicio1, int _iFin1, int _iInicio2, int _iFin2, int _iColumna, bool _bTipo, bool _bConcurrencia)
        {
            object[] aoResultado = new object[_iFin1 - _iInicio1 + _iFin2 - _iInicio2 + 2];

            for (int i = 0; i < aoResultado.Length; i++)
            {
                if (_iInicio2 != _aoLista.Length)
                {
                    if (_iInicio1 > _iFin1 && _iInicio2 <= _iFin2)
                    {
                        aoResultado[i] = _aoLista[_iInicio2];
                        _iInicio2++;
                    }
                    if (_iInicio2 > _iFin2 && _iInicio1 <= _iFin1)
                    {
                        aoResultado[i] = _aoLista[_iInicio1];
                        _iInicio1++;
                    }
                    if (_iInicio1 <= _iFin1 && _iInicio2 <= _iFin2)
                    {
                        if (_bTipo)
                        {
                            if (((string[])_aoLista[_iInicio2])[_iColumna].CompareTo(((string[])_aoLista[_iInicio1])[_iColumna]) <= 0)
                            {
                                aoResultado[i] = _aoLista[_iInicio2];
                                _iInicio2++;
                            }
                            else
                            {
                                aoResultado[i] = _aoLista[_iInicio1];
                                _iInicio1++;
                            }
                        }
                        else 
                        {
                            if (((string[])_aoLista[_iInicio2])[_iColumna].CompareTo(((string[])_aoLista[_iInicio1])[_iColumna]) >= 0)
                            {
                                aoResultado[i] = _aoLista[_iInicio2];
                                _iInicio2++;
                            }
                            else
                            {
                                aoResultado[i] = _aoLista[_iInicio1];
                                _iInicio1++;
                            }
                        }
                    }
                }
                else
                {
                    if (_iInicio1 <= _iFin1)
                    {
                        aoResultado[i] = _aoLista[_iInicio1];
                        _iInicio1++;
                    }
                }
            }
            return aoResultado;
        }
    }
}
