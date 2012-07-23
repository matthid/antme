using System.Windows.Forms;

namespace AntMe.Plugin.Mdx {
    internal sealed partial class InstructionForm : Form {
        public InstructionForm() {
            InitializeComponent();
        }

        private void form_close(object sender, FormClosingEventArgs e) {
            DialogResult = erneutAnzeigenCheckbox.Checked ? DialogResult.Retry : DialogResult.Ignore;
        }
    }
}