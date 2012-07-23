using System.Windows.Forms;

using AntMe.Gui.Properties;

namespace AntMe.Gui {
    internal sealed partial class UpdateRequest : Form {
        public UpdateRequest() {
            InitializeComponent();

            newsCheckBox.Checked = Settings.Default.showNews;
            updateCheckBox.Checked = Settings.Default.checkForUpdates;
        }

        private void UpdateRequest_FormClosed(object sender, FormClosedEventArgs e) {
            if (DialogResult == DialogResult.OK) {
                Settings.Default.showNews = newsCheckBox.Checked;
                Settings.Default.checkForUpdates = updateCheckBox.Checked;
                Settings.Default.firstStart = false;
            }
        }
    }
}