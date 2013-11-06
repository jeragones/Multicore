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
    public class clsEncriptarCesar
    {   
        /// <summary>
        /// Metodo de cifrado por sustitucion secuencial(Cifrado Cesar)
        /// </summary>
        /// <param name="texto"> Texto que se desea encriptar </param>
        /// <param name="salto"> Numero de saltos entre los caragteres de la tabla ascii</param>
        /// <returns>Retorna el mensaje encriptado</returns>
        public static string encriptarCesar_Secuencial(string _sTexto,int _iSalto)
        {
            //texto encriptado
            string encriptado="";
            //valor ascii de la letra a cambiar
            int letra;
            for (int i = 0; i < _sTexto.Length; i++)
            {
                int carac = Convert.ToInt32(_sTexto[i]);
                if (carac > 64 && carac < 91 || carac > 96 && carac < 123)
                {
                    //obtiene el valor ascii
                    letra = Convert.ToInt32(_sTexto[i]) + _iSalto;
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
        /// <param name="_sTexto"> Texto que se desea encriptar </param>
        /// <param name="_iSalto"> Numero de saltos entre los caragteres de la tabla ascii</param>
        /// <returns>Retorna el mensaje encriptado</returns>
        public static string encriptarCesar_Parallel(string _sTexto, int _iSalto)
        {
            //texto encriptado
            string encriptado = "";
            //valor ascii de la letra a cambiar
            int letra;
            Parallel.For(0, _sTexto.Length, i =>
            {
                int carac = Convert.ToInt32(_sTexto[i]);
                if (carac > 64 && carac < 91 || carac > 96 && carac < 123)
                {
                    //obtiene el valor ascii
                    letra = Convert.ToInt32(_sTexto[i]) + _iSalto;
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
        /// <param name="_sTexto">Texto que sera encriptado</param>
        /// <param name="_iSalto">Numero de saltos que se necesitan para encriptar</param>
        /// <param name="_bParallel">Bandera que indica si la encriptacion sera de forma paralela o secuencial</param>
        /// <returns>Retorna el mensaje encriptado</returns>
        public static string encriptCesar(string _sTexto, int _iSalto, bool _bParallel)
        {
            string sMitad1;
            string sMitad2;
            int largoTexto = _sTexto.Count();
            int iMitad = largoTexto / 2;
            sMitad1 = _sTexto.Substring(0,iMitad);
            sMitad2 = _sTexto.Substring(iMitad,iMitad);
            //texto encriptado
            string sEncriptado = "";
            //valor ascii de la letra a cambiar
            if (_bParallel == false)
            {
                sEncriptado=encriptarCesar_Secuencial(_sTexto, _iSalto);
                
            }
            else if (_bParallel == true)
            {
                string sResult1="";
                string sResult2="";
                Parallel.Invoke(()=>
                             {
                                 sResult1=encriptarCesar_Parallel(sMitad1,_iSalto);
                             }, //close second Action

                             () =>
                             {
                                 sResult2=encriptarCesar_Parallel(sMitad2,_iSalto);
                             } //close third Action
                         ); //close parallel.invoke

                sEncriptado = sResult1 + sResult2;
                
            }

            return sEncriptado;
        }


        /// <summary>
        /// Metodo que desencripta un texto de manera Paralela
        /// </summary>
        /// <param name="_sEncriptado">Mensaje encriptado</param>
        /// <param name="_iSalto">Nuemro de saltos necesarios para desencriptar el mensaje</param>
        /// <returns>Retorna el mensaje original</returns>
        public static string desencriptarCesar_Parallel(string _sEncriptado, int _iSalto)
        {
            //texto encriptado
            string s_Desencriptado = "";
            //valor ascii de la letra a cambiar
            int _iLetra;
            Parallel.For(0, _sEncriptado.Length, i =>
            {
                int carac = Convert.ToInt32(_sEncriptado[i]);
                if (carac > 64 && carac < 91 || carac > 96 && carac < 123)
                {
                    //obtiene el valor ascii
                    _iLetra = Convert.ToInt32(_sEncriptado[i]) - _iSalto;
                    //pasa de Z a A y viceversa
                    if (_iLetra < 65 || _iLetra > 90 && _iLetra < 97 || _iLetra > 122)
                    {
                        _iLetra = _iLetra + 26;
                    }

                    //convieerte el valor ascii a caracter
                    s_Desencriptado += Convert.ToChar(_iLetra);
                }
                else
                {
                    _sEncriptado += Convert.ToChar(carac);
                }

            });

            return s_Desencriptado;
        }

        /// <summary>
        /// Metodo que desencripta un texto de manera secuencial
        /// </summary>
        /// <param name="_sEncriptado">Mensaje encriptado</param>
        /// <param name="_iSalto">Nuemro de saltos necesarios para desencriptar el mensaje</param>
        /// <returns>Retorna el mensaje original</returns>
        public static string desencriptarCesar_Secuencial(string _sEncriptado, int _iSalto)
        {
            //texto encriptado
            string desencriptado = "";
            //valor ascii de la letra a cambiar
            int letra;
            for (int i = 0; i < _sEncriptado.Length; i++)
            {
                int carac = Convert.ToInt32(_sEncriptado[i]);
                if (carac > 64 && carac < 91 || carac > 96 && carac < 123)
                {
                    //obtiene el valor ascii
                    letra = Convert.ToInt32(_sEncriptado[i]) - _iSalto;
                    //pasa de Z a A 
                    if (letra < 41)
                    {
                        letra = letra + 26;
                    }
                    //convieerte el valor ascii a caracter
                    desencriptado += Convert.ToChar(letra);
                }
                else
                {
                    desencriptado += Convert.ToChar(carac);
                }
            }
            return desencriptado;
        }


        /// <summary>
        /// Metodo que desencripta un texto por medio del cifrado cesar, se ejecuta de forma paralela o secuancial esto depende de una bandera que indica el modo de ejecucion
        /// </summary>
        /// <param name="_sEncriptado">Texto que sera encriptado</param>
        /// <param name="_iSalto">Numero de saltos que se necesitan para desencriptar</param>
        /// <param name="_iParallel">Bandera que indica si la desencriptacion sera de forma paralela o secuencial</param>
        /// <returns>Retorna el mensaje original desencriptado</returns>
        public static string desencriptCesar(string _sEncriptado, int _iSalto, bool _iParallel)
        {
            string desencriptado = "";

            string mitad1;
            string mitad2;
            int largoTexto = _sEncriptado.Count();
            int mitad = largoTexto / 2;
            mitad1 = _sEncriptado.Substring(0, mitad);
            mitad2 = _sEncriptado.Substring(mitad, mitad);
            //texto encriptado
          
            //valor ascii de la letra a cambiar
            if (_iParallel == false)
            {
                desencriptado = desencriptarCesar_Secuencial(_sEncriptado, _iSalto);

            }
            else if (_iParallel == true)
            {
                string r1 = "";
                string r2 = "";
                Parallel.Invoke(() =>
                    {
                        r1 = desencriptarCesar_Parallel(mitad1, _iSalto);
                    }, //close second Action

                    () =>
                    {
                        r2 = desencriptarCesar_Parallel(mitad2, _iSalto);
                    } //close third Action
                ); //close parallel.invoke

                _sEncriptado = r1 + r2;
               
            }


            return desencriptado;

        }
    }
}
