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
            this.schlie�enButton = new System.Windows.Forms.Button();
            this.oberfl�che1 = new AntMe.Plugin.Mdx.InstructionPanel();
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
            // schlie�enButton
            // 
            this.schlie�enButton.AccessibleDescription = null;
            this.schlie�enButton.AccessibleName = null;
            resources.ApplyResources(this.schlie�enButton, "schlie�enButton");
            this.schlie�enButton.BackgroundImage = null;
            this.schlie�enButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.schlie�enButton.Font = null;
            this.schlie�enButton.Name = "schlie�enButton";
            this.schlie�enButton.UseVisualStyleBackColor = true;
            // 
            // oberfl�che1
            // 
            this.oberfl�che1.AccessibleDescription = null;
            this.oberfl�che1.AccessibleName = null;
            resources.ApplyResources(this.oberfl�che1, "oberfl�che1");
            this.oberfl�che1.BackgroundImage = null;
            this.oberfl�che1.Font = null;
            this.oberfl�che1.Name = "oberfl�che1";
            // 
            // InstructionForm
            // 
            this.AcceptButton = this.schlie�enButton;
            this.AccessibleDescription = null;
            this.AccessibleName = null;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = null;
            this.CancelButton = this.schlie�enButton;
            this.Controls.Add(this.schlie�enButton);
            this.Controls.Add(this.erneutAnzeigenCheckbox);
            this.Controls.Add(this.oberfl�che1);
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

        private InstructionPanel oberfl�che1;
        private System.Windows.Forms.CheckBox erneutAnzeigenCheckbox;
        private System.Windows.Forms.Button schlie�enButton;
    }
}