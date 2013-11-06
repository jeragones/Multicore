using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Multicore.Negocio
{
    class clsQuickSort
    {
        public static string quickSort(int _iColumna, bool _bTipo, bool _bConcurrencia) 
        {
            clsArchivo insArchivo = new clsArchivo();
            List<string> aoLineas = insArchivo.cargarArchivo();
            int iSegmento = (aoLineas.Count / 3) + 1;
            StringBuilder sbTexto = new StringBuilder();

            var vTiempo = Stopwatch.StartNew();

            /* ***************************************************************************************** */
            if (_bConcurrencia)
            {
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
                        sbTexto = quickSort(vLista.ElementAt(0), _iColumna, _bTipo, _bConcurrencia);
                    },
                    () =>
                    {
                        sbTextoTmp1 = quickSort(vLista.ElementAt(1), _iColumna, _bTipo, _bConcurrencia);
                    },
                    () =>
                    {
                        sbTextoTmp2 = quickSort(vLista.ElementAt(2), _iColumna, _bTipo, _bConcurrencia);
                    }
                );
                vLista = null;
                sbTexto.Append(sbTextoTmp1.Append(sbTextoTmp2.ToString()).ToString());
            }
            else /* ------------------------------------------------------------------------------------ */
            {
                sbTexto = quickSort(aoLineas, _iColumna, _bTipo, _bConcurrencia);
                aoLineas = null;
            }
            /* ***************************************************************************************** */

            vTiempo.Stop();

            insArchivo.guardarArchivo(sbTexto);

            return Convert.ToString(vTiempo.Elapsed);
        }

        private static StringBuilder quickSort(List<string> _aoLineas, int _iColumna, bool _bTipo, bool _bConcurrencia)
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

            quickSort(loLista, 0, loLista.Count - 1, _iColumna, _bTipo, _bConcurrencia);

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

        private static void quickSort(List<string[]> _loLista, int _iInicio, int _iFin, int _iColumna, bool _bTipo, bool _bConcurrencia)
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
                quickSort(_loLista, _iInicio, j, _iColumna, _bTipo, _bConcurrencia);
            if (i < _iFin)
                quickSort(_loLista, i, _iFin, _iColumna, _bTipo, _bConcurrencia);
        }
    }
}
