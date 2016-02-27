namespace FunWithCSharpAsync {
    partial class MainForm {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.btnCallMethod = new System.Windows.Forms.Button();
            this.txtinput = new System.Windows.Forms.TextBox();
            this.btnvoidCallMethod = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnCallMethod
            // 
            this.btnCallMethod.Location = new System.Drawing.Point(241, 164);
            this.btnCallMethod.Name = "btnCallMethod";
            this.btnCallMethod.Size = new System.Drawing.Size(75, 23);
            this.btnCallMethod.TabIndex = 0;
            this.btnCallMethod.Text = "CallMethod";
            this.btnCallMethod.UseVisualStyleBackColor = true;
            this.btnCallMethod.Click += new System.EventHandler(this.btnCallMethod_Click);
            // 
            // txtinput
            // 
            this.txtinput.Location = new System.Drawing.Point(33, 28);
            this.txtinput.Name = "txtinput";
            this.txtinput.Size = new System.Drawing.Size(350, 20);
            this.txtinput.TabIndex = 1;
            // 
            // btnvoidCallMethod
            // 
            this.btnvoidCallMethod.Location = new System.Drawing.Point(352, 164);
            this.btnvoidCallMethod.Name = "btnvoidCallMethod";
            this.btnvoidCallMethod.Size = new System.Drawing.Size(117, 23);
            this.btnvoidCallMethod.TabIndex = 2;
            this.btnvoidCallMethod.Text = "VoidCallMethod";
            this.btnvoidCallMethod.UseVisualStyleBackColor = true;
            this.btnvoidCallMethod.Click += new System.EventHandler(this.btnvoidCallMethod_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(520, 221);
            this.Controls.Add(this.btnvoidCallMethod);
            this.Controls.Add(this.txtinput);
            this.Controls.Add(this.btnCallMethod);
            this.Name = "MainForm";
            this.Text = "MainForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnCallMethod;
        private System.Windows.Forms.TextBox txtinput;
        private System.Windows.Forms.Button btnvoidCallMethod;
    }
}

