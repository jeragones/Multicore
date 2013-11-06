using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Multicore.Negocio
{
    class clsNumero
    {
        /// <summary>
        /// Identifica los numeros primos por los que esta compuesto el numero
        /// </summary>
        /// <param name="_iNumero">Numero que se evalua</param>
        /// <returns>Retorna los numeros primos por los que esta compuesto el numero evaluado</returns>
        public StringBuilder numerosPrimos(int _iNumero, bool _bConcurrencia)
        {
            StringBuilder sbTexto = new StringBuilder();
            int iNumero = 2;

            while (_iNumero > 1)
            {
                if (primo(iNumero) && (_iNumero % iNumero) == 0)
                {
                    _iNumero = _iNumero / iNumero;
                    if (sbTexto.Length > 0)
                        sbTexto.Append(", ");
                    sbTexto.Append(iNumero.ToString());
                }
                else
                    iNumero++;
            }
            return sbTexto;
        }

        /// <summary>
        /// identifica si un numero es primo o no
        /// </summary>
        /// <param name="_iNumero">Numero que se evalua</param>
        /// <returns>Retorna tru si el numero efectivamente es primo</returns>
        private bool primo(int _iNumero) 
        {
            int iContador = 0;
            
            for (int i = 1; i <= _iNumero; i++) 
            {
                if ((_iNumero % i) == 0)
                    iContador++;
            }

            if (iContador == 2)
                return true;
            else
                return false;
        }
    }
}
