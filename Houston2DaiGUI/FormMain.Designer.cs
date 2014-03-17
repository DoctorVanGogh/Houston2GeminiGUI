namespace Houston2DaiGUI
{
    partial class FormMain
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
            this.components = new System.ComponentModel.Container();
            this.btnLoadXML = new System.Windows.Forms.Button();
            this.cboForms = new System.Windows.Forms.ComboBox();
            this.rtbOutput = new System.Windows.Forms.RichTextBox();
            this.ofdHoustonForm = new System.Windows.Forms.OpenFileDialog();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.chkUseSpacesForIndent = new System.Windows.Forms.CheckBox();
            this.nudIndentSize = new System.Windows.Forms.NumericUpDown();
            this.lblIndentSize = new System.Windows.Forms.Label();
            this.btnCopyToClipboard = new System.Windows.Forms.Button();
            this.tmrClearStatus = new System.Windows.Forms.Timer(this.components);
            this.chkTranslateAnchorPoints = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.nudIndentSize)).BeginInit();
            this.SuspendLayout();
            // 
            // btnLoadXML
            // 
            this.btnLoadXML.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLoadXML.Location = new System.Drawing.Point(148, 14);
            this.btnLoadXML.Name = "btnLoadXML";
            this.btnLoadXML.Size = new System.Drawing.Size(628, 23);
            this.btnLoadXML.TabIndex = 0;
            this.btnLoadXML.Text = "…";
            this.btnLoadXML.UseVisualStyleBackColor = true;
            this.btnLoadXML.Click += new System.EventHandler(this.btnLoadXML_Click);
            // 
            // cboForms
            // 
            this.cboForms.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cboForms.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboForms.FormattingEnabled = true;
            this.cboForms.Location = new System.Drawing.Point(148, 43);
            this.cboForms.Name = "cboForms";
            this.cboForms.Size = new System.Drawing.Size(628, 21);
            this.cboForms.TabIndex = 1;
            this.cboForms.SelectionChangeCommitted += new System.EventHandler(this.cboForms_SelectionChangeCommitted);
            // 
            // rtbOutput
            // 
            this.rtbOutput.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.rtbOutput.Font = new System.Drawing.Font("Segoe UI Mono", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rtbOutput.Location = new System.Drawing.Point(13, 110);
            this.rtbOutput.Name = "rtbOutput";
            this.rtbOutput.Size = new System.Drawing.Size(763, 347);
            this.rtbOutput.TabIndex = 2;
            this.rtbOutput.Text = "";
            this.rtbOutput.WordWrap = false;
            // 
            // ofdHoustonForm
            // 
            this.ofdHoustonForm.FileName = "NewForm.xml";
            this.ofdHoustonForm.Filter = "XML Files|*.xml|All Files|*.*";
            this.ofdHoustonForm.InitialDirectory = "%appdata%\\ncsoft\\wildstar\\addons";
            this.ofdHoustonForm.Title = "Open Houston XML File";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(106, 47);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(36, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Form:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 19);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(129, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Houston Form XML File:";
            // 
            // chkUseSpacesForIndent
            // 
            this.chkUseSpacesForIndent.AutoSize = true;
            this.chkUseSpacesForIndent.Location = new System.Drawing.Point(13, 84);
            this.chkUseSpacesForIndent.Name = "chkUseSpacesForIndent";
            this.chkUseSpacesForIndent.Size = new System.Drawing.Size(162, 17);
            this.chkUseSpacesForIndent.TabIndex = 5;
            this.chkUseSpacesForIndent.Text = "Use spaces instead of tabs";
            this.chkUseSpacesForIndent.UseVisualStyleBackColor = true;
            this.chkUseSpacesForIndent.CheckedChanged += new System.EventHandler(this.chkSpacesForIndent_CheckedChanged);
            // 
            // nudIndentSize
            // 
            this.nudIndentSize.Enabled = false;
            this.nudIndentSize.Location = new System.Drawing.Point(269, 81);
            this.nudIndentSize.Name = "nudIndentSize";
            this.nudIndentSize.Size = new System.Drawing.Size(47, 22);
            this.nudIndentSize.TabIndex = 6;
            this.nudIndentSize.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.nudIndentSize.Value = new decimal(new int[] {
            4,
            0,
            0,
            0});
            this.nudIndentSize.ValueChanged += new System.EventHandler(this.nudIndentSize_ValueChanged);
            // 
            // lblIndentSize
            // 
            this.lblIndentSize.AutoSize = true;
            this.lblIndentSize.Enabled = false;
            this.lblIndentSize.Location = new System.Drawing.Point(196, 86);
            this.lblIndentSize.Name = "lblIndentSize";
            this.lblIndentSize.Size = new System.Drawing.Size(66, 13);
            this.lblIndentSize.TabIndex = 7;
            this.lblIndentSize.Text = "Indent size:";
            // 
            // btnCopyToClipboard
            // 
            this.btnCopyToClipboard.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCopyToClipboard.Enabled = false;
            this.btnCopyToClipboard.Location = new System.Drawing.Point(637, 81);
            this.btnCopyToClipboard.Name = "btnCopyToClipboard";
            this.btnCopyToClipboard.Size = new System.Drawing.Size(139, 23);
            this.btnCopyToClipboard.TabIndex = 8;
            this.btnCopyToClipboard.Text = "Copy to Clipboard";
            this.btnCopyToClipboard.UseVisualStyleBackColor = true;
            this.btnCopyToClipboard.Click += new System.EventHandler(this.btnCopyToClipboard_Click);
            // 
            // tmrClearStatus
            // 
            this.tmrClearStatus.Interval = 1500;
            this.tmrClearStatus.Tick += new System.EventHandler(this.tmrClearStatus_Tick);
            // 
            // chkTranslateAnchorPoints
            // 
            this.chkTranslateAnchorPoints.AutoSize = true;
            this.chkTranslateAnchorPoints.Location = new System.Drawing.Point(344, 85);
            this.chkTranslateAnchorPoints.Name = "chkTranslateAnchorPoints";
            this.chkTranslateAnchorPoints.Size = new System.Drawing.Size(234, 17);
            this.chkTranslateAnchorPoints.TabIndex = 10;
            this.chkTranslateAnchorPoints.Text = "Translate AnchorPoints to named variant";
            this.chkTranslateAnchorPoints.UseVisualStyleBackColor = true;
            this.chkTranslateAnchorPoints.CheckedChanged += new System.EventHandler(this.chkTranslateAnchorPoints_CheckedChanged);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(788, 469);
            this.Controls.Add(this.chkTranslateAnchorPoints);
            this.Controls.Add(this.btnCopyToClipboard);
            this.Controls.Add(this.lblIndentSize);
            this.Controls.Add(this.nudIndentSize);
            this.Controls.Add(this.chkUseSpacesForIndent);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.rtbOutput);
            this.Controls.Add(this.cboForms);
            this.Controls.Add(this.btnLoadXML);
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "FormMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Houston to DaiGUI Converter";
            ((System.ComponentModel.ISupportInitialize)(this.nudIndentSize)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnLoadXML;
        private System.Windows.Forms.ComboBox cboForms;
        private System.Windows.Forms.RichTextBox rtbOutput;
        private System.Windows.Forms.OpenFileDialog ofdHoustonForm;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox chkUseSpacesForIndent;
        private System.Windows.Forms.NumericUpDown nudIndentSize;
        private System.Windows.Forms.Label lblIndentSize;
        private System.Windows.Forms.Button btnCopyToClipboard;
        private System.Windows.Forms.Timer tmrClearStatus;
        private System.Windows.Forms.CheckBox chkTranslateAnchorPoints;
    }
}

