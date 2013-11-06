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
        /// <returns>Retorna la clave creada aleatoriamente</returns>
        private static string generarClave(string txt)
        {
            string str="";
            
            Random random = new Random();
            

            for (int i = 0; i < txt.Length;i++ )
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
        /// <param name="clave">Clave necesaria para encriptar y desencriptar</param>
        /// <param name="texto">Texto que se desea encriptar</param>
        /// <returns>Retorna el texto encriptado</returns>
     private static string[] encriptXORSecuencial(string clave, string texto)
        {
            string[] words = texto.Split(' ');
            for (int e = 0; e < words.Length; e++)
            {
                int i = 0;
                string nWord = "";
                foreach (char c in words[e])
                {
                    int cl = Convert.ToInt16(clave[i]);
                    int _c = (int)c + cl;
                    nWord += ((char)_c);
                    i++;
                }
                words[e] = nWord;
            }
            return words;
        }

     private static string[] desencriptXORSecuencial(string clave, string texto)
     {
         string[] words = texto.Split(' ');
         for (int e = 0; e < words.Length; e++)
         {
             int i = 0;
             string nWord = "";
             foreach (char c in words[e])
             {
                 int cl = Convert.ToInt16(clave[i]);
                 int _c = (int)c - cl;
                 nWord += ((char)_c);
                 i++;
             }
             words[e] = nWord;
         }
         return words;
     }

     private static string[] encriptXORParalelo(string clave, string texto)
     {
         string[] words = texto.Split(' ');
         Parallel.For(0, words.Length, e =>
         {
             int i = 0;
             string nWord = "";
             foreach (char c in words[e])
             {
                 int cl = Convert.ToInt16(clave[i]);
                 int _c = (int)c + cl;
                 nWord += ((char)_c);
                 i++;
             }
             words[e] = nWord;
         });
         
         return words;
     }

     private static string[] desencriptXORParalelo(string clave, string texto)
     {
         string[] words = texto.Split(' ');
         Parallel.For(0, words.Length, e =>
         {
             int i = 0;
             string nWord = "";
             foreach (char c in words[e])
             {
                 int cl = Convert.ToInt16(clave[i]);
                 int _c = (int)c - cl;
                 nWord += ((char)_c);
                 i++;
             }
             words[e] = nWord;
         });

         return words;
     }

              
        
        
        public static List<object> encriptarXOR(string txt, bool parallel)
        {
            
            string clave = generarClave(txt);
            string[] mensaje = null;
            
            if (parallel == false)
            {
                
                mensaje = encriptXORSecuencial(clave, txt);
                
                
            }
            else if (parallel == true)
            {
                mensaje = encriptXORParalelo(clave, txt);
                
            }
            List<object> resultado = new List<object>() {clave,mensaje};
            return resultado;

        }

        public static string[] desencriptarXOR(string clave,string txt, bool parallel)
        {

            
            string[] mensaje = null;

            if (parallel == false)
            {

                mensaje = desencriptXORSecuencial(clave, txt);


            }
            else if (parallel == true)
            {
                mensaje = desencriptXORParalelo(clave, txt);

            }
            
            return mensaje;

        }



    }
}
