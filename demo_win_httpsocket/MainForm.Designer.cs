﻿namespace demo_win_httpsocket
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
            this.picErWeiMa.Location = new System.Drawing.Point(476, 191);
            this.picErWeiMa.Margin = new System.Windows.Forms.Padding(6);
            this.picErWeiMa.Name = "picErWeiMa";
            this.picErWeiMa.Size = new System.Drawing.Size(600, 625);
            this.picErWeiMa.TabIndex = 0;
            this.picErWeiMa.TabStop = false;
            // 
            // txtBoxMessage
            // 
            this.txtBoxMessage.Location = new System.Drawing.Point(682, 6);
            this.txtBoxMessage.Margin = new System.Windows.Forms.Padding(6);
            this.txtBoxMessage.Multiline = true;
            this.txtBoxMessage.Name = "txtBoxMessage";
            this.txtBoxMessage.Size = new System.Drawing.Size(1554, 489);
            this.txtBoxMessage.TabIndex = 12;
            this.txtBoxMessage.Visible = false;
            this.txtBoxMessage.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtBoxMessage_KeyUp);
            // 
            // btnSendFile
            // 
            this.btnSendFile.Location = new System.Drawing.Point(1850, 513);
            this.btnSendFile.Margin = new System.Windows.Forms.Padding(6);
            this.btnSendFile.Name = "btnSendFile";
            this.btnSendFile.Size = new System.Drawing.Size(150, 48);
            this.btnSendFile.TabIndex = 10;
            this.btnSendFile.Text = "发送文件";
            this.btnSendFile.UseVisualStyleBackColor = true;
            this.btnSendFile.Visible = false;
            this.btnSendFile.Click += new System.EventHandler(this.btnSendFile_Click);
            // 
            // btnSend
            // 
            this.btnSend.Location = new System.Drawing.Point(2090, 513);
            this.btnSend.Margin = new System.Windows.Forms.Padding(6);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(150, 48);
            this.btnSend.TabIndex = 11;
            this.btnSend.Text = "发送信息";
            this.btnSend.UseVisualStyleBackColor = true;
            this.btnSend.Visible = false;
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
            // 
            // lstBoxUser
            // 
            this.lstBoxUser.FormattingEnabled = true;
            this.lstBoxUser.ItemHeight = 25;
            this.lstBoxUser.Location = new System.Drawing.Point(6, 6);
            this.lstBoxUser.Margin = new System.Windows.Forms.Padding(6);
            this.lstBoxUser.Name = "lstBoxUser";
            this.lstBoxUser.ScrollAlwaysVisible = true;
            this.lstBoxUser.Size = new System.Drawing.Size(658, 554);
            this.lstBoxUser.TabIndex = 8;
            this.lstBoxUser.Visible = false;
            // 
            // lstMessage
            // 
            this.lstMessage.FormattingEnabled = true;
            this.lstMessage.HorizontalScrollbar = true;
            this.lstMessage.ItemHeight = 25;
            this.lstMessage.Location = new System.Drawing.Point(6, 573);
            this.lstMessage.Margin = new System.Windows.Forms.Padding(6);
            this.lstMessage.Name = "lstMessage";
            this.lstMessage.ScrollAlwaysVisible = true;
            this.lstMessage.Size = new System.Drawing.Size(2230, 479);
            this.lstMessage.TabIndex = 7;
            this.lstMessage.Visible = false;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(2248, 1063);
            this.Controls.Add(this.picErWeiMa);
            this.Controls.Add(this.txtBoxMessage);
            this.Controls.Add(this.btnSendFile);
            this.Controls.Add(this.btnSend);
            this.Controls.Add(this.lstBoxUser);
            this.Controls.Add(this.lstMessage);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Margin = new System.Windows.Forms.Padding(6);
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
