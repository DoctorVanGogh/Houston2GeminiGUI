using System;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Linq;
using Houston2DaiGUI.Properties;

namespace Houston2DaiGUI
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();

            ofdHoustonForm.InitialDirectory = Environment.ExpandEnvironmentVariables(@"%APPDATA%\NCSoft\WildStar\Addons\");
            chkUseSpacesForIndent.Checked = Settings.Default.UseSpacesForIndent;
            chkTranslateAnchorPoints.Checked = Settings.Default.TranslateAnchorPoints;
            nudIndentSize.Value = Settings.Default.IndentSize;
        }

        private void btnLoadXML_Click(object sender, EventArgs e)
        {
            var result = ofdHoustonForm.ShowDialog();
            if (result != DialogResult.OK) return;

            btnLoadXML.Text = ofdHoustonForm.FileName;

            PopulateFormComboBox();

            rtbOutput.Clear();
            btnCopyToClipboard.Enabled = false;
        }

        private void PopulateFormComboBox()
        {
            if (!System.IO.File.Exists(ofdHoustonForm.FileName)) return;
            cboForms.Items.Clear();
            var doc = XElement.Load(ofdHoustonForm.FileName);

            foreach (var formNode in doc.Elements().Where(x => x.Name == "Form"))
            {
                var formName = formNode.Attributes().FirstOrDefault(x => x.Name == "Name");
                if (formName != null) cboForms.Items.Add(new ComboBoxFormItem{Name= formName.Value, XElement = formNode});
            }
        }

        private void cboForms_SelectionChangeCommitted(object sender, EventArgs e)
        {
            ParseHoustonForm();
        }

        private void ParseHoustonForm()
        {
            if (cboForms.SelectedIndex == -1 || cboForms.SelectedItem == null)
            {
                btnCopyToClipboard.Enabled = false;
                rtbOutput.Clear();
                return;
            }

            var selectedItem = (ComboBoxFormItem) cboForms.SelectedItem;
            var formConverter = new FormConverter(selectedItem.XElement)
                {
                    UseSpacesForTabs = chkUseSpacesForIndent.Checked,
                    IndentSize = (int) nudIndentSize.Value,
                    TranslateAnchorPoints = chkTranslateAnchorPoints.Checked,
                };
            rtbOutput.Text = formConverter.Parse();
            btnCopyToClipboard.Enabled = true;
        }

        private void chkSpacesForIndent_CheckedChanged(object sender, EventArgs e)
        {
            nudIndentSize.Enabled = chkUseSpacesForIndent.Checked;
            lblIndentSize.Enabled = chkUseSpacesForIndent.Checked;

            ParseHoustonForm();

            Settings.Default["UseSpacesForIndent"] = chkUseSpacesForIndent.Checked;
            Settings.Default.Save();
        }

        private void nudIndentSize_ValueChanged(object sender, EventArgs e)
        {
            ParseHoustonForm();

            Settings.Default["IndentSize"] = (int)nudIndentSize.Value;
            Settings.Default.Save();
        }

        private void btnCopyToClipboard_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(rtbOutput.Text);
            btnCopyToClipboard.Text = Resources.CopiedToClipboard;
            tmrClearStatus.Enabled = true;
        }

        private void tmrClearStatus_Tick(object sender, EventArgs e)
        {
            tmrClearStatus.Enabled = false;
            btnCopyToClipboard.Text = Resources.CopyToClipboard;
        }

        private void chkTranslateAnchorPoints_CheckedChanged(object sender, EventArgs e)
        {
            ParseHoustonForm();

            Settings.Default["TranslateAnchorPoints"] = chkTranslateAnchorPoints.Checked;
            Settings.Default.Save();
        }

    }
}
