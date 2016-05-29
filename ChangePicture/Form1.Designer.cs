namespace ChangePicture
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.btnApply = new System.Windows.Forms.Button();
            this.pbPicture = new System.Windows.Forms.PictureBox();
            this.labLoading = new System.Windows.Forms.Label();
            this.labSetting = new System.Windows.Forms.Label();
            this.nfiTips = new System.Windows.Forms.NotifyIcon(this.components);
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmExit = new System.Windows.Forms.ToolStripMenuItem();
            this.tmClose = new System.Windows.Forms.Timer(this.components);
            this.txtPath = new System.Windows.Forms.TextBox();
            this.cbSaveAs = new System.Windows.Forms.CheckBox();
            this.txtFold = new System.Windows.Forms.TextBox();
            this.btnEdit = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.cbAutoRun = new System.Windows.Forms.CheckBox();
            this.fbdFold = new System.Windows.Forms.FolderBrowserDialog();
            this.labweb = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pbPicture)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnApply
            // 
            this.btnApply.Enabled = false;
            this.btnApply.Font = new System.Drawing.Font("宋体", 12F);
            this.btnApply.Location = new System.Drawing.Point(286, 38);
            this.btnApply.Name = "btnApply";
            this.btnApply.Size = new System.Drawing.Size(191, 57);
            this.btnApply.TabIndex = 1;
            this.btnApply.Text = "加载中";
            this.btnApply.UseVisualStyleBackColor = true;
            this.btnApply.Click += new System.EventHandler(this.btnApply_Click);
            // 
            // pbPicture
            // 
            this.pbPicture.Location = new System.Drawing.Point(12, 13);
            this.pbPicture.Name = "pbPicture";
            this.pbPicture.Size = new System.Drawing.Size(240, 180);
            this.pbPicture.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbPicture.TabIndex = 3;
            this.pbPicture.TabStop = false;
            // 
            // labLoading
            // 
            this.labLoading.AutoSize = true;
            this.labLoading.Font = new System.Drawing.Font("宋体", 14F);
            this.labLoading.Location = new System.Drawing.Point(78, 91);
            this.labLoading.Name = "labLoading";
            this.labLoading.Size = new System.Drawing.Size(130, 24);
            this.labLoading.TabIndex = 4;
            this.labLoading.Text = "Loading...";
            // 
            // labSetting
            // 
            this.labSetting.AutoSize = true;
            this.labSetting.Font = new System.Drawing.Font("宋体", 12F);
            this.labSetting.ForeColor = System.Drawing.Color.SteelBlue;
            this.labSetting.Location = new System.Drawing.Point(406, 133);
            this.labSetting.Name = "labSetting";
            this.labSetting.Size = new System.Drawing.Size(49, 20);
            this.labSetting.TabIndex = 6;
            this.labSetting.Text = "设置";
            this.labSetting.Click += new System.EventHandler(this.labSetting_Click);
            // 
            // nfiTips
            // 
            this.nfiTips.ContextMenuStrip = this.contextMenuStrip1;
            this.nfiTips.Icon = ((System.Drawing.Icon)(resources.GetObject("nfiTips.Icon")));
            this.nfiTips.Text = "ChangePicture";
            this.nfiTips.Visible = true;
            this.nfiTips.DoubleClick += new System.EventHandler(this.nfiTips_DoubleClick);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmExit});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(115, 30);
            // 
            // tsmExit
            // 
            this.tsmExit.Name = "tsmExit";
            this.tsmExit.Size = new System.Drawing.Size(114, 26);
            this.tsmExit.Text = "退出";
            this.tsmExit.Click += new System.EventHandler(this.tsmExit_Click);
            // 
            // tmClose
            // 
            this.tmClose.Enabled = true;
            this.tmClose.Interval = 10000;
            this.tmClose.Tick += new System.EventHandler(this.tmClose_Tick);
            // 
            // txtPath
            // 
            this.txtPath.Location = new System.Drawing.Point(218, 325);
            this.txtPath.Name = "txtPath";
            this.txtPath.Size = new System.Drawing.Size(257, 25);
            this.txtPath.TabIndex = 14;
            this.txtPath.Text = "yy_mm_dd";
            this.txtPath.Visible = false;
            // 
            // cbSaveAs
            // 
            this.cbSaveAs.AutoSize = true;
            this.cbSaveAs.Font = new System.Drawing.Font("宋体", 12F);
            this.cbSaveAs.Location = new System.Drawing.Point(25, 326);
            this.cbSaveAs.Name = "cbSaveAs";
            this.cbSaveAs.Size = new System.Drawing.Size(171, 24);
            this.cbSaveAs.TabIndex = 13;
            this.cbSaveAs.Text = "按日期保存壁纸";
            this.cbSaveAs.UseVisualStyleBackColor = true;
            this.cbSaveAs.CheckedChanged += new System.EventHandler(this.cbSaveAs_CheckedChanged);
            // 
            // txtFold
            // 
            this.txtFold.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.txtFold.Location = new System.Drawing.Point(25, 248);
            this.txtFold.Name = "txtFold";
            this.txtFold.ReadOnly = true;
            this.txtFold.Size = new System.Drawing.Size(450, 25);
            this.txtFold.TabIndex = 12;
            // 
            // btnEdit
            // 
            this.btnEdit.Location = new System.Drawing.Point(189, 208);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(75, 34);
            this.btnEdit.TabIndex = 11;
            this.btnEdit.Text = "修改";
            this.btnEdit.UseVisualStyleBackColor = true;
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 12F);
            this.label1.Location = new System.Drawing.Point(21, 213);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(149, 20);
            this.label1.TabIndex = 10;
            this.label1.Text = "图片保存地址：";
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("宋体", 12F);
            this.button1.Location = new System.Drawing.Point(24, 372);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(139, 51);
            this.button1.TabIndex = 9;
            this.button1.Text = "保存";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // cbAutoRun
            // 
            this.cbAutoRun.AutoSize = true;
            this.cbAutoRun.Font = new System.Drawing.Font("宋体", 12F);
            this.cbAutoRun.Location = new System.Drawing.Point(25, 291);
            this.cbAutoRun.Name = "cbAutoRun";
            this.cbAutoRun.Size = new System.Drawing.Size(191, 24);
            this.cbAutoRun.TabIndex = 8;
            this.cbAutoRun.Text = "开机自动更换壁纸";
            this.cbAutoRun.UseVisualStyleBackColor = true;
            // 
            // labweb
            // 
            this.labweb.AutoSize = true;
            this.labweb.ForeColor = System.Drawing.Color.DarkSeaGreen;
            this.labweb.Location = new System.Drawing.Point(407, 181);
            this.labweb.Name = "labweb";
            this.labweb.Size = new System.Drawing.Size(103, 15);
            this.labweb.TabIndex = 15;
            this.labweb.Text = "blazenur.com";
            this.labweb.Click += new System.EventHandler(this.labweb_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(513, 205);
            this.Controls.Add(this.labweb);
            this.Controls.Add(this.txtPath);
            this.Controls.Add(this.cbSaveAs);
            this.Controls.Add(this.txtFold);
            this.Controls.Add(this.btnEdit);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.cbAutoRun);
            this.Controls.Add(this.labSetting);
            this.Controls.Add(this.btnApply);
            this.Controls.Add(this.labLoading);
            this.Controls.Add(this.pbPicture);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form1";
            this.Text = "ChangePicture:必应壁纸自动更换V0.3";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pbPicture)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnApply;
        private System.Windows.Forms.PictureBox pbPicture;
        private System.Windows.Forms.Label labLoading;
        private System.Windows.Forms.Label labSetting;
        private System.Windows.Forms.NotifyIcon nfiTips;
        private System.Windows.Forms.Timer tmClose;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem tsmExit;
        private System.Windows.Forms.TextBox txtPath;
        private System.Windows.Forms.CheckBox cbSaveAs;
        private System.Windows.Forms.TextBox txtFold;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.CheckBox cbAutoRun;
        private System.Windows.Forms.FolderBrowserDialog fbdFold;
        private System.Windows.Forms.Label labweb;
    }
}

