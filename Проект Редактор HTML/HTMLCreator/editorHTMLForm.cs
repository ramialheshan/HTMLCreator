
#region Список используемых классов 

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.ComponentModel.Design;
using System.Drawing.Design;
using System.IO;
using System.Windows.Forms.Design;
using System.Xml;
using mshtml;
using System.Diagnostics;
using System.Security.Cryptography;

#endregion

namespace HTMLCreator
{
    public partial class editorHTMLForm : Form
    {
        #region КЛАСС "CRYPTO" 

        public class Crypto
        {
            private static byte[] _salt = Encoding.ASCII.GetBytes("o6806642kbM7c5");
            /// <summary>
            /// Encrypt the given string using AES.  The string can be decrypted using 
            /// DecryptStringAES().  The sharedSecret parameters must match.
            /// </summary>
            /// <param name="plainText">The text to encrypt.</param>
            /// <param name="sharedSecret">A password used to generate a key for encryption.</param>
            public static string EncryptStringAES(string plainText, string sharedSecret)
            {
                if (string.IsNullOrEmpty(plainText))
                    throw new ArgumentNullException("plainText");
                if (string.IsNullOrEmpty(sharedSecret))
                    throw new ArgumentNullException("sharedSecret");

                string outStr = null;                       // Encrypted string to return
                RijndaelManaged aesAlg = null;              // RijndaelManaged object used to encrypt the data.

                try
                {
                    // generate the key from the shared secret and the salt
                    Rfc2898DeriveBytes key = new Rfc2898DeriveBytes(sharedSecret, _salt);

                    // Create a RijndaelManaged object
                    // with the specified key and IV.
                    aesAlg = new RijndaelManaged();
                    aesAlg.Key = key.GetBytes(aesAlg.KeySize / 8);
                    aesAlg.IV = key.GetBytes(aesAlg.BlockSize / 8);

                    // Create a decrytor to perform the stream transform.
                    ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                    // Create the streams used for encryption.
                    using (MemoryStream msEncrypt = new MemoryStream())
                    {
                        using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                        {
                            using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                            {

                                //Write all data to the stream.
                                swEncrypt.Write(plainText);
                            }
                        }
                        outStr = Convert.ToBase64String(msEncrypt.ToArray());
                    }
                }
                finally
                {
                    // Clear the RijndaelManaged object.
                    if (aesAlg != null)
                        aesAlg.Clear();
                }

                // Return the encrypted bytes from the memory stream.
                return outStr;
            }
            /// <summary>
            /// Decrypt the given string.  Assumes the string was encrypted using 
            /// EncryptStringAES(), using an identical sharedSecret.
            /// </summary>
            /// <param name="cipherText">The text to decrypt.</param>
            /// <param name="sharedSecret">A password used to generate a key for decryption.</param>
            public static string DecryptStringAES(string cipherText, string sharedSecret)
            {
                if (string.IsNullOrEmpty(cipherText))
                    throw new ArgumentNullException("cipherText");
                if (string.IsNullOrEmpty(sharedSecret))
                    throw new ArgumentNullException("sharedSecret");

                // Declare the RijndaelManaged object
                // used to decrypt the data.
                RijndaelManaged aesAlg = null;

                // Declare the string used to hold
                // the decrypted text.
                string plaintext = null;

                try
                {
                    // generate the key from the shared secret and the salt
                    Rfc2898DeriveBytes key = new Rfc2898DeriveBytes(sharedSecret, _salt);

                    // Create a RijndaelManaged object
                    // with the specified key and IV.
                    aesAlg = new RijndaelManaged();
                    aesAlg.Key = key.GetBytes(aesAlg.KeySize / 8);
                    aesAlg.IV = key.GetBytes(aesAlg.BlockSize / 8);

                    // Create a decrytor to perform the stream transform.
                    ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);
                    // Create the streams used for decryption.                
                    byte[] bytes = Convert.FromBase64String(cipherText);
                    using (MemoryStream msDecrypt = new MemoryStream(bytes))
                    {
                        using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                        {
                            using (StreamReader srDecrypt = new StreamReader(csDecrypt))

                                // Read the decrypted bytes from the decrypting stream
                                // and place them in a string.
                                plaintext = srDecrypt.ReadToEnd();
                        }
                    }
                }
                finally
                {
                    // Clear the RijndaelManaged object.
                    if (aesAlg != null)
                        aesAlg.Clear();
                }

                return plaintext;
            }
        }

        #endregion

        #region Класс получения имени файла с помощью OpenDialog 

        public class FileSelectorTypeEditor : UITypeEditor
        {
            public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
            {
                if (context == null || context.Instance == null)
                    return base.GetEditStyle(context);
                return UITypeEditorEditStyle.Modal;
            }

            public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
            {
                IWindowsFormsEditorService editorService;

                if (context == null || context.Instance == null || provider == null)
                    return value;

                try
                {
                    // get the editor service, just like in windows forms
                    editorService = (IWindowsFormsEditorService)
                       provider.GetService(typeof(IWindowsFormsEditorService));

                    OpenFileDialog dlg = new OpenFileDialog();
                    dlg.Filter = "Изображения (*.jpg;*.gif;*.bmp;*.png)|*.jpg;*.gif;*.bmp;*.png;|Все файлы (*.*)|*.*";
                    dlg.FilterIndex = 1;
                    dlg.CheckFileExists = true;
                    dlg.RestoreDirectory = true;
                    dlg.CheckFileExists = true;
                    dlg.InitialDirectory = Application.StartupPath;

                    string filename = (string)value;
                    if (!File.Exists(filename))
                        filename = null;
                    dlg.FileName = filename;

                    using (dlg)
                    {
                        DialogResult res = dlg.ShowDialog();
                        if (res == DialogResult.OK)
                        {
                            filename = dlg.FileName;
                        }
                    }
                    return filename;

                }
                finally
                {
                    editorService = null;
                }
            }
        } // class FileSelectorTypeEditor
        #endregion

        #region Вспомогательный класс - Свойства позиционирование датчика 

        //[TypeConverter(typeof(ExpandableObjectConverter))]
        //public class locationSensor : Point
        //{
        //    [DisplayName("Ширина")]
        //    [Description("Ширина отображаемого датчика")]

        //    public override X { get; set; }
        //    [DisplayName("Высота")]
        //    [Description("Длина отображаемого датчика")]
        //    public float height { get; set; }

        //    public locationSensor()
        //    {
        //        width = 0;
        //        height = 0;
        //    }
        //}

        #endregion

        #region Вспомагательный класс (Размер датчика) 

        [TypeConverter(typeof(ExpandableObjectConverter))]
        public class sizeSensor
        {
            [DisplayName("Ширина")]
            [Description("Ширина отображаемого датчика")]
            public int width{get;set;}
            [DisplayName("Высота")]
            [Description("Длина отображаемого датчика")]
            public int height{get;set;}

            public sizeSensor()
            {
                width = 0;
                height = 0;
            }
            /// <summary>
            /// Представление в виде строки
            /// </summary>
            //public override string ToString()
            //{
            //    return width + "; " + height;
            //}
        }

        #endregion

        #region Класс редактор 
        /// <summary>
        /// Класс: Хранит всю информацию о датчике 
        /// </summary>
        private class Editor
        {  
            /// <summary>
            /// Конструктор создает класс
            /// </summary>
            public Editor()
            {
                _angleRotation = 0; _filename = ""; _alarmText = ""; absolutPathSensor = ""; _sizeSensor = new sizeSensor();
            }
            
            private double _angleRotation;
            string _filename;
            private string _alarmText;
            public sizeSensor _sizeSensor;

            /// <summary>
            /// Хранит номер датчика 
            /// </summary>
            [DisplayName("Номер датчика"), Category("Датчик"), Description("Номер датчика по проекту")]
            public int NumSensor { get; set; }

            /// <summary>
            /// Хранит тип ситуации (До критическая, Критическая, Отказ, Восстановление) 
            /// </summary>
            [DisplayName("Ситуация"), Category("Датчик"), Description("Одна из четырех типов ситуаций")]
            public TypeSituation NumSituation { get; set; }

            //[DisplayName("Размер датчика"), Description("Размер датчика"), Category("Датчик")]
            //public sizeSensor SizeSensor
            //{
            //    get { return _sizeSensor; }
            //    set { _sizeSensor = value; }
            //}

            /// <summary>
            /// Хранит угол поворота датчика 
            /// </summary>
            [ReadOnly(false), DisplayName("Угол поворота"), Description("Угол поворота изображение, которое используется для отображения датчика"), Category("Датчик")]
            public string angleRotation 
            {
                get { return String.Format("{0:0.00}",_angleRotation); }
                set { _angleRotation = Convert.ToDouble(value); } 
            }

            /// <summary>
            /// Хранит координаты датчика 
            /// </summary>
            [ReadOnly(false), DisplayName("Координаты"), Description("Координаты расположения датчика на плане"), Category("Датчик")]
            public Point locationSensor { get; set; }

            /// <summary>
            /// Хранит тревожного извещения 
            /// </summary>
            [ReadOnly(false),Editor(typeof(MultilineStringEditor), typeof(UITypeEditor)), DisplayName("Текст извещения"), Description("Текст, который отображается в окне сообщения"), Category("Датчик")]
            public string AlarmText { 
                get
                {
                    return _alarmText.Replace("<br>","\r\n");
                }
                set
                {
                    if (value!=null||value!="")
                    {
                        _alarmText = value.Replace("<br>", "\r\n");
                    }
                }
            }
            /// <summary>
            /// Хранит имя файла изображения датчика 
            /// </summary>
            [ReadOnly(false), Editor(typeof(FileSelectorTypeEditor), typeof(UITypeEditor)), DisplayName("Изображение"), Description("Файл изображение, которое используется для отображения датчика на плане"), Category("Датчик")]
            public string Filename
            {
                get
                {
                    absolutPathSensor = _filename;
                    if (_filename != null)
                    {
                        char[] array = new char[] { '/', '\\' };
                        string[] temp = _filename.Split(array);
                        return temp[temp.Length - 1];
                    }
                    return _filename;
                }
                set
                {
                    if (value!=null)
                    {
                        try
                        {
                            FileStream stream = new FileStream(value.ToString(), FileMode.Open);
                            stream.Close();
                            _filename = value;
                        }
                        catch (Exception)
                        {
                            
                            if (Filename==string.Empty)
                            {
                                _filename = "";
                            }

                            return;
                        }
                    }
                }
            }
            /// <summary>
            /// Хранит абсолютный путь к файлу изображения датчика 
            /// </summary>
            [ReadOnly(true), DisplayName("Абсолютный адресс изображения"), Description("Полный адресс файла изображения, который используется для отображения датчика на плане"),Category("Датчик")]
            public string absolutPathSensor{get; set;}

