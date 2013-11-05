using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Multicore.Negocio
{
    class clsEncriptarXOR
    {
        
        /// <summary>
        /// Metodo que genera la clave necesaria para encriptar y desencriptar
        /// </summary>
        /// <returns>Retorna la clave creada aleatoriamente</returns>
        public static string generarClave()
        {
            string str="";
            
            Random random = new Random();
            

            for (int i = 0; i < 100;i++ )
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
        public static string encriptXOR(string clave, string texto)
        {
            string encrip = "";
            int i = 0;
            foreach (char c in texto)
            {
                if (i >= clave.Length)
                {
                    i -= clave.Length;
                }
                int _c = (int)c ^ clave[i];
                
                encrip+=((char)_c);
                i++;
            }
            
            
            
            return encrip;
        }



        public static string[] XOR(string txt)
        {
            string[] result= new string[2];
            string clave = generarClave();
            string mensaje = encriptXOR(clave, txt);
            result[0] = clave;
            result[1] = mensaje;
            return result;
        }


    }
}
