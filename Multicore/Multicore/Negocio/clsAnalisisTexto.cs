using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;

namespace Multicore.Negocio
{
    /// <summary>
    /// Clase para analizar un archivo de texto y evaluar lo siguiente:
    /// 1. Reconocer el lenguaje en el que se encuentra el texto
    /// 2. Identificar cuales son las 10 palabras mas comunes en el texto
    /// 3. Identificar la cantidad de palabras que contiene el texto
    /// 4. Identificar la cantidad de caracteres que tiene el texto
    /// </summary>
    class clsAnalisisTexto
    {
        private object[] aoIdioma = new object[] { "", 0 };
        private int iCantidadPalabras = 0;
        private int iCantidadCaracteres = 0;
        private List<object[]> loPalabrasComunes = new List<object[]>();

        /// <summary>
        /// Obtiene el idioma del idioma del texto
        /// </summary>
        /// <returns>Nombre del lenguaje</returns>
        public string getIdioma() 
        {
            return (string)aoIdioma[0];
        }

        /// <summary>
        /// Obtiene la cantidad de palabras que contiene el texto
        /// </summary>
        /// <returns>Numero de palabras</returns>
        public string getCantidadPalagras() 
        {
            return iCantidadPalabras.ToString();
        }

        /// <summary>
        /// Obtiene la cantidad de caracteres que tiene el texto
        /// </summary>
        /// <returns>Numero de caracteres</returns>
        public string getCantidadCaracteres() 
        {
            return iCantidadCaracteres.ToString();
        }

        /// <summary>
        /// Obtiene las 10 palabras mas comunes en el texto
        /// </summary>
        /// <returns>10 palabras</returns>
        public StringBuilder getPalabrasComunes() 
        {
            StringBuilder sPalabras = new StringBuilder();
            for (int i = 0; i < 10; i++) 
            {
                if (i == 0)
                    sPalabras.Append(loPalabrasComunes.ElementAt(i));
                else 
                {
                    sPalabras.Append(", ");
                    sPalabras.Append(loPalabrasComunes.ElementAt(i));
                }
            }
            return sPalabras;
        }