            /// <summary>
            /// Хранит цвет текста тревожного извещения
            /// </summary>
            [ReadOnly(false), DisplayName("Цвет тревожного извещения"), Description("Цвет, которым будет закрашен текст тревожного извещения"), Category("Датчик")]
            public Color TextColor { get; set; }

            //[ReadOnly(true), DisplayName("размер изображения"), Description("Размер изображения, которое используется для отображения датчика на плане"), Category("Датчик")]
            //public Size sizeSensor { get { return _sizeSensor; } set { _sizeSensor = value; } }
        }

        #endregion
        
        #region Описание переменных 
       
        #region Переменные 

        Editor editClass;
        DataTable sensorListProperty;
        Int32 currentSensor;
        string PathMainImage;
        bool FlagSave=false;
        string PathScriptFile = Application.StartupPath + "\\" + "HTMLFunctionScript.js";
        string sensorStandPath;
        public XmlDocument doc = new XmlDocument();
        bool nideToSave;

        #region Переменные для проверки пароля 
        
        /// <summary>
        /// Пароль до первичной разшифровки 
        /// </summary>
        string Password_Before_Decrypted;
        /// <summary>
        /// Пароль после первого дешифрования 
        /// </summary>
        string Password_After_First_Decrypted;
        /// <summary>
        /// Количество останвшихся запусков
        /// </summary>
        string DecryptedNumberOfTry;
        /// <summary>
        /// Время создания лиценции после шифрования 
        /// </summary>
        DateTime DecryptedTimeCreatedLisence;
        /// <summary>
        /// Время последнего использования программы после шифрования 
        /// </summary>
        DateTime DecryptedTimeLastUsedLisence;
        /// <summary>
        /// Количество дней для использования лицензии после шифрования 
        /// </summary>
        string DecryptedDaysUsedLisence;

        #endregion

        #endregion

        #region Перечисления 

        /// <summary>
        /// Типы ситуации, которые используються для определения датчика 
        /// </summary>
        public enum TypeSituation
        {
            /// <summary>
            /// Ситуация докритическая 
            /// </summary>
            Докритическая,
            /// <summary>
            /// Ситуация критическая 
            /// </summary>
            Критическая,
            /// <summary>
            /// Ситуация отказ 
            /// </summary>
            Отказ, 
            /// <summary>
            /// Ситуация восстановления 
            /// </summary>
            Восстановление,
            /// <summary>
            /// Ситуация неопределена
            /// </summary>
            Неопределено
        }
        /// <summary>
        /// Типы действий при редактировании датчиков 
        /// </summary>
        public enum TypeActionForEditSensor
        {
            /// <summary>
            /// Добавляем датчик
            /// </summary>
            Add,
            /// <summary>
            /// Дублируем датчик
            /// </summary>
            Duplicate,
            /// <summary>
            /// Редактирование датчика
            /// </summary>
            Edit,
            /// <summary>
            /// Удаление датчика
            /// </summary>
            Remove,
        }

        #endregion

        #endregion

        #region Конструктор формы 

