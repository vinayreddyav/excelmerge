namespace ExcelMerge
{
    partial class MainForm
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
            this.txtFile1 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnSelectFile1 = new System.Windows.Forms.Button();
            this.btnSelectFIle2 = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.txtFile2 = new System.Windows.Forms.TextBox();
            this.btnMerge = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txtFile1
            // 
            this.txtFile1.Location = new System.Drawing.Point(132, 26);
            this.txtFile1.Name = "txtFile1";
            this.txtFile1.Size = new System.Drawing.Size(507, 22);
            this.txtFile1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(22, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(42, 17);
            this.label1.TabIndex = 1;
            this.label1.Text = "File 1";
            // 
            // btnSelectFile1
            // 
            this.btnSelectFile1.Location = new System.Drawing.Point(691, 26);
            this.btnSelectFile1.Name = "btnSelectFile1";
            this.btnSelectFile1.Size = new System.Drawing.Size(75, 23);
            this.btnSelectFile1.TabIndex = 2;
            this.btnSelectFile1.Text = "Select";
            this.btnSelectFile1.UseVisualStyleBackColor = true;
            this.btnSelectFile1.Click += new System.EventHandler(this.btnSelectFile1_Click);
            // 
            // btnSelectFIle2
            // 
            this.btnSelectFIle2.Location = new System.Drawing.Point(691, 74);
            this.btnSelectFIle2.Name = "btnSelectFIle2";
            this.btnSelectFIle2.Size = new System.Drawing.Size(75, 23);
            this.btnSelectFIle2.TabIndex = 5;
            this.btnSelectFIle2.Text = "Select";
            this.btnSelectFIle2.UseVisualStyleBackColor = true;
            this.btnSelectFIle2.Click += new System.EventHandler(this.btnSelectFIle2_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(22, 77);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(42, 17);
            this.label2.TabIndex = 4;
            this.label2.Text = "File 2";
            // 
            // txtFile2
            // 
            this.txtFile2.Location = new System.Drawing.Point(132, 74);
            this.txtFile2.Name = "txtFile2";
            this.txtFile2.Size = new System.Drawing.Size(507, 22);
            this.txtFile2.TabIndex = 3;
            // 
            // btnMerge
            // 
            this.btnMerge.Location = new System.Drawing.Point(282, 149);
            this.btnMerge.Name = "btnMerge";
            this.btnMerge.Size = new System.Drawing.Size(142, 39);
            this.btnMerge.TabIndex = 6;
            this.btnMerge.Text = "Merge";
            this.btnMerge.UseVisualStyleBackColor = true;
            this.btnMerge.Click += new System.EventHandler(this.btnMerge_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(801, 253);
            this.Controls.Add(this.btnMerge);
            this.Controls.Add(this.btnSelectFIle2);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtFile2);
            this.Controls.Add(this.btnSelectFile1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtFile1);
            this.Name = "MainForm";
            this.Text = "Merge";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtFile1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnSelectFile1;
        private System.Windows.Forms.Button btnSelectFIle2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtFile2;
        private System.Windows.Forms.Button btnMerge;
    }
}

