using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Security.Cryptography;
using System.IO;
using System.Xml;

namespace CREATE_CRYPTO_KEY
{
    public partial class CreateCryptoPasswordForm : Form
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

        //Блок инициализации и описания 

        #region Описания переменных 
        
        /// <summary>
        /// Пароль после дешифрования
        /// </summary>
        string DecryptedPassword;
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

        #region Конструктор формы 

        public CreateCryptoPasswordForm()
        {
            InitializeComponent();
        }

        #endregion

        //------------------------------------------------------------------------------------------------------------------------------

        //Методы 

        //Разшифрование пароля 

        #region Метод: Разшифровать пароль,который привезан к пользователю ПК 

        /// <summary>
        /// Расшифровать закодированную строку
        /// </summary>
        /// <param name="cryptedString">Кодированая строка</param>
        /// <returns>Расшифрованая строка</returns>
        /// <exception cref="ArgumentNullException">Это будет вызвано исключение, когда кодировка строки нулевым или пустым</exception>
        public static string Decrypt(string cryptedString, string additionalString)
        {
            try
            {
                byte[] toDeEncrypt = Convert.FromBase64String(cryptedString);
                byte[] secret = Encoding.UTF8.GetBytes(additionalString);
                byte[] DeEncryptData = ProtectedData.Unprotect(toDeEncrypt, secret, DataProtectionScope.CurrentUser);
                return Encoding.UTF8.GetString(DeEncryptData);
            }
            catch (Exception) { return string.Empty; }
        }

        #endregion
      
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
                if (decryptedPassword==string.Empty) { throw new Exception();}
                docSettings.LoadXml(decryptedPassword);

                XmlNode root = docSettings.DocumentElement;
                
                foreach (XmlNode root_Item in root.ChildNodes)
                {
                    switch (root_Item.Name)
                    {
                        case "PasswordValue": DecryptedPassword = root_Item.InnerXml; break;
                        case "NumberOfTry": DecryptedNumberOfTry = root_Item.InnerXml;break;
                        case "TimeGenerationPassword": DecryptedTimeCreatedLisence = DateTime.Parse(root_Item.InnerXml);break;
                        case "TimeLastUsed": DecryptedTimeLastUsedLisence = DateTime.Parse(root_Item.InnerXml);break;
                        case "NumberDaysOfUse": DecryptedDaysUsedLisence = root_Item.InnerXml;break;
                    }
                }
            }
            catch (Exception)
            {
                return false;
            }
            
            #endregion

            return true;
        }

        #endregion

        //Шифрование пароля 

        #region Метод: Шифруем строку с паролем 

        /// <summary>
        /// Шифрование строки.
        /// </summary>
        /// <param name="originalString">Исходную строку</param>
        /// <returns>Зашифрованную строку</returns>
        /// <exception cref="ArgumentNullException">Это будет вызвано исключение, когда исходная строка нулевым или пустым</exception>
        public static string Encrypt(string originalString, string additionalString)
        {
            try
            {
                byte[] toEncrypt = Encoding.UTF8.GetBytes(originalString.Trim());
                byte[] secret = Encoding.UTF8.GetBytes(additionalString);
                byte[] encryptData = ProtectedData.Protect(toEncrypt, secret, DataProtectionScope.CurrentUser);
                return Convert.ToBase64String(encryptData);
            }
            catch (Exception) { return string.Empty; }
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
                    case "PasswordValue":  rootItem.InnerText = CreaptedValueOfPassword; break;
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

        #region Метод:



        #endregion

        //------------------------------------------------------------------------------------------------------------------------------


        //События
        
        #region Событие: Обработка данных 

        private void CryptoButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(passwordBox.Text))
            { MessageBox.Show("Введите пароль!", "Ошибка ввода пароля", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }

            //Шифрование и вывод 
            string passwordCreatedString = Encrypt(passwordBox.Text.Trim(), passwordBox.Text.Trim());
            string ValueOfXMLCreatedString = rezultOfCrypto.Text = CreateXMLNodeValue(passwordBox.Text.Trim(), passwordCreatedString, DateTime.Now, DateTime.Now, NumberOfTray.Value, numberOfDayUsedLisence.Value);
            switch (DecryptXMLNode(ValueOfXMLCreatedString, passwordBox.Text.Trim()))
            {
                case true:
                    DecodingPasswrod.Text = Decrypt(DecryptedPassword,passwordBox.Text.Trim());
                    DecodingNumberOfCoding.Text = DecryptedNumberOfTry;
                    break;
                case false:

                    break;
            }
        }

        #endregion

        //------------------------------------------------------------------------------------------------------------------------------
    }
}
