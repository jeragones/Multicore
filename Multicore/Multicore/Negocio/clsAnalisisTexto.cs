using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;

namespace Multicore.Negocio
{
    class clsAnalisisTexto
    {
        public static object[] asIdioma = new object[] { "", 0 };
        public static int iCantidadPalabras = 0;
        public static int iCantidadCaracteres = 0;
        public static LinkedList<object[]> lsPalabrasComunes = new LinkedList<object[]>();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_sTexto"></param>
        /// <param name="_bConcurrencia"></param>
        public void analizarTexto(string _sTexto, bool _bConcurrencia)
        {
            string sPalabra;
            string sPath = Directory.GetParent(Path.GetDirectoryName(Application.StartupPath)).FullName + "\\Datos\\diccionario.txt";
            string[] asIdiomas = leerArchivo(sPath);
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
                Console.WriteLine(asIdioma[0] + " - " + asIdioma[1]);
            }
            /* ***************************************************************************************** */

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_sPalabra"></param>
        /// <param name="_bConcurrencia"></param>
        /// <returns></returns>
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
        /// 
        /// </summary>
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

        private string[] leerArchivo(string _sPath)
        {
            System.IO.StreamReader srFile = new System.IO.StreamReader(_sPath, System.Text.Encoding.Default);
            string sTexto = srFile.ReadToEnd();
            srFile.Close();
            if (sTexto.Length > 0)
                return sTexto.Split(new char[] { '\n' });
            else
                return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_sPalabra"></param>
        /// <param name="_bConcurrencia"></param>
        private void palabrasComunes(string _sPalabra, bool _bConcurrencia)
        {
            bool bExiste = true;
            /* ***************************************************************************************** */
            if (_bConcurrencia)
            {
                Parallel.ForEach(lsPalabrasComunes, sPalabra =>
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
                    lsPalabrasComunes.AddLast(new object[] { _sPalabra, 1 });
                    bExiste = true;
                }
            }
            else /* ------------------------------------------------------------------------------------ */
            {
                foreach (object[] sPalabra in lsPalabrasComunes)
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
                    lsPalabrasComunes.AddLast(new object[] { _sPalabra, 1 });
                    bExiste = true;
                }
            }
            /* ***************************************************************************************** */
            var lsPalabras = lsPalabrasComunes.OrderByDescending(x => x[1]).ToList();
            //lsPalabrasComunes = new LinkedList<object[]>(lsPalabras);
        }
    }
}