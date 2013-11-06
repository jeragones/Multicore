using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace Multicore.Negocio
{
    class clsArchivo
    {
        /// <summary>
        /// Carga archivos de texto del explorador
        /// </summary>
        /// <param name="_sPath">Direccion del archivo</param>
        /// <returns>Informacion que contiene el archivo</returns>
        public List<string> cargarArchivo()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Archivos txt|*.txt";
            openFileDialog.Title = "Cargar Archivo";
            openFileDialog.ShowDialog();
            if (openFileDialog.FileName != "")
            {
                StreamReader srFile = new StreamReader(openFileDialog.OpenFile(), Encoding.UTF8);
                string sTexto;
                List<string> lsTexto = new List<string>();
                while ((sTexto = srFile.ReadLine()) != null)
                    lsTexto.Add(sTexto); 
                srFile.Close();
                return lsTexto;
            }
            return null;
        }

        public string cargarArchivo(string _sPath)
        {
            StreamReader srFile = new StreamReader(_sPath, Encoding.UTF8);
            string sTexto = srFile.ReadToEnd();
            srFile.Close();
            return sTexto;   
        }



        public void guardarArchivo(StringBuilder _sTexto)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Archivos txt|*.txt";
            saveFileDialog.Title = "Guardar Archivo";
            saveFileDialog.ShowDialog();

            if (saveFileDialog.FileName != "")
            {
                StreamWriter sw = new StreamWriter(saveFileDialog.OpenFile());
                sw.WriteLine(_sTexto);
                sw.Close(); 
            }
        }
    }
}
