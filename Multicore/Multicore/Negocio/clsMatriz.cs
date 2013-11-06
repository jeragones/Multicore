using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Multicore.Negocio
{
    class clsMatriz
    {
        int[,] miMatriz1 = null;
        int[,] miMatriz2 = null;

        public object separar(string _aoOperacion, char[] _acOperandos, int _iColumna) 
        {
            List<object> lsOperacion = new List<object>();
            object[] aoOperacion = null;
            if (_iColumna == 4)
                return _aoOperacion;
            else
                aoOperacion = _aoOperacion.Split(new char[] { _acOperandos[_iColumna] });
            if (aoOperacion.Length == 1)
            {
                lsOperacion.Add(separar((string)aoOperacion[0], _acOperandos, _iColumna + 1));
                if (lsOperacion.Count == 1)
                    return lsOperacion[0];
                else
                    return lsOperacion;
            }
            else 
            {
                for (int i = 0; i < aoOperacion.Length; i++)
                {
                    if (i == aoOperacion.Length - 1)
                        lsOperacion.Add(separar((string)aoOperacion[i], _acOperandos, _iColumna + 1));
                    else
                    {
                        lsOperacion.Add(separar((string)aoOperacion[i], _acOperandos, _iColumna + 1));
                        lsOperacion.Add(_acOperandos[_iColumna].ToString());
                    }
                }
            }
            if (lsOperacion.Count == 1)
                return lsOperacion[0];
            else
                return lsOperacion;
        }

        public int[,] operacionMatriz(string _sOperacion, int _iX, int _iY, bool _bConcurrencia) 
        {
            object oOperacion = separar(_sOperacion, new char[] { '-', '+', '/', '*' }, 0);
            int[,] miResultado = operacionMatriz(oOperacion,_iX,_iY,_bConcurrencia);
            return miResultado;
        }

        private int[,] operacionMatriz(string _sMatriz1, string _sOperador, string _sMatriz2, int _iX, int _iY) 
        {
            int[,] miResultado = new int[_iX, _iY];
            switch (_sOperador) 
            {
                case "+":
                    for (int x = 0; x < _iX; x++) 
                    {
                        for (int y = 0; y < _iY; y++)
                            miResultado[x, y] = miMatriz1[x, y] + miMatriz2[x, y];
                    }
                    break;
                case "-":
                    for (int x = 0; x < _iX; x++)
                    {
                        for (int y = 0; y < _iY; y++) 
                        {
                            if(_sMatriz1.Equals("m1"))
                                miResultado[x, y] = miMatriz1[x, y] - miMatriz2[x, y];
                            else
                                miResultado[x, y] = miMatriz2[x, y] + miMatriz1[x, y];
                        }
                    }
                    break;
                case "*":
                    for (int y = 0; y < _iY; y++)
                    {
                        for (int x = 0; x < _iX; x++)
                        {
                            if (_sMatriz1.Equals("m1"))
                            {

                            }
                        }
                        
                    }
                    break;
                case "/":
                    break;
            }
            if (_sMatriz1.Equals("m1")) 
            {
            }
            return miResultado;
        }

        public int[,] operacionMatriz(object _oOperacion, int _iX, int _iY, bool _bConcurrencia) 
        {
            int[,] miResultado = new int[_iX, _iY];
            crearMatriz(_iX, _iY, _bConcurrencia);
            bool bTmp = true;

            //class Animal { } 
            //class Dog : Animal { }

            //void PrintTypes(Animal a) { 
            //    print(a.GetType() == typeof(Animal)) // false 
            //    print(a is Animal)                   // true 
            //    print(a.GetType() == typeof(Dog))    // true
            //}

            //Dog spot = new Dog(); 
            //PrintTypes(spot);


            foreach (object oTmp in (List<object>)_oOperacion) 
            {
                if (oTmp.GetType() != typeof(string))
                    bTmp = false;
            }
            if (bTmp)
                miResultado = operacionMatriz((string)((List<object>)_oOperacion)[0], (string)((List<object>)_oOperacion)[1], (string)((List<object>)_oOperacion)[2], _iX, _iY);
            else 
            {
                miResultado = operacionMatriz(((List<object>)_oOperacion)[2], _iX, _iY, _bConcurrencia);
            }
            //int loOperacion = ((List<object>)oOperacion).Count;


            return miResultado;
        }

        private void crearMatriz(int _iX, int _iY, bool _bConcurrencia) 
        {
            miMatriz1 = new int[_iX, _iY];
            miMatriz2 = new int[_iX, _iY];

            if (_bConcurrencia)
            {
                Parallel.Invoke(
                    () => { llenarMatriz(miMatriz1, _iX, _iY); },
                    () => { llenarMatriz(miMatriz2, _iX, _iY); }
                );
            }
            else 
            {
                llenarMatriz(miMatriz1, _iX, _iY);
                llenarMatriz(miMatriz2, _iX, _iY);
            }
        }

        static private void llenarMatriz(int[,] _miMatriz, int _iX, int _iY) 
        {
            Random rValor = new Random();

            for (int x = 0; x < _iX; x++)
            {
                for (int y = 0; y < _iY; y++)
                    _miMatriz[x, y] = rValor.Next(0,100);
            }
        }
    }
}
