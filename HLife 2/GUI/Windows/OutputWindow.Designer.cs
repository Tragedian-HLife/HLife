namespace HLife_2
{
    partial class OutputWindow
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
            this.rtb_output = new System.Windows.Forms.RichTextBox();
            this.btn_clear = new System.Windows.Forms.Button();
            this.chk_autoScroll = new System.Windows.Forms.CheckBox();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // rtb_output
            // 
            this.rtb_output.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtb_output.Location = new System.Drawing.Point(0, 0);
            this.rtb_output.Name = "rtb_output";
            this.rtb_output.ReadOnly = true;
            this.rtb_output.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.rtb_output.Size = new System.Drawing.Size(1404, 106);
            this.rtb_output.TabIndex = 0;
            this.rtb_output.Text = "";
            this.rtb_output.TextChanged += new System.EventHandler(this.rtb_output_TextChanged);
            // 
            // btn_clear
            // 
            this.btn_clear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_clear.Location = new System.Drawing.Point(1329, 5);
            this.btn_clear.Name = "btn_clear";
            this.btn_clear.Size = new System.Drawing.Size(75, 23);
            this.btn_clear.TabIndex = 1;
            this.btn_clear.Text = "Clear";
            this.btn_clear.UseVisualStyleBackColor = true;
            this.btn_clear.Click += new System.EventHandler(this.btn_clear_Click);
            // 
            // chk_autoScroll
            // 
            this.chk_autoScroll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.chk_autoScroll.AutoSize = true;
            this.chk_autoScroll.Checked = true;
            this.chk_autoScroll.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chk_autoScroll.Location = new System.Drawing.Point(3, 9);
            this.chk_autoScroll.Name = "chk_autoScroll";
            this.chk_autoScroll.Size = new System.Drawing.Size(77, 17);
            this.chk_autoScroll.TabIndex = 2;
            this.chk_autoScroll.Text = "Auto Scroll";
            this.chk_autoScroll.UseVisualStyleBackColor = true;
            this.chk_autoScroll.CheckedChanged += new System.EventHandler(this.chk_autoScroll_CheckedChanged);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.rtb_output);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.btn_clear);
            this.splitContainer1.Panel2.Controls.Add(this.chk_autoScroll);
            this.splitContainer1.Size = new System.Drawing.Size(1404, 141);
            this.splitContainer1.SplitterDistance = 106;
            this.splitContainer1.TabIndex = 3;
            // 
            // OutputWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1404, 141);
            this.ControlBox = false;
            this.Controls.Add(this.splitContainer1);
            this.Location = new System.Drawing.Point(0, 850);
            this.Name = "OutputWindow";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "OutputWindow";
            this.Load += new System.EventHandler(this.OutputWindow_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox rtb_output;
        private System.Windows.Forms.Button btn_clear;
        private System.Windows.Forms.CheckBox chk_autoScroll;
        private System.Windows.Forms.SplitContainer splitContainer1;
    }
}