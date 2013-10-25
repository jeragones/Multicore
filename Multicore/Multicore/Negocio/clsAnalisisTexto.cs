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
        public static string sIdioma = "";
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
            string sPath = Directory.GetParent(Path.GetDirectoryName(Application.StartupPath)).FullName + "\\Datos\\diccionario.txt";
            //LinkedList<object[]> lsCaracteres = new LinkedList<object[]>();

            iCantidadCaracteres = _sTexto.Length;

            string[] asPalabras = _sTexto.Split(new char[]{' '});
            iCantidadPalabras = asPalabras.Length;

            /* ***************************************************************************************** */
            if (_bConcurrencia)
            {
                Parallel.ForEach(asPalabras, sPalabra =>
                {
                    if (sPalabra != "")
                    {
                        palabrasComunes(sPalabra, _bConcurrencia);
                        //lsCaracteres = caracteresComunes(sPalabra, lsCaracteres, _bConcurrencia);
                    }
                });
            }
            else /* ------------------------------------------------------------------------------------ */ 
            {
                foreach (string sPalabra in asPalabras)
                {
                    if (sPalabra != "") 
                    {
                        palabrasComunes(sPalabra, _bConcurrencia);
                        //lsCaracteres = caracteresComunes(sPalabra, lsCaracteres, _bConcurrencia);
                    }
                }
            }
            /* ***************************************************************************************** */
            string[] asIdiomas = leerArchivo(sPath);
            if (String.IsNullOrEmpty(asIdiomas[0]))
                analizarIdioma(asIdiomas);
        }

        /// <summary>
        /// 
        /// </summary>
        private void analizarIdioma(string[] _asIdiomas) 
        {

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
        /// <param name="_lsCaracteres"></param>
        /// <param name="_bConcurrencia"></param>
        /// <returns></returns>
        /*private LinkedList<object[]> caracteresComunes(string _sPalabra, LinkedList<object[]> _lsCaracteres, bool _bConcurrencia)
        {
            bool bExiste = true;
            /* ***************************************************************************************** */
            /*if (_bConcurrencia)
            {
                Parallel.ForEach(_sPalabra, cCaracter =>
                {
                    Parallel.ForEach(_lsCaracteres, aoCaracter =>
                    {
                        if (cCaracter == (char)aoCaracter[0]) 
                        {
                            int iCantidad = (int)aoCaracter[1];
                            aoCaracter[1] = iCantidad++;
                            bExiste = false;
                        }
                    });
                    if (bExiste) 
                    {
                        _lsCaracteres.AddLast(new object[]{cCaracter,1});
                        bExiste = true;
                    }
                });
            }
            else /* ------------------------------------------------------------------------------------ */
            /*{
                foreach (char cCaracter in _sPalabra)
                {
                    foreach (object[] aoCaracter in _lsCaracteres)
                    {
                        if (cCaracter == (char)aoCaracter[0])
                        {
                            int iCantidad = (int)aoCaracter[1];
                            aoCaracter[1] = ++iCantidad;
                            bExiste = false;
                        }
                    }
                    if (bExiste)
                    {
                        _lsCaracteres.AddLast(new object[] { cCaracter, 1 });
                        bExiste = true;
                    }
                }
            }
            /* ***************************************************************************************** */
            /*var lsCaracteres = _lsCaracteres.OrderByDescending(x => x[1]).ToList();
            return new LinkedList<object[]>(lsCaracteres);
        }*/

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
                        int iCantidad = (int)sPalabra[1];
                        sPalabra[1] = ++iCantidad;
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
            lsPalabrasComunes = new LinkedList<object[]>(lsPalabras);
        }
    }
}