        /// <summary>
        /// Recorre el texto analizandolo con funciones auxiliares
        /// </summary>
        /// <param name="_sTexto">Texto que se debe analizar</param>
        /// <param name="_bConcurrencia">Bandera que indica si se debe analizar con procesamiento concurrente o no</param>
        public void analizarTexto(string _sTexto, bool _bConcurrencia)
        {
            string sPath = Directory.GetParent(Path.GetDirectoryName(Application.StartupPath)).FullName + "\\Datos\\diccionario.txt";
            string[] asIdiomas = cargarArchivo(sPath);
            LinkedList<object[]> lsIdiomas = new LinkedList<object[]>();
            string[] asPalabras = _sTexto.Split(new char[] { ' ' });
            object[,] moIdiomas;

            if (!String.IsNullOrEmpty(asIdiomas[0])) 
            {
                /* ***************************************************************************************** */
                if (_bConcurrencia)
                {
                    object[] aoIdioma1 = null;
                    object[] aoIdioma2 = null;
                    object[] aoIdioma3 = null;
                    
                    Parallel.Invoke(
                        () => { aoIdioma1 = separar(asIdiomas[0]); }, 
                        () => { aoIdioma2 = separar(asIdiomas[1]); }, 
                        () => { aoIdioma3 = separar(asIdiomas[2]); }
                    );
                    
                    lsIdiomas.AddLast(aoIdioma1);
                    lsIdiomas.AddLast(aoIdioma2);
                    lsIdiomas.AddLast(aoIdioma3);

                    object[] aoDatos1 = null;
                    object[] aoDatos2 = null;
                    object[] aoDatos3 = null;

                    int iTamano = asPalabras.Length;
                    int iSegmento = (iTamano / 3) + 1;
                    var vLista = asPalabras.Select((x, i) => new { Index = i, Value = x })
                                           .GroupBy(x => x.Index / iSegmento)
                                           .Select(x => x.Select(v => v.Value).ToList())
                                           .ToList();
                    Parallel.Invoke(
                        () => 
                        {
                            aoDatos1 = analisisTextual(vLista.ElementAt(0).ToArray(), asIdiomas.Length, lsIdiomas, _bConcurrencia); 
                        },
                        () => 
                        {
                            aoDatos2 = analisisTextual(vLista.ElementAt(1).ToArray(), asIdiomas.Length, lsIdiomas, _bConcurrencia); 
                        },
                        () => 
                        {
                            aoDatos3 = analisisTextual(vLista.ElementAt(2).ToArray(), asIdiomas.Length, lsIdiomas, _bConcurrencia); 
                        }
                    );
                    iCantidadPalabras = ((int[])aoDatos1[0])[0] + ((int[])aoDatos2[0])[0] + ((int[])aoDatos3[0])[0];
                    iCantidadCaracteres = ((int[])aoDatos1[0])[1] + ((int[])aoDatos2[0])[1] + ((int[])aoDatos3[0])[1];
                    moIdiomas = (object[,])aoDatos1[1];
                    moIdiomas[0, 1] = (int)moIdiomas[0, 1] + (int)((object[,])aoDatos2[1])[0, 1] + (int)((object[,])aoDatos3[1])[0, 1];
                    moIdiomas[1, 1] = (int)moIdiomas[1, 1] + (int)((object[,])aoDatos2[1])[1, 1] + (int)((object[,])aoDatos3[1])[1, 1];
                    moIdiomas[2, 1] = (int)moIdiomas[2, 1] + (int)((object[,])aoDatos2[1])[2, 1] + (int)((object[,])aoDatos3[1])[2, 1];
                    identificarIdioma(moIdiomas);
                }
                else /* ------------------------------------------------------------------------------------ */
                {
                    lsIdiomas = separar(asIdiomas);
                    moIdiomas = idiomas(lsIdiomas, asIdiomas.Length);
                    object[] aoDatos = analisisTextual(asPalabras, asIdiomas.Length, lsIdiomas, _bConcurrencia);
                    iCantidadPalabras = ((int[])aoDatos[0])[0];
                    iCantidadCaracteres = ((int[])aoDatos[0])[1];
                    moIdiomas = (object[,])aoDatos[1];
                    identificarIdioma(moIdiomas);
                }
                /* ***************************************************************************************** */
            }
            var lsPalabras = loPalabrasComunes.OrderByDescending(x => x[1]).ToList();
            loPalabrasComunes = new List<object[]>(lsPalabras);
            int t = 5;
        }

        /// <summary>
        /// 777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777
        /// </summary>
        /// <param name="_moIdiomas"></param>
        private void identificarIdioma(object[,] _moIdiomas) 
        {
            for (int i = 0; i < _moIdiomas.Length / 2; i++) 
            {
                if (String.IsNullOrEmpty((string)aoIdioma[0]) && (int)aoIdioma[1] == 0)
                {
                    aoIdioma[0] = _moIdiomas[i, 0];
                    aoIdioma[1] = _moIdiomas[i, 1];
                }
                else if ((int)aoIdioma[1] < (int)_moIdiomas[i, 1])
                {
                    aoIdioma[0] = _moIdiomas[i, 0];
                    aoIdioma[1] = _moIdiomas[i, 1];
                }
            }
        }

        /// <summary>
        /// 777777777777777777777777777777777777777777777777777777777777777777777777777777777777
        /// </summary>
        /// <param name="_asPalabras"></param>
        /// <param name="_bConcurrencia"></param>
        /// <param name="_moIdiomas"></param>
        /// <param name="lsIdiomas"></param>
        /// <returns></returns>
        private object[] analisisTextual(string[] _asPalabras, int _iLongitud, LinkedList<object[]> lsIdiomas, bool _bConcurrencia) 
        {
            string sPalabra = "";
            int[] aiPalabracaracter = { 0, 0 };
            object[] aoRetorno = new object[2];
            object[,] moIdiomas = idiomas(lsIdiomas, _iLongitud); ;
            /* ***************************************************************************************** */
            if (_bConcurrencia)
            {
                for (int i = 0; i < _asPalabras.Length; i++)
                {
                    if (!String.IsNullOrEmpty(_asPalabras[i]))
                    {
                        Parallel.Invoke(
                            () => { sPalabra = limpiarPalabra(_asPalabras[i]); },
                            () => { aiPalabracaracter[1] += _asPalabras[i].Length; },
                            () => { aiPalabracaracter[0]++; }
                        );
                        palabrasComunes(sPalabra, _bConcurrencia);
                        moIdiomas = analizarIdioma(sPalabra, lsIdiomas, _bConcurrencia, moIdiomas);
                    }
                }
            }
            else /* ------------------------------------------------------------------------------------ */
            {
                for (int i = 0; i < _asPalabras.Length; i++)
                {
                    sPalabra = limpiarPalabra(_asPalabras[i]);
                    if (!String.IsNullOrEmpty(_asPalabras[i]))
                    {
                        aiPalabracaracter[1] += _asPalabras[i].Length;
                        aiPalabracaracter[0]++;
                        palabrasComunes(sPalabra, _bConcurrencia);
                        moIdiomas = analizarIdioma(sPalabra, lsIdiomas, _bConcurrencia, moIdiomas);
                    }
                }
            }
            /* ***************************************************************************************** */
            aoRetorno[0] = aiPalabracaracter;
            aoRetorno[1] = moIdiomas;
            return aoRetorno;
        }

