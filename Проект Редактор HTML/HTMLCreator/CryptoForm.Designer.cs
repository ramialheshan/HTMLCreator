namespace HTMLCreator
{
    partial class CryptoForm
    {
        /// <summary>
        /// Требуется переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Обязательный метод для поддержки конструктора - не изменяйте
        /// содержимое данного метода при помощи редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CryptoForm));
            this.maskedPassword = new System.Windows.Forms.MaskedTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.EnterButton = new System.Windows.Forms.Button();
            this.userGroupBox = new System.Windows.Forms.GroupBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.userGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // maskedPassword
            // 
            this.maskedPassword.AllowPromptAsInput = false;
            this.maskedPassword.Culture = new System.Globalization.CultureInfo("en-US");
            this.maskedPassword.Location = new System.Drawing.Point(76, 37);
            this.maskedPassword.Name = "maskedPassword";
            this.maskedPassword.PasswordChar = '*';
            this.maskedPassword.PromptChar = ' ';
            this.maskedPassword.ShortcutsEnabled = false;
            this.maskedPassword.Size = new System.Drawing.Size(233, 20);
            this.maskedPassword.TabIndex = 4;
            this.maskedPassword.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.maskedPassword_KeyPress);
            this.maskedPassword.TextChanged += new System.EventHandler(this.maskedPassword_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(73, 21);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(48, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Пароль:";
            // 
            // EnterButton
            // 
            this.EnterButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.EnterButton.Enabled = false;
            this.EnterButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.EnterButton.Location = new System.Drawing.Point(316, 32);
            this.EnterButton.Name = "EnterButton";
            this.EnterButton.Size = new System.Drawing.Size(75, 28);
            this.EnterButton.TabIndex = 3;
            this.EnterButton.Text = "Вход";
            this.EnterButton.UseVisualStyleBackColor = true;
            this.EnterButton.Click += new System.EventHandler(this.EnterButton_Click);
            // 
            // userGroupBox
            // 
            this.userGroupBox.Controls.Add(this.EnterButton);
            this.userGroupBox.Controls.Add(this.maskedPassword);
            this.userGroupBox.Controls.Add(this.label2);
            this.userGroupBox.Controls.Add(this.pictureBox1);
            this.userGroupBox.Location = new System.Drawing.Point(7, 5);
            this.userGroupBox.Name = "userGroupBox";
            this.userGroupBox.Size = new System.Drawing.Size(397, 86);
            this.userGroupBox.TabIndex = 2;
            this.userGroupBox.TabStop = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(11, 17);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(56, 51);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            // 
            // CryptoForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(410, 102);
            this.ControlBox = false;
            this.Controls.Add(this.userGroupBox);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CryptoForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Идентификация пользователя";
            this.TopMost = true;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.CryptoForm_FormClosing);
            this.userGroupBox.ResumeLayout(false);
            this.userGroupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.MaskedTextBox maskedPassword;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button EnterButton;
        private System.Windows.Forms.GroupBox userGroupBox;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}