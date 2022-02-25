using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace HTMLCreator
{
    public partial class SettingsForm : Form
    {
        #region Переменные формы 

        public string pathsensor;

        #endregion

        #region Конструктор формы 
        
        public SettingsForm(string PathSensor)
        {
            InitializeComponent();

            if (PathSensor != string.Empty || PathSensor!=null)
            {
                pathsensor = PathSensor;
                PathSensorBox.Text = pathsensor;
            }
        }

        #endregion

        //Кнопки формы 

        #region Событие: Вызов диалога на открытие файла 
        
        private void OpenDialogScriptButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.InitialDirectory = Application.StartupPath;
            dialog.AddExtension = true;
            dialog.AutoUpgradeEnabled = false;
            dialog.CheckFileExists = true;
            dialog.Title = "Открытие изображение";
            dialog.Filter = "Изображения (*.jpg;*.gif;*.bmp;*.png)|*.jpg;*.gif;*.bmp;*.png";

            switch (dialog.ShowDialog())
            {
                case DialogResult.OK:
                    PathSensorBox.Text = dialog.FileName;
                    break;
            }
        }

        #endregion
     
        #region Событие: При нажатии кнопки "Применить" 

        private void ApplyButton_Click(object sender, EventArgs e)
        {
            pathsensor = PathSensorBox.Text;
            this.Close();
        }

        #endregion

        #region Событие: При нажатии кнопки "Отмена" 

        private void CancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #endregion

        //-----------------------------------------
    }
}
