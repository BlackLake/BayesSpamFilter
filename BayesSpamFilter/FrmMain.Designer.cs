namespace BayesSpamFilter
{
    partial class FrmMain
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
            this.btnHashLexemes = new System.Windows.Forms.Button();
            this.txtOutput = new System.Windows.Forms.TextBox();
            this.btnCalculateSpamacities = new System.Windows.Forms.Button();
            this.btnTestNewFiles = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnHashLexemes
            // 
            this.btnHashLexemes.Location = new System.Drawing.Point(12, 12);
            this.btnHashLexemes.Name = "btnHashLexemes";
            this.btnHashLexemes.Size = new System.Drawing.Size(124, 50);
            this.btnHashLexemes.TabIndex = 0;
            this.btnHashLexemes.Text = "Hash Lexemes";
            this.btnHashLexemes.UseVisualStyleBackColor = true;
            this.btnHashLexemes.Click += new System.EventHandler(this.btnHashLexemes_Click);
            // 
            // txtOutput
            // 
            this.txtOutput.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtOutput.Location = new System.Drawing.Point(142, 12);
            this.txtOutput.Multiline = true;
            this.txtOutput.Name = "txtOutput";
            this.txtOutput.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtOutput.Size = new System.Drawing.Size(646, 351);
            this.txtOutput.TabIndex = 1;
            // 
            // btnCalculateSpamacities
            // 
            this.btnCalculateSpamacities.Enabled = false;
            this.btnCalculateSpamacities.Location = new System.Drawing.Point(12, 68);
            this.btnCalculateSpamacities.Name = "btnCalculateSpamacities";
            this.btnCalculateSpamacities.Size = new System.Drawing.Size(124, 50);
            this.btnCalculateSpamacities.TabIndex = 0;
            this.btnCalculateSpamacities.Text = "Calculate Spamacities";
            this.btnCalculateSpamacities.UseVisualStyleBackColor = true;
            this.btnCalculateSpamacities.Click += new System.EventHandler(this.btnCalculateSpamacities_Click);
            // 
            // btnTestNewFiles
            // 
            this.btnTestNewFiles.Enabled = false;
            this.btnTestNewFiles.Location = new System.Drawing.Point(12, 124);
            this.btnTestNewFiles.Name = "btnTestNewFiles";
            this.btnTestNewFiles.Size = new System.Drawing.Size(124, 50);
            this.btnTestNewFiles.TabIndex = 2;
            this.btnTestNewFiles.Text = "Test New Files";
            this.btnTestNewFiles.UseVisualStyleBackColor = true;
            this.btnTestNewFiles.Click += new System.EventHandler(this.btnTestNewFiles_Click);
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 375);
            this.Controls.Add(this.btnTestNewFiles);
            this.Controls.Add(this.txtOutput);
            this.Controls.Add(this.btnCalculateSpamacities);
            this.Controls.Add(this.btnHashLexemes);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "FrmMain";
            this.Text = "Bayes Spam Filter";
            this.Load += new System.EventHandler(this.FrmMain_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnHashLexemes;
        private System.Windows.Forms.TextBox txtOutput;
        private System.Windows.Forms.Button btnCalculateSpamacities;
        private System.Windows.Forms.Button btnTestNewFiles;
    }
}

