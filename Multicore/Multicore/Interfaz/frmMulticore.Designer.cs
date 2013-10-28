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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.archivoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.opcionesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.normalToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.paraleloToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.salirToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.eToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.análisisDeTextoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.encriptaciónDeTextoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.metodo1ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.metodo2ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ordenamientoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.factorizaciónToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.temaLibreToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pnlAnalisis = new System.Windows.Forms.Panel();
            this.pnlEncriptacion = new System.Windows.Forms.Panel();
            this.rest = new System.Windows.Forms.TextBox();
            this.Msg = new System.Windows.Forms.TextBox();
            this.btnEncriptar = new System.Windows.Forms.Button();
            this.btnAnalisis = new System.Windows.Forms.Button();
            this.txtTexto = new System.Windows.Forms.TextBox();
            this.pnlAcciones = new System.Windows.Forms.Panel();
            this.txtPalabrasComunes = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.lblCaracteres = new System.Windows.Forms.Label();
            this.lblPalabras = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblIdioma = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.menuStrip1.SuspendLayout();
            this.pnlAnalisis.SuspendLayout();
            this.pnlEncriptacion.SuspendLayout();
            this.pnlAcciones.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.archivoToolStripMenuItem,
            this.eToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(598, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // archivoToolStripMenuItem
            // 
            this.archivoToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.opcionesToolStripMenuItem,
            this.salirToolStripMenuItem});
            this.archivoToolStripMenuItem.Name = "archivoToolStripMenuItem";
            this.archivoToolStripMenuItem.Size = new System.Drawing.Size(60, 20);
            this.archivoToolStripMenuItem.Text = "Archivo";
            // 
            // opcionesToolStripMenuItem
            // 
            this.opcionesToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.normalToolStripMenuItem,
            this.paraleloToolStripMenuItem});
            this.opcionesToolStripMenuItem.Name = "opcionesToolStripMenuItem";
            this.opcionesToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.opcionesToolStripMenuItem.Text = "Opciones";
            // 
            // normalToolStripMenuItem
            // 
            this.normalToolStripMenuItem.Name = "normalToolStripMenuItem";
            this.normalToolStripMenuItem.Size = new System.Drawing.Size(116, 22);
            this.normalToolStripMenuItem.Text = "Normal";
            // 
            // paraleloToolStripMenuItem
            // 
            this.paraleloToolStripMenuItem.Name = "paraleloToolStripMenuItem";
            this.paraleloToolStripMenuItem.Size = new System.Drawing.Size(116, 22);
            this.paraleloToolStripMenuItem.Text = "Paralelo";
            // 
            // salirToolStripMenuItem
            // 
            this.salirToolStripMenuItem.Name = "salirToolStripMenuItem";
            this.salirToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.salirToolStripMenuItem.Text = "Salir";
            // 
            // eToolStripMenuItem
            // 
            this.eToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.análisisDeTextoToolStripMenuItem,
            this.encriptaciónDeTextoToolStripMenuItem,
            this.ordenamientoToolStripMenuItem,
            this.factorizaciónToolStripMenuItem,
            this.temaLibreToolStripMenuItem});
            this.eToolStripMenuItem.Name = "eToolStripMenuItem";
            this.eToolStripMenuItem.Size = new System.Drawing.Size(53, 20);
            this.eToolStripMenuItem.Text = "Tareas";
            // 
            // análisisDeTextoToolStripMenuItem
            // 
            this.análisisDeTextoToolStripMenuItem.Name = "análisisDeTextoToolStripMenuItem";
            this.análisisDeTextoToolStripMenuItem.Size = new System.Drawing.Size(188, 22);
            this.análisisDeTextoToolStripMenuItem.Text = "Análisis de Texto";
            // 
            // encriptaciónDeTextoToolStripMenuItem
            // 
            this.encriptaciónDeTextoToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.metodo1ToolStripMenuItem,
            this.metodo2ToolStripMenuItem});
            this.encriptaciónDeTextoToolStripMenuItem.Name = "encriptaciónDeTextoToolStripMenuItem";
            this.encriptaciónDeTextoToolStripMenuItem.Size = new System.Drawing.Size(188, 22);
            this.encriptaciónDeTextoToolStripMenuItem.Text = "Encriptación de Texto";
            // 
            // metodo1ToolStripMenuItem
            // 
            this.metodo1ToolStripMenuItem.Name = "metodo1ToolStripMenuItem";
            this.metodo1ToolStripMenuItem.Size = new System.Drawing.Size(125, 22);
            this.metodo1ToolStripMenuItem.Text = "Metodo 1";
            // 
            // metodo2ToolStripMenuItem
            // 
            this.metodo2ToolStripMenuItem.Name = "metodo2ToolStripMenuItem";
            this.metodo2ToolStripMenuItem.Size = new System.Drawing.Size(125, 22);
            this.metodo2ToolStripMenuItem.Text = "Metodo 2";
            // 
            // ordenamientoToolStripMenuItem
            // 
            this.ordenamientoToolStripMenuItem.Name = "ordenamientoToolStripMenuItem";
            this.ordenamientoToolStripMenuItem.Size = new System.Drawing.Size(188, 22);
            this.ordenamientoToolStripMenuItem.Text = "Ordenamiento";
            // 
            // factorizaciónToolStripMenuItem
            // 
            this.factorizaciónToolStripMenuItem.Name = "factorizaciónToolStripMenuItem";
            this.factorizaciónToolStripMenuItem.Size = new System.Drawing.Size(188, 22);
            this.factorizaciónToolStripMenuItem.Text = "Factorización";
            // 
            // temaLibreToolStripMenuItem
            // 
            this.temaLibreToolStripMenuItem.Name = "temaLibreToolStripMenuItem";
            this.temaLibreToolStripMenuItem.Size = new System.Drawing.Size(188, 22);
            this.temaLibreToolStripMenuItem.Text = "Tema Libre";
            // 
            // pnlAnalisis
            // 
            this.pnlAnalisis.Controls.Add(this.btnAnalisis);
            this.pnlAnalisis.Controls.Add(this.txtTexto);
            this.pnlAnalisis.Controls.Add(this.pnlAcciones);
            this.pnlAnalisis.Controls.Add(this.pnlEncriptacion);
            this.pnlAnalisis.Location = new System.Drawing.Point(0, 27);
            this.pnlAnalisis.Name = "pnlAnalisis";
            this.pnlAnalisis.Size = new System.Drawing.Size(597, 400);
            this.pnlAnalisis.TabIndex = 1;
            // 
            // pnlEncriptacion
            // 
            this.pnlEncriptacion.Controls.Add(this.btnEncriptar);
            this.pnlEncriptacion.Location = new System.Drawing.Point(1, 26);
            this.pnlEncriptacion.Name = "pnlEncriptacion";
            this.pnlEncriptacion.Size = new System.Drawing.Size(597, 400);
            this.pnlEncriptacion.TabIndex = 2;
            // 
            // rest
            // 
            this.rest.Location = new System.Drawing.Point(12, 104);
            this.rest.Multiline = true;
            this.rest.Name = "rest";
            this.rest.Size = new System.Drawing.Size(408, 101);
            this.rest.TabIndex = 5;
            // 
            // Msg
            // 
            this.Msg.Location = new System.Drawing.Point(11, 16);
            this.Msg.Name = "Msg";
            this.Msg.Size = new System.Drawing.Size(100, 20);
            this.Msg.TabIndex = 4;
            // 
            // btnEncriptar
            // 
            this.btnEncriptar.Location = new System.Drawing.Point(11, 15);
            this.btnEncriptar.Name = "btnEncriptar";
            this.btnEncriptar.Size = new System.Drawing.Size(75, 23);
            this.btnEncriptar.TabIndex = 3;
            this.btnEncriptar.Text = "button1";
            this.btnEncriptar.UseVisualStyleBackColor = true;
            this.btnEncriptar.Click += new System.EventHandler(this.btnEncriptar_Click);
            // 
            // btnAnalisis
            // 
            this.btnAnalisis.Location = new System.Drawing.Point(510, 193);
            this.btnAnalisis.Name = "btnAnalisis";
            this.btnAnalisis.Size = new System.Drawing.Size(75, 23);
            this.btnAnalisis.TabIndex = 2;
            this.btnAnalisis.Text = "Analizar";
            this.btnAnalisis.UseVisualStyleBackColor = true;
            this.btnAnalisis.Click += new System.EventHandler(this.btnAnalisis_Click);
            // 
            // txtTexto
            // 
            this.txtTexto.Location = new System.Drawing.Point(12, 14);
            this.txtTexto.Multiline = true;
            this.txtTexto.Name = "txtTexto";
            this.txtTexto.Size = new System.Drawing.Size(573, 173);
            this.txtTexto.TabIndex = 1;
            // 
            // pnlAcciones
            // 
            this.pnlAcciones.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlAcciones.Controls.Add(this.txtPalabrasComunes);
            this.pnlAcciones.Controls.Add(this.label4);
            this.pnlAcciones.Controls.Add(this.lblCaracteres);
            this.pnlAcciones.Controls.Add(this.lblPalabras);
            this.pnlAcciones.Controls.Add(this.label3);
            this.pnlAcciones.Controls.Add(this.label2);
            this.pnlAcciones.Controls.Add(this.lblIdioma);
            this.pnlAcciones.Controls.Add(this.label1);
            this.pnlAcciones.Location = new System.Drawing.Point(12, 222);
            this.pnlAcciones.Name = "pnlAcciones";
            this.pnlAcciones.Size = new System.Drawing.Size(573, 166);
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
            // frmMulticore
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(598, 452);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.pnlAnalisis);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "frmMulticore";
            this.Text = "Ejecuciones Paralelas";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.pnlAnalisis.ResumeLayout(false);
            this.pnlAnalisis.PerformLayout();
            this.pnlEncriptacion.ResumeLayout(false);
            this.pnlAcciones.ResumeLayout(false);
            this.pnlAcciones.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem archivoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem salirToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem eToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem análisisDeTextoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem encriptaciónDeTextoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ordenamientoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem factorizaciónToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem temaLibreToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem metodo1ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem metodo2ToolStripMenuItem;
        private System.Windows.Forms.Panel pnlAnalisis;
        private System.Windows.Forms.TextBox txtTexto;
        private System.Windows.Forms.Panel pnlAcciones;
        private System.Windows.Forms.Label lblIdioma;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtPalabrasComunes;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblCaracteres;
        private System.Windows.Forms.Label lblPalabras;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ToolStripMenuItem opcionesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem normalToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem paraleloToolStripMenuItem;
        private System.Windows.Forms.Button btnAnalisis;
        private System.Windows.Forms.Panel pnlEncriptacion;
        private System.Windows.Forms.Button btnEncriptar;
    }
}

