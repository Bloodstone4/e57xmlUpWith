namespace e57xmlUp
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.button1 = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.button2 = new System.Windows.Forms.Button();
            this.renameCB = new System.Windows.Forms.CheckBox();
            this.sortCB = new System.Windows.Forms.CheckBox();
            this.deleteFilesXML = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(67, 127);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(165, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "Выгрузить";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(67, 42);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(209, 20);
            this.textBox1.TabIndex = 1;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(298, 39);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 2;
            this.button2.Text = "Обзор...";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // renameCB
            // 
            this.renameCB.AutoSize = true;
            this.renameCB.Location = new System.Drawing.Point(67, 86);
            this.renameCB.Name = "renameCB";
            this.renameCB.Size = new System.Drawing.Size(144, 17);
            this.renameCB.TabIndex = 3;
            this.renameCB.Text = "Переименовать файлы";
            this.renameCB.UseVisualStyleBackColor = true;
            // 
            // sortCB
            // 
            this.sortCB.AutoSize = true;
            this.sortCB.Location = new System.Drawing.Point(230, 86);
            this.sortCB.Name = "sortCB";
            this.sortCB.Size = new System.Drawing.Size(143, 17);
            this.sortCB.TabIndex = 4;
            this.sortCB.Text = "Выполнить сортировку";
            this.sortCB.UseVisualStyleBackColor = true;
            // 
            // deleteFilesXML
            // 
            this.deleteFilesXML.AutoSize = true;
            this.deleteFilesXML.Location = new System.Drawing.Point(394, 86);
            this.deleteFilesXML.Name = "deleteFilesXML";
            this.deleteFilesXML.Size = new System.Drawing.Size(131, 17);
            this.deleteFilesXML.TabIndex = 5;
            this.deleteFilesXML.Text = "Удалить файлы XML";
            this.deleteFilesXML.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(559, 191);
            this.Controls.Add(this.deleteFilesXML);
            this.Controls.Add(this.sortCB);
            this.Controls.Add(this.renameCB);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.button1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "Image2D E57 => xlsx";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.CheckBox renameCB;
        private System.Windows.Forms.CheckBox sortCB;
        private System.Windows.Forms.CheckBox deleteFilesXML;
    }
}

