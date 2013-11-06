namespace Multicore
{
    partial class frmMulticore
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnAnalisis = new System.Windows.Forms.Button();
            this.pnlAcciones = new System.Windows.Forms.Panel();
            this.txtPalabrasComunes = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.lblCaracteres = new System.Windows.Forms.Label();
            this.lblPalabras = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblIdioma = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.pnlEncriptacio = new System.Windows.Forms.Panel();
            this.desencriptar = new System.Windows.Forms.Button();
            this.cargarClave = new System.Windows.Forms.Button();
            this.btnEncriptar = new System.Windows.Forms.Button();
            this.encriptarXOR = new System.Windows.Forms.Button();
            this.btnDesencriptar = new System.Windows.Forms.Button();
            this.checkParallel = new System.Windows.Forms.CheckBox();
            this.labelresultado = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.lblNumeros = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.txtNumero = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.btnNumeros = new System.Windows.Forms.Button();
            this.chkOrden = new System.Windows.Forms.CheckBox();
            this.btnMergeSort = new System.Windows.Forms.Button();
            this.btnQuickSort = new System.Windows.Forms.Button();
            this.lblNumero = new System.Windows.Forms.Label();
            this.pnlAcciones.SuspendLayout();
            this.pnlEncriptacio.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnAnalisis
            // 
            this.btnAnalisis.Location = new System.Drawing.Point(493, 126);
            this.btnAnalisis.Name = "btnAnalisis";
            this.btnAnalisis.Size = new System.Drawing.Size(75, 23);
            this.btnAnalisis.TabIndex = 2;
            this.btnAnalisis.Text = "Analizar";
            this.btnAnalisis.UseVisualStyleBackColor = true;
            this.btnAnalisis.Click += new System.EventHandler(this.btnAnalisis_Click);
            // 
            // pnlAcciones
            // 
            this.pnlAcciones.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlAcciones.Controls.Add(this.btnAnalisis);
            this.pnlAcciones.Controls.Add(this.txtPalabrasComunes);
            this.pnlAcciones.Controls.Add(this.label4);
            this.pnlAcciones.Controls.Add(this.lblCaracteres);
            this.pnlAcciones.Controls.Add(this.lblPalabras);
            this.pnlAcciones.Controls.Add(this.label3);
            this.pnlAcciones.Controls.Add(this.label2);
            this.pnlAcciones.Controls.Add(this.lblIdioma);
            this.pnlAcciones.Controls.Add(this.label1);
            this.pnlAcciones.Location = new System.Drawing.Point(12, 12);
            this.pnlAcciones.Name = "pnlAcciones";
            this.pnlAcciones.Size = new System.Drawing.Size(573, 154);
            this.pnlAcciones.TabIndex = 0;
            // 
            // txtPalabrasComunes
            // 
            this.txtPalabrasComunes.BackColor = System.Drawing.SystemColors.Control;
            this.txtPalabrasComunes.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtPalabrasComunes.Location = new System.Drawing.Point(145, 87);
            this.txtPalabrasComunes.Multiline = true;
            this.txtPalabrasComunes.Name = "txtPalabrasComunes";
            this.txtPalabrasComunes.Size = new System.Drawing.Size(412, 66);
            this.txtPalabrasComunes.TabIndex = 7;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.label4.Location = new System.Drawing.Point(12, 87);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(97, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Palabras comunes:";
            // 
            // lblCaracteres
            // 
            this.lblCaracteres.AutoSize = true;
            this.lblCaracteres.Location = new System.Drawing.Point(142, 62);
            this.lblCaracteres.Name = "lblCaracteres";
            this.lblCaracteres.Size = new System.Drawing.Size(31, 13);
            this.lblCaracteres.TabIndex = 5;
            this.lblCaracteres.Text = "        \r\n";
            // 
            // lblPalabras
            // 
            this.lblPalabras.AutoSize = true;
            this.lblPalabras.Location = new System.Drawing.Point(142, 36);
            this.lblPalabras.Name = "lblPalabras";
            this.lblPalabras.Size = new System.Drawing.Size(31, 13);
            this.lblPalabras.TabIndex = 4;
            this.lblPalabras.Text = "        \r\n";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.label3.Location = new System.Drawing.Point(12, 62);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(120, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "Cantidad de caracteres:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.label2.Location = new System.Drawing.Point(12, 36);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(110, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Cantidad de palabras:";
            // 
            // lblIdioma
            // 
            this.lblIdioma.AutoSize = true;
            this.lblIdioma.Location = new System.Drawing.Point(142, 11);
            this.lblIdioma.Name = "lblIdioma";
            this.lblIdioma.Size = new System.Drawing.Size(31, 13);
            this.lblIdioma.TabIndex = 1;
            this.lblIdioma.Text = "        \r\n";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.label1.Location = new System.Drawing.Point(12, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Idioma:";
            // 
            // pnlEncriptacio
            // 
            this.pnlEncriptacio.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlEncriptacio.Controls.Add(this.lblNumeros);
            this.pnlEncriptacio.Controls.Add(this.desencriptar);
            this.pnlEncriptacio.Controls.Add(this.cargarClave);
            this.pnlEncriptacio.Controls.Add(this.btnEncriptar);
            this.pnlEncriptacio.Controls.Add(this.encriptarXOR);
            this.pnlEncriptacio.Controls.Add(this.btnDesencriptar);
            this.pnlEncriptacio.Controls.Add(this.checkParallel);
            this.pnlEncriptacio.Controls.Add(this.labelresultado);
            this.pnlEncriptacio.Controls.Add(this.label6);
            this.pnlEncriptacio.Location = new System.Drawing.Point(12, 172);
            this.pnlEncriptacio.Name = "pnlEncriptacio";
            this.pnlEncriptacio.Size = new System.Drawing.Size(573, 136);
            this.pnlEncriptacio.TabIndex = 2;
            // 
            // desencriptar
            // 
            this.desencriptar.Location = new System.Drawing.Point(15, 93);
            this.desencriptar.Name = "desencriptar";
            this.desencriptar.Size = new System.Drawing.Size(107, 23);
            this.desencriptar.TabIndex = 14;
            this.desencriptar.Text = "Desencriptar XOR";
            this.desencriptar.UseVisualStyleBackColor = true;
            this.desencriptar.Click += new System.EventHandler(this.desencriptar_Click);
            // 
            // cargarClave
            // 
            this.cargarClave.Location = new System.Drawing.Point(15, 64);
            this.cargarClave.Name = "cargarClave";
            this.cargarClave.Size = new System.Drawing.Size(107, 23);
            this.cargarClave.TabIndex = 13;
            this.cargarClave.Text = "Cargar Clave";
            this.cargarClave.UseVisualStyleBackColor = true;
            this.cargarClave.Click += new System.EventHandler(this.encriptDesencript_Click);
            // 
            // btnEncriptar
            // 
            this.btnEncriptar.Location = new System.Drawing.Point(128, 35);
            this.btnEncriptar.Name = "btnEncriptar";
            this.btnEncriptar.Size = new System.Drawing.Size(106, 23);
            this.btnEncriptar.TabIndex = 3;
            this.btnEncriptar.Text = "Encriptar Cesar";
            this.btnEncriptar.UseVisualStyleBackColor = true;
            this.btnEncriptar.Click += new System.EventHandler(this.btnEncriptar_Click);
            // 
            // encriptarXOR
            // 
            this.encriptarXOR.Location = new System.Drawing.Point(15, 35);
            this.encriptarXOR.Name = "encriptarXOR";
            this.encriptarXOR.Size = new System.Drawing.Size(107, 23);
            this.encriptarXOR.TabIndex = 12;
            this.encriptarXOR.Text = "Encriptar XOR";
            this.encriptarXOR.UseVisualStyleBackColor = true;
            this.encriptarXOR.Click += new System.EventHandler(this.generarClave_Click);
            // 
            // btnDesencriptar
            // 
            this.btnDesencriptar.Location = new System.Drawing.Point(128, 65);
            this.btnDesencriptar.Name = "btnDesencriptar";
            this.btnDesencriptar.Size = new System.Drawing.Size(106, 23);
            this.btnDesencriptar.TabIndex = 11;
            this.btnDesencriptar.Text = "Desencriptar Cesar";
            this.btnDesencriptar.UseVisualStyleBackColor = true;
            this.btnDesencriptar.Click += new System.EventHandler(this.btnDesencriptar_Click);
            // 
            // checkParallel
            // 
            this.checkParallel.AutoSize = true;
            this.checkParallel.Location = new System.Drawing.Point(16, 12);
            this.checkParallel.Name = "checkParallel";
            this.checkParallel.Size = new System.Drawing.Size(60, 17);
            this.checkParallel.TabIndex = 10;
            this.checkParallel.Text = "Parallel";
            this.checkParallel.UseVisualStyleBackColor = true;
            // 
            // labelresultado
            // 
            this.labelresultado.AutoSize = true;
            this.labelresultado.Location = new System.Drawing.Point(287, 46);
            this.labelresultado.Name = "labelresultado";
            this.labelresultado.Size = new System.Drawing.Size(0, 13);
            this.labelresultado.TabIndex = 9;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(271, 12);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(116, 13);
            this.label6.TabIndex = 8;
            this.label6.Text = "Tiempo de Ejecucuión:";
            // 
            // lblNumeros
            // 
            this.lblNumeros.AutoSize = true;
            this.lblNumeros.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.lblNumeros.Location = new System.Drawing.Point(410, 65);
            this.lblNumeros.Name = "lblNumeros";
            this.lblNumeros.Size = new System.Drawing.Size(0, 13);
            this.lblNumeros.TabIndex = 3;
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.lblNumero);
            this.panel1.Controls.Add(this.txtNumero);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.btnNumeros);
            this.panel1.Controls.Add(this.chkOrden);
            this.panel1.Controls.Add(this.btnMergeSort);
            this.panel1.Controls.Add(this.btnQuickSort);
            this.panel1.Location = new System.Drawing.Point(12, 314);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(573, 100);
            this.panel1.TabIndex = 3;
            // 
            // txtNumero
            // 
            this.txtNumero.Location = new System.Drawing.Point(181, 68);
            this.txtNumero.Name = "txtNumero";
            this.txtNumero.Size = new System.Drawing.Size(100, 20);
            this.txtNumero.TabIndex = 27;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.label7.Location = new System.Drawing.Point(128, 71);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(47, 13);
            this.label7.TabIndex = 26;
            this.label7.Text = "Número:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.label5.Location = new System.Drawing.Point(128, 43);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(52, 13);
            this.label5.TabIndex = 21;
            this.label5.Text = "Números:";
            // 
            // btnNumeros
            // 
            this.btnNumeros.Location = new System.Drawing.Point(128, 14);
            this.btnNumeros.Name = "btnNumeros";
            this.btnNumeros.Size = new System.Drawing.Size(106, 23);
            this.btnNumeros.TabIndex = 25;
            this.btnNumeros.Text = "Numeros Primos";
            this.btnNumeros.UseVisualStyleBackColor = true;
            this.btnNumeros.Click += new System.EventHandler(this.btnNumeros_Click_1);
            // 
            // chkOrden
            // 
            this.chkOrden.AutoSize = true;
            this.chkOrden.Location = new System.Drawing.Point(16, 72);
            this.chkOrden.Name = "chkOrden";
            this.chkOrden.Size = new System.Drawing.Size(90, 17);
            this.chkOrden.TabIndex = 24;
            this.chkOrden.Text = "Descendente";
            this.chkOrden.UseVisualStyleBackColor = true;
            // 
            // btnMergeSort
            // 
            this.btnMergeSort.Location = new System.Drawing.Point(16, 43);
            this.btnMergeSort.Name = "btnMergeSort";
            this.btnMergeSort.Size = new System.Drawing.Size(106, 23);
            this.btnMergeSort.TabIndex = 23;
            this.btnMergeSort.Text = "Ordenar MergeSort";
            this.btnMergeSort.UseVisualStyleBackColor = true;
            this.btnMergeSort.Click += new System.EventHandler(this.btnMergeSort_Click_1);
            // 
            // btnQuickSort
            // 
            this.btnQuickSort.Location = new System.Drawing.Point(16, 14);
            this.btnQuickSort.Name = "btnQuickSort";
            this.btnQuickSort.Size = new System.Drawing.Size(106, 23);
            this.btnQuickSort.TabIndex = 22;
            this.btnQuickSort.Text = "Ordenar QuickSort";
            this.btnQuickSort.UseVisualStyleBackColor = true;
            this.btnQuickSort.Click += new System.EventHandler(this.btnQuickSort_Click_1);
            // 
            // lblNumero
            // 
            this.lblNumero.AutoSize = true;
            this.lblNumero.Location = new System.Drawing.Point(186, 43);
            this.lblNumero.Name = "lblNumero";
            this.lblNumero.Size = new System.Drawing.Size(0, 13);
            this.lblNumero.TabIndex = 28;
            // 
            // frmMulticore
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(594, 520);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.pnlEncriptacio);
            this.Controls.Add(this.pnlAcciones);
            this.Name = "frmMulticore";
            this.Text = "Ejecuciones Paralelas";
            this.pnlAcciones.ResumeLayout(false);
            this.pnlAcciones.PerformLayout();
            this.pnlEncriptacio.ResumeLayout(false);
            this.pnlEncriptacio.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnAnalisis;
        private System.Windows.Forms.Panel pnlEncriptacio;
        private System.Windows.Forms.Button btnEncriptar;
        private System.Windows.Forms.Label labelresultado;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btnDesencriptar;
        private System.Windows.Forms.CheckBox checkParallel;
        private System.Windows.Forms.Button cargarClave;
        private System.Windows.Forms.Button encriptarXOR;
        private System.Windows.Forms.Button desencriptar;
        private System.Windows.Forms.Panel pnlAcciones;
        private System.Windows.Forms.TextBox txtPalabrasComunes;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblCaracteres;
        private System.Windows.Forms.Label lblPalabras;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblIdioma;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblNumeros;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblNumero;
        private System.Windows.Forms.TextBox txtNumero;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnNumeros;
        private System.Windows.Forms.CheckBox chkOrden;
        private System.Windows.Forms.Button btnMergeSort;
        private System.Windows.Forms.Button btnQuickSort;
    }
}