        public editorHTMLForm()
        {
            #region Проверка пароля 

            #region Читаем файл настроек 

            #region Загружаем файл настроек 
           
            try
            {
                doc.Load(Application.StartupPath + "\\Settings.xml");
            }
            catch (XmlException)
            {
                MessageBox.Show("Файл настроек поврежден.\r\nПриложение будет закрыто.\r\nОбратитесь за помощью к разработчику", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit(); this.Close(); return;
            }
            catch (IOException)
            {
                MessageBox.Show("Файл настроек не найден.\r\nПриложение будет закрыто.\r\nОбратитесь за помощью к разработчику", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit(); this.Close(); return;
            }

            #endregion

            if (doc != null)
            {
                XmlNode root = doc.DocumentElement;
                foreach (XmlNode i in root.ChildNodes)
                {
                    switch (i.Name)
                    {
                        #region Проверка файла стандартного изображения 

                        case "PasswordLisence": 
                            try
                            { 
                                Password_Before_Decrypted = i.InnerText;
                                if (Password_Before_Decrypted == string.Empty)
                                {
                                    MessageBox.Show("Лицензия отсутствует.\r\nПриложение будет закрыто.\r\nОбратитесь за помощью к разработчику", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    Application.Exit(); this.Close(); return;
                                }
                            }
                            catch (Exception) 
                            {
                                MessageBox.Show("Файл настроек не найден.\r\nПриложение будет закрыто.\r\nОбратитесь за помощью к разработчику", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                Application.Exit(); this.Close(); return;
                            }

                            break;
                        
                        //Путь к стандартному файлу, который используется для отображения датчика на плане 
                        case "pathStandSensor":
                            try
                            {
                                FileStream fileSensor = new FileStream(i.InnerText, FileMode.Open);
                                sensorStandPath = i.InnerText;
                                fileSensor.Close();
                            }
                            catch (Exception)
                            {
                                MessageBox.Show("Файл изображения для отображения датчика задан не коректно.\r\nЗадайте коректный файл с помощью настроек программы.", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                sensorStandPath = string.Empty;
                                i.InnerText = string.Empty;
                            }
                            break;

                        #endregion
                    }
                }

                if (string.IsNullOrEmpty(Password_Before_Decrypted))
                {
                    MessageBox.Show("Лицензия повреждена.\r\nПриложение будет закрыто.\r\nОбратитесь за помощью к разработчику", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Application.Exit(); this.Close(); return;
                }
            }

            #endregion

            #region Загружаем пароль 

            CryptoForm formControl ;
            bool LoopStop = false;

            while (!LoopStop)
            {
                formControl = new CryptoForm();

                switch (formControl.ShowDialog())
                {
                    case DialogResult.OK:

                        #region Проверка пароля 

                        switch (DecryptXMLNode(Password_Before_Decrypted, formControl.passwordString))
                        {
                            case true:

                                #region Проверка количества оставшихся попыток 

                                if (Convert.ToInt32(DecryptedNumberOfTry)>0)
                                {
                                    #region Записать новое количество попыток на запуск 

                                    XmlNode root = doc.DocumentElement;
                                    foreach (XmlNode XML_Item in root.ChildNodes)
                                    {
                                        switch (XML_Item.Name)
                                        {
                                            case "PasswordLisence":
                                                try
                                                {
                                                    DecryptedNumberOfTry = (Convert.ToInt32(DecryptedNumberOfTry) - 1).ToString();
                                                    XML_Item.InnerText = CreateXMLNodeValue(formControl.passwordString, Password_After_First_Decrypted, DecryptedTimeCreatedLisence, DecryptedTimeLastUsedLisence, Convert.ToDecimal(DecryptedNumberOfTry), Convert.ToDecimal(DecryptedDaysUsedLisence));
                                                }
                                                catch (Exception)
                                                {
                                                    MessageBox.Show("Приложение изчерпало количество попыток запуска для данной лицензии." + "\r\n" + "Приложение будет закрыто. " + "\r\n" + "Обратитесь за помощью к разработчику", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                                    Application.Exit(); this.Close(); return;
                                                }

                                                break;
                                        }
                                    }
                                    doc.Save(Application.StartupPath + "\\Settings.xml");

                                    #endregion
                                }
                                else
                                {
                                    MessageBox.Show("Приложение изчерпало количество попыток запуска для данной лицензии." + "\r\n" + "Приложение будет закрыто. " + "\r\n" + "Обратитесь за помощью к разработчику", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    Application.Exit(); this.Close(); return;
                                }

                                #endregion

                                #region Проверка времени создании лицензии 

                                if (DateTime.Now < DecryptedTimeCreatedLisence)
                                {
                                    MessageBox.Show("Приложение не прошло стадию проверки подлинности лицензии." + "\r\n" + "Приложение будет закрыто. " + "\r\n" + "Обратитесь за помощью к разработчику", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    Application.Exit(); this.Close(); return;
                                }

                                #endregion

                                #region Проверка времени последнего использования 

                                if (DateTime.Now < DecryptedTimeLastUsedLisence)
                                {
                                    MessageBox.Show("Приложение не прошло стадию проверки подлинности лицензии." + "\r\n" + "Приложение будет закрыто. " + "\r\n" + "Обратитесь за помощью к разработчику", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    Application.Exit(); this.Close(); return;
                                }
                                else
                                {
                                    #region Записать новое количество попыток на запуск 

                                    XmlNode root = doc.DocumentElement;
                                    foreach (XmlNode XML_Item in root.ChildNodes)
                                    {
                                        switch (XML_Item.Name)
                                        {
                                            case "PasswordLisence":
                                                try
                                                {
                                                    DecryptedTimeLastUsedLisence = DateTime.Now;
                                                    XML_Item.InnerText = CreateXMLNodeValue(formControl.passwordString, Password_After_First_Decrypted, DecryptedTimeCreatedLisence, DecryptedTimeLastUsedLisence, Convert.ToDecimal(DecryptedNumberOfTry), Convert.ToDecimal(DecryptedDaysUsedLisence));
                                                }
                                                catch (Exception)
                                                {
                                                    MessageBox.Show("Приложение не прошло стадию проверки подлинности лицензии." + "\r\n" + "Приложение будет закрыто. " + "\r\n" + "Обратитесь за помощью к разработчику", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                                    Application.Exit(); this.Close(); return;
                                                }
                                                break;
                                        }
                                    }
                                    doc.Save(Application.StartupPath + "\\Settings.xml");

                                    #endregion
                                }

                                #endregion

                                #region Проверяем количество дней, которое используется лицензия 

                                if ((DecryptedTimeLastUsedLisence - DecryptedTimeCreatedLisence).Days > Convert.ToInt32(DecryptedDaysUsedLisence))
                                {
                                    MessageBox.Show("Приложение изчерпало количество дней использования лицензии." + "\r\n" + "Приложение будет закрыто. " + "\r\n" + "Обратитесь за помощью к разработчику", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    Application.Exit(); this.Close(); return;
                                }

                                #endregion

                                #region Показываем пользователю количество дней до последнего запуска 

                                string numberLeftDays = (Convert.ToInt32(DecryptedDaysUsedLisence) - (DecryptedTimeLastUsedLisence - DecryptedTimeCreatedLisence).Days).ToString();
                                MessageBox.Show("Информация о состоянии лицензии: " + "\r\n" + "Количество попыток запуска программы: " + DecryptedNumberOfTry + "\r\n" +
                                    "Количество дней до деактивации программы: " + numberLeftDays,"Информация о состоянии лицензии",MessageBoxButtons.OK,MessageBoxIcon.Information);

                                #endregion


                                LoopStop = true;
                                break;
                            case false:
                                MessageBox.Show("Пароль пользователя был введен не верно. Повторите ввод пароля", "!!!Внимание!!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                break;
                        }
                        
                        #endregion

                        break;
                }
            }

            #endregion

            #endregion

            InitializeComponent();
            editClass = new Editor();

            #region Формирование таблицы с данными о датчиках 
            
            sensorListProperty=CreateSensorTable();

            //sensorListProperty = new DataTable();
            //sensorListProperty.Columns.Add("NumberSensor", typeof(Int32)); 
            //sensorListProperty.Columns.Add("SituationType", typeof(String));
            //sensorListProperty.Columns.Add("AbsolutPathImage", typeof(String));
            //sensorListProperty.Columns.Add("RelativePathImage", typeof(String));
            //sensorListProperty.Columns.Add("KordSensor", typeof(Point));
            ////sensorListProperty.Columns.Add("SizeSensor", typeof(Size));
            //sensorListProperty.Columns.Add("angleRotationSensor", typeof(decimal));
            //sensorListProperty.Columns.Add("AlarmText", typeof(string));
            //sensorListProperty.Columns.Add("ColorAlarmText", typeof(Color));


            #endregion

            #region Настройка таблицы списка датчиков на форме 
            
            ListSensorGrid.AutoGenerateColumns = false;
            ListSensorGrid.DataSource = sensorListProperty;
            ListSensorGrid.Columns["NumSensor"].DataPropertyName = "NumberSensor";
            ListSensorGrid.Columns["Situation"].DataPropertyName = "SituationType";
            ListSensorGrid.Columns["ImageSensor"].DataPropertyName = "RelativePathImage";
	
            #endregion
        }

        #endregion


        //Методы формы 

        #region Методы для ЗАЩИТЫ приложения 

        #region Метод: Разшифровать содержание теги XML и передать на обработку 

        bool DecryptXMLNode(string ValuOfXMLNode, string PasswordForDeCreapted)
        {
            #region Разшифровка оснавного пароля 

            string decryptedPassword = string.Empty;
            try { decryptedPassword = Crypto.DecryptStringAES(ValuOfXMLNode, PasswordForDeCreapted); }
            catch (Exception) { return false; }

            #endregion

            #region Чтение XML структуры 

            try
            {
                XmlDocument docSettings = new XmlDocument();
                if (decryptedPassword == string.Empty) { throw new Exception(); }
                docSettings.LoadXml(decryptedPassword);

                XmlNode root = docSettings.DocumentElement;

                foreach (XmlNode root_Item in root.ChildNodes)
                {
                    switch (root_Item.Name)
                    {
                        case "PasswordValue": Password_After_First_Decrypted = root_Item.InnerXml; break;
                        case "NumberOfTry": DecryptedNumberOfTry = root_Item.InnerXml; break;
                        case "TimeGenerationPassword": DecryptedTimeCreatedLisence = DateTime.Parse(root_Item.InnerXml); break;
                        case "TimeLastUsed": DecryptedTimeLastUsedLisence = DateTime.Parse(root_Item.InnerXml); break;
                        case "NumberDaysOfUse": DecryptedDaysUsedLisence = root_Item.InnerXml; break;
                    }
                }
                if (Password_After_First_Decrypted==string.Empty || DecryptedNumberOfTry==string.Empty || DecryptedTimeCreatedLisence == null ||DecryptedTimeLastUsedLisence == null||DecryptedDaysUsedLisence==string.Empty) { return false; }
            }
            catch (Exception)
            {
                return false;
            }

            #endregion

            return true;
        }

        #endregion

        #region Метод: Шифрование содержание XML теги 

        string CreateXMLNodeValue(string passwordOfUser, string CreaptedValueOfPassword, DateTime TimeOfCreatedlisence, DateTime TimeOfLastUseLisence, decimal numberOfTry, decimal NumberDaysOfUsedLisense)
        {
            XmlDocument bodyMessage = new XmlDocument();
            bodyMessage.LoadXml("<MNEMOKADRY><PasswordValue></PasswordValue><NumberOfTry></NumberOfTry><TimeGenerationPassword></TimeGenerationPassword><TimeLastUsed></TimeLastUsed><NumberDaysOfUse></NumberDaysOfUse></MNEMOKADRY>");

            XmlNode root = bodyMessage.DocumentElement;
            foreach (XmlNode rootItem in root.ChildNodes)
            {
                switch (rootItem.Name)
                {
                    case "PasswordValue": rootItem.InnerText = CreaptedValueOfPassword; break;
                    case "NumberOfTry": rootItem.InnerText = numberOfTry.ToString(); break;
                    case "TimeGenerationPassword": rootItem.InnerText = TimeOfCreatedlisence.ToString(); break;
                    case "TimeLastUsed": rootItem.InnerText = TimeOfLastUseLisence.ToString(); break;
                    case "NumberDaysOfUse": rootItem.InnerText = NumberDaysOfUsedLisense.ToString(); break;
                }
            }
            #region Шифруем XML

            return Crypto.EncryptStringAES(bodyMessage.OuterXml, passwordOfUser);

            #endregion
        }

        #endregion

        #endregion

        #region Метод: Получение имени и разширения файла с абсолютного пути 

        private string GetFileName(String AbsolutPath)
        {
            if (AbsolutPath != null)
            {
                char[] array = new char[] { '/', '\\' };
                string[] temp = AbsolutPath.Split(array);
                return temp[temp.Length - 1];
            }
            return AbsolutPath; 
        }

        #endregion

        #region Метод: Добавления датчика на план 

        private void ShowUpdateSensor(
            WebBrowser HTMLSourse,
            Editor sourseEditor,
            string JavaFunction
            )
        {
            object[] array = new object[6];
            array[0] = (object)sourseEditor.locationSensor.X.ToString();
            array[1] = (object)sourseEditor.locationSensor.Y.ToString();
            array[2] = (object)parseFullPath(sourseEditor.absolutPathSensor);
            array[3] = (object)sourseEditor.angleRotation.Replace(",", ".").ToString();
            array[4] = (object)sourseEditor.AlarmText.Replace("\r\n","<br>").ToString();
            array[5] = (object)System.Drawing.ColorTranslator.ToHtml(sourseEditor.TextColor);

            HTMLSourse.Document.InvokeScript(JavaFunction, array);
        }

        #region Метод: Обработка пути файла 
        
        public string parseFullPath(string PathFile)
        {
            if (PathFile != null || PathFile!="")
            {
                String temp = PathFile.Replace(":", "|");
                return "file:///" + temp.Replace("\\", "/");
            }
            return null;
        }

        #endregion

        #endregion

        #region Метод: Запись, редактирование таблицы с данными о датчиках 

        private void SaveEditSensorTable(int currentNumsendor,TypeSituation currentTypeSituation,DataTable tableSensor,Editor RedactorClass)
        {
            if (tableSensor.Rows.Count>0)
            {
                for (int i = 0; i < tableSensor.Rows.Count; i++)
                {
                    try
                    {
                        if (tableSensor.Rows[i]["NumberSensor"].ToString() == currentNumsendor.ToString() && sensorListProperty.Rows[i]["SituationType"].ToString() == currentTypeSituation.ToString())
                        {
                            tableSensor.Rows[i]["NumberSensor"] = RedactorClass.NumSensor;
                            tableSensor.Rows[i]["SituationType"] = RedactorClass.NumSituation;
                            tableSensor.Rows[i]["AbsolutPathImage"] = RedactorClass.absolutPathSensor;
                            tableSensor.Rows[i]["RelativePathImage"] = RedactorClass.Filename;
                            tableSensor.Rows[i]["KordSensor"] = RedactorClass.locationSensor;
                            tableSensor.Rows[i]["angleRotationSensor"] = RedactorClass.angleRotation;
                            tableSensor.Rows[i]["AlarmText"] = RedactorClass.AlarmText.Replace("\r\n","<br>");
                            tableSensor.Rows[i]["ColorAlarmText"] = RedactorClass.TextColor;
                        }
                    }
                    catch (Exception)
                    {
                        
                    }
                    
                }
            }
        
        }

        #endregion

        #region Метод: Для загрузки и удаления html скрипта 

        public bool LoadRemoveScript(HtmlDocument document,string PathScriptFile,bool LoadOrRemove)
        {
            switch (LoadOrRemove)
            {
                case true:

                    #region Подключение к структуре HTML документа 

                    HtmlElement script = document.CreateElement("script");
                    IHTMLScriptElement ScriptElement = (IHTMLScriptElement)script.DomElement;
                    HtmlElement head = document.GetElementsByTagName("head")[0];

                    #endregion
                    
                    #region Загрузка скрипта из файла 

                    try
                    {
                        FileStream tempStream = new FileStream(PathScriptFile, FileMode.Open, FileAccess.ReadWrite, FileShare.Read);
                        byte[] buffer = new byte[tempStream.Length];
                        tempStream.Read(buffer, 0, (int)tempStream.Length);
                        tempStream.Close();

                        string sourse = Encoding.Default.GetString(buffer, 0, buffer.Length);
                        ScriptElement.text = sourse;
                        head.AppendChild(script);
                        return true;
                    }
                    catch (Exception)
                    {
                        MessageBox.Show(" Не удалось подключится к файлу со скриптами","Внимание!!!",MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
                        return false;
                    }

                    #endregion

                    return true;

                    break;

                case false:

                    #region Подключение к структуре HTML документа 

                    HtmlElement script2 = document.GetElementsByTagName("script")[0];
                    script2.OuterHtml = string.Empty;
                    LoadRemoveScript(document, PathScriptFile, true);

                    #endregion

                    return true;
                    break;
            }


            return false;
        }

        #endregion

        #region Метод: Создает HTML и заполняет его данными 

        private void CreateORLoadHTML(
            WebBrowser Browser, 
            string MainImagePath
            )
        {
            #region Создаем новый HTML документ

            HtmlDocument htmlDoc = Browser.Document.OpenNew(true);
            htmlDoc.Write("<html><head></head><body></body></html>");
            HtmlElement head = htmlDoc.GetElementsByTagName("head")[0];

            #endregion

            #region Создаем основную таблицу 

            #region Описание таблици 

            HtmlElement tableElement = htmlDoc.CreateElement("table");
            tableElement.Id = "tableMain";
            tableElement.SetAttribute("cellpadding", "10");
            tableElement.SetAttribute("height", "100%");
            tableElement.SetAttribute("width", "100%");
            tableElement.SetAttribute("border", "1");

            #endregion

            #region Описание ячейки с изображением 

            HtmlElement row = htmlDoc.CreateElement("tr");
            HtmlElement cellImage = htmlDoc.CreateElement("td");
            cellImage.Id = "ImageCell";
            cellImage.SetAttribute("height", "80%");
            cellImage.SetAttribute("align", "center");
            cellImage.SetAttribute("bgcolor", "#FFFFFF");

            #endregion

            #region Описание основного изображения 

            HtmlElement img = htmlDoc.CreateElement("img");
            img.Id = "Plan";
            if (MainImagePath != null || MainImagePath != "" || Path.GetFileName(MainImagePath) != "undefined")
            {
                img.SetAttribute("src", MainImagePath);
            }
            if (MainImagePath == null || MainImagePath==""||Path.GetFileName(MainImagePath) == "undefined")
            {
                img.SetAttribute("src", "");
                cellImage.InnerHtml = "<h1 id=\"H\">Изображение датчика</h1>";
            }

            #endregion

            #region Вставка основного изображения 
            
            cellImage.AppendChild(img);

            #endregion

            #region Описание ячейки с тревожным тестом 

            HtmlElement cellText = htmlDoc.CreateElement("td");
            IHTMLTableCell IcellText = (IHTMLTableCell)cellText.DomElement;
            IcellText.width = "30%";
            IcellText.vAlign = "center";
            IcellText.align = "center";
            cellText.SetAttribute("id", "AlarmTextCell");
            cellText.SetAttribute("align", "center");
            cellText.InnerHtml = "<h1 id=\"AlarmText\">ТЕКСТ<br>СООБЩЕНИЯ</h1>";

            #endregion

            #region вставка всех элементов в документ 

            row.AppendChild(cellImage);
            row.AppendChild(cellText);

            tableElement.AppendChild(row);
            htmlDoc.Body.InnerHtml = tableElement.OuterHtml;

            #endregion

            #endregion

            #region Описание функции скрипта 

            LoadRemoveScript(htmlDoc, PathScriptFile, true);

            #endregion

            #region Описание кодировки страницы 

            HtmlElement metaElement = Browser.Document.CreateElement("meta");
            metaElement.SetAttribute("http-equiv", "Content-Type");
            metaElement.SetAttribute("content", "text/html; charset=utf-8");
            head.AppendChild(metaElement);

            #endregion
        }

        #endregion

        #region Метод: Новая запись в вертуальной таблице датчиков 

        /// <summary>
        /// Метод добавляет новую строку в таблицу датчиков 
        /// </summary>
        /// <param name="ResourceTable">Таблица с датчиками</param>
        /// <param name="NumberSensor">Номер датчика</param>
        /// <param name="typeSituation">Тип ситуации, к которой привязан датчик</param>
        /// <param name="PathSensor">Полный адресс размещения изображения, которое используется для отображения датчика</param>
        /// <param name="Kordsensor">Координаты размещения датчика</param>
        /// <param name="Rotation">Коэффициент, поворота изображения</param>
        /// <param name="AlarmText">Текст тревожного извещания</param>
        /// <param name="SensorColor">Цвет текста тревожного извещания</param>
        private void AddRowSensorTable(
            DataTable ResourceTable,
            int NumberSensor,
            TypeSituation typeSituation,
            string PathSensor,
            Point Kordsensor,
            float Rotation,
            string AlarmText,
            Color SensorColor
            )
        {
            DataRow rowNew = ResourceTable.NewRow();
            rowNew["NumberSensor"] = NumberSensor;
            rowNew["SituationType"] = typeSituation.ToString();
            rowNew["AbsolutPathImage"] = PathSensor;
            rowNew["RelativePathImage"] =  Path.GetFileName(PathSensor);
            rowNew["KordSensor"] = Kordsensor;
            rowNew["angleRotationSensor"] = Rotation;
            rowNew["AlarmText"] = AlarmText;
            rowNew["ColorAlarmText"] = SensorColor;
            ResourceTable.Rows.Add(rowNew); 
        }

        #endregion

        #region Метод: Формирует структуру таблицы датчиков 

        private DataTable CreateSensorTable() 
        {
            DataTable TableSensor = new DataTable();
            TableSensor.Columns.Clear();
            TableSensor.Columns.Add("NumberSensor", typeof(Int32));
            TableSensor.Columns.Add("SituationType", typeof(String));
            TableSensor.Columns.Add("AbsolutPathImage", typeof(String));
            TableSensor.Columns.Add("RelativePathImage", typeof(String));
            TableSensor.Columns.Add("KordSensor", typeof(Point));
            //sensorListProperty.Columns.Add("SizeSensor", typeof(Size));
            TableSensor.Columns.Add("angleRotationSensor", typeof(float));
            TableSensor.Columns.Add("AlarmText", typeof(string));
            TableSensor.Columns.Add("ColorAlarmText", typeof(Color));

            return TableSensor;
        }

        #endregion

        #region Метод: Добавляем\Дублируем датчик в список датчиков и на план 

        void Add_Or_Duplicate_Sensor(TypeActionForEditSensor typeAction)
        {
            #region Определить следующий номер датчика 

            if (sensorListProperty.Rows.Count > 0)
            {
                int[] mass = new int[sensorListProperty.Rows.Count];
                for (int i = 0; i < sensorListProperty.Rows.Count; i++)
                {
                    mass[i] = Convert.ToInt32(sensorListProperty.Rows[i]["NumberSensor"].ToString());
                }
                currentSensor = mass.Max() + 1;
            }
            else
            {
                currentSensor = 1;
            }

            #endregion

            #region Проверка наличия датчика 

            switch (typeAction)
            {
                case TypeActionForEditSensor.Add:

                    #region Если добавляем датчик 
                    
                    for (int i = 0; i < ListSensorGrid.Rows.Count; i++)
                    {
                        if (ListSensorGrid.Rows[i].Cells["NumSensor"].Value.ToString() == currentSensor.ToString())
                        {
                            if (ListSensorGrid.Rows[i].Cells["Situation"].Value.ToString() == TypeSituation.Неопределено.ToString())
                            {
                                MessageBox.Show("Внимание", "Датчик с номером " + currentSensor.ToString() + "и ситуацией" + TypeSituation.Неопределено.ToString() + "существует в списке\r\n Необходимо присвоить уже существующему датчику другой или выбрать другой тип ситуации", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                return;
                            }
                        }
                    }

                    #endregion

                    break;
                case TypeActionForEditSensor.Duplicate:

                    #region Если дублируем датчик 

                    for (int i = 0; i < ListSensorGrid.Rows.Count; i++)
                    {
                        if (ListSensorGrid.Rows[i].Cells["NumSensor"].Value.ToString() == currentSensor.ToString())
                        {
                            if (ListSensorGrid.Rows[i].Cells["Situation"].Value.ToString() == editClass.NumSituation.ToString())
                            {
                                MessageBox.Show("Внимание", "Датчик с номером " + currentSensor.ToString() + "и ситуацией" + editClass.NumSituation.ToString() + "существует в списке\r\n Необходимо присвоить уже существующему датчику другой или выбрать другой тип ситуации", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                return;
                            }
                        }
                    }

                    #endregion

                    break;
            }

           

            #endregion

            #region Активация списка датчиков на форме 

            ListSensorGrid.Enabled = true;

            #endregion

            #region Получаем размер стандартного сенсора 

            sizeSensor ImageSize = new sizeSensor();

            if (sensorStandPath != "")
            {
                switch (Path.GetExtension(sensorStandPath))
                {
                    case ".gif":
                        break;
                    default:
                        break;
                }

            }

            #endregion

            #region Заполнение таблицы редактора 

            switch (typeAction)
            {
                case TypeActionForEditSensor.Add:

                    #region Если добавляем датчик 

                    AddRowSensorTable (sensorListProperty, currentSensor, TypeSituation.Неопределено,sensorStandPath, new Point(0, 0),(float)0.00, "ТЕКСТ ТРЕВОЖНОГО ИЗВЕЩЕНИЯ", Color.Black);

                    #endregion

                    break;
                case TypeActionForEditSensor.Duplicate:

                    #region Если дублируем датчик 

                    AddRowSensorTable(sensorListProperty, currentSensor, editClass.NumSituation, editClass.absolutPathSensor, editClass.locationSensor, Convert.ToSingle(editClass.angleRotation), editClass.AlarmText, editClass.TextColor);

                    #endregion

                    break;
            }

           
            nideToSave = true;

            #endregion

            #region Переход на новый датчик 

            ListSensorGrid.Rows[ListSensorGrid.Rows.Count - 1].Selected = true;

            #endregion
        }

        #endregion

        #region Метод:

        #endregion

        //------------------------------------------------------


        //События формы 

        #region Событие: При загрузке формы 
        
        private void Form1_Load(object sender, EventArgs e)
        {
            #region Настройка окна при старте 
            
            SettingHTMLGroup.Enabled = false;
            ImagePathBox.Clear();
            editorLabel.Enabled = false;
            sensorProperty.Enabled = false;
            ListSensorGrid.AutoGenerateColumns = false;

            #endregion
        }

        #endregion

        #region Событие: При нажатии клавиши и отпускании 
        
        private void editorHTMLForm_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Control)
            {
                switch (e.KeyCode)
                {
                    case Keys.Escape:
                        break;
                    case Keys.N:
                        CreateHTMLButton.PerformClick();
                        break;
                    case Keys.D:
                        DownLoadHTMLButton.PerformClick();
                        break;
                    case Keys.O:
                        LoadMainImageButton.PerformClick();
                        break;
                    case Keys.S:
                        SaveHTMLButton.PerformClick();
                        break;
                    case Keys.Insert:
                        if (sensorPanel.Enabled)
                        {addSensorButton.PerformClick();}
                        break;
                    case Keys.Delete:
                        if (sensorPanel.Enabled)
                        {
                            removeSensorButton.PerformClick();
                        }
                        break;
                }
            }
        }

        #endregion

        #region Событие: При закрытии формы 

        private void editorHTMLForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            #region Проверка если необходимость сохранить документ 

            if (nideToSave)
            {
                switch (MessageBox.Show("Сохранить изменения внесенные в HTML документ?", "Внимание!", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                {
                    case DialogResult.Yes: SaveHTMLButton.PerformClick(); break;
                }
            }

            #endregion

            #region Запись настроек

            if (doc!=null)
            {
                XmlNode root = doc.DocumentElement;
                foreach (XmlNode i in root)
                {
                    switch (i.Name)
                    {
                        //Путь к стандартному файлу, который используется для отображения датчика на плане 
                        case "pathStandSensor":
                            i.InnerText = sensorStandPath;
                            break;
                    }
                }
                doc.Save(Application.StartupPath + "\\Settings.xml");
            }
            else
            {
                FileStream streamXML = null;
                try
                {
                    streamXML = new FileStream(Application.StartupPath + "\\Settings.xml", FileMode.Truncate, FileAccess.ReadWrite, FileShare.Read);
                }
                catch (Exception)
                {
                    streamXML = new FileStream(Application.StartupPath + "\\Settings.xml", FileMode.Create, FileAccess.Write, FileShare.Read);
                }

                XmlTextWriter writer = new XmlTextWriter(streamXML,Encoding.UTF8);
                writer.WriteStartDocument();
                writer.WriteStartElement("setting");
                writer.WriteStartElement("pathStandSensor");
                writer.WriteString(sensorStandPath);
                writer.WriteEndElement();
                writer.WriteEndElement();
                writer.WriteEndDocument();
                writer.Flush();
                streamXML.Flush();
                streamXML.Close();
            }

            #endregion
        }

        #endregion

        //------------------------------------------------------


        //События веб броузера

        #region Событие: Изменение размера окна броузера 

        private void HTMLBrowser_Resize(object sender, EventArgs e)
        {
            if (HTMLBrowser.Visible && ListSensorGrid.Rows.Count!=0)
            {
                ShowUpdateSensor(HTMLBrowser, editClass, "TransformSensor");
            }
        }

        #endregion

        #region Событие: Изменение видимости броузера 

        private void HTMLBrowser_VisibleChanged(object sender, EventArgs e)
        {
            if ((sender as WebBrowser).Visible)
            {
                SettingHTMLGroup.Enabled = true;
                CreateHTMLButton.Enabled = false;
                DownLoadHTMLButton.Enabled = false;
            }
            else
            {
                SettingHTMLGroup.Enabled = false;
                CreateHTMLButton.Enabled = true;
                DownLoadHTMLButton.Enabled = true;
            }
        }

        #endregion

        #region Событие: При загрузке документа в HTML броузер 

        private void HTMLBrowser_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            #region Чтение документа 

            #region Обработка массива датчиков 

            string mainImageLink = HTMLBrowser.Document.GetElementById("Plan").GetAttribute("src");
            string massSorse = HTMLBrowser.Document.GetElementsByTagName("script")[0].InnerHtml;
            string localPathDirectory = Path.GetDirectoryName(HTMLBrowser.Url.LocalPath);

            #region Присваевание изображения в поле основного изображения 

            if (mainImageLink != null || mainImageLink != "undefined")
            {
                if (Path.GetFileName(mainImageLink)!="undefined")
                {
                    ImagePathBox.Enabled = true;
                    ImagePathBox.Text = Path.GetFileName(mainImageLink);
                    PathMainImage = localPathDirectory + "\\" + ImagePathBox.Text;
                }
            }

            #endregion

            #region Создаем шаблон HTML 
           
            CreateORLoadHTML(HTMLBrowser, mainImageLink);

            #region Показ окна броузера 
            
            if (HTMLBrowser.Visible != true) { HTMLBrowser.Visible = true; }

            #endregion
            
            #endregion

            #region Проверка существование массива с датчиками заполнение датчиками таблицы 

            int NumStart = massSorse.IndexOf("var arrayData");
            int endMass = massSorse.IndexOf("];");
            if (NumStart >= 0 && endMass >= 0)
            {
                string mass = massSorse.Substring(NumStart, endMass).Replace("= [", "= ").Replace("];", ";").Replace("\r\n", "").Replace("/", "").Replace(";", "").Replace("var arrayData = ", "");
                string[] rezultMass = mass.Split(new string[] { "[", "],", "]" }, StringSplitOptions.RemoveEmptyEntries);

                if (rezultMass.Length > 0)
                {
                    float tempRotation=0;
                    ListSensorGrid.Enabled = true;
                    ListSensorGrid.DataSource = null;
                    
                    for (int i = 0; i < rezultMass.Length; i++)
                    {
                        #region Записываем значения датчика в таблицу датчиков на форме 

                        string[] massSensorValues = rezultMass[i].Split(new string[] { "," }, StringSplitOptions.None);

                        if (massSensorValues.Length == 8)
                        {
                            #region Тип ситуации 

                            TypeSituation situation = TypeSituation.Неопределено;

                            switch (massSensorValues[1])
                            {
                                case "1": situation = TypeSituation.Докритическая; break;
                                case "2": situation = TypeSituation.Критическая; break;
                                case "3": situation = TypeSituation.Отказ; break;
                                case "4": situation = TypeSituation.Восстановление; break;
                                case "0": situation = TypeSituation.Неопределено; break;
                                default: situation = TypeSituation.Неопределено; break;
                            }

                            #endregion

                            Point tempPoint = new Point(-1, -1);
                            try
                            {
                                tempPoint = new Point(Convert.ToInt32(massSensorValues[3]), Convert.ToInt32(massSensorValues[4]));
                            }
                            catch (Exception) { }

                            try
                            {
                                tempRotation = float.Parse(massSensorValues[5].Replace(".", ","));
                            }
                            catch (Exception) { }

                            string pathSensor = localPathDirectory + "\\" + massSensorValues[2].Replace("'", "");
                            string color = massSensorValues[7].Replace("'", "");
                            AddRowSensorTable(sensorListProperty, Convert.ToInt32(massSensorValues[0]), situation, pathSensor, tempPoint, tempRotation, massSensorValues[6].Replace("'", ""), Color.FromName(color));
                        }
                        else
                        {
                            MessageBox.Show("Информация о датчике повреждена","Внимание!",MessageBoxButtons.OK,MessageBoxIcon.Error);
                        }

                        #endregion
                    }
                    ListSensorGrid.DataSource = sensorListProperty;
                    ListSensorGrid.Rows[0].Selected = true;
                }
                
            }

            #endregion
            
            #endregion

            #endregion
        }

        #endregion

        //------------------------------------------------------


        //Кнопки формы 

        #region Событие: Создание HTML-шаблона документа 

        #region Событие: Дублирование создание документа через ссылку 
       
        private void CreateLabel_Click(object sender, EventArgs e)
        {
            CreateHTMLButton.PerformClick();
        }

        #endregion

        private void CreateHTMLButton_Click(object sender, EventArgs e)
        {
            #region Убераем Стартовую панель 

            StartPanel.Visible = false;

            #endregion

            #region Создаем шаблон HTML страницы 

            CreateORLoadHTML(HTMLBrowser, null);

            #endregion

            #region Показываем броузер, активизируем меню настройки 

            HTMLBrowser.Visible = true;

            #endregion

            return;

            #region Мусор 

            //#region Создаем новый HTML документ

            //HtmlDocument htmlDoc = HTMLBrowser.Document.OpenNew(true);
            //htmlDoc.Write("<html><head></head><body></body></html>");
            //HtmlElement head = htmlDoc.GetElementsByTagName("head")[0];

            //#endregion

            //#region Создаем основную таблицу

            //#region Описание таблици

            //HtmlElement tableElement = htmlDoc.CreateElement("table");
            //tableElement.Id = "tableMain";
            //tableElement.SetAttribute("cellpadding", "10");
            //tableElement.SetAttribute("height", "100%");
            //tableElement.SetAttribute("width", "100%");
            //tableElement.SetAttribute("border", "1");

            //#endregion

            //#region Описание ячейки с изображением

            //HtmlElement row = htmlDoc.CreateElement("tr");
            //HtmlElement cellImage = htmlDoc.CreateElement("td");
            //cellImage.Id = "ImageCell";
            //cellImage.SetAttribute("height", "80%");
            //cellImage.SetAttribute("align", "center");
            //cellImage.SetAttribute("bgcolor", "#FFFFFF");

            //#endregion

            //#region Описание основного изображения

            //HtmlElement img = htmlDoc.CreateElement("img");
            //img.Id = "Plan";
            //img.SetAttribute("src", "");

            //#endregion

            //#region Вставка основного изображения

            //cellImage.InnerHtml = "<h1 id=\"H\">Изображение датчика</h1>";
            //cellImage.AppendChild(img);

            //#endregion

            //#region Описание ячейки с тревожным тестом

            //HtmlElement cellText = htmlDoc.CreateElement("td");
            //IHTMLTableCell IcellText = (IHTMLTableCell)cellText.DomElement;
            //IcellText.width = "30%";
            //IcellText.vAlign = "center";
            //IcellText.align = "center";
            //cellText.SetAttribute("id", "AlarmTextCell");
            //cellText.SetAttribute("align", "center");
            //cellText.InnerHtml = "<h1 id=\"AlarmText\">ТЕКСТ<br>СООБЩЕНИЯ</h1>";

            //#endregion

            //#region вставка всех элементов в документ

            //row.AppendChild(cellImage);
            //row.AppendChild(cellText);

            //tableElement.AppendChild(row);
            //htmlDoc.Body.InnerHtml = tableElement.OuterHtml;

            //#endregion

            //#endregion

            //#region Описание функции скрипта

            //LoadRemoveScript(htmlDoc, Application.StartupPath + "\\HTML\\" + "HTMLFunctionScript.js", true);

            ////HtmlElement script = htmlDoc.CreateElement("script");
            ////IHTMLScriptElement ScriptElement = (IHTMLScriptElement)script.DomElement;

            //#region Парсинг адреса

            ////string source = Application.StartupPath.Replace("\\", "/").Replace(":", "|");
            ////string urlScript = "file:///" + source + "HTML/" + "HTMLFunctionScript.js";
            ////script.SetAttribute("src", urlScript);
            //////script.SetAttribute("language", "javascript");
            //////script.SetAttribute("type", "text/javascript");
            ////head.AppendChild(script);

            //#endregion

            //#region Загрузка скрипта из файла

            ////FileStream tempStream = new FileStream(Application.StartupPath + "\\HTML\\" + "HTMLFunctionScript.js",FileMode.Open,FileAccess.ReadWrite,FileShare.Read );
            ////byte[] buffer = new byte[tempStream.Length];
            ////tempStream.Read(buffer,0,(int)tempStream.Length);
            ////tempStream.Close();

            ////string sourse = Encoding.Default.GetString(buffer, 0, buffer.Length);

            ////#endregion

            ////ScriptElement.text = sourse;
            ////head.AppendChild(script);

            //#endregion

            //#endregion

            //#region Описание кодировки страницы

            //HtmlElement metaElement = HTMLBrowser.Document.CreateElement("meta");
            //metaElement.SetAttribute("http-equiv", "Content-Type");
            //metaElement.SetAttribute("content", "text/html; charset=utf-8");
            //head.AppendChild(metaElement);

            //#endregion

            //#region Показываем броузер, активизируем меню настройки

            //if (HTMLBrowser.Visible == false)
            //{
            //    HTMLBrowser.Visible = true;
            //}

            //#endregion

            //#region Блок сохраняет файл

            ////File.WriteAllText(Application.StartupPath + "/" + "HTML/TEST.html", HTMLBrowser.Document.Body.Parent.OuterHtml);
            ////string pathHTML = Application.StartupPath + "/" + "HTML/TEST.html";
            ////Directory.CreateDirectory(Application.StartupPath+"/HTML");

            ////FileStream HTMLfile = null;
            ////StreamWriter sw= null;
            ////try
            ////{
            ////    HTMLfile = new FileStream(pathHTML, FileMode.CreateNew, FileAccess.Write, FileShare.ReadWrite);
            ////    sw = new StreamWriter(HTMLfile);
            ////    sw.Write(htmlDoc);
            ////}
            ////catch (IOException)
            ////{
            ////    switch (MessageBox.Show("Стандартный файл уже создан. Перезаписать существующий файл?","Ошибка создания файла",MessageBoxButtons.YesNo,MessageBoxIcon.Exclamation))
            ////    {
            ////        case DialogResult.Yes:

            ////            #region Презапись существующего файла 

            ////            HTMLfile = new FileStream(pathHTML, FileMode.Truncate, FileAccess.Write, FileShare.ReadWrite);
            ////            sw = new StreamWriter(HTMLfile);
            ////            sw.Write(htmlDoc);

            ////            #endregion

            ////            break;
            ////        case DialogResult.No:

            ////            #region Создать файл со следующим индексом 

            ////            string[] files = Directory.GetFiles(Application.StartupPath + "/HTML", "*.html");

            ////            int[] arrayMAX = new int[files.Length];  
            ////            for (int i = 0; i < files.Length; i++)
            ////            {
            ////                string[] array = new string[] { "TEST","." };
            ////                string[] num = files[i].Split(array,StringSplitOptions.None);

            ////                try 
            ////                {
            ////                    arrayMAX[i]=Convert.ToInt32(num[1]);
            ////                }
            ////                catch(Exception) 
            ////                {

            ////                }
            ////            }
            ////            int numCurrent = arrayMAX.Max()+1;
            ////            pathHTML = Application.StartupPath + "/" + "HTML/TEST" + numCurrent.ToString()+ ".html";

            ////            HTMLfile = new FileStream(pathHTML, FileMode.CreateNew, FileAccess.Write, FileShare.ReadWrite);
            ////            sw = new StreamWriter(HTMLfile);
            ////            sw.Write(htmlDoc);


            ////            #endregion

            ////            break;
            ////    }

            ////}
            ////finally 
            ////{
            ////    sw.Flush();
            ////    sw.Close(); HTMLfile.Close();
            ////}

            //#endregion

            #endregion
        }

        #endregion

        #region Событие: Загрузки изображение 

        private void LoadMainImageButton_Click(object sender, EventArgs e)
        {
            openHTMLDialog.InitialDirectory = Application.StartupPath;
            openHTMLDialog.FilterIndex = 1 ;
            openHTMLDialog.AddExtension = true;
            openHTMLDialog.RestoreDirectory = false;
            openHTMLDialog.Title="Открытие изображение для сообщения";
            openHTMLDialog.Filter = "Изображения (*.jpg;*.gif;*.bmp;*.png)|*.jpg;*.gif;*.bmp;*.png;|Все файлы(*.*)|*.*";

            switch (openHTMLDialog.ShowDialog())
            {
                case DialogResult.OK:

                    #region Загрузка основного изображения 
                    
                    HtmlElement imgCell = HTMLBrowser.Document.GetElementById("ImageCell");
                    HtmlElement h = HTMLBrowser.Document.GetElementById("H");
                    if (h!=null){h.OuterHtml = "";}
                    HtmlElement imgINCell = HTMLBrowser.Document.GetElementById("Plan");
                    IHTMLElement imgReopen = (IHTMLElement)imgINCell.DomElement;
                    imgReopen.removeAttribute("height",0);
                    imgReopen.removeAttribute("width", 0);
                    PathMainImage = openHTMLDialog.FileName;
                    imgINCell.SetAttribute("src", PathMainImage);

                    ImagePathBox.Enabled = true;
                    ImagePathBox.Text = GetFileName(openHTMLDialog.FileName);
                    nideToSave = true;

                    #endregion

                    break;
            }
        }

        #endregion

        #region Событие: Сохранение текущей HTML страницы 

        private void SaveHTMLButton_Click(object sender, EventArgs e)
        {
            #region Запрос пути на сохранения файла 
            
            switch (saveHTMLFileDialog.ShowDialog())
            {
                case DialogResult.OK:

                    #region Очищаем folder от всех файлов 

                    string[] filePaths = Directory.GetFiles(Path.GetDirectoryName(saveHTMLFileDialog.FileName));
                    bool flag=false;
                    foreach (string filePath in filePaths)
                    {
                        for (int i = 0; i < sensorListProperty.Rows.Count; i++)
                        {
                            if (filePath == sensorListProperty.Rows[i]["AbsolutPathImage"].ToString()) 
                            { 
                                flag = true; 
                            } 
                        }
                        if (filePath == PathMainImage)
                        {
                            flag = true; 
                        }
                        switch (flag)
                        {
                            case false: File.Delete(filePath); break;
                        }
                        flag = false;
                    }

                    #endregion

                    #region Открываем файл или создаем файл HTML

                    FileStream fstream = null;
                    try { fstream = new FileStream(saveHTMLFileDialog.FileName, FileMode.Truncate, FileAccess.ReadWrite, FileShare.Read); }
                    catch (Exception)
                    {
                        if (fstream == null)
                        {
                            fstream = new FileStream(saveHTMLFileDialog.FileName, FileMode.CreateNew, FileAccess.ReadWrite, FileShare.None);
                        }
                    }

                    #endregion

                    #region Настройка страницы для отображения датчиков 

                    IHTMLBodyElement bodyEvent = (IHTMLBodyElement)HTMLBrowser.Document.Body.DomElement;
                    HTMLBrowser.Document.Body.SetAttribute("onResize", "GetDataSensor()");
                    bodyEvent.onload = "GetDataSensor()";
                    
                    var planImage = HTMLBrowser.Document.GetElementById("Plan");
                    planImage.SetAttribute("src", Path.GetFileName(PathMainImage));

                    #endregion

                    #region Сброс или заполнение области скрипта 

                    if (FlagSave) 
                    { 
                        LoadRemoveScript(HTMLBrowser.Document,PathScriptFile,false); 
                        //Очищаем каталог 
                    }

                    #endregion

                    #region Создания массива данных и файла log с именами файлов 

                    HtmlElement script = HTMLBrowser.Document.GetElementsByTagName("script")[0];
                    IHTMLScriptElement ScriptElement = (IHTMLScriptElement)script.DomElement;
                    FileStream logStream = File.Create(Path.GetDirectoryName(saveHTMLFileDialog.FileName) + "\\Paths.txt");
                    StreamWriter writerLog = new StreamWriter(logStream);

                    string massSourse = "\r\n var arrayData = [";
                    string typeSituation = "0";

                    #region Если датчик в списке один 

                    if (sensorListProperty.Rows.Count == 1)
                    {
                        switch (sensorListProperty.Rows[0]["SituationType"].ToString())
                        {
                            case "Докритическая": typeSituation = "1"; break;
                            case "Критическая": typeSituation = "2"; break;
                            case "Отказ": typeSituation = "3"; break;
                            case "Восстановление": typeSituation = "4"; break;
                            case "Неопределено": typeSituation = "0"; break;
                        }

                        Point kord = (Point)sensorListProperty.Rows[0]["KordSensor"];
                        string ItemArray = sensorListProperty.Rows[0]["NumberSensor"].ToString() + "," +
                            typeSituation + "," + "'" + sensorListProperty.Rows[0]["RelativePathImage"].ToString() + "'" + "," +
                            kord.X.ToString() + "," + kord.Y.ToString() + "," +
                            sensorListProperty.Rows[0]["angleRotationSensor"].ToString().Replace(",", ".").ToString() + "," +
                            "'" + sensorListProperty.Rows[0]["AlarmText"].ToString() + "'" + "," +
                            "'" + ColorTranslator.ToHtml((Color)sensorListProperty.Rows[0]["ColorAlarmText"]) + "'";

                        massSourse += ItemArray;

                        #region Запись в лог файл

                        writerLog.WriteLine(parseFullPath(saveHTMLFileDialog.FileName) + "?ID=" + sensorListProperty.Rows[0]["NumberSensor"].ToString() + "&S=" + typeSituation);
                        writerLog.Flush();

                        #endregion

                        #region Сохраняем все изображения html документа

                        try
                        {
                            File.Copy(sensorListProperty.Rows[0]["AbsolutPathImage"].ToString(), Path.GetDirectoryName(saveHTMLFileDialog.FileName) + "\\" + sensorListProperty.Rows[0]["RelativePathImage"].ToString(), true);
                        }
                        catch (Exception){}

                        #endregion
                    }

                    #endregion

                    #region Если в списке датчиков больше одного датчика 

                    if (sensorListProperty.Rows.Count > 1)
                    {
                        for (int i = 0; i < sensorListProperty.Rows.Count; i++)
                        {
                            #region Строка массива 

                            #region Определеить тип 

                            switch (sensorListProperty.Rows[i]["SituationType"].ToString())
                            {
                                case "Докритическая": typeSituation = "1"; break;
                                case "Критическая": typeSituation = "2"; break;
                                case "Отказ": typeSituation = "3"; break;
                                case "Восстановление": typeSituation = "4"; break;
                                case "Неопределено": typeSituation = "0"; break;
                            }

                            #endregion

                            #region Формируем строку 

                            Point kord = (Point)sensorListProperty.Rows[i]["KordSensor"];

                            if (i == sensorListProperty.Rows.Count - 1)
                            {
                                string ItemArray = "[" + sensorListProperty.Rows[i]["NumberSensor"].ToString() + "," +
                                typeSituation + "," + "'" + sensorListProperty.Rows[i]["RelativePathImage"].ToString() + "'" + "," +
                                kord.X.ToString() + "," + kord.Y.ToString() + "," +
                                sensorListProperty.Rows[i]["angleRotationSensor"].ToString().Replace(",", ".").ToString() + "," +
                                "'" + sensorListProperty.Rows[i]["AlarmText"].ToString() + "'" + "," +
                                "'" + ColorTranslator.ToHtml((Color)sensorListProperty.Rows[i]["ColorAlarmText"]) + "'"+"]";
                                massSourse += ItemArray;
                            }
                            else
                            {
                                string ItemArray = "[" + sensorListProperty.Rows[i]["NumberSensor"].ToString() + "," +
                                typeSituation + "," + "'" + sensorListProperty.Rows[i]["RelativePathImage"].ToString() + "'" + "," +
                                kord.X.ToString() + "," + kord.Y.ToString() + "," +
                                sensorListProperty.Rows[i]["angleRotationSensor"].ToString().Replace(",", ".").ToString() + "," +
                                "'" + sensorListProperty.Rows[i]["AlarmText"].ToString() + "'" + "," +
                                "'" + ColorTranslator.ToHtml((Color)sensorListProperty.Rows[i]["ColorAlarmText"]) + "'" + "],";
                                massSourse += ItemArray;
                            }

                            #endregion

                            #endregion

                            #region Сохраняем все изображения html документа 

                            try
                            {
                                File.Copy(sensorListProperty.Rows[i]["AbsolutPathImage"].ToString(), Path.GetDirectoryName(saveHTMLFileDialog.FileName) + "\\" + sensorListProperty.Rows[i]["RelativePathImage"].ToString(), true);
                            }
                            catch (Exception) { }

                            #endregion

                            #region Запись в лог файл 

                            writerLog.WriteLine(parseFullPath(saveHTMLFileDialog.FileName) + "?ID=" + sensorListProperty.Rows[i]["NumberSensor"].ToString() + "&S=" + typeSituation);
                            writerLog.Flush();

                            #endregion
                        }
                    }

                    #endregion

                    #region Закрываем массив 

                    massSourse += "];\r\n";

                    #endregion

                    #region Сохраняем основное изображение схемы 

                    try
                    {
                        File.Copy(PathMainImage, Path.GetDirectoryName(saveHTMLFileDialog.FileName) + "\\" + Path.GetFileName(PathMainImage), false);        
                    }
                    catch (Exception){}

                    #endregion


                    #endregion

                    #region Запись в script массив 

                    ScriptElement.text = ScriptElement.text.Insert(0, massSourse);

                    #endregion

                    #region Запись в файл HTML 

                    byte[] b = System.Text.Encoding.UTF8.GetBytes(HTMLBrowser.Document.Body.Parent.OuterHtml);
                    fstream.Write(b, 0, b.Length);
                    fstream.Flush(); fstream.Close();
                    writerLog.Close();

                    #endregion

                    #region Сообщение и настройка дальнейшей работы 
 
                    planImage.SetAttribute("src", parseFullPath(PathMainImage));

                    MessageBox.Show("Html файл сохранен.", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    FlagSave = true;
                    nideToSave = false;

                    #endregion

                    break;
            }

            #endregion
        }

        #endregion

        #region Событие: При нажатии кнопки загрузки HTML 

        #region Дублирование события: При нажатии кнопки загрузки HTML 
        
        private void DownLoadLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            DownLoadHTMLButton.PerformClick();
        }

        #endregion

        private void DownLoadHTMLButton_Click(object sender, EventArgs e)
        {
            openHTMLDialog.FilterIndex = 1;
            openHTMLDialog.AddExtension = true;
            openHTMLDialog.RestoreDirectory = false;
            openHTMLDialog.Title = "Открыть HTML файл проекта";
            openHTMLDialog.Filter = "HTML файл (*.html)|*.html";

            switch (openHTMLDialog.ShowDialog())
            {
                case DialogResult.OK:

                    #region Открываем документ 

                    HTMLBrowser.Navigate(openHTMLDialog.FileName);
                    
                    #endregion

                    #region Показ элементов адрессной строки 

                    AdressStripLabel.Visible = true;
                    AdressHTMLBox.Visible = true;
                    AdressHTMLBox.Text = openHTMLDialog.FileName;

                    #endregion

                    break;
            }
        }

        #endregion

        #region Событие: При нажатии кнопки справки 

        private void HelpButton_Click(object sender, EventArgs e)
        {
            //Object[] objArray = new Object[3];
            //objArray[0] = (Object)"str1";
            //objArray[1] = (Object)"str2"; 
            //objArray[2] = (Object)"str3";

            //HTMLBrowser.Document.InvokeScript("myTest", objArray);

            Process.Start("IExplore.exe", "file:///D|/Рабочие файлы/Проекты/Текущие проекты/Проект Редактор HTML/HTMLCreator/bin/Debug/HTML/ProjectTestNEW2/TEST.html?ID=1&S=0");
        }

        #endregion

        #region Событие: При закрытии текущего HTML файла 

        private void CloseHTMLButton_Click(object sender, EventArgs e)
        {
            #region Запрос действий при закрытии 

            if (HTMLBrowser.Document != null&&nideToSave)
            {
                switch (MessageBox.Show("Сохранить изменения внесенные в HTML документ?", "Внимание!", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                {
                    case DialogResult.Yes: SaveHTMLButton.PerformClick(); break;
                }
            }

            FlagSave = false;
            nideToSave = false;

            #region Показ елементов адрессной строки 

            AdressStripLabel.Visible = false;
            AdressHTMLBox.Visible = false;

            #endregion

            #region Настройка окна 

            HTMLBrowser.Visible = false;
            StartPanel.Visible = true;
            try { sensorListProperty.Rows.Clear(); }
            catch (Exception) { }
            sensorProperty.SelectedObject = null;
            HTMLBrowser.Focus();
            ImagePathBox.Text = string.Empty;
            PathMainImage = string.Empty;
            sensorPanel.Enabled = false;

            #endregion

            #endregion
        }

        #endregion

        #region Событие: При нажатие кнопки настройки программы 

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            SettingsForm formSetting = new SettingsForm(sensorStandPath);
            formSetting.ShowDialog();
            
            sensorStandPath = formSetting.pathsensor;
        }

        #endregion

        //------------------------------------------------------


        //Редактор свойств датчика 

        #region Событие: При изменении значения свойства датчика 
         
        private void sensorProperty_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            switch (e.ChangedItem.Label)
            {
                case "Номер датчика":

                    #region Проверка наличия датчика с такими же параметрами в таблице данных 

                    for (int i = 0; i < sensorListProperty.Rows.Count; i++)
                    {
                        if (sensorListProperty.Rows[i]["NumberSensor"].ToString() == editClass.NumSensor.ToString() && sensorListProperty.Rows[i]["SituationType"].ToString() == editClass.NumSituation.ToString())
                        {
                            MessageBox.Show("Датчик с номером " + editClass.NumSensor.ToString() + " и типом ситуации (" + editClass.NumSituation.ToString() + ") присутствует в списке датчиков", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            editClass.NumSensor = (int)e.OldValue;
                            sensorProperty.Refresh();
                            return;
                        }
                    }

                    SaveEditSensorTable((int)e.OldValue, (TypeSituation)editClass.NumSituation, sensorListProperty, editClass);
                    sensorProperty.Refresh();
                    return;


                    #endregion

                    break;
                case "Ситуация":

                    #region Проверка наличия датчика с такими же параметрами в таблице данных 

                    for (int i = 0; i < sensorListProperty.Rows.Count; i++)
                    {
                        //MessageBox.Show(sensorListProperty.Rows[i]["NumberSensor"].ToString() + "    " + sensorListProperty.Rows[i]["SituationType"].ToString() + "  "  +editClass.NumSituation.ToString() +"  "+ editClass.NumSensor.ToString());
                        if (sensorListProperty.Rows[i]["NumberSensor"].ToString() == editClass.NumSensor.ToString() && sensorListProperty.Rows[i]["SituationType"].ToString() == editClass.NumSituation.ToString())
                        {
                            MessageBox.Show("Датчик с номером " + editClass.NumSensor.ToString() + " и типом ситуации (" + editClass.NumSituation.ToString() + ") присутствует в списке датчиков", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            editClass.NumSituation = (TypeSituation)e.OldValue;
                            sensorProperty.Refresh();
                            return;
                        }
                    }

                    SaveEditSensorTable(editClass.NumSensor, (TypeSituation)e.OldValue, sensorListProperty, editClass);
                    sensorProperty.Refresh();
                    return;


                    #endregion

                    break;
                case "Координаты":
                    break;
                case "Текст извещения":
                    break;
                case "Цвет тревожного извещения":
                    break;
                case "Изображение":
                    break;
                case "X":
                    break;
                case "Y":
                    break;

            }
            nideToSave = true;
            SaveEditSensorTable(editClass.NumSensor,editClass.NumSituation, sensorListProperty, editClass);
            ShowUpdateSensor(HTMLBrowser, editClass, "TransformSensor");
        }

        #endregion

        #region Событие: Переход к объекту 

        //private void sensorProperty_SelectedObjectsChanged(object sender, EventArgs e)
        //{
        //    if ((sender as PropertyGrid).SelectedObjects!= null)    
        //    {
        //        (sender as PropertyGrid).Enabled = true;
        //    }
        //}

        #endregion

        #region Событие: Масштабирование рисунка 
        
        private void ScaleImageChecked_CheckedChanged(object sender, EventArgs e)
        {
            HtmlElement node;
            switch ((sender as CheckBox).CheckState)
            {
                case CheckState.Checked:

                    #region Масштабировать изображение

                    node = HTMLBrowser.Document.GetElementById("Plan");
                    node.Style = "width:100%;height:100%";

                    //HTMLImgClass elem = (HTMLImgClass)HTMLBrowser.Document.GetElementById("Plan").DomElement;
                    //elem.clearAttributes();
                    //elem.setAttribute("height", "100%",0);
                    //elem.setAttribute("width", "100%", 0);


                    #endregion

                    break;
                case CheckState.Unchecked:

                    #region Масштабировать изображение

                    node = HTMLBrowser.Document.GetElementById("Plan");
                    node.Style = "";

                    #endregion

                    break;
            }
        }

        #endregion

        #region Событие: При изменении значения свойства датчика 

       

        #endregion

        #region Событие: Активация панели датчиков 

        private void sensorPanel_EnabledChanged(object sender, EventArgs e)
        {
            switch ((sender as Panel).Enabled)
            {
                case true:

                    #region Активация остальных элементов 

                    if (ListSensorGrid.Rows.Count > 0)
                    {
                        sensorProperty.SelectedObject = editClass;
                    }
                    else
                    {
                        removeSensorButton.Enabled = false;
                        CloneSensorButton.Enabled = false;
                    }

                    #endregion

                    break;
            }
        }

        #endregion

        //----------------------------------------------------------------------------------------------------


        //Список датчиков  

        #region Событие: При выборе датчика 
        
        private void ListSensorGrid_SelectionChanged(object sender, EventArgs e)
        {
            #region Загружаем значение из списка датчиков 

            if (sensorListProperty.Rows.Count > 0)
            {
                for (int i = 0; i < sensorListProperty.Rows.Count; i++)
                {
                    try
                    {
                        if (sensorListProperty.Rows[i]["NumberSensor"].ToString() == ListSensorGrid.SelectedRows[0].Cells["NumSensor"].Value.ToString() && sensorListProperty.Rows[i]["SituationType"].ToString() == ListSensorGrid.SelectedRows[0].Cells["Situation"].Value.ToString())
                        {
                            sensorProperty.Enabled = false;

                            editClass = new Editor();
                            string d = (string)sensorListProperty.Rows[i]["AbsolutPathImage"];
                            editClass.Filename = sensorListProperty.Rows[i]["AbsolutPathImage"].ToString();
                            editClass.AlarmText = sensorListProperty.Rows[i]["AlarmText"].ToString();
                            editClass.angleRotation = sensorListProperty.Rows[i]["angleRotationSensor"].ToString();
                            editClass.locationSensor = (Point)sensorListProperty.Rows[i]["KordSensor"];
                            editClass.NumSensor = (int)sensorListProperty.Rows[i]["NumberSensor"];
                            editClass.TextColor = (Color)sensorListProperty.Rows[i]["ColorAlarmText"];

                            #region Тип ситуации 

                            switch (sensorListProperty.Rows[i]["SituationType"].ToString())
                            {
                                case "Докритическая":
                                    editClass.NumSituation = TypeSituation.Докритическая;
                                    break;
                                case "Критическая":
                                    editClass.NumSituation = TypeSituation.Критическая;
                                    break;
                                case "Отказ":
                                    editClass.NumSituation = TypeSituation.Отказ;
                                    break;
                                case "Восстановление":
                                    editClass.NumSituation = TypeSituation.Восстановление;
                                    break;
                                case "Неопределено":
                                    editClass.NumSituation = TypeSituation.Неопределено;
                                    break;
                            }

                            #endregion

                            sensorProperty.Enabled = true;

                            #region Активируем редактор при необходимости 

                            sensorProperty.SelectedObject = editClass;

                            #endregion

                            #region Прорисовываем датчик 

                            var element = HTMLBrowser.Document.GetElementById("ImageDiv");
                            if (element == null)
                            {
                                #region Создаем слой и помещаем туда изображение

                                var divImage = HTMLBrowser.Document.CreateElement("div");
                                var IdivImage = (IHTMLElement)divImage.DomElement;
                                IdivImage.id = "ImageDiv";
                                divImage.Style = "position:absolute; left:0px; top:0px;";

                                var imageSensor = HTMLBrowser.Document.CreateElement("img");
                                imageSensor.Id = "sensor";
                                imageSensor.SetAttribute("src", parseFullPath(editClass.absolutPathSensor));

                                divImage.AppendChild(imageSensor);
                                HTMLBrowser.Document.Body.AppendChild(divImage);

                                #endregion

                                ShowUpdateSensor(HTMLBrowser, editClass, "TransformSensor");
                            }
                            else
                            {
                                ShowUpdateSensor(HTMLBrowser, editClass, "TransformSensor");
                            }

                            #endregion

                            ListSensorGrid.FirstDisplayedScrollingRowIndex = ListSensorGrid.SelectedRows[0].Index;

                            return;
                        }
                    }
                    catch (Exception) { }
                }
                try
                {
                    ListSensorGrid.Rows[ListSensorGrid.Rows.Count - 1].Selected = true;
                    ListSensorGrid.FirstDisplayedScrollingRowIndex = ListSensorGrid.SelectedRows[0].Index;
                }
                catch (Exception)
                { 
                    removeSensorButton.Enabled = false;
                    CloneSensorButton.Enabled = false;
                    sensorProperty.SelectedObject = null;

                    #region Удаляем область датчика из HTML 

                    var element = HTMLBrowser.Document.GetElementById("ImageDiv");
                    element.OuterHtml = string.Empty;

                    var alarmCell = HTMLBrowser.Document.GetElementById("AlarmTextCell");
                    IHTMLTableCell cell = (IHTMLTableCell)alarmCell.DomElement;
                    cell.vAlign = "middle";
                    cell.align = "center";

                    var alarm = HTMLBrowser.Document.GetElementById("AlarmText");
                    alarm.Style = "COLOR: black";
                    alarm.InnerHtml = "ТЕКСТ<br>СООБЩЕНИЯ";

                    #endregion
                }
            }

            #endregion

            #region Активация редактора датчика 

            editorLabel.Enabled = true;
            sensorProperty.Enabled = true;

            #endregion
        }

        #endregion

        #region Событие: Добавление датчика в список 
        
        private void addSensorButton_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;

            #region Определить следующий номер датчика 

            if (sensorListProperty.Rows.Count > 0)
            {
                int[] mass = new int[sensorListProperty.Rows.Count];
                for (int i = 0; i < sensorListProperty.Rows.Count; i++)
                {
                    mass[i] = Convert.ToInt32(sensorListProperty.Rows[i]["NumberSensor"].ToString());
                }
                currentSensor = mass.Max() + 1;
            }
            else
            {
                currentSensor = 1;
            }

            #endregion

            #region Проверка наличия датчика 
           
            for (int i = 0; i < ListSensorGrid.Rows.Count; i++)
            {
                if (ListSensorGrid.Rows[i].Cells["NumSensor"].Value.ToString()==currentSensor.ToString())
                {
                    if (ListSensorGrid.Rows[i].Cells["Situation"].Value.ToString()==TypeSituation.Неопределено.ToString())
                    {
                        MessageBox.Show("Внимание","Датчик с номером "+currentSensor.ToString()+ "и ситуацией"+ TypeSituation.Неопределено.ToString()+"существует в списке\r\n Необходимо присвоить уже существующему датчику другой или выбрать другой тип ситуации",MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
                        return;
                    }
                }
            }

            #endregion

            #region Активация списка датчиков на форме 

            ListSensorGrid.Enabled = true;

            #endregion

            #region Получаем размер стандартного сенсора 

            sizeSensor ImageSize = new sizeSensor();

            if (sensorStandPath!="")
            {
                switch (Path.GetExtension(sensorStandPath))
                {
                    case ".gif":
                        break;
                    default:
                        break;
                }
                
            }

            #endregion

            #region Заполнение таблицы редактора 

            AddRowSensorTable
                (
                sensorListProperty, currentSensor, TypeSituation.Неопределено,
                sensorStandPath, new Point(0, 0), 
                (float)0.00, "ТЕКСТ ТРЕВОЖНОГО ИЗВЕЩЕНИЯ", Color.Black
                );
            nideToSave = true;

            //DataRow rowNew = sensorListProperty.NewRow();
            //rowNew["NumberSensor"] = currentSensor;
            //rowNew["SituationType"] = TypeSituation.Неопределено.ToString();
            //rowNew["AbsolutPathImage"] = Path.GetFullPath("Warning.png");
            //rowNew["RelativePathImage"] = "Warning.png";
            //rowNew["KordSensor"] = new Point(0, 0);
            //rowNew["angleRotationSensor"] = (float)0.01;
            //rowNew["AlarmText"] = "ТЕКСТ ТРЕВОЖНОГО ИЗВЕЩЕНИЯ";
            //rowNew["ColorAlarmText"] = Color.Black;
            //sensorListProperty.Rows.Add(rowNew); 
           
            #endregion

            #region Переход на новый датчик 

            ListSensorGrid.Rows[ListSensorGrid.Rows.Count-1].Selected = true;

            #endregion

            this.Cursor = Cursors.Default; 
        }

        #endregion
        
        #region Событие: Удаление датчика из списка 

        private void removeSensorButton_Click(object sender, EventArgs e)
        {
            switch (MessageBox.Show("Удалить датчик из списка?","Внимание",MessageBoxButtons.YesNo,MessageBoxIcon.Question))
            {
                case DialogResult.No:
                    return;
                    break;
                case DialogResult.Yes:

                    for (int i = 0; i < sensorListProperty.Rows.Count; i++)
                    {
                        if (sensorListProperty.Rows[i]["NumberSensor"].ToString() == ListSensorGrid.SelectedRows[0].Cells["NumSensor"].Value.ToString() && sensorListProperty.Rows[i]["SituationType"].ToString() == ListSensorGrid.SelectedRows[0].Cells["Situation"].Value.ToString())
                        {
                            sensorListProperty.Rows.Remove(sensorListProperty.Rows[i]);
                            nideToSave = true;
                            return;
                        }
                    }

                    break;
            }


           
        }

        #endregion

        #region Событие: Клонирование датчика 

        private void CloneSensorButton_Click(object sender, EventArgs e)
        {
            Add_Or_Duplicate_Sensor(TypeActionForEditSensor.Duplicate);

            #region МУСОР!!! 

            //#region Определяем какой датчик активный

            //while (true)
            //{
            //    DataView find = new DataView(sensorListProperty);
            //    find.RowFilter = "NumSensor=";
            //}

            //#endregion

            //#region Заполнение таблицы редактора

            //AddRowSensorTable
            //    (
            //    sensorListProperty, (int)ListSensorGrid.SelectedRows[0].Cells["NumSensor"].Value,
            //    TypeSituation.Неопределено,
            //    sensorStandPath, new Point(0, 0),
            //    (float)0.00, "ТЕКСТ ТРЕВОЖНОГО ИЗВЕЩЕНИЯ", Color.Black
            //    );
            //nideToSave = true;

            ////DataRow rowNew = sensorListProperty.NewRow();
            ////rowNew["NumberSensor"] = currentSensor;
            ////rowNew["SituationType"] = TypeSituation.Неопределено.ToString();
            ////rowNew["AbsolutPathImage"] = Path.GetFullPath("Warning.png");
            ////rowNew["RelativePathImage"] = "Warning.png";
            ////rowNew["KordSensor"] = new Point(0, 0);
            ////rowNew["angleRotationSensor"] = (float)0.01;
            ////rowNew["AlarmText"] = "ТЕКСТ ТРЕВОЖНОГО ИЗВЕЩЕНИЯ";
            ////rowNew["ColorAlarmText"] = Color.Black;
            ////sensorListProperty.Rows.Add(rowNew); 

            //#endregion

            //#region Переход на новый датчик

            //ListSensorGrid.Rows[ListSensorGrid.Rows.Count - 1].Selected = true;

            //#endregion

            #endregion
        }

        #endregion

        //------------------------------------------------------


        //События списка датчиков на форме 

        #region Событие: При добавлении записи в список 

        private void ListSensorGrid_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            removeSensorButton.Enabled = true;
            CloneSensorButton.Enabled = true;
        }

        #endregion

        #region Событие: При удалении записи о датчике 

        private void ListSensorGrid_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            if (ListSensorGrid.Rows.Count == 0)
            {
                removeSensorButton.Enabled = false;
                CloneSensorButton.Enabled = false;
                
                #region Очищаем список редактора 

                editClass = new Editor();

                sensorProperty.SelectedObject = "";

                #endregion
            }
           
        }

        #endregion

        #region Событие: При изменении текста имени основного изображения 

        private void ImagePathBox_TextChanged(object sender, EventArgs e)
        {
            #region Активация остального меню 

            sensorPanel.Enabled = true;

            #endregion
        }

        #endregion

        

        //------------------------------------------------------

    }
}
