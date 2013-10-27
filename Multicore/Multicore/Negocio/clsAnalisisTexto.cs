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
        private object[] asIdioma = new object[] { "", 0 };
        private int iCantidadPalabras = 0;
        private int iCantidadCaracteres = 0;
        private LinkedList<object[]> loPalabrasComunes = new LinkedList<object[]>();

        /// <summary>
        /// Obtiene el idioma del idioma del texto
        /// </summary>
        /// <returns>Nombre del lenguaje</returns>
        public string getIdioma() 
        {
            return (string)asIdioma[0];
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
            string sPalabra;
            string sPath = Directory.GetParent(Path.GetDirectoryName(Application.StartupPath)).FullName + "\\Datos\\diccionario.txt";
            string[] asIdiomas = cargarArchivo(sPath);
            LinkedList<object[]> lsIdiomas = new LinkedList<object[]>();
            string[] asPalabras = _sTexto.Split(new char[] { ' ' });
            object[,] moIdiomas = new object[asIdiomas.Length, 2];

            /* ***************************************************************************************** */
            if (_bConcurrencia && !String.IsNullOrEmpty(asIdiomas[0]))
            {
                Parallel.ForEach(asIdiomas, sTmp =>
                {
                    string[] sSeparador = sTmp.Split(new char[] { ':' });
                    string[] asPalabra = sSeparador[1].Split(new char[] { ',' });
                    lsIdiomas.AddLast(new object[] { sSeparador[0], asPalabra });
                });
                
                Parallel.For(0, lsIdiomas.Count, i =>
                {
                    moIdiomas[i, 0] = lsIdiomas.ElementAt(i)[0];
                    moIdiomas[i, 1] = 0;
                });
                
                Parallel.ForEach(asPalabras, sTmp =>
                {
                    sPalabra = limpiarPalabra(sTmp, _bConcurrencia);
                    if (!String.IsNullOrEmpty(sTmp))
                    {
                        iCantidadCaracteres += sTmp.Length;
                        iCantidadPalabras++;
                        palabrasComunes(sPalabra, _bConcurrencia);
                        moIdiomas = analizarIdioma(sPalabra, lsIdiomas, _bConcurrencia, moIdiomas);
                    }
                });

                Parallel.For(0, moIdiomas.Length / 2, i => 
                {
                    if (String.IsNullOrEmpty((string)asIdioma[0]) && (int)asIdioma[1] == 0)
                    {
                        asIdioma[0] = moIdiomas[i, 0];
                        asIdioma[1] = moIdiomas[i, 1];
                    }
                    else if ((int)asIdioma[1] < (int)moIdiomas[i, 1])
                    {
                        asIdioma[0] = moIdiomas[i, 0];
                        asIdioma[1] = moIdiomas[i, 1];
                    }
                });
                
            }
            else /* ------------------------------------------------------------------------------------ */
            {
                foreach (string sTmp in asIdiomas)
                {
                    string[] sSeparador = sTmp.Split(new char[] { ':' });
                    string[] asPalabra = sSeparador[1].Split(new char[] { ',' });
                    lsIdiomas.AddLast(new object[] { sSeparador[0], asPalabra });
                }
                
                for (int i = 0; i < lsIdiomas.Count; i++)
                {
                    moIdiomas[i, 0] = lsIdiomas.ElementAt(i)[0];
                    moIdiomas[i, 1] = 0;
                }
                
                foreach (string sTmp in asPalabras)
                {
                    if (!String.IsNullOrEmpty(sTmp))
                    {
                        sPalabra = limpiarPalabra(sTmp, _bConcurrencia);
                        if (!String.IsNullOrEmpty(sTmp))
                        {
                            iCantidadCaracteres += sTmp.Length;
                            iCantidadPalabras++;
                            palabrasComunes(sPalabra, _bConcurrencia);
                            moIdiomas = analizarIdioma(sPalabra, lsIdiomas, _bConcurrencia, moIdiomas);
                        }
                    }
                }
                
                for (int i = 0; i < moIdiomas.Length / 2; i++)
                {
                    if (String.IsNullOrEmpty((string)asIdioma[0]) && (int)asIdioma[1] == 0)
                    {
                        asIdioma[0] = moIdiomas[i, 0];
                        asIdioma[1] = moIdiomas[i, 1];
                    }
                    else if ((int)asIdioma[1] < (int)moIdiomas[i, 1])
                    {
                        asIdioma[0] = moIdiomas[i, 0];
                        asIdioma[1] = moIdiomas[i, 1];
                    }
                }
            }
            /* ***************************************************************************************** */
        }

        /// <summary>
        /// Elimina todos los caracteres que no son letras o numeros que se encuentran concatenados a las
        /// palabras
        /// </summary>
        /// <param name="_sPalabra">Palabra que se le va a quitar los caracteres</param>
        /// <param name="_bConcurrencia">Bandera que indica si se debe analizar con procesamiento concurrente o no</param>
        /// <returns>La palabra sin caracteres especiales</returns>
        private string limpiarPalabra(string _sPalabra, bool _bConcurrencia)
        {
            string sPalabra = "";
            string[] asPalabra = _sPalabra.Split(new char[] { ',', '.', '-', '!', '?', '¿', ';', ':', '"', '\'', '\n' });
            /* ***************************************************************************************** */
            if (_bConcurrencia)
            {
                Parallel.For(0, asPalabra.Length, i =>
                {
                    if (asPalabra[i].Length > 1)
                        sPalabra = asPalabra[i];
                });
            }
            else /* ------------------------------------------------------------------------------------ */
            {
                for (int i = 0; i < asPalabra.Length; i++)
                {
                    if (asPalabra[i].Length > 0)
                        sPalabra = asPalabra[i];
                }
            }
            /* ***************************************************************************************** */
            return sPalabra;
        }

        /// <summary>
        /// Identifica cual es el idioma del texto
        /// </summary>
        /// <param name="_sPalabra">Palabra que se va a analizar</param>
        /// <param name="_asIdiomas">Los idiomas que reconoce la aplicacion</param>
        /// <param name="_bConcurrencia">Bandera que indica si se debe analizar con procesamiento concurrente o no</param>
        /// <param name="moIdioma">Porcentaje de cada idioma presente en el texto</param>
        /// <returns>Porcentaje de cada idioma presente en el texto</returns>
        private object[,] analizarIdioma(string _sPalabra, LinkedList<object[]> _asIdiomas, bool _bConcurrencia, object[,] moIdioma)
        {
            int iTamano = ((string[])((object[])_asIdiomas.ElementAt(0)).ElementAt(1)).Length;
            /* ***************************************************************************************** */
            if (_bConcurrencia)
            {
                Parallel.For(0, iTamano, i =>
                {
                    Parallel.For(0, moIdioma.Length, j =>
                    {
                        if (_sPalabra.Equals(((string[])((object[])_asIdiomas.ElementAt(j)).ElementAt(1))[i],
                                         StringComparison.OrdinalIgnoreCase))
                            moIdioma[j, 1] = (int)moIdioma[j, 1] + 1;
                    });
                });
            }
            else /* ------------------------------------------------------------------------------------ */
            {
                for (int i = 0; i < iTamano; i++)
                {
                    for (int j = 0; j < moIdioma.Length / 2; j++)
                    {
                        if (_sPalabra.Equals(((string[])((object[])_asIdiomas.ElementAt(j)).ElementAt(1))[i],
                                         StringComparison.OrdinalIgnoreCase))
                            moIdioma[j, 1] = (int)moIdioma[j, 1] + 1;
                    }
                }
            }
            /* ***************************************************************************************** */
            return moIdioma;
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
        /// Busca las palabras mas comunes en el texto
        /// </summary>
        /// <param name="_sPalabra">Palabra que se va a evaluar</param>
        /// <param name="_bConcurrencia">Bandera que indica si se debe analizar con procesamiento concurrente o no</param>
        private void palabrasComunes(string _sPalabra, bool _bConcurrencia)
        {
            bool bExiste = true;
            /* ***************************************************************************************** */
            if (_bConcurrencia)
            {
                Parallel.ForEach(loPalabrasComunes, sPalabra =>
                {
                    if (_sPalabra.Equals((string)sPalabra[0], StringComparison.Ordinal))
                    {
                        int iCantidad = (int)sPalabra[1];
                        sPalabra[1] = ++iCantidad;
                        bExiste = false;
                    }
                });
                if (bExiste)
                {
                    loPalabrasComunes.AddLast(new object[] { _sPalabra, 1 });
                    bExiste = true;
                }
            }
            else /* ------------------------------------------------------------------------------------ */
            {
                foreach (object[] sPalabra in loPalabrasComunes)
                {
                    if (_sPalabra.Equals((string)sPalabra[0], StringComparison.OrdinalIgnoreCase))
                    {
                        //int iCantidad = (int)sPalabra[1];
                        sPalabra[1] = ((int)sPalabra[1]) + 1;/*++iCantidad;*/
                        bExiste = false;
                    }
                }
                if (bExiste)
                {
                    loPalabrasComunes.AddLast(new object[] { _sPalabra, 1 });
                    bExiste = true;
                }
            }
            /* ***************************************************************************************** */
            var lsPalabras = loPalabrasComunes.OrderByDescending(x => x[1]).ToList();
            //lsPalabrasComunes = new LinkedList<object[]>(lsPalabras);
        }
    }
}