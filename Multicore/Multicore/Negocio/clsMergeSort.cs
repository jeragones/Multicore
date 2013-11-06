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
        /// <summary>
        /// Funcion principal evalua el tiempo que dura la ejecucion
        /// </summary>
        /// <param name="_iColumna">Columna que se va a ordenar</param>
        /// <param name="_bTipo">Tipo de ordenamiento, ascendente o descendente</param>
        /// <param name="_bConcurrencia">Indica si se ejecuta de manera concurrente o no</param>
        /// <returns>retorna el tiempo de ejecucion</returns>
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

        /// <summary>
        /// Une las sublistas ordenandas
        /// </summary>
        /// <param name="_asLineas">Lista con las sublistas que se deben unir</param>
        /// <returns>Lista completa</returns>
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

        /// <summary>
        /// Ordena el texto con el metodo mergeSort
        /// </summary>
        /// <param name="_lsLineas">Lista que se va a ordenar</param>
        /// <param name="_iColumna">Columna en la que se basa para ordenar las filas</param>
        /// <param name="_bTipo">Tipo de ordenamiento</param>
        /// <param name="_bConcurrencia">Indica si se ordena de forma concurrente o no</param>
        /// <returns>Texto ordenado</returns>
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
        
        /// <summary>
        /// Metodo auxiliar del metodo de ordenamiento mergeSort
        /// </summary>
        /// <param name="_lsLineas">Lista que se va a ordenar</param>
        /// <param name="_iColumna">Columna en la que se basa para ordenar las filas</param>
        /// <param name="_bTipo">Tipo de ordenamiento</param>
        /// <param name="_bConcurrencia">Indica si se ordena de forma concurrente o no</param>
        /// <returns>Objetos del archivo ordenados</returns>
        private static object[] MergeSort(object[] _aoLista, int _iColumna, bool _bTipo, bool _bConcurrencia)
        {
            MergeSort(_aoLista, 0, _aoLista.Length - 1, _iColumna,_bTipo,_bConcurrencia);
            return _aoLista;
        }

        /// <summary>
        /// Metodo auxiliar del metodo de ordenamiento mergeSort
        /// </summary>
        /// <param name="_aoLista">Lista que se va a ordenar</param>
        /// <param name="_iInicio">index de inicio de la lista</param>
        /// <param name="_iFin">index final de la lista</param>
        /// <param name="_iColumna">Columna en la que se basa para ordenar las filas</param>
        /// <param name="_bTipo">Tipo de ordenamiento</param>
        /// <param name="_bConcurrencia">Indica si se ordena de forma concurrente o no</param>
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

        /// <summary>
        /// Une las sublistas ordenadas
        /// </summary>
        /// <param name="_aoLista">Lista que se va a ordenar</param>
        /// <param name="_iInicio1">index de inicio de la sublista</param>
        /// <param name="_iFin1">index final de la sublista</param>
        /// <param name="_iInicio2">index de inicio de la sublista</param>
        /// <param name="_iFin2">index final de la sublista</param>
        /// <param name="_iColumna">Columna en la que se basa para ordenar las filas</param>
        /// <param name="_bTipo">Tipo de ordenamiento</param>
        /// <param name="_bConcurrencia">Indica si se ordena de forma concurrente o no</param>
        /// <returns>Retorna una lista ordenada</returns>
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
