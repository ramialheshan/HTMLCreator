namespace HTMLCreator
{
    partial class editorHTMLForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(editorHTMLForm));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.ListViewGroup listViewGroup1 = new System.Windows.Forms.ListViewGroup("Опции датчиков", System.Windows.Forms.HorizontalAlignment.Left);
            System.Windows.Forms.ListViewItem listViewItem1 = new System.Windows.Forms.ListViewItem("");
            System.Windows.Forms.ListViewGroup listViewGroup2 = new System.Windows.Forms.ListViewGroup("Опции HTML мнемосхемы", System.Windows.Forms.HorizontalAlignment.Left);
            System.Windows.Forms.ListViewItem listViewItem2 = new System.Windows.Forms.ListViewItem("");
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.HTMLBrowser = new System.Windows.Forms.WebBrowser();
            this.StartPanel = new System.Windows.Forms.Panel();
            this.DownLoadLabel = new System.Windows.Forms.LinkLabel();
            this.CreateLabel = new System.Windows.Forms.LinkLabel();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.SettingHTMLGroup = new System.Windows.Forms.GroupBox();
            this.HelpButton = new System.Windows.Forms.Label();
            this.CloseHTMLButton = new System.Windows.Forms.Label();
            this.sensorPanel = new System.Windows.Forms.Panel();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.editorLabel = new System.Windows.Forms.Label();
            this.sensorProperty = new System.Windows.Forms.PropertyGrid();
            this.toolStrip2 = new System.Windows.Forms.ToolStrip();
            this.addSensorButton = new System.Windows.Forms.ToolStripButton();
            this.removeSensorButton = new System.Windows.Forms.ToolStripButton();
            this.CloneSensorButton = new System.Windows.Forms.ToolStripButton();
            this.ListSensorGrid = new System.Windows.Forms.DataGridView();
            this.NumSensor = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Situation = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ImageSensor = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.listView2 = new System.Windows.Forms.ListView();
            this.SaveHTMLButton = new System.Windows.Forms.Button();
            this.LoadMainImageButton = new System.Windows.Forms.Button();
            this.ImagePathBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.listView1 = new System.Windows.Forms.ListView();
            this.toolsHTMLPanel = new System.Windows.Forms.ToolStrip();
            this.CreateHTMLButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripSeparator();
            this.DownLoadHTMLButton = new System.Windows.Forms.ToolStripButton();
            this.SettingsButton = new System.Windows.Forms.ToolStripButton();
            this.AdressStripLabel = new System.Windows.Forms.ToolStripLabel();
            this.saveHTMLFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.openHTMLDialog = new System.Windows.Forms.OpenFileDialog();
            this.AdressHTMLBox = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            this.StartPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SettingHTMLGroup.SuspendLayout();
            this.sensorPanel.SuspendLayout();
            this.toolStrip2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ListSensorGrid)).BeginInit();
            this.toolsHTMLPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.BackColor = System.Drawing.SystemColors.Control;
            this.groupBox1.Controls.Add(this.HTMLBrowser);
            this.groupBox1.Controls.Add(this.StartPanel);
            this.groupBox1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.groupBox1.Location = new System.Drawing.Point(4, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(624, 564);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Окно редактора HTML мнемосхемы";
            // 
            // HTMLBrowser
            // 
            this.HTMLBrowser.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.HTMLBrowser.IsWebBrowserContextMenuEnabled = false;
            this.HTMLBrowser.Location = new System.Drawing.Point(6, 19);
            this.HTMLBrowser.Margin = new System.Windows.Forms.Padding(0);
            this.HTMLBrowser.MinimumSize = new System.Drawing.Size(20, 20);
            this.HTMLBrowser.Name = "HTMLBrowser";
            this.HTMLBrowser.Size = new System.Drawing.Size(615, 534);
            this.HTMLBrowser.TabIndex = 1;
            this.HTMLBrowser.Url = new System.Uri("", System.UriKind.Relative);
            this.HTMLBrowser.Visible = false;
            this.HTMLBrowser.Resize += new System.EventHandler(this.HTMLBrowser_Resize);
            this.HTMLBrowser.DocumentCompleted += new System.Windows.Forms.WebBrowserDocumentCompletedEventHandler(this.HTMLBrowser_DocumentCompleted);
            this.HTMLBrowser.VisibleChanged += new System.EventHandler(this.HTMLBrowser_VisibleChanged);
            // 
            // StartPanel
            // 
            this.StartPanel.Controls.Add(this.DownLoadLabel);
            this.StartPanel.Controls.Add(this.CreateLabel);
            this.StartPanel.Controls.Add(this.label3);
            this.StartPanel.Controls.Add(this.label4);
            this.StartPanel.Controls.Add(this.label2);
            this.StartPanel.Controls.Add(this.pictureBox1);
            this.StartPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.StartPanel.Location = new System.Drawing.Point(3, 16);
            this.StartPanel.Name = "StartPanel";
            this.StartPanel.Size = new System.Drawing.Size(618, 545);
            this.StartPanel.TabIndex = 3;
            // 
            // DownLoadLabel
            // 
            this.DownLoadLabel.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.DownLoadLabel.AutoSize = true;
            this.DownLoadLabel.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.DownLoadLabel.Location = new System.Drawing.Point(303, 389);
            this.DownLoadLabel.Name = "DownLoadLabel";
            this.DownLoadLabel.Size = new System.Drawing.Size(107, 24);
            this.DownLoadLabel.TabIndex = 2;
            this.DownLoadLabel.TabStop = true;
            this.DownLoadLabel.Text = "Загрузить";
            this.DownLoadLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.DownLoadLabel_LinkClicked);
            // 
            // CreateLabel
            // 
            this.CreateLabel.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.CreateLabel.AutoSize = true;
            this.CreateLabel.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.CreateLabel.Location = new System.Drawing.Point(177, 389);
            this.CreateLabel.Name = "CreateLabel";
            this.CreateLabel.Size = new System.Drawing.Size(92, 24);
            this.CreateLabel.TabIndex = 2;
            this.CreateLabel.TabStop = true;
            this.CreateLabel.Text = "Создать";
            this.CreateLabel.Click += new System.EventHandler(this.CreateLabel_Click);
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label3.Location = new System.Drawing.Point(266, 389);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(43, 24);
            this.label3.TabIndex = 1;
            this.label3.Text = "или";
            // 
            // label4
            // 
            this.label4.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label4.Location = new System.Drawing.Point(405, 389);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(161, 24);
            this.label4.TabIndex = 1;
            this.label4.Text = "HTML документ";
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.Location = new System.Drawing.Point(53, 389);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(130, 24);
            this.label2.TabIndex = 1;
            this.label2.Text = "Необходимо";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(166, 108);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(302, 265);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // SettingHTMLGroup
            // 
            this.SettingHTMLGroup.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.SettingHTMLGroup.BackColor = System.Drawing.SystemColors.Control;
            this.SettingHTMLGroup.Controls.Add(this.HelpButton);
            this.SettingHTMLGroup.Controls.Add(this.CloseHTMLButton);
            this.SettingHTMLGroup.Controls.Add(this.sensorPanel);
            this.SettingHTMLGroup.Controls.Add(this.SaveHTMLButton);
            this.SettingHTMLGroup.Controls.Add(this.LoadMainImageButton);
            this.SettingHTMLGroup.Controls.Add(this.ImagePathBox);
            this.SettingHTMLGroup.Controls.Add(this.label1);
            this.SettingHTMLGroup.Controls.Add(this.listView1);
            this.SettingHTMLGroup.Enabled = false;
            this.SettingHTMLGroup.Location = new System.Drawing.Point(634, 3);
            this.SettingHTMLGroup.Name = "SettingHTMLGroup";
            this.SettingHTMLGroup.Size = new System.Drawing.Size(303, 564);
            this.SettingHTMLGroup.TabIndex = 1;
            this.SettingHTMLGroup.TabStop = false;
            this.SettingHTMLGroup.Text = "Опции настройки HTML мнемосхемы";
            // 
            // HelpButton
            // 
            this.HelpButton.Image = ((System.Drawing.Image)(resources.GetObject("HelpButton.Image")));
            this.HelpButton.Location = new System.Drawing.Point(249, 10);
            this.HelpButton.Name = "HelpButton";
            this.HelpButton.Size = new System.Drawing.Size(25, 24);
            this.HelpButton.TabIndex = 9;
            this.HelpButton.Click += new System.EventHandler(this.HelpButton_Click);
            // 
            // CloseHTMLButton
            // 
            this.CloseHTMLButton.Image = ((System.Drawing.Image)(resources.GetObject("CloseHTMLButton.Image")));
            this.CloseHTMLButton.Location = new System.Drawing.Point(273, 10);
            this.CloseHTMLButton.Name = "CloseHTMLButton";
            this.CloseHTMLButton.Size = new System.Drawing.Size(25, 24);
            this.CloseHTMLButton.TabIndex = 9;
            this.CloseHTMLButton.Click += new System.EventHandler(this.CloseHTMLButton_Click);
            // 
            // sensorPanel
            // 
            this.sensorPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.sensorPanel.Controls.Add(this.groupBox2);
            this.sensorPanel.Controls.Add(this.editorLabel);
            this.sensorPanel.Controls.Add(this.sensorProperty);
            this.sensorPanel.Controls.Add(this.toolStrip2);
            this.sensorPanel.Controls.Add(this.ListSensorGrid);
            this.sensorPanel.Controls.Add(this.listView2);
            this.sensorPanel.Enabled = false;
            this.sensorPanel.Location = new System.Drawing.Point(2, 87);
            this.sensorPanel.Name = "sensorPanel";
            this.sensorPanel.Size = new System.Drawing.Size(296, 417);
            this.sensorPanel.TabIndex = 3;
            this.sensorPanel.EnabledChanged += new System.EventHandler(this.sensorPanel_EnabledChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Location = new System.Drawing.Point(113, 172);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(185, 2);
            this.groupBox2.TabIndex = 15;
            this.groupBox2.TabStop = false;
            // 
            // editorLabel
            // 
            this.editorLabel.AutoSize = true;
            this.editorLabel.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.editorLabel.Location = new System.Drawing.Point(8, 165);
            this.editorLabel.Name = "editorLabel";
            this.editorLabel.Size = new System.Drawing.Size(102, 13);
            this.editorLabel.TabIndex = 14;
            this.editorLabel.Text = "Редактор датчика";
            // 
            // sensorProperty
            // 
            this.sensorProperty.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.sensorProperty.Enabled = false;
            this.sensorProperty.Location = new System.Drawing.Point(10, 185);
            this.sensorProperty.Name = "sensorProperty";
            this.sensorProperty.Size = new System.Drawing.Size(283, 229);
            this.sensorProperty.TabIndex = 10;
            this.sensorProperty.ToolbarVisible = false;
            this.sensorProperty.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.sensorProperty_PropertyValueChanged);
            // 
            // toolStrip2
            // 
            this.toolStrip2.CanOverflow = false;
            this.toolStrip2.Dock = System.Windows.Forms.DockStyle.None;
            this.toolStrip2.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addSensorButton,
            this.removeSensorButton,
            this.CloneSensorButton});
            this.toolStrip2.Location = new System.Drawing.Point(10, 37);
            this.toolStrip2.Name = "toolStrip2";
            this.toolStrip2.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolStrip2.Size = new System.Drawing.Size(277, 25);
            this.toolStrip2.TabIndex = 13;
            // 
            // addSensorButton
            // 
            this.addSensorButton.Image = ((System.Drawing.Image)(resources.GetObject("addSensorButton.Image")));
            this.addSensorButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.addSensorButton.Name = "addSensorButton";
            this.addSensorButton.Size = new System.Drawing.Size(77, 22);
            this.addSensorButton.Text = "Добавить";
            this.addSensorButton.ToolTipText = "Добавить датчик";
            this.addSensorButton.Click += new System.EventHandler(this.addSensorButton_Click);
            // 
            // removeSensorButton
            // 
            this.removeSensorButton.Enabled = false;
            this.removeSensorButton.Image = ((System.Drawing.Image)(resources.GetObject("removeSensorButton.Image")));
            this.removeSensorButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.removeSensorButton.Name = "removeSensorButton";
            this.removeSensorButton.Size = new System.Drawing.Size(71, 22);
            this.removeSensorButton.Text = "Удалить";
            this.removeSensorButton.ToolTipText = "Удалить датчик";
            this.removeSensorButton.Click += new System.EventHandler(this.removeSensorButton_Click);
            // 
            // CloneSensorButton
            // 
            this.CloneSensorButton.Enabled = false;
            this.CloneSensorButton.Image = ((System.Drawing.Image)(resources.GetObject("CloneSensorButton.Image")));
            this.CloneSensorButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.CloneSensorButton.Name = "CloneSensorButton";
            this.CloneSensorButton.Size = new System.Drawing.Size(95, 22);
            this.CloneSensorButton.Text = "Дублировать";
            this.CloneSensorButton.Click += new System.EventHandler(this.CloneSensorButton_Click);
            // 
            // ListSensorGrid
            // 
            this.ListSensorGrid.AllowUserToAddRows = false;
            this.ListSensorGrid.AllowUserToDeleteRows = false;
            this.ListSensorGrid.AllowUserToResizeColumns = false;
            this.ListSensorGrid.AllowUserToResizeRows = false;
            this.ListSensorGrid.BackgroundColor = System.Drawing.Color.White;
            this.ListSensorGrid.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.ListSensorGrid.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.EnableWithoutHeaderText;
            this.ListSensorGrid.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.ListSensorGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.ListSensorGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.NumSensor,
            this.Situation,
            this.ImageSensor});
            this.ListSensorGrid.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.ListSensorGrid.Enabled = false;
            this.ListSensorGrid.Location = new System.Drawing.Point(8, 65);
            this.ListSensorGrid.MultiSelect = false;
            this.ListSensorGrid.Name = "ListSensorGrid";
            this.ListSensorGrid.ReadOnly = true;
            this.ListSensorGrid.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.ListSensorGrid.RowHeadersVisible = false;
            this.ListSensorGrid.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.ListSensorGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.ListSensorGrid.ShowEditingIcon = false;
            this.ListSensorGrid.Size = new System.Drawing.Size(285, 91);
            this.ListSensorGrid.TabIndex = 12;
            this.ListSensorGrid.TabStop = false;
            this.ListSensorGrid.RowsAdded += new System.Windows.Forms.DataGridViewRowsAddedEventHandler(this.ListSensorGrid_RowsAdded);
            this.ListSensorGrid.RowsRemoved += new System.Windows.Forms.DataGridViewRowsRemovedEventHandler(this.ListSensorGrid_RowsRemoved);
            this.ListSensorGrid.SelectionChanged += new System.EventHandler(this.ListSensorGrid_SelectionChanged);
            // 
            // NumSensor
            // 
            this.NumSensor.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.NumSensor.DefaultCellStyle = dataGridViewCellStyle1;
            this.NumSensor.FillWeight = 72.9567F;
            this.NumSensor.HeaderText = "№ датчика";
            this.NumSensor.Name = "NumSensor";
            this.NumSensor.ReadOnly = true;
            this.NumSensor.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.NumSensor.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Situation
            // 
            this.Situation.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.Situation.DefaultCellStyle = dataGridViewCellStyle2;
            this.Situation.FillWeight = 69.79695F;
            this.Situation.HeaderText = "Ситуация";
            this.Situation.Name = "Situation";
            this.Situation.ReadOnly = true;
            this.Situation.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Situation.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // ImageSensor
            // 
            this.ImageSensor.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.ImageSensor.FillWeight = 107.2463F;
            this.ImageSensor.HeaderText = "Изображение";
            this.ImageSensor.Name = "ImageSensor";
            this.ImageSensor.ReadOnly = true;
            this.ImageSensor.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // listView2
            // 
            this.listView2.BackColor = System.Drawing.SystemColors.Control;
            this.listView2.BorderStyle = System.Windows.Forms.BorderStyle.None;
            listViewGroup1.Header = "Опции датчиков";
            listViewGroup1.Name = "listViewGroup1";
            this.listView2.Groups.AddRange(new System.Windows.Forms.ListViewGroup[] {
            listViewGroup1});
            listViewItem1.Group = listViewGroup1;
            this.listView2.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem1});
            this.listView2.Location = new System.Drawing.Point(4, 7);
            this.listView2.Name = "listView2";
            this.listView2.Size = new System.Drawing.Size(291, 88);
            this.listView2.TabIndex = 11;
            this.listView2.UseCompatibleStateImageBehavior = false;
            // 
            // SaveHTMLButton
            // 
            this.SaveHTMLButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.SaveHTMLButton.FlatAppearance.BorderSize = 2;
            this.SaveHTMLButton.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.SaveHTMLButton.Image = ((System.Drawing.Image)(resources.GetObject("SaveHTMLButton.Image")));
            this.SaveHTMLButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.SaveHTMLButton.Location = new System.Drawing.Point(80, 512);
            this.SaveHTMLButton.Name = "SaveHTMLButton";
            this.SaveHTMLButton.Size = new System.Drawing.Size(143, 43);
            this.SaveHTMLButton.TabIndex = 1;
            this.SaveHTMLButton.Text = "Сохранить         ";
            this.SaveHTMLButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.SaveHTMLButton.UseVisualStyleBackColor = true;
            this.SaveHTMLButton.Click += new System.EventHandler(this.SaveHTMLButton_Click);
            // 
            // LoadMainImageButton
            // 
            this.LoadMainImageButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.LoadMainImageButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.LoadMainImageButton.Location = new System.Drawing.Point(269, 61);
            this.LoadMainImageButton.Margin = new System.Windows.Forms.Padding(0);
            this.LoadMainImageButton.Name = "LoadMainImageButton";
            this.LoadMainImageButton.Size = new System.Drawing.Size(29, 20);
            this.LoadMainImageButton.TabIndex = 5;
            this.LoadMainImageButton.Text = "...";
            this.LoadMainImageButton.UseVisualStyleBackColor = true;
            this.LoadMainImageButton.Click += new System.EventHandler(this.LoadMainImageButton_Click);
            // 
            // ImagePathBox
            // 
            this.ImagePathBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.ImagePathBox.Enabled = false;
            this.ImagePathBox.Location = new System.Drawing.Point(7, 61);
            this.ImagePathBox.Margin = new System.Windows.Forms.Padding(0);
            this.ImagePathBox.Name = "ImagePathBox";
            this.ImagePathBox.ReadOnly = true;
            this.ImagePathBox.Size = new System.Drawing.Size(262, 20);
            this.ImagePathBox.TabIndex = 4;
            this.ImagePathBox.TextChanged += new System.EventHandler(this.ImagePathBox_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 45);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(131, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Основное изображение:";
            // 
            // listView1
            // 
            this.listView1.BackColor = System.Drawing.SystemColors.Control;
            this.listView1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            listViewGroup2.Header = "Опции HTML мнемосхемы";
            listViewGroup2.Name = "listViewGroup1";
            this.listView1.Groups.AddRange(new System.Windows.Forms.ListViewGroup[] {
            listViewGroup2});
            listViewItem2.Group = listViewGroup2;
            this.listView1.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem2});
            this.listView1.Location = new System.Drawing.Point(7, 19);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(291, 88);
            this.listView1.TabIndex = 2;
            this.listView1.UseCompatibleStateImageBehavior = false;
            // 
            // toolsHTMLPanel
            // 
            this.toolsHTMLPanel.CanOverflow = false;
            this.toolsHTMLPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.toolsHTMLPanel.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolsHTMLPanel.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.CreateHTMLButton,
            this.toolStripLabel1,
            this.DownLoadHTMLButton,
            this.SettingsButton,
            this.AdressStripLabel});
            this.toolsHTMLPanel.Location = new System.Drawing.Point(0, 579);
            this.toolsHTMLPanel.Name = "toolsHTMLPanel";
            this.toolsHTMLPanel.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolsHTMLPanel.Size = new System.Drawing.Size(942, 25);
            this.toolsHTMLPanel.Stretch = true;
            this.toolsHTMLPanel.TabIndex = 2;
            this.toolsHTMLPanel.Text = "toolStrip1";
            // 
            // CreateHTMLButton
            // 
            this.CreateHTMLButton.Image = ((System.Drawing.Image)(resources.GetObject("CreateHTMLButton.Image")));
            this.CreateHTMLButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.CreateHTMLButton.Name = "CreateHTMLButton";
            this.CreateHTMLButton.Size = new System.Drawing.Size(99, 22);
            this.CreateHTMLButton.Text = "Создать HTML";
            this.CreateHTMLButton.Click += new System.EventHandler(this.CreateHTMLButton_Click);
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(6, 25);
            // 
            // DownLoadHTMLButton
            // 
            this.DownLoadHTMLButton.Image = ((System.Drawing.Image)(resources.GetObject("DownLoadHTMLButton.Image")));
            this.DownLoadHTMLButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.DownLoadHTMLButton.Name = "DownLoadHTMLButton";
            this.DownLoadHTMLButton.Size = new System.Drawing.Size(108, 22);
            this.DownLoadHTMLButton.Text = "Загрузить HTML";
            this.DownLoadHTMLButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.DownLoadHTMLButton.TextDirection = System.Windows.Forms.ToolStripTextDirection.Horizontal;
            this.DownLoadHTMLButton.Click += new System.EventHandler(this.DownLoadHTMLButton_Click);
            // 
            // SettingsButton
            // 
            this.SettingsButton.Image = ((System.Drawing.Image)(resources.GetObject("SettingsButton.Image")));
            this.SettingsButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.SettingsButton.Name = "SettingsButton";
            this.SettingsButton.Size = new System.Drawing.Size(81, 22);
            this.SettingsButton.Text = "Настройки";
            this.SettingsButton.Click += new System.EventHandler(this.toolStripButton1_Click);
            // 
            // AdressStripLabel
            // 
            this.AdressStripLabel.Name = "AdressStripLabel";
            this.AdressStripLabel.Size = new System.Drawing.Size(76, 22);
            this.AdressStripLabel.Text = "Адресс HTML:";
            this.AdressStripLabel.Visible = false;
            // 
            // saveHTMLFileDialog
            // 
            this.saveHTMLFileDialog.CheckPathExists = false;
            this.saveHTMLFileDialog.Filter = "HTML файл (*.html)|*.html";
            this.saveHTMLFileDialog.RestoreDirectory = true;
            // 
            // AdressHTMLBox
            // 
            this.AdressHTMLBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.AdressHTMLBox.Location = new System.Drawing.Point(404, 581);
            this.AdressHTMLBox.Name = "AdressHTMLBox";
            this.AdressHTMLBox.ReadOnly = true;
            this.AdressHTMLBox.Size = new System.Drawing.Size(534, 20);
            this.AdressHTMLBox.TabIndex = 3;
            this.AdressHTMLBox.Visible = false;
            // 
            // editorHTMLForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(942, 606);
            this.Controls.Add(this.AdressHTMLBox);
            this.Controls.Add(this.toolsHTMLPanel);
            this.Controls.Add(this.SettingHTMLGroup);
            this.Controls.Add(this.groupBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MinimumSize = new System.Drawing.Size(950, 640);
            this.Name = "editorHTMLForm";
            this.Padding = new System.Windows.Forms.Padding(0, 0, 0, 2);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Редактор HTML мнемосхем";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.editorHTMLForm_KeyUp);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.editorHTMLForm_FormClosing);
            this.groupBox1.ResumeLayout(false);
            this.StartPanel.ResumeLayout(false);
            this.StartPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.SettingHTMLGroup.ResumeLayout(false);
            this.SettingHTMLGroup.PerformLayout();
            this.sensorPanel.ResumeLayout(false);
            this.sensorPanel.PerformLayout();
            this.toolStrip2.ResumeLayout(false);
            this.toolStrip2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ListSensorGrid)).EndInit();
            this.toolsHTMLPanel.ResumeLayout(false);
            this.toolsHTMLPanel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox SettingHTMLGroup;
        private System.Windows.Forms.ToolStrip toolsHTMLPanel;
        private System.Windows.Forms.ToolStripButton DownLoadHTMLButton;
        private System.Windows.Forms.ToolStripSeparator toolStripLabel1;
        private System.Windows.Forms.ToolStripLabel AdressStripLabel;
        private System.Windows.Forms.Button SaveHTMLButton;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button LoadMainImageButton;
        private System.Windows.Forms.TextBox ImagePathBox;
        private System.Windows.Forms.LinkLabel DownLoadLabel;
        private System.Windows.Forms.LinkLabel CreateLabel;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.SaveFileDialog saveHTMLFileDialog;
        private System.Windows.Forms.WebBrowser HTMLBrowser;
        private System.Windows.Forms.OpenFileDialog openHTMLDialog;
        private System.Windows.Forms.ToolStripButton CreateHTMLButton;
        private System.Windows.Forms.Panel sensorPanel;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label editorLabel;
        private System.Windows.Forms.PropertyGrid sensorProperty;
        private System.Windows.Forms.ToolStrip toolStrip2;
        private System.Windows.Forms.ToolStripButton addSensorButton;
        private System.Windows.Forms.ToolStripButton removeSensorButton;
        public System.Windows.Forms.DataGridView ListSensorGrid;
        private System.Windows.Forms.ListView listView2;
        private System.Windows.Forms.TextBox AdressHTMLBox;
        private System.Windows.Forms.Panel StartPanel;
        private System.Windows.Forms.ToolStripButton SettingsButton;
        private System.Windows.Forms.Label CloseHTMLButton;
        private System.Windows.Forms.Label HelpButton;
        private System.Windows.Forms.DataGridViewTextBoxColumn NumSensor;
        private System.Windows.Forms.DataGridViewTextBoxColumn Situation;
        private System.Windows.Forms.DataGridViewTextBoxColumn ImageSensor;
        private System.Windows.Forms.ToolStripButton CloneSensorButton;
    }
}

