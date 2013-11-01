﻿using System;
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
            List<char> str = new List<char>();
            int i = 0;
            Random random = new Random();
            while (i < 50)
            {
                char c = (char)random.Next(0, 100);
                str.Add(c);
                i++;
            }
            return new string(str.ToArray());
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
            if (!(clave.Length < texto.Length))
            {
                
                int i = 0;
                foreach (char c in texto)
                {
                    int _c = (int)c ^ (int)clave[i];
                    encrip+=((char)_c);
                    i++;
                }
            }
            else
            {
                MessageBox.Show("Error: key must be larger than text", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return encrip;
        }




    }
}
