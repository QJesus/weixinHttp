namespace demo_win_httpsocket
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.picErWeiMa = new System.Windows.Forms.PictureBox();
            this.txtBoxMessage = new System.Windows.Forms.TextBox();
            this.btnSendFile = new System.Windows.Forms.Button();
            this.btnSend = new System.Windows.Forms.Button();
            this.lstBoxUser = new System.Windows.Forms.ListBox();
            this.lstMessage = new System.Windows.Forms.ListBox();
            ((System.ComponentModel.ISupportInitialize)(this.picErWeiMa)).BeginInit();
            this.SuspendLayout();
            // 
            // picErWeiMa
            // 
            this.picErWeiMa.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.picErWeiMa.Location = new System.Drawing.Point(208, 3);
            this.picErWeiMa.Name = "picErWeiMa";
            this.picErWeiMa.Size = new System.Drawing.Size(300, 300);
            this.picErWeiMa.TabIndex = 0;
            this.picErWeiMa.TabStop = false;
            // 
            // txtBoxMessage
            // 
            this.txtBoxMessage.Location = new System.Drawing.Point(308, 3);
            this.txtBoxMessage.Multiline = true;
            this.txtBoxMessage.Name = "txtBoxMessage";
            this.txtBoxMessage.Size = new System.Drawing.Size(430, 273);
            this.txtBoxMessage.TabIndex = 12;
            this.txtBoxMessage.Visible = false;
            this.txtBoxMessage.KeyUp += new System.Windows.Forms.KeyEventHandler(this.TxtBoxMessage_KeyUp);
            // 
            // btnSendFile
            // 
            this.btnSendFile.Location = new System.Drawing.Point(561, 280);
            this.btnSendFile.Name = "btnSendFile";
            this.btnSendFile.Size = new System.Drawing.Size(75, 23);
            this.btnSendFile.TabIndex = 10;
            this.btnSendFile.Text = "发送文件";
            this.btnSendFile.UseVisualStyleBackColor = true;
            this.btnSendFile.Visible = false;
            this.btnSendFile.Click += new System.EventHandler(this.BtnSendFile_Click);
            // 
            // btnSend
            // 
            this.btnSend.Location = new System.Drawing.Point(662, 280);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(75, 23);
            this.btnSend.TabIndex = 11;
            this.btnSend.Text = "发送信息";
            this.btnSend.UseVisualStyleBackColor = true;
            this.btnSend.Visible = false;
            this.btnSend.Click += new System.EventHandler(this.BtnSend_Click);
            // 
            // lstBoxUser
            // 
            this.lstBoxUser.FormattingEnabled = true;
            this.lstBoxUser.ItemHeight = 12;
            this.lstBoxUser.Location = new System.Drawing.Point(3, 3);
            this.lstBoxUser.Name = "lstBoxUser";
            this.lstBoxUser.ScrollAlwaysVisible = true;
            this.lstBoxUser.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.lstBoxUser.Size = new System.Drawing.Size(302, 304);
            this.lstBoxUser.TabIndex = 8;
            this.lstBoxUser.Visible = false;
            // 
            // lstMessage
            // 
            this.lstMessage.FormattingEnabled = true;
            this.lstMessage.HorizontalScrollbar = true;
            this.lstMessage.ItemHeight = 12;
            this.lstMessage.Location = new System.Drawing.Point(3, 311);
            this.lstMessage.Name = "lstMessage";
            this.lstMessage.ScrollAlwaysVisible = true;
            this.lstMessage.Size = new System.Drawing.Size(736, 232);
            this.lstMessage.TabIndex = 7;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(740, 495);
            this.Controls.Add(this.picErWeiMa);
            this.Controls.Add(this.txtBoxMessage);
            this.Controls.Add(this.btnSendFile);
            this.Controls.Add(this.btnSend);
            this.Controls.Add(this.lstBoxUser);
            this.Controls.Add(this.lstMessage);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "微信客户端";
            ((System.ComponentModel.ISupportInitialize)(this.picErWeiMa)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox picErWeiMa;
        private System.Windows.Forms.TextBox txtBoxMessage;
        private System.Windows.Forms.Button btnSendFile;
        private System.Windows.Forms.Button btnSend;
        private System.Windows.Forms.ListBox lstBoxUser;
        private System.Windows.Forms.ListBox lstMessage;
    }
}

