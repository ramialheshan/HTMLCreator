namespace gifTrain
{
    partial class Form1
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
            this.OpenButton = new System.Windows.Forms.Button();
            this.ImageGIF = new System.Windows.Forms.PictureBox();
            this.saveButton = new System.Windows.Forms.Button();
            this.CreateGIF = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.ImageGIF)).BeginInit();
            this.SuspendLayout();
            // 
            // OpenButton
            // 
            this.OpenButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.OpenButton.Location = new System.Drawing.Point(18, 12);
            this.OpenButton.Name = "OpenButton";
            this.OpenButton.Size = new System.Drawing.Size(63, 31);
            this.OpenButton.TabIndex = 0;
            this.OpenButton.Text = "Open";
            this.OpenButton.UseVisualStyleBackColor = true;
            this.OpenButton.Click += new System.EventHandler(this.OpenButton_Click);
            // 
            // ImageGIF
            // 
            this.ImageGIF.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.ImageGIF.Location = new System.Drawing.Point(104, 12);
            this.ImageGIF.Name = "ImageGIF";
            this.ImageGIF.Size = new System.Drawing.Size(386, 265);
            this.ImageGIF.TabIndex = 1;
            this.ImageGIF.TabStop = false;
            // 
            // saveButton
            // 
            this.saveButton.Location = new System.Drawing.Point(18, 49);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(63, 31);
            this.saveButton.TabIndex = 2;
            this.saveButton.Text = "Save gif";
            this.saveButton.UseVisualStyleBackColor = true;
            this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
            // 
            // CreateGIF
            // 
            this.CreateGIF.Location = new System.Drawing.Point(18, 86);
            this.CreateGIF.Name = "CreateGIF";
            this.CreateGIF.Size = new System.Drawing.Size(63, 31);
            this.CreateGIF.TabIndex = 2;
            this.CreateGIF.Text = "Create gif";
            this.CreateGIF.UseVisualStyleBackColor = true;
            this.CreateGIF.Click += new System.EventHandler(this.CreateGIF_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(502, 289);
            this.Controls.Add(this.CreateGIF);
            this.Controls.Add(this.saveButton);
            this.Controls.Add(this.ImageGIF);
            this.Controls.Add(this.OpenButton);
            this.KeyPreview = true;
            this.Name = "Form1";
            this.Text = "Обработка формата gif";
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyUp);
            ((System.ComponentModel.ISupportInitialize)(this.ImageGIF)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button OpenButton;
        private System.Windows.Forms.PictureBox ImageGIF;
        private System.Windows.Forms.Button saveButton;
        private System.Windows.Forms.Button CreateGIF;
    }
}

