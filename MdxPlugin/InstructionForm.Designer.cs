using InstructionPanel=AntMe.Plugin.Mdx.InstructionPanel;

namespace AntMe.Plugin.Mdx {
    partial class InstructionForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(InstructionForm));
            this.erneutAnzeigenCheckbox = new System.Windows.Forms.CheckBox();
            this.schließenButton = new System.Windows.Forms.Button();
            this.oberfläche1 = new AntMe.Plugin.Mdx.InstructionPanel();
            this.SuspendLayout();
            // 
            // erneutAnzeigenCheckbox
            // 
            this.erneutAnzeigenCheckbox.AccessibleDescription = null;
            this.erneutAnzeigenCheckbox.AccessibleName = null;
            resources.ApplyResources(this.erneutAnzeigenCheckbox, "erneutAnzeigenCheckbox");
            this.erneutAnzeigenCheckbox.BackgroundImage = null;
            this.erneutAnzeigenCheckbox.Checked = true;
            this.erneutAnzeigenCheckbox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.erneutAnzeigenCheckbox.Font = null;
            this.erneutAnzeigenCheckbox.Name = "erneutAnzeigenCheckbox";
            this.erneutAnzeigenCheckbox.UseVisualStyleBackColor = true;
            // 
            // schließenButton
            // 
            this.schließenButton.AccessibleDescription = null;
            this.schließenButton.AccessibleName = null;
            resources.ApplyResources(this.schließenButton, "schließenButton");
            this.schließenButton.BackgroundImage = null;
            this.schließenButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.schließenButton.Font = null;
            this.schließenButton.Name = "schließenButton";
            this.schließenButton.UseVisualStyleBackColor = true;
            // 
            // oberfläche1
            // 
            this.oberfläche1.AccessibleDescription = null;
            this.oberfläche1.AccessibleName = null;
            resources.ApplyResources(this.oberfläche1, "oberfläche1");
            this.oberfläche1.BackgroundImage = null;
            this.oberfläche1.Font = null;
            this.oberfläche1.Name = "oberfläche1";
            // 
            // InstructionForm
            // 
            this.AcceptButton = this.schließenButton;
            this.AccessibleDescription = null;
            this.AccessibleName = null;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = null;
            this.CancelButton = this.schließenButton;
            this.Controls.Add(this.schließenButton);
            this.Controls.Add(this.erneutAnzeigenCheckbox);
            this.Controls.Add(this.oberfläche1);
            this.Font = null;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "InstructionForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.form_close);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private InstructionPanel oberfläche1;
        private System.Windows.Forms.CheckBox erneutAnzeigenCheckbox;
        private System.Windows.Forms.Button schließenButton;
    }
}