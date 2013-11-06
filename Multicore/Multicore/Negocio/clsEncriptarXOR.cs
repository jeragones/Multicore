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
        public static string generarClave()
        {
            string str="";
            
            Random random = new Random();
            

            for (int i = 0; i < 50;i++ )
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
        public static string encriptXORSecuencial(string clave, string texto)
        {
            string encrip = "";
            int i = 0;
            foreach (char c in texto)
            {
                if (i >= clave.Length)
                {
                    i -= clave.Length;
                }
                int cl = Convert.ToInt16(clave[i]);
                int _c = (int)c + cl;
                encrip += ((char)_c);
                i++;
            }
            return encrip;
        }

        public static string encriptXORParalelo(string clave, string texto)
        {
            string encrip = "";
            /*int i = 0;
            Parallel.ForEach(texto,c=>
            {
                if (i >= clave.Length)
                {
                    i -= clave.Length;
                }
                int cl = Convert.ToInt16(clave[i]);
                int _c = (int)c + cl;
                encrip += ((char)_c);
                i++;
            });*/
            int i = 0;
            Parallel.For(0, texto.Length, e =>
            {
                if (i >= clave.Length)
                {
                    i -= clave.Length;
                }
                int cl = Convert.ToInt16(clave[i]);
                int _c = texto[e] + cl;
                encrip += ((char)_c);
                i++;
            });
            
            return encrip;
        }

        public static string desencriptXOR(string clave, string texto)
        {
            string encrip = "";
            int i = 0;
            foreach (char c in texto)
            {
                if (i >= clave.Length)
                {
                    i -= clave.Length;
                }
                int cl = Convert.ToInt16(clave[i]);
                int _c = (int)c - cl;
                encrip += ((char)_c);
                i++;
            }
            return encrip;
        }
        
        
        public static string[] encriptarXOR(string txt, bool parallel)
        {
            string[] resultado = new string[2];
            string clave = generarClave();
            resultado[0] = clave;
            if (parallel == false)
            {
                
                string mensaje = encriptXORSecuencial(clave, txt);
                
                resultado[1] = mensaje;
            }
            else if (parallel == true)
            {
                string encr = encriptXORParalelo(clave, txt);
                resultado[1]=encr;
            }
            
            return resultado;

        }



    }
}
