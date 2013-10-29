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
        /// Metodo de cifrado por sustitucion
        /// </summary>
        /// <param name="texto"> Texto que se desea encriptar </param>
        /// <param name="salto"> Numero de saltos entre los caragteres de la tabla ascii</param>
        /// <param name="parallel"> Boleano que indica la manera en que se ejecutara el ciclo, true para paralelo y false para secuencial</param>
        /// <returns>Retorna un arreglo de 2 posiciones, en la posicion 0 se encuentra el tecto encriptado y en la posicion 1 el tiempo de ejecucion</returns>
        public static object[] encriptarCesar(string texto,int salto,bool parallel)
        {
            // arreglo de objetos para guardar el mensaje encriptado y el tiempo de ejecucion
            object[] resultado = new object[2];
           
            //texto encriptado
            string encriptado="";
            //valor ascii de la letra a cambiar
            int letra;

            if (parallel == false)
            {
                //variable que medira el tiempo de ejecucion
                var timer = Stopwatch.StartNew();
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
                timer.Stop();
                resultado[0] = encriptado;
                resultado[1] = timer.Elapsed;
            }
            else if(parallel==true)
            {
                //variable que medira el tiempo de ejecucion
                var timer = Stopwatch.StartNew();
                Parallel.For(0, texto.Count(), (i) =>
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
                });
                
                timer.Stop();
                resultado[0] = encriptado;
                resultado[1] = timer.Elapsed;
            }
            return resultado;
        }
    }
}
