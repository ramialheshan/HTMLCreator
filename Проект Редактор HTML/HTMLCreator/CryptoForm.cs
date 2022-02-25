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
    public partial class CryptoForm : Form
    {
        //Блок инициализации

        #region Описание переменных 
        
        /// <summary>
        /// Строка с паролем
        /// </summary>
        public string passwordString;
        /// <summary>
        /// Показывает нужно ли закрыть форму 
        /// </summary>
        public bool realClosedForm = false;

        #endregion

        #region Конструтор формы 

        /// <summary>
        /// Конструктор формы 
        /// </summary>
        public CryptoForm()
        {
            InitializeComponent();
        }

        #endregion

        //-----------------------------------------------------------------------------------------------

        //События

        #region Событие: Ввод пароля пользователем 

        private void EnterButton_Click(object sender, EventArgs e)
        {
            if (maskedPassword.Text != string.Empty)
            {
                passwordString = maskedPassword.Text.Trim();
                realClosedForm = true;
                this.DialogResult = DialogResult.OK;

            }
            else { return; }
        }

        #endregion

        #region Событие: Ввод пароля пользователем 

        private void CryptoForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            switch (e.CloseReason)
            {
                default: e.Cancel = false; break;
            }
            switch (realClosedForm)
            {
                case true: e.Cancel = false; break;
                case false: e.Cancel = true; break;

            }
        }

        #endregion

        #region Событие: Нажатие кнопок на клавиатуре 

        private void maskedPassword_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                e.Handled = true;
                if (EnterButton.Enabled)
                {
                    EnterButton.PerformClick();
                }
            }
            else
                base.OnKeyPress(e);

            if (e.KeyChar == (char)27)
            {
                e.Handled = true;
            }
            else base.OnKeyPress(e);
        }

        #endregion

        #region Событие: Ввод текста пароля 

        private void maskedPassword_TextChanged(object sender, EventArgs e)
        {
            if (maskedPassword.Text.Length > 0)
            {
                EnterButton.Enabled = true;
            }
            else
            {
                EnterButton.Enabled = false;
            }
        }

        #endregion

        //-----------------------------------------------------------------------------------------------

    }
}