        /// <summary>
        /// 7777777777777777777777777777777777777777777777777777777777777777777777777777777777
        /// </summary>
        /// <param name="_sCadena"></param>
        /// <returns></returns>
        private object[] separar(string _sCadena) 
        {
            string[] sSeparador = _sCadena.Split(new char[] { ':' });
            string[] asPalabra = sSeparador[1].Split(new char[] { ',' });
            return new object[] { sSeparador[0], asPalabra };
        }

        /// <summary>
        /// 777777777777777777777777777777777777777777777777777777777777777777777777777777777
        /// </summary>
        /// <param name="_asIdiomas"></param>
        /// <returns></returns>
        private LinkedList<object[]> separar(string[] _asIdiomas) 
        {
            LinkedList<object[]> lsIdiomas = new LinkedList<object[]>();
            foreach (string sTmp in _asIdiomas)
                lsIdiomas.AddLast(separar(sTmp));
            return lsIdiomas;
        }

        /// <summary>
        /// 777777777777777777777777777777777777777777777777777777777777777777777777777777777777
        /// </summary>
        /// <param name="_lsIdiomas"></param>
        /// <param name="_iFila"></param>
        /// <returns></returns>
        private object[,] idiomas(LinkedList<object[]> _lsIdiomas, int _iFila) 
        {
            object[,] moIdiomas = new object[_iFila, 2];
            for (int i = 0; i < _lsIdiomas.Count; i++)
            {
                moIdiomas[i, 0] = _lsIdiomas.ElementAt(i)[0];
                moIdiomas[i, 1] = 0;
            }
            return moIdiomas;
        }


        /// <summary>
        /// 777777777777777777777777777777777777777777777777777777777777777777777777777777777777777
        /// </summary>
        /// <param name="_sPalabra"></param>
        /// <returns></returns>
        private string limpiarPalabra(string _sPalabra)
        {
            string[] asPalabra = _sPalabra.Split(new char[] { ',', '.', '-', '!', '?', '¿', ';', ':', '"', '\'', '\n','(',')' });         
            for (int i = 0; i < asPalabra.Length; i++)
            {
                if (asPalabra[i].Length > 0)
                    return asPalabra[i];
            }
            return "";
        }

        /// <summary>
        /// 777777777777777777777777777777777777777777777777777777777777777777777777777777777777777
        /// </summary>
        /// <param name="_sPalabra"></param>
        /// <param name="_asIdiomas"></param>
        /// <param name="_aiIndices"></param>
        /// <returns></returns>
        private bool compararPalabra(string _sPalabra, string[] _asIdiomas, int[] _aiIndices) 
        {
            for (int i = _aiIndices[0]; i < _aiIndices[1]; i++)
            {
                if (_sPalabra.Equals(_asIdiomas[i], StringComparison.OrdinalIgnoreCase))
                    return true;
            }
            return false; 
        }

