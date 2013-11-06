using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading.Tasks;

namespace Multicore.Negocio
{
    
    class clsEncriptarXOR
    {
        
        /// <summary>
        /// Metodo que genera la clave necesaria para encriptar y desencriptar
        /// </summary>
        /// <param name="_sTxt">Texto que se desea encriptar</param>
        /// <returns>Retorna la clave creada aleatoriamente</returns>
        private static string generarClave(string _sTxt)
        {
            string str="";
            
            Random random = new Random();
            

            for (int i = 0; i < _sTxt.Length;i++ )
            {
                int rdm = random.Next(48, 126);
                if (rdm > 57 && rdm < 65 || rdm > 90 && rdm < 97 || rdm > 122)
                {
                    rdm = random.Next(65, 90);
                }

                char x = (char)rdm;
                str += x;

            }
            
            return str;
        }

        /// <summary>
        /// Metodo que encripta de forma secuencial un texto mediante convinacion XOR 
        /// </summary>
        /// <param name="_sClave">Clave necesaria para encriptar y desencriptar</param>
        /// <param name="_sTexto">Texto que se desea encriptar</param>
        /// <returns>Retorna el texto encriptado</returns>
     private static string[] encriptXORSecuencial(string _sClave, string _sTexto)
        {
            string[] words = _sTexto.Split(' ');
            for (int e = 0; e < words.Length; e++)
            {
                int i = 0;
                string nWord = "";
                foreach (char c in words[e])
                {
                    int cl = Convert.ToInt16(_sClave[i]);
                    int _c = (int)c + cl;
                    nWord += ((char)_c);
                    i++;
                }
                words[e] = nWord;
            }
            return words;
        }

        /// <summary>
        /// Metodo que desencripta de forma secuencial un texto mediante convinacion XOR
        /// </summary>
        /// <param name="_sClave">Clave necesaria para desencriptar</param>
        /// <param name="_sTexto">Texto que se desencriptara</param>
        /// <returns>Retorna un arreglo de strings con las palabras del texto ya desencriptadas</returns>
     private static string[] desencriptXORSecuencial(string _sClave, string _sTexto)
     {
         string[] words = _sTexto.Split(' ');
         for (int e = 0; e < words.Length; e++)
         {
             int i = 0;
             string nWord = "";
             foreach (char c in words[e])
             {
                 int cl = Convert.ToInt16(_sClave[i]);
                 int _c = (int)c - cl;
                 nWord += ((char)_c);
                 i++;
             }
             words[e] = nWord;
         }
         return words;
     }

        /// <summary>
        /// Metodo que encripta un texto de forma paralela mediante convinacion XOR
        /// </summary>
        /// <param name="_sClave">Clave necesaria para encriptar el texto</param>
        /// <param name="_sTexto">Texto que se desea encriptar</param>
        /// <returns>Retorna un arreglo de strings con las palabras encriptadas</returns>
     private static string[] encriptXORParalelo(string _sClave, string _sTexto)
     {
         string[] words = _sTexto.Split(' ');
         Parallel.For(0, words.Length, e =>
         {
             int i = 0;
             string nWord = "";
             foreach (char c in words[e])
             {
                 int cl = Convert.ToInt16(_sClave[i]);
                 int _c = (int)c + cl;
                 nWord += ((char)_c);
                 i++;
             }
             words[e] = nWord;
         });
         
         return words;
     }

     /// <summary>
     /// Metodo que desencripta de forma paralela un texto mediante convinacion XOR
     /// </summary>
     /// <param name="_sClave">Clave necesaria para desencriptar</param>
     /// <param name="_sTexto">Texto que se desencriptara</param>
     /// <returns>Retorna un arreglo de strings con las palabras del texto ya desencriptadas</returns>
     private static string[] desencriptXORParalelo(string _sClave, string _sTexto)
     {
         string[] words = _sTexto.Split(' ');
         Parallel.For(0, words.Length, e =>
         {
             int i = 0;
             string nWord = "";
             foreach (char c in words[e])
             {
                 int cl = Convert.ToInt16(_sClave[i]);
                 int _c = (int)c - cl;
                 nWord += ((char)_c);
                 i++;
             }
             words[e] = nWord;
         });

         return words;
     }

              
        
        /// <summary>
        /// Metodo principal para encriptar un texto mediante convinacion XOR
        /// </summary>
        /// <param name="_sTxt">Texto que se desea encriptar</param>
        /// <param name="_bParallel">Bandera que indica la forma de ejecución, true = paralelo y false = secuencial</param>
        /// <returns>Retorna un lista de objetos, en esta se encuentra la clave y el texto encriptado</returns>
        public static List<object> encriptarXOR(string _sTxt, bool _bParallel)
        {
            
            string clave = generarClave(_sTxt);
            string[] mensaje = null;
            
            if (_bParallel == false)
            {

                mensaje = encriptXORSecuencial(clave, _sTxt);
                
                
            }
            else if (_bParallel == true)
            {
                mensaje = encriptXORParalelo(clave, _sTxt);
                
            }
            List<object> resultado = new List<object>() {clave,mensaje};
            return resultado;

        }

        /// <summary>
        /// Metodo principal para desencriptar un texto mediante convinacion XOR
        /// </summary>
        /// <param name="_sTxt">Texto que se desea desencriptar</param>
        /// <param name="_bParallel">Bandera que indica la forma de ejecución, true = paralelo y false = secuencial</param>
        /// <returns>Retorna un lista de objetos, en esta se encuentra la clave y el texto desencriptado</returns>
        public static string[] desencriptarXOR(string _sClave,string _sTxt, bool _bParallel)
        {

            
            string[] mensaje = null;

            if (_bParallel == false)
            {

                mensaje = desencriptXORSecuencial(_sClave, _sTxt);


            }
            else if (_bParallel == true)
            {
                mensaje = desencriptXORParalelo(_sClave, _sTxt);

            }
            
            return mensaje;

        }



    }
}
