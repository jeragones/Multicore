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
            clsArchivo insArvhivo = new clsArchivo();
            string[] asIdiomas = insArvhivo.cargarArchivo(sPath).Split(new char[] { '\n' });
            LinkedList<object[]> lsIdiomas = new LinkedList<object[]>();
            string[] asPalabras = _sTexto.Split(new char[] { ' ' });
            
            iCantidadPalabras = asPalabras.Length;
            object[,] moIdiomas = null;
            List<object[]> comunes = null;

            if (!String.IsNullOrEmpty(asIdiomas[0]) && iCantidadPalabras > 0) 
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

                    int iSegmento = (asPalabras.Length / 3) + 1;
                    var vLista = asPalabras.Select((x, i) => new { Index = i, Value = x })
                                           .GroupBy(x => x.Index / iSegmento)
                                           .Select(x => x.Select(v => v.Value).ToList())
                                           .ToList();
                    Parallel.Invoke(
                        () => 
                        {
                            aoDatos1 = analisisTextual(vLista.ElementAt(0).ToArray(), asIdiomas.Length, lsIdiomas); 
                        },
                        () => 
                        {
                            aoDatos2 = analisisTextual(vLista.ElementAt(1).ToArray(), asIdiomas.Length, lsIdiomas); 
                        },
                        () => 
                        {
                            aoDatos3 = analisisTextual(vLista.ElementAt(2).ToArray(), asIdiomas.Length, lsIdiomas); 
                        }
                    );
                    iCantidadCaracteres = (int)aoDatos1[0] + (int)aoDatos2[0] + (int)aoDatos3[0];
                    comunes = palabrasComunes((List<object[]>)aoDatos1[1], (List<object[]>)aoDatos2[1], (List<object[]>)aoDatos3[1]);
                    moIdiomas = (object[,])aoDatos1[2];
                    moIdiomas[0, 1] = (int)moIdiomas[0, 1] + (int)((object[,])aoDatos2[2])[0, 1] + (int)((object[,])aoDatos3[2])[0, 1];
                    moIdiomas[1, 1] = (int)moIdiomas[1, 1] + (int)((object[,])aoDatos2[2])[1, 1] + (int)((object[,])aoDatos3[2])[1, 1];
                    moIdiomas[2, 1] = (int)moIdiomas[2, 1] + (int)((object[,])aoDatos2[2])[2, 1] + (int)((object[,])aoDatos3[2])[2, 1];
                }
                else /* ------------------------------------------------------------------------------------ */
                {
                    lsIdiomas = separar(asIdiomas);
                    object[] aoDatos = analisisTextual(asPalabras, asIdiomas.Length, lsIdiomas);
                    iCantidadCaracteres = (int)aoDatos[0];
                    comunes = (List<object[]>)aoDatos[1];
                    moIdiomas = (object[,])aoDatos[2];
                }
                /* ***************************************************************************************** */
            }
            identificarIdioma(moIdiomas);
            var lsPalabras = comunes.OrderByDescending(x => x[1]).ToList();
            loPalabrasComunes = new List<object[]>(lsPalabras);
        }

        /// <summary>
        /// Une las sublistas e identifica cuales son las palabras mas comunes en el texto
        /// </summary>
        /// <param name="_loDatos1">Sublista de palabras comunes del texto</param>
        /// <param name="_loDatos2">Sublista de palabras comunes del texto</param>
        /// <param name="_loDatos3">Sublista de palabras comunes del texto</param>
        /// <returns>Lista completa con las palabras mas comunes</returns>
        private List<object[]> palabrasComunes(List<object[]> _loDatos1, List<object[]> _loDatos2, List<object[]> _loDatos3) 
        {
            List<object[]> loLista = null;
            List<object[]> loDatos = _loDatos3;
            Parallel.Invoke(
                () => 
                {
                    for (int i = 0; i < loDatos.Count; i++)
                    {
                        for (int j = 0; j < _loDatos1.Count; j++)
                        {
                            if (((string)loDatos.ElementAt(i)[0]).Equals((string)_loDatos1.ElementAt(j)[0], StringComparison.OrdinalIgnoreCase))
                            {
                                loDatos.ElementAt(i)[1] = (int)loDatos.ElementAt(i)[1] + (int)_loDatos1.ElementAt(j)[1];
                                _loDatos1.RemoveAt(j);
                                j--;
                            }
                        }
                    }
                },
                () => 
                {
                    for (int i = 0; i < loDatos.Count; i++)
                    {
                        for (int j = 0; j < _loDatos2.Count; j++)
                        {
                            if (((string)loDatos.ElementAt(i)[0]).Equals((string)_loDatos2.ElementAt(j)[0], StringComparison.OrdinalIgnoreCase))
                            {
                                loDatos.ElementAt(i)[1] = (int)loDatos.ElementAt(i)[1] + (int)_loDatos2.ElementAt(j)[1];
                                _loDatos2.RemoveAt(j);
                                j--;
                            }
                        }
                    }
                }
            );
            loLista = loDatos.Concat(_loDatos1.Concat(_loDatos2).ToList()).ToList();
            return new List<object[]>(loLista);
        }

        /// <summary>
        /// Identifica cual es el idioma dominante en el texto
        /// </summary>
        /// <param name="_moIdiomas">Estadisticas de la presencia de cada idioma en el texto</param>
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
        /// Analiza el en cuanto a las palabras, caracteres presentes en el texto
        /// </summary>
        /// <param name="_asPalabras">Palabras por las que esta compuesto el texto a analizar</param>
        /// <param name="_iLongitud">Tamaño de la lista de palabras mas importantes de cada dialecto</param>
        /// <param name="_loIdiomas">Lista de palabras mas importantes de cada dialecto</param>
        /// <returns>Canidad de caracteres presentes en el texto, palabras comunes y estadisticas en cuanto 
        ///          a la presencia de cada idioma en el texto</returns>
        private object[] analisisTextual(string[] _asPalabras, int _iLongitud, LinkedList<object[]> _loIdiomas) 
        {
            int iCaracter = 0;
            object[] aoRetorno = new object[3];
            object[,] moIdiomas = idiomas(_loIdiomas, _iLongitud);
            List<object[]> loComunes = new List<object[]>();
            bool bExiste = true;

            foreach(string palabra in _asPalabras)
            {
                string sPalabra = limpiarPalabra(palabra);
                iCaracter += palabra.Length;

                for (int i = 0; i < _loIdiomas.Count; i++) 
                {
                    foreach (string sIdioma in ((string[])((object[])_loIdiomas.ElementAt(i))[1])) 
                    {
                        if (sPalabra.Equals(sIdioma, StringComparison.OrdinalIgnoreCase))
                        {
                            moIdiomas[i, 1] = (int)moIdiomas[i, 1] + 1;
                            break;
                        }
                    }
                }
                for (int j = 0; j < loComunes.Count; j++)
                {
                    if (sPalabra.Equals((string)loComunes.ElementAt(j)[0], StringComparison.OrdinalIgnoreCase))
                    {
                        loComunes.ElementAt(j)[1] = (int)loComunes.ElementAt(j)[1] + 1;
                        bExiste = false;
                        break;
                    }
                }
                if (bExiste)
                    loComunes.Add(new object[] { sPalabra, 1 });
                bExiste = true;
            }
            aoRetorno[0] = iCaracter;
            aoRetorno[1] = loComunes;
            aoRetorno[2] = moIdiomas;
            return aoRetorno;
        }

        /// <summary>
        /// Separa la cadena de caracteres en las distintas palabras claves de un idioma
        /// </summary>
        /// <param name="_sCadena">Cadena de caracteres</param>
        /// <returns>Arreglo con las palabras claves ordenadas</returns>
        private object[] separar(string _sCadena) 
        {
            string[] sSeparador = _sCadena.Split(new char[] { ':' });
            string[] asPalabra = sSeparador[1].Split(new char[] { ',' });
            return new object[] { sSeparador[0], asPalabra };
        }

        /// <summary>
        /// Separa la cadena de caracteres en las distintas palabras claves de cada idioma
        /// </summary>
        /// <param name="_asIdiomas">Cadena de caracteres</param>
        /// <returns>Lista con las palabras claves ordenadas</returns>
        private LinkedList<object[]> separar(string[] _asIdiomas) 
        {
            LinkedList<object[]> lsIdiomas = new LinkedList<object[]>();
            foreach (string sTmp in _asIdiomas)
                lsIdiomas.AddLast(separar(sTmp));
            return lsIdiomas;
        }

        /// <summary>
        /// Clasifica los distintos idiomas a evaluar en una matriz
        /// </summary>
        /// <param name="_lsIdiomas">Lista de idiomas</param>
        /// <param name="_iFila">Cantidad de idiomas</param>
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
        /// Elimina todos los caracteres eespeciales ligados a una palabra
        /// </summary>
        /// <param name="_sPalabra">Palabras que se quiere limpiar</param>
        /// <returns>Palabra sin caracteres especiales ligados</returns>
        private string limpiarPalabra(string _sPalabra)
        {
            string[] asPalabra = _sPalabra.Split(new char[] { ',', '.', '-', '!', '?', '¿', ';', ':', '"', '\'', '\n','(',')',' ' });         
            for (int i = 0; i < asPalabra.Length; i++)
            {
                if (asPalabra[i].Length > 0)
                    return asPalabra[i];
            }
            return "";
        }
    }
}