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
        /// Metodo de cifrado por sustitucion secuencial(Cifrado Cesar)
        /// </summary>
        /// <param name="texto"> Texto que se desea encriptar </param>
        /// <param name="salto"> Numero de saltos entre los caragteres de la tabla ascii</param>
        /// <returns>Retorna el mensaje encriptado</returns>
        public static string encriptarCesar_Secuencial(string texto,int salto)
        {
            //texto encriptado
            string encriptado="";
            //valor ascii de la letra a cambiar
            int letra;
            for (int i = 0; i < texto.Length; i++)
            {
                int carac=Convert.ToInt32(texto[i]);
                if (carac > 64 && carac < 91 || carac > 96 && carac < 123)
                {
                    //obtiene el valor ascii
                    letra = Convert.ToInt32(texto[i]) + salto;
                    //pasa de Z a A 
                    if (letra > 90 && letra <90 ||letra > 122)
                    {
                        letra = letra - 26;
                    }
                    //convieerte el valor ascii a caracter
                    encriptado += Convert.ToChar(letra);
                }
                else
                {
                    encriptado += Convert.ToChar(carac);
                }

            }
            return encriptado;
        }

        /// <summary>
        /// Metodo de cifrado por sustitucion Paralelo(Cifrado Cesar)
        /// </summary>
        /// <param name="texto"> Texto que se desea encriptar </param>
        /// <param name="salto"> Numero de saltos entre los caragteres de la tabla ascii</param>
        /// <returns>Retorna el mensaje encriptado</returns>
        public static string encriptarCesar_Parallel(string texto, int salto)
        {
            //texto encriptado
            string encriptado = "";
            //valor ascii de la letra a cambiar
            int letra;
            Parallel.For(0, texto.Length, i =>
            {
                int carac = Convert.ToInt32(texto[i]);
                if (carac > 64 && carac < 91 || carac > 96 && carac < 123)
                {
                    //obtiene el valor ascii
                    letra = Convert.ToInt32(texto[i]) + salto;
                    //pasa de Z a A 
                    if (letra > 90 && letra < 97 || letra > 122)
                    {
                        letra = letra - 26;
                    }
                    //convieerte el valor ascii a caracter
                    encriptado += Convert.ToChar(letra);
                }
                else
                {
                    encriptado += Convert.ToChar(carac);
                }
                
            });
          
            return encriptado;
        }

        /// <summary>
        /// Metodo que encripta un texto por medio del cifrado cesar, se ejecuta de forma paralela o secuancial esto depende de una bandera que indica el modo de ejecucion
        /// </summary>
        /// <param name="texto">Texto que sera encriptado</param>
        /// <param name="salto">Numero de saltos que se necesitan para encriptar</param>
        /// <param name="parallel">Bandera que indica si la encriptacion sera de forma paralela o secuencial</param>
        /// <returns>Retorna el mensaje encriptado</returns>
        public static string encriptCesar(string texto, int salto, bool parallel)
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
                encriptado=encriptarCesar_Secuencial(texto, salto);
                
            }
            else if (parallel == true)
            {
                string r1="";
                string r2="";
                Parallel.Invoke(()=>
                             {
                                 r1=encriptarCesar_Parallel(mitad1,salto);
                             }, //close second Action

                             () =>
                             {
                                 r2=encriptarCesar_Parallel(mitad2,salto);
                             } //close third Action
                         ); //close parallel.invoke

                encriptado = r1 + r2;
                
            }

            return encriptado;
        }


        /// <summary>
        /// Metodo que desencripta un texto de manera Paralela
        /// </summary>
        /// <param name="encriptado">Mensaje encriptado</param>
        /// <param name="salto">Nuemro de saltos necesarios para desencriptar el mensaje</param>
        /// <returns>Retorna el mensaje original</returns>
        public static string desencriptarCesar_Parallel(string encriptado, int salto)
        {
            //texto encriptado
            string desencriptado = "";
            //valor ascii de la letra a cambiar
            int letra;
            Parallel.For(0, encriptado.Length, i =>
            {
                //obtiene el valor ascii
                letra = Convert.ToInt32(encriptado[i]) - salto;
                //pasa de Z a A 
                if (letra < 41)
                {
                    letra = letra + 26;
                }
                //convieerte el valor ascii a caracter
                desencriptado += Convert.ToChar(letra);
            });

            return desencriptado;
        }

        /// <summary>
        /// Metodo que desencripta un texto de manera secuencial
        /// </summary>
        /// <param name="encriptado">Mensaje encriptado</param>
        /// <param name="salto">Nuemro de saltos necesarios para desencriptar el mensaje</param>
        /// <returns>Retorna el mensaje original</returns>
        public static string desencriptarCesar_Secuencial(string encriptado, int salto)
        {
            //texto encriptado
            string desencriptado = "";
            //valor ascii de la letra a cambiar
            int letra;
            for (int i = 0; i < encriptado.Length; i++)
            {
                //obtiene el valor ascii
                letra = Convert.ToInt32(encriptado[i]) - salto;
                //pasa de Z a A 
                if (letra < 41)
                {
                    letra = letra + 26;
                }
                //convieerte el valor ascii a caracter
                desencriptado += Convert.ToChar(letra);
            }
            return desencriptado;
        }


        /// <summary>
        /// Metodo que desencripta un texto por medio del cifrado cesar, se ejecuta de forma paralela o secuancial esto depende de una bandera que indica el modo de ejecucion
        /// </summary>
        /// <param name="encriptado">Texto que sera encriptado</param>
        /// <param name="salto">Numero de saltos que se necesitan para desencriptar</param>
        /// <param name="parallel">Bandera que indica si la desencriptacion sera de forma paralela o secuencial</param>
        /// <returns>Retorna el mensaje original desencriptado</returns>
        public static string desencriptCesar(string encriptado, int salto, bool parallel)
        {
            string desencriptado = "";

            string mitad1;
            string mitad2;
            int largoTexto = encriptado.Count();
            int mitad = largoTexto / 2;
            mitad1 = encriptado.Substring(0, mitad);
            mitad2 = encriptado.Substring(mitad, mitad);
            //texto encriptado
          
            //valor ascii de la letra a cambiar
            if (parallel == false)
            {
                desencriptado = desencriptarCesar_Secuencial(encriptado, salto);

            }
            else if (parallel == true)
            {
                string r1 = "";
                string r2 = "";
                Parallel.Invoke(() =>
                    {
                        r1 = desencriptarCesar_Parallel(mitad1, salto);
                    }, //close second Action

                    () =>
                    {
                        r2 = desencriptarCesar_Parallel(mitad2, salto);
                    } //close third Action
                ); //close parallel.invoke

                encriptado = r1 + r2;
               
            }


            return desencriptado;

        }


        


        // 65 = A
        // 90 =Z
        // 97 =a
        //122 = z
        
        //arreglar problema con Z
        public static string desencriptarCesar_prueba(string texto, int salto)
        {

            string encriptado = "";

            int letra = 0;
            for (int i = 0; i < texto.Length; i++)
            {
                int carac = Convert.ToInt32(texto[i]);
                if (carac > 64 && carac < 91 || carac > 96 && carac < 123)
                {
                    letra = Convert.ToInt32(texto[i]) - salto;
                                       
                    if (letra < 65 ||letra > 90 && letra < 97 || letra > 122)
                    {
                        letra = letra + 26;
                    }

                    encriptado += Convert.ToChar(letra);
                }
                else
                {
                    encriptado += Convert.ToChar(carac);
                }
            }
            return encriptado;
        }







    }
}
