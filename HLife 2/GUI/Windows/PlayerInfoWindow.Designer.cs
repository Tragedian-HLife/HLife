namespace HLife_2
{
    partial class PlayerInfoWindow
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PlayerInfoWindow));
            this.tabs_Container = new System.Windows.Forms.TabControl();
            this.tab_playerStats = new System.Windows.Forms.TabPage();
            this.tab_attributes = new System.Windows.Forms.TabPage();
            this.tab_inventory = new System.Windows.Forms.TabPage();
            this.flow_inventory = new System.Windows.Forms.FlowLayoutPanel();
            this.pic_avatar = new System.Windows.Forms.PictureBox();
            this.tabs_Container.SuspendLayout();
            this.tab_inventory.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pic_avatar)).BeginInit();
            this.SuspendLayout();
            // 
            // tabs_Container
            // 
            this.tabs_Container.Controls.Add(this.tab_playerStats);
            this.tabs_Container.Controls.Add(this.tab_attributes);
            this.tabs_Container.Controls.Add(this.tab_inventory);
            this.tabs_Container.Location = new System.Drawing.Point(139, 12);
            this.tabs_Container.Name = "tabs_Container";
            this.tabs_Container.SelectedIndex = 0;
            this.tabs_Container.Size = new System.Drawing.Size(321, 427);
            this.tabs_Container.TabIndex = 0;
            // 
            // tab_playerStats
            // 
            this.tab_playerStats.Location = new System.Drawing.Point(4, 22);
            this.tab_playerStats.Name = "tab_playerStats";
            this.tab_playerStats.Padding = new System.Windows.Forms.Padding(3);
            this.tab_playerStats.Size = new System.Drawing.Size(313, 401);
            this.tab_playerStats.TabIndex = 0;
            this.tab_playerStats.Text = "Status";
            this.tab_playerStats.UseVisualStyleBackColor = true;
            // 
            // tab_attributes
            // 
            this.tab_attributes.Location = new System.Drawing.Point(4, 22);
            this.tab_attributes.Name = "tab_attributes";
            this.tab_attributes.Size = new System.Drawing.Size(313, 401);
            this.tab_attributes.TabIndex = 2;
            this.tab_attributes.Text = "Attributes";
            this.tab_attributes.UseVisualStyleBackColor = true;
            // 
            // tab_inventory
            // 
            this.tab_inventory.Controls.Add(this.flow_inventory);
            this.tab_inventory.Location = new System.Drawing.Point(4, 22);
            this.tab_inventory.Name = "tab_inventory";
            this.tab_inventory.Padding = new System.Windows.Forms.Padding(3);
            this.tab_inventory.Size = new System.Drawing.Size(313, 401);
            this.tab_inventory.TabIndex = 1;
            this.tab_inventory.Text = "Inventory";
            this.tab_inventory.UseVisualStyleBackColor = true;
            // 
            // flow_inventory
            // 
            this.flow_inventory.Location = new System.Drawing.Point(3, 6);
            this.flow_inventory.Name = "flow_inventory";
            this.flow_inventory.Size = new System.Drawing.Size(304, 389);
            this.flow_inventory.TabIndex = 0;
            // 
            // pic_avatar
            // 
            this.pic_avatar.Image = ((System.Drawing.Image)(resources.GetObject("pic_avatar.Image")));
            this.pic_avatar.Location = new System.Drawing.Point(12, 12);
            this.pic_avatar.Name = "pic_avatar";
            this.pic_avatar.Size = new System.Drawing.Size(121, 423);
            this.pic_avatar.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pic_avatar.TabIndex = 1;
            this.pic_avatar.TabStop = false;
            // 
            // PlayerInfoWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(472, 451);
            this.Controls.Add(this.pic_avatar);
            this.Controls.Add(this.tabs_Container);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "PlayerInfoWindow";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "Player Information";
            this.Load += new System.EventHandler(this.PlayerInfoWindow_Load);
            this.tabs_Container.ResumeLayout(false);
            this.tab_inventory.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pic_avatar)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabs_Container;
        private System.Windows.Forms.TabPage tab_playerStats;
        private System.Windows.Forms.TabPage tab_inventory;
        private System.Windows.Forms.TabPage tab_attributes;
        private System.Windows.Forms.PictureBox pic_avatar;
        private System.Windows.Forms.FlowLayoutPanel flow_inventory;
    }
}