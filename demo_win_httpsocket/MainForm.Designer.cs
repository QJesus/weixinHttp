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
            this.cbChanged = new System.Windows.Forms.CheckBox();
            this.txtNickName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cbEveryHour = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txt_train_date = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txt_station_train_code = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txt_from_station = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txt_to_station = new System.Windows.Forms.TextBox();
            this.cbStudent = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.picErWeiMa)).BeginInit();
            this.SuspendLayout();
            // 
            // picErWeiMa
            // 
            this.picErWeiMa.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.picErWeiMa.Location = new System.Drawing.Point(416, 6);
            this.picErWeiMa.Margin = new System.Windows.Forms.Padding(6);
            this.picErWeiMa.Name = "picErWeiMa";
            this.picErWeiMa.Size = new System.Drawing.Size(600, 625);
            this.picErWeiMa.TabIndex = 0;
            this.picErWeiMa.TabStop = false;
            // 
            // txtBoxMessage
            // 
            this.txtBoxMessage.Location = new System.Drawing.Point(616, 6);
            this.txtBoxMessage.Margin = new System.Windows.Forms.Padding(6);
            this.txtBoxMessage.Multiline = true;
            this.txtBoxMessage.Name = "txtBoxMessage";
            this.txtBoxMessage.Size = new System.Drawing.Size(856, 564);
            this.txtBoxMessage.TabIndex = 12;
            this.txtBoxMessage.Visible = false;
            this.txtBoxMessage.KeyUp += new System.Windows.Forms.KeyEventHandler(this.TxtBoxMessage_KeyUp);
            // 
            // btnSendFile
            // 
            this.btnSendFile.Location = new System.Drawing.Point(1122, 583);
            this.btnSendFile.Margin = new System.Windows.Forms.Padding(6);
            this.btnSendFile.Name = "btnSendFile";
            this.btnSendFile.Size = new System.Drawing.Size(150, 48);
            this.btnSendFile.TabIndex = 10;
            this.btnSendFile.Text = "发送文件";
            this.btnSendFile.UseVisualStyleBackColor = true;
            this.btnSendFile.Visible = false;
            this.btnSendFile.Click += new System.EventHandler(this.BtnSendFile_Click);
            // 
            // btnSend
            // 
            this.btnSend.Location = new System.Drawing.Point(1324, 583);
            this.btnSend.Margin = new System.Windows.Forms.Padding(6);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(150, 48);
            this.btnSend.TabIndex = 11;
            this.btnSend.Text = "发送信息";
            this.btnSend.UseVisualStyleBackColor = true;
            this.btnSend.Visible = false;
            this.btnSend.Click += new System.EventHandler(this.BtnSend_Click);
            // 
            // lstBoxUser
            // 
            this.lstBoxUser.FormattingEnabled = true;
            this.lstBoxUser.ItemHeight = 25;
            this.lstBoxUser.Location = new System.Drawing.Point(6, 6);
            this.lstBoxUser.Margin = new System.Windows.Forms.Padding(6);
            this.lstBoxUser.Name = "lstBoxUser";
            this.lstBoxUser.ScrollAlwaysVisible = true;
            this.lstBoxUser.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.lstBoxUser.Size = new System.Drawing.Size(600, 629);
            this.lstBoxUser.TabIndex = 8;
            this.lstBoxUser.Visible = false;
            // 
            // lstMessage
            // 
            this.lstMessage.FormattingEnabled = true;
            this.lstMessage.HorizontalScrollbar = true;
            this.lstMessage.ItemHeight = 25;
            this.lstMessage.Location = new System.Drawing.Point(6, 648);
            this.lstMessage.Margin = new System.Windows.Forms.Padding(6);
            this.lstMessage.Name = "lstMessage";
            this.lstMessage.ScrollAlwaysVisible = true;
            this.lstMessage.Size = new System.Drawing.Size(1468, 479);
            this.lstMessage.TabIndex = 7;
            // 
            // cbChanged
            // 
            this.cbChanged.AutoSize = true;
            this.cbChanged.Checked = true;
            this.cbChanged.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbChanged.Location = new System.Drawing.Point(1481, 204);
            this.cbChanged.Name = "cbChanged";
            this.cbChanged.Size = new System.Drawing.Size(170, 29);
            this.cbChanged.TabIndex = 13;
            this.cbChanged.Text = "有变更再通知";
            this.cbChanged.UseVisualStyleBackColor = true;
            // 
            // txtNickName
            // 
            this.txtNickName.Location = new System.Drawing.Point(1637, 155);
            this.txtNickName.Name = "txtNickName";
            this.txtNickName.Size = new System.Drawing.Size(262, 31);
            this.txtNickName.TabIndex = 14;
            this.txtNickName.Text = "月半";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(1481, 158);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(138, 25);
            this.label1.TabIndex = 15;
            this.label1.Text = "通知人昵称：";
            // 
            // cbEveryHour
            // 
            this.cbEveryHour.AutoSize = true;
            this.cbEveryHour.Checked = true;
            this.cbEveryHour.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbEveryHour.Location = new System.Drawing.Point(1481, 248);
            this.cbEveryHour.Name = "cbEveryHour";
            this.cbEveryHour.Size = new System.Drawing.Size(359, 29);
            this.cbEveryHour.TabIndex = 16;
            this.cbEveryHour.Text = "一小时通知一次（无论是否变更）";
            this.cbEveryHour.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(1481, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(117, 25);
            this.label2.TabIndex = 18;
            this.label2.Text = "发车日期：";
            // 
            // txt_train_date
            // 
            this.txt_train_date.Location = new System.Drawing.Point(1637, 6);
            this.txt_train_date.Name = "txt_train_date";
            this.txt_train_date.Size = new System.Drawing.Size(262, 31);
            this.txt_train_date.TabIndex = 17;
            this.txt_train_date.Text = "2019-10-01";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(1481, 46);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(75, 25);
            this.label3.TabIndex = 20;
            this.label3.Text = "车次：";
            // 
            // txt_station_train_code
            // 
            this.txt_station_train_code.Location = new System.Drawing.Point(1637, 43);
            this.txt_station_train_code.Name = "txt_station_train_code";
            this.txt_station_train_code.Size = new System.Drawing.Size(262, 31);
            this.txt_station_train_code.TabIndex = 19;
            this.txt_station_train_code.Text = "G89";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(1481, 83);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(117, 25);
            this.label4.TabIndex = 22;
            this.label4.Text = "上车车站：";
            // 
            // txt_from_station
            // 
            this.txt_from_station.Location = new System.Drawing.Point(1637, 80);
            this.txt_from_station.Name = "txt_from_station";
            this.txt_from_station.Size = new System.Drawing.Size(262, 31);
            this.txt_from_station.TabIndex = 21;
            this.txt_from_station.Text = "BXP";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(1481, 120);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(117, 25);
            this.label5.TabIndex = 24;
            this.label5.Text = "下车车站：";
            // 
            // txt_to_station
            // 
            this.txt_to_station.Location = new System.Drawing.Point(1637, 117);
            this.txt_to_station.Name = "txt_to_station";
            this.txt_to_station.Size = new System.Drawing.Size(262, 31);
            this.txt_to_station.TabIndex = 23;
            this.txt_to_station.Text = "ICW";
            // 
            // cbStudent
            // 
            this.cbStudent.AutoSize = true;
            this.cbStudent.Location = new System.Drawing.Point(1717, 204);
            this.cbStudent.Name = "cbStudent";
            this.cbStudent.Size = new System.Drawing.Size(107, 29);
            this.cbStudent.TabIndex = 25;
            this.cbStudent.Text = "学生票";
            this.cbStudent.UseVisualStyleBackColor = true;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(1912, 1031);
            this.Controls.Add(this.cbStudent);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txt_to_station);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txt_from_station);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txt_station_train_code);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txt_train_date);
            this.Controls.Add(this.cbEveryHour);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtNickName);
            this.Controls.Add(this.cbChanged);
            this.Controls.Add(this.picErWeiMa);
            this.Controls.Add(this.txtBoxMessage);
            this.Controls.Add(this.btnSendFile);
            this.Controls.Add(this.btnSend);
            this.Controls.Add(this.lstBoxUser);
            this.Controls.Add(this.lstMessage);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
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
        private System.Windows.Forms.CheckBox cbChanged;
        private System.Windows.Forms.TextBox txtNickName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox cbEveryHour;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txt_train_date;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txt_station_train_code;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txt_from_station;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txt_to_station;
        private System.Windows.Forms.CheckBox cbStudent;
    }
}

