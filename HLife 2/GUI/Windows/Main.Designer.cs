namespace HLife_2
{
    partial class Hlife2
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
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.toolStrip_file = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip_view = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip_windows = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip_edit = new System.Windows.Forms.ToolStripMenuItem();
            this.lbl_date = new System.Windows.Forms.Label();
            this.tabs_Location = new System.Windows.Forms.TabControl();
            this.tab_Info = new System.Windows.Forms.TabPage();
            this.tab_Occupants = new System.Windows.Forms.TabPage();
            this.flow = new System.Windows.Forms.FlowLayoutPanel();
            this.tab_Inventory = new System.Windows.Forms.TabPage();
            this.flow_objects = new System.Windows.Forms.FlowLayoutPanel();
            this.panel_View = new System.Windows.Forms.Panel();
            this.split = new System.Windows.Forms.SplitContainer();
            this.menuStrip.SuspendLayout();
            this.tabs_Location.SuspendLayout();
            this.tab_Occupants.SuspendLayout();
            this.tab_Inventory.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.split)).BeginInit();
            this.split.Panel1.SuspendLayout();
            this.split.Panel2.SuspendLayout();
            this.split.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip
            // 
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStrip_file,
            this.toolStrip_view,
            this.toolStrip_edit});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(1404, 24);
            this.menuStrip.TabIndex = 0;
            this.menuStrip.Text = "menuStrip1";
            // 
            // toolStrip_file
            // 
            this.toolStrip_file.Name = "toolStrip_file";
            this.toolStrip_file.Size = new System.Drawing.Size(37, 20);
            this.toolStrip_file.Text = "File";
            // 
            // toolStrip_view
            // 
            this.toolStrip_view.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStrip_windows});
            this.toolStrip_view.Name = "toolStrip_view";
            this.toolStrip_view.Size = new System.Drawing.Size(44, 20);
            this.toolStrip_view.Text = "View";
            // 
            // toolStrip_windows
            // 
            this.toolStrip_windows.Name = "toolStrip_windows";
            this.toolStrip_windows.Size = new System.Drawing.Size(123, 22);
            this.toolStrip_windows.Text = "Windows";
            // 
            // toolStrip_edit
            // 
            this.toolStrip_edit.Name = "toolStrip_edit";
            this.toolStrip_edit.Size = new System.Drawing.Size(52, 20);
            this.toolStrip_edit.Text = "About";
            // 
            // lbl_date
            // 
            this.lbl_date.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lbl_date.AutoSize = true;
            this.lbl_date.BackColor = System.Drawing.SystemColors.Control;
            this.lbl_date.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_date.Location = new System.Drawing.Point(1257, 4);
            this.lbl_date.Name = "lbl_date";
            this.lbl_date.Size = new System.Drawing.Size(135, 17);
            this.lbl_date.TabIndex = 1;
            this.lbl_date.Text = "January 1, 12:00:00";
            // 
            // tabs_Location
            // 
            this.tabs_Location.Controls.Add(this.tab_Info);
            this.tabs_Location.Controls.Add(this.tab_Occupants);
            this.tabs_Location.Controls.Add(this.tab_Inventory);
            this.tabs_Location.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabs_Location.Location = new System.Drawing.Point(0, 0);
            this.tabs_Location.Name = "tabs_Location";
            this.tabs_Location.SelectedIndex = 0;
            this.tabs_Location.Size = new System.Drawing.Size(230, 787);
            this.tabs_Location.TabIndex = 2;
            // 
            // tab_Info
            // 
            this.tab_Info.Location = new System.Drawing.Point(4, 22);
            this.tab_Info.Name = "tab_Info";
            this.tab_Info.Padding = new System.Windows.Forms.Padding(3);
            this.tab_Info.Size = new System.Drawing.Size(222, 761);
            this.tab_Info.TabIndex = 0;
            this.tab_Info.Text = "Info";
            this.tab_Info.UseVisualStyleBackColor = true;
            // 
            // tab_Occupants
            // 
            this.tab_Occupants.Controls.Add(this.flow);
            this.tab_Occupants.Location = new System.Drawing.Point(4, 22);
            this.tab_Occupants.Name = "tab_Occupants";
            this.tab_Occupants.Padding = new System.Windows.Forms.Padding(3);
            this.tab_Occupants.Size = new System.Drawing.Size(222, 761);
            this.tab_Occupants.TabIndex = 1;
            this.tab_Occupants.Text = "Occupants";
            this.tab_Occupants.UseVisualStyleBackColor = true;
            // 
            // flow
            // 
            this.flow.AutoScroll = true;
            this.flow.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flow.Location = new System.Drawing.Point(3, 3);
            this.flow.Name = "flow";
            this.flow.Size = new System.Drawing.Size(216, 755);
            this.flow.TabIndex = 0;
            // 
            // tab_Inventory
            // 
            this.tab_Inventory.Controls.Add(this.flow_objects);
            this.tab_Inventory.Location = new System.Drawing.Point(4, 22);
            this.tab_Inventory.Name = "tab_Inventory";
            this.tab_Inventory.Padding = new System.Windows.Forms.Padding(3);
            this.tab_Inventory.Size = new System.Drawing.Size(222, 761);
            this.tab_Inventory.TabIndex = 2;
            this.tab_Inventory.Text = "Inventory";
            this.tab_Inventory.UseVisualStyleBackColor = true;
            // 
            // flow_objects
            // 
            this.flow_objects.AutoScroll = true;
            this.flow_objects.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flow_objects.Location = new System.Drawing.Point(3, 3);
            this.flow_objects.Name = "flow_objects";
            this.flow_objects.Size = new System.Drawing.Size(216, 755);
            this.flow_objects.TabIndex = 0;
            // 
            // panel_View
            // 
            this.panel_View.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.panel_View.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_View.Location = new System.Drawing.Point(0, 0);
            this.panel_View.Name = "panel_View";
            this.panel_View.Size = new System.Drawing.Size(1170, 787);
            this.panel_View.TabIndex = 3;
            // 
            // split
            // 
            this.split.Dock = System.Windows.Forms.DockStyle.Fill;
            this.split.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.split.Location = new System.Drawing.Point(0, 24);
            this.split.Name = "split";
            // 
            // split.Panel1
            // 
            this.split.Panel1.Controls.Add(this.tabs_Location);
            // 
            // split.Panel2
            // 
            this.split.Panel2.Controls.Add(this.panel_View);
            this.split.Size = new System.Drawing.Size(1404, 787);
            this.split.SplitterDistance = 230;
            this.split.TabIndex = 4;
            // 
            // Hlife2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1404, 811);
            this.Controls.Add(this.split);
            this.Controls.Add(this.lbl_date);
            this.Controls.Add(this.menuStrip);
            this.DoubleBuffered = true;
            this.MainMenuStrip = this.menuStrip;
            this.Name = "Hlife2";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Hlife 2";
            this.Load += new System.EventHandler(this.Hlife2_Load);
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.tabs_Location.ResumeLayout(false);
            this.tab_Occupants.ResumeLayout(false);
            this.tab_Inventory.ResumeLayout(false);
            this.split.Panel1.ResumeLayout(false);
            this.split.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.split)).EndInit();
            this.split.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem toolStrip_file;
        private System.Windows.Forms.ToolStripMenuItem toolStrip_view;
        private System.Windows.Forms.ToolStripMenuItem toolStrip_edit;
        private System.Windows.Forms.ToolStripMenuItem toolStrip_windows;
        private System.Windows.Forms.Label lbl_date;
        private System.Windows.Forms.TabControl tabs_Location;
        private System.Windows.Forms.TabPage tab_Info;
        private System.Windows.Forms.TabPage tab_Occupants;
        private System.Windows.Forms.Panel panel_View;
        private System.Windows.Forms.TabPage tab_Inventory;
        private System.Windows.Forms.FlowLayoutPanel flow;
        private System.Windows.Forms.FlowLayoutPanel flow_objects;
        private System.Windows.Forms.SplitContainer split;
    }
}