        /// <summary>
        /// 77777777777777777777777777777777777777777777777777777777777777777777777777777777777
        /// </summary>
        /// <param name="_sPalabra"></param>
        /// <param name="_aoIdiomas"></param>
        /// <param name="_bConcurrencia"></param>
        /// <returns></returns>
        private bool analizarIdioma(string _sPalabra, object[] _aoIdiomas, bool _bConcurrencia) 
        {
            bool bResultado = false;
            int iTamano = ((string[])_aoIdiomas.ElementAt(1)).Length;
            /* ***************************************************************************************** */
            if (_bConcurrencia)
            {
                int iLongitud = iTamano / 3;
                int[] aiSegmento1 = { 0, iLongitud };
                int[] aiSegmento2 = { iLongitud, iLongitud * 2 };
                int[] aiSegmento3 = { iLongitud * 2, iTamano };
                
                Parallel.Invoke(
                    () => 
                    { 
                        if (compararPalabra(_sPalabra, (string[])_aoIdiomas.ElementAt(1), aiSegmento1)) 
                            bResultado = true; 
                    },
                    () => 
                    { 
                        if (compararPalabra(_sPalabra, (string[])_aoIdiomas.ElementAt(1), aiSegmento2)) 
                            bResultado = true; 
                    },
                    () => 
                    { 
                        if (compararPalabra(_sPalabra, (string[])_aoIdiomas.ElementAt(1), aiSegmento3)) 
                            bResultado = true; 
                    }
                );
            }
            else /* ------------------------------------------------------------------------------------ */
            {
                if (compararPalabra(_sPalabra, (string[])_aoIdiomas.ElementAt(1), new int[] { 0, iTamano }))
                    bResultado = true;
            }
            /* ***************************************************************************************** */
            return bResultado;
        }

        /// <summary>
        /// Identifica cual es el idioma del texto
        /// </summary>
        /// <param name="_sPalabra">Palabra que se va a analizar</param>
        /// <param name="_lsIdiomas">Los idiomas que reconoce la aplicacion</param>
        /// <param name="_bConcurrencia">Bandera que indica si se debe analizar con procesamiento concurrente o no</param>
        /// <param name="_moIdioma">Porcentaje de cada idioma presente en el texto</param>
        /// <returns>Porcentaje de cada idioma presente en el texto</returns>
        private object[,] analizarIdioma(string _sPalabra, LinkedList<object[]> _lsIdiomas, bool _bConcurrencia, object[,] _moIdioma)
        {
            int iTamano = ((string[])((object[])_lsIdiomas.ElementAt(0)).ElementAt(1)).Length;
            int iLongitud = iTamano / 3;
            int[] aiSegmento1 = {0, iLongitud};
            int[] aiSegmento2 = { iLongitud, iLongitud * 2 };
            int[] aiSegmento3 = { iLongitud * 2, iTamano };
            /* ***************************************************************************************** */
            if (_bConcurrencia)
            {
                Parallel.Invoke(
                    () => 
                    { 
                        if (analizarIdioma(_sPalabra, _lsIdiomas.ElementAt(0), _bConcurrencia)) 
                            _moIdioma[0, 1] = (int)_moIdioma[0, 1] + 1; 
                    },
                    () =>
                    {
                        if (analizarIdioma(_sPalabra, _lsIdiomas.ElementAt(1), _bConcurrencia))
                            _moIdioma[1, 1] = (int)_moIdioma[1, 1] + 1;
                    },
                    () => 
                    { 
                        if (analizarIdioma(_sPalabra, _lsIdiomas.ElementAt(2), _bConcurrencia)) 
                            _moIdioma[2, 1] = (int)_moIdioma[2, 1] + 1; 
                    }
                );
            }
            else /* ------------------------------------------------------------------------------------ */
            {
                for (int i = 0; i < _lsIdiomas.Count; i++)
                {
                    if (analizarIdioma(_sPalabra, _lsIdiomas.ElementAt(i), _bConcurrencia))
                        _moIdioma[i, 1] = (int)_moIdioma[i, 1] + 1;
                }
            }
            /* ***************************************************************************************** */
            return _moIdioma;
        }

        /// <summary>
        /// Carga archivos de texto
        /// </summary>
        /// <param name="_sPath">Direccion del archivo</param>
        /// <returns>Informacion que contiene el archivo</returns>
        private string[] cargarArchivo(string _sPath)
        {
            StreamReader srFile = new StreamReader(_sPath, System.Text.Encoding.Default);
            string sTexto = srFile.ReadToEnd();
            srFile.Close();
            if (sTexto.Length > 0)
                return sTexto.Split(new char[] { '\n' });
            else
                return null;
        }

