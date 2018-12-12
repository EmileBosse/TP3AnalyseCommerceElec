namespace Tp3InterfaceAnalyse
{
    partial class SignIn
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SignIn));
            this.lblNomSignIn = new System.Windows.Forms.Label();
            this.txtNomSignIn = new System.Windows.Forms.TextBox();
            this.txtPrenomSignIn = new System.Windows.Forms.TextBox();
            this.btnConnexionSignIn = new System.Windows.Forms.Button();
            this.btnAnnulerSignIn = new System.Windows.Forms.Button();
            this.lblPrenom = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblNomSignIn
            // 
            this.lblNomSignIn.AutoSize = true;
            this.lblNomSignIn.Location = new System.Drawing.Point(12, 25);
            this.lblNomSignIn.Name = "lblNomSignIn";
            this.lblNomSignIn.Size = new System.Drawing.Size(35, 13);
            this.lblNomSignIn.TabIndex = 0;
            this.lblNomSignIn.Text = "Nom :";
            // 
            // txtNomSignIn
            // 
            this.txtNomSignIn.Location = new System.Drawing.Point(67, 22);
            this.txtNomSignIn.Name = "txtNomSignIn";
            this.txtNomSignIn.Size = new System.Drawing.Size(138, 20);
            this.txtNomSignIn.TabIndex = 1;
            // 
            // txtPrenomSignIn
            // 
            this.txtPrenomSignIn.Location = new System.Drawing.Point(67, 48);
            this.txtPrenomSignIn.Name = "txtPrenomSignIn";
            this.txtPrenomSignIn.Size = new System.Drawing.Size(138, 20);
            this.txtPrenomSignIn.TabIndex = 2;
            // 
            // btnConnexionSignIn
            // 
            this.btnConnexionSignIn.Location = new System.Drawing.Point(12, 87);
            this.btnConnexionSignIn.Name = "btnConnexionSignIn";
            this.btnConnexionSignIn.Size = new System.Drawing.Size(90, 23);
            this.btnConnexionSignIn.TabIndex = 3;
            this.btnConnexionSignIn.Text = "connexion";
            this.btnConnexionSignIn.UseVisualStyleBackColor = true;
            this.btnConnexionSignIn.Click += new System.EventHandler(this.btnConnexionSignIn_Click);
            // 
            // btnAnnulerSignIn
            // 
            this.btnAnnulerSignIn.Location = new System.Drawing.Point(115, 87);
            this.btnAnnulerSignIn.Name = "btnAnnulerSignIn";
            this.btnAnnulerSignIn.Size = new System.Drawing.Size(90, 23);
            this.btnAnnulerSignIn.TabIndex = 4;
            this.btnAnnulerSignIn.Text = "annuler";
            this.btnAnnulerSignIn.UseVisualStyleBackColor = true;
            this.btnAnnulerSignIn.Click += new System.EventHandler(this.btnAnnulerSignIn_Click);
            // 
            // lblPrenom
            // 
            this.lblPrenom.AutoSize = true;
            this.lblPrenom.Location = new System.Drawing.Point(12, 51);
            this.lblPrenom.Name = "lblPrenom";
            this.lblPrenom.Size = new System.Drawing.Size(49, 13);
            this.lblPrenom.TabIndex = 5;
            this.lblPrenom.Text = "Prénom :";
            // 
            // SignIn
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(217, 121);
            this.Controls.Add(this.lblPrenom);
            this.Controls.Add(this.btnAnnulerSignIn);
            this.Controls.Add(this.btnConnexionSignIn);
            this.Controls.Add(this.txtPrenomSignIn);
            this.Controls.Add(this.txtNomSignIn);
            this.Controls.Add(this.lblNomSignIn);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "SignIn";
            this.Text = "SignIn";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblNomSignIn;
        private System.Windows.Forms.TextBox txtNomSignIn;
        private System.Windows.Forms.TextBox txtPrenomSignIn;
        private System.Windows.Forms.Button btnConnexionSignIn;
        private System.Windows.Forms.Button btnAnnulerSignIn;
        private System.Windows.Forms.Label lblPrenom;
    }
}