﻿namespace AntMe.Gui {
    partial class Plugins
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Plugins));
            this.pluginListView = new System.Windows.Forms.ListView();
            this.nameColumn = new System.Windows.Forms.ColumnHeader();
            this.versionColumn = new System.Windows.Forms.ColumnHeader();
            this.descriptionColumn = new System.Windows.Forms.ColumnHeader();
            this.cancelButton = new System.Windows.Forms.Button();
            this.addPluginButton = new System.Windows.Forms.Button();
            this.descriptionLabel = new System.Windows.Forms.Label();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.SuspendLayout();
            // 
            // pluginListView
            // 
            this.pluginListView.AccessibleDescription = null;
            this.pluginListView.AccessibleName = null;
            resources.ApplyResources(this.pluginListView, "pluginListView");
            this.pluginListView.BackgroundImage = null;
            this.pluginListView.CheckBoxes = true;
            this.pluginListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.nameColumn,
            this.versionColumn,
            this.descriptionColumn});
            this.pluginListView.Font = null;
            this.pluginListView.Groups.AddRange(new System.Windows.Forms.ListViewGroup[] {
            ((System.Windows.Forms.ListViewGroup)(resources.GetObject("pluginListView.Groups"))),
            ((System.Windows.Forms.ListViewGroup)(resources.GetObject("pluginListView.Groups1")))});
            this.pluginListView.MultiSelect = false;
            this.pluginListView.Name = "pluginListView";
            this.pluginListView.ShowItemToolTips = true;
            this.pluginListView.UseCompatibleStateImageBehavior = false;
            this.pluginListView.View = System.Windows.Forms.View.Details;
            this.pluginListView.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.pluginListView_ItemCheck);
            // 
            // nameColumn
            // 
            resources.ApplyResources(this.nameColumn, "nameColumn");
            // 
            // versionColumn
            // 
            resources.ApplyResources(this.versionColumn, "versionColumn");
            // 
            // descriptionColumn
            // 
            resources.ApplyResources(this.descriptionColumn, "descriptionColumn");
            // 
            // cancelButton
            // 
            this.cancelButton.AccessibleDescription = null;
            this.cancelButton.AccessibleName = null;
            resources.ApplyResources(this.cancelButton, "cancelButton");
            this.cancelButton.BackgroundImage = null;
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.cancelButton.Font = null;
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.UseVisualStyleBackColor = true;
            // 
            // addPluginButton
            // 
            this.addPluginButton.AccessibleDescription = null;
            this.addPluginButton.AccessibleName = null;
            resources.ApplyResources(this.addPluginButton, "addPluginButton");
            this.addPluginButton.BackgroundImage = null;
            this.addPluginButton.Font = null;
            this.addPluginButton.Name = "addPluginButton";
            this.addPluginButton.UseVisualStyleBackColor = true;
            this.addPluginButton.Click += new System.EventHandler(this.addPluginButton_Click);
            // 
            // descriptionLabel
            // 
            this.descriptionLabel.AccessibleDescription = null;
            this.descriptionLabel.AccessibleName = null;
            resources.ApplyResources(this.descriptionLabel, "descriptionLabel");
            this.descriptionLabel.Font = null;
            this.descriptionLabel.Name = "descriptionLabel";
            // 
            // openFileDialog
            // 
            this.openFileDialog.FileName = "openFileDialog1";
            resources.ApplyResources(this.openFileDialog, "openFileDialog");
            this.openFileDialog.Multiselect = true;
            // 
            // Plugins
            // 
            this.AcceptButton = this.cancelButton;
            this.AccessibleDescription = null;
            this.AccessibleName = null;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = null;
            this.CancelButton = this.cancelButton;
            this.Controls.Add(this.descriptionLabel);
            this.Controls.Add(this.addPluginButton);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.pluginListView);
            this.Font = null;
            this.Icon = null;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Plugins";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView pluginListView;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.ColumnHeader nameColumn;
        private System.Windows.Forms.ColumnHeader versionColumn;
        private System.Windows.Forms.ColumnHeader descriptionColumn;
        private System.Windows.Forms.Button addPluginButton;
        private System.Windows.Forms.Label descriptionLabel;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
    }
}