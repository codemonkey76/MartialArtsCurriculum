namespace MartialArtsCurriculum
{
    partial class frmGenerateGradingSheets
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
            this.label1 = new System.Windows.Forms.Label();
            this.lblTemplate = new System.Windows.Forms.Label();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.btnBrowseTemplate = new System.Windows.Forms.Button();
            this.btnBrowseStudents = new System.Windows.Forms.Button();
            this.lblStudents = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.btnGenerate = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(84, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "HTML Template";
            // 
            // lblTemplate
            // 
            this.lblTemplate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblTemplate.Location = new System.Drawing.Point(12, 22);
            this.lblTemplate.Name = "lblTemplate";
            this.lblTemplate.Size = new System.Drawing.Size(201, 23);
            this.lblTemplate.TabIndex = 1;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // btnBrowseTemplate
            // 
            this.btnBrowseTemplate.Location = new System.Drawing.Point(219, 22);
            this.btnBrowseTemplate.Name = "btnBrowseTemplate";
            this.btnBrowseTemplate.Size = new System.Drawing.Size(28, 23);
            this.btnBrowseTemplate.TabIndex = 2;
            this.btnBrowseTemplate.Text = "...";
            this.btnBrowseTemplate.UseVisualStyleBackColor = true;
            // 
            // btnBrowseStudents
            // 
            this.btnBrowseStudents.Location = new System.Drawing.Point(219, 67);
            this.btnBrowseStudents.Name = "btnBrowseStudents";
            this.btnBrowseStudents.Size = new System.Drawing.Size(28, 23);
            this.btnBrowseStudents.TabIndex = 5;
            this.btnBrowseStudents.Text = "...";
            this.btnBrowseStudents.UseVisualStyleBackColor = true;
            // 
            // lblStudents
            // 
            this.lblStudents.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblStudents.Location = new System.Drawing.Point(12, 67);
            this.lblStudents.Name = "lblStudents";
            this.lblStudents.Size = new System.Drawing.Size(201, 23);
            this.lblStudents.TabIndex = 4;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 54);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(89, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "Students Grading";
            // 
            // btnGenerate
            // 
            this.btnGenerate.Location = new System.Drawing.Point(272, 9);
            this.btnGenerate.Name = "btnGenerate";
            this.btnGenerate.Size = new System.Drawing.Size(110, 23);
            this.btnGenerate.TabIndex = 6;
            this.btnGenerate.Text = "&Generate Sheets";
            this.btnGenerate.UseVisualStyleBackColor = true;
            // 
            // btnClose
            // 
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.Location = new System.Drawing.Point(272, 38);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(110, 23);
            this.btnClose.TabIndex = 7;
            this.btnClose.Text = "&Close";
            this.btnClose.UseVisualStyleBackColor = true;
            // 
            // frmGenerateGradingSheets
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnClose;
            this.ClientSize = new System.Drawing.Size(396, 102);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnGenerate);
            this.Controls.Add(this.btnBrowseStudents);
            this.Controls.Add(this.lblStudents);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.btnBrowseTemplate);
            this.Controls.Add(this.lblTemplate);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmGenerateGradingSheets";
            this.Text = "Generate Grading Sheets";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblTemplate;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button btnBrowseTemplate;
        private System.Windows.Forms.Button btnBrowseStudents;
        private System.Windows.Forms.Label lblStudents;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnGenerate;
        private System.Windows.Forms.Button btnClose;
    }
}