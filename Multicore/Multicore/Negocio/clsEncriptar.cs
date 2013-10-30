using System.Collections.Generic;
using System.Linq;
using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Diagnostics;
using System.Threading.Tasks;




namespace Multicore.Negocio
{
    public class clsEncriptar
    {   
        /// <summary>
        /// Metodo de cifrado por sustitucion (Cifrado Cesar)
        /// </summary>
        /// <param name="texto"> Texto que se desea encriptar </param>
        /// <param name="salto"> Numero de saltos entre los caragteres de la tabla ascii</param>
        /// <param name="parallel"> Boleano que indica la manera en que se ejecutara el ciclo, true para paralelo y false para secuencial</param>
        /// <returns>Retorna un arreglo de 2 posiciones, en la posicion 0 se encuentra el tecto encriptado y en la posicion 1 el tiempo de ejecucion</returns>
        public static string encriptarCesar(string texto,int salto)
        {
            //texto encriptado
            string encriptado="";
            //valor ascii de la letra a cambiar
            int letra;
            for (int i = 0; i < texto.Length; i++)
            {
                //obtiene el valor ascii
                letra = Convert.ToInt32(texto[i]) + salto;
                //pasa de Z a A 
                if (letra > 122)
                {
                    letra = letra - 26;
                }
                //convieerte el valor ascii a caracter
                encriptado += Convert.ToChar(letra);
            }
            return encriptado;
        }


        public static string encrip(string texto, int salto, bool parallel)
        {
            string mitad1;
            string mitad2;
            int largoTexto = texto.Count();
            int mitad = largoTexto / 2;
            mitad1 = texto.Substring(0,mitad);
            mitad2 = texto.Substring(mitad,mitad);
            //texto encriptado
            string encriptado = "";
            //valor ascii de la letra a cambiar
            if (parallel == false)
            {
                encriptado=encriptarCesar(texto, salto);
                
            }
            else if (parallel == true)
            {
                string r1="";
                string r2="";
                Parallel.Invoke(()=>
                             {
                                 r1=encriptarCesar(mitad1,salto);
                             }, //close second Action

                             () =>
                             {
                                 r2=encriptarCesar(mitad2,salto);
                             } //close third Action
                         ); //close parallel.invoke

                encriptado = r1 + r2;
                
            }

            return encriptado;
        }

    }
}