        /// <summary>
        /// 77777777777777777777777777777777777777777777777777777777777777777777777777777777777777
        /// </summary>
        /// <param name="_sPalabra"></param>
        /// <param name="aiIndices"></param>
        private object[] palabrasComunes(string _sPalabra, List<object[]> _loPalabrasComunes) 
        {
            object[] aoDatos = new object[2];
            for (int i = 0; i < _loPalabrasComunes.Count; i++) 
            {
                if (_sPalabra.Equals((string)_loPalabrasComunes.ElementAt(i)[0], StringComparison.Ordinal))
                {
                    _loPalabrasComunes.ElementAt(i)[1] = ((int)_loPalabrasComunes.ElementAt(i)[1]) + 1;
                    aoDatos[0] = false;
                    aoDatos[1] = _loPalabrasComunes;
                    return aoDatos;
                }
            }
            aoDatos[0] = true;
            aoDatos[1] = _loPalabrasComunes;
            return aoDatos;
        }

        /// <summary>
        /// Busca las palabras mas comunes en el texto
        /// </summary>
        /// <param name="_sPalabra">Palabra que se va a evaluar</param>
        /// <param name="_bConcurrencia">Bandera que indica si se debe analizar con procesamiento concurrente o no</param>
        private void palabrasComunes(string _sPalabra, bool _bConcurrencia)
        {
            int iTamano = loPalabrasComunes.Count;
            int iSegmento = (iTamano / 3) + 1;
            var vLista = loPalabrasComunes.Select((x, i) => new { Index = i, Value = x })
                                          .GroupBy(x => x.Index / iSegmento)
                                          .Select(x => x.Select(v => v.Value).ToList())
                                          .ToList();
            /* ***************************************************************************************** */
            if (_bConcurrencia)
            {
                object[] aoListas = new object[3];
                
                if (vLista.Count >= 3)
                {
                    Parallel.Invoke(
                        () => { aoListas[0] = palabrasComunes(_sPalabra, new List<object[]>(vLista.ElementAt(0))); },
                        () => { aoListas[1] = palabrasComunes(_sPalabra, new List<object[]>(vLista.ElementAt(1))); },
                        () => { aoListas[2] = palabrasComunes(_sPalabra, new List<object[]>(vLista.ElementAt(2))); }
                    );
                    loPalabrasComunes = ((List<object[]>)((object[])aoListas[0])[1]).Concat(
                                            ((List<object[]>)((object[])aoListas[1])[1]).Concat(
                                                (List<object[]>)((object[])aoListas[2])[1])
                                            .ToList())
                                        .ToList();
                    if (((bool)((object[])aoListas[0])[0]) && 
                        ((bool)((object[])aoListas[1])[0]) && 
                        ((bool)((object[])aoListas[2])[0]))
                        loPalabrasComunes.Add(new object[] { _sPalabra, 1 });
                }
                else if (vLista.Count == 2)
                {
                    Parallel.Invoke(
                        () => { aoListas[0] = palabrasComunes(_sPalabra, new List<object[]>(vLista.ElementAt(0))); },
                        () => { aoListas[1] = palabrasComunes(_sPalabra, new List<object[]>(vLista.ElementAt(1))); }
                    );
                    loPalabrasComunes = ((List<object[]>)((object[])aoListas[0])[1]).Concat(
                                            ((List<object[]>)((object[])aoListas[1])[1]))
                                        .ToList();
                    if (((bool)((object[])aoListas[0])[0]) &&
                        ((bool)((object[])aoListas[1])[0]))
                        loPalabrasComunes.Add(new object[] { _sPalabra, 1 });
                }
                else
                {
                    if ((bool)palabrasComunes(_sPalabra, loPalabrasComunes)[0])
                        loPalabrasComunes.Add(new object[] { _sPalabra, 1 });
                }
            }
            else /* ------------------------------------------------------------------------------------ */
            {
                if ((bool)palabrasComunes(_sPalabra, loPalabrasComunes)[0])
                    loPalabrasComunes.Add(new object[] { _sPalabra, 1 });
            }
            /* ***************************************************************************************** */
            
        }
    }
}