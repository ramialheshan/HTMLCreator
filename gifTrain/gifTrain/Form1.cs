using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.Collections;



namespace gifTrain
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        #region Методы формы

        #region Метод записи строки в поток

        protected void WriteString(String s, Stream stream)
        {
            char[] chars = s.ToCharArray();
            for (int i = 0; i < chars.Length; i++)
            {
                stream.WriteByte((byte)chars[i]);
            }
        }

        #endregion

        #region Метод запись в файл на диск

        protected void WriteFile(String Path, Stream stream)
        {



        }

        #endregion

        #region Метод: Read Gif file 

        protected void ReadGIFFile(
            MemoryStream streamGif

            )
        {
            #region Чтение байтовой структуры файла gif 

            byte[] SourceMass = streamGif.ToArray();

            #endregion

            #region Header файла 

            byte[] DiscScreen = new byte[6];
            Array.Copy(SourceMass, 0, DiscScreen, 0, 6);

            #endregion

            #region Ширина и высота логического экрана 

            byte[] W = new byte[4];
            byte[] H = new byte[4];

            Array.Copy(SourceMass, 6, W, 0, 2);
            Array.Copy(SourceMass, 6, H, 0, 2);

            #endregion

            #region Байт глобальной палитры 

            byte sorse = SourceMass[10];
            string strPallette = Convert.ToString(sorse, 2);
            bool ctIS = false;
            switch (strPallette[7].ToString())//установлена ли глобальная палитра 
            {
                case "1": ctIS = true; break;
                case "0": ctIS = false; break;
            }

            #endregion

            #region Цветовое разрешение  

            string color = strPallette.Substring(5, 3).PadLeft(8, '0');
            int valueColor = Convert.ToInt32(color, 2);

            #endregion

            #region Значимость цвета 

            bool signifIS = false;

            switch (strPallette[4].ToString())
            {
                case "1": signifIS = true;
                    break;
            }

            #endregion

            #region Количество цветов 

            int valueSize = Convert.ToInt32(strPallette.Substring(1, 3).PadLeft(8, '0'), 2);
            int numColor = 0;
            int startDiscImage = 0;

            switch (valueSize)
            {
                case 0: numColor = 2; startDiscImage = 13 + 6; break;
                case 1: numColor = 4; startDiscImage = 13 + 12; break;
                case 2: numColor = 8; startDiscImage = 13 + 24; break;
                case 3: numColor = 16; startDiscImage = 13 + 48; break;
                case 4: numColor = 32; startDiscImage = 13 + 96; break;
                case 5: numColor = 64; startDiscImage = 13 + 192; break;
                case 6: numColor = 128; startDiscImage = 13 + 384; break;
                case 7: numColor = 256; startDiscImage = 13 + 768; break;
            }

            #endregion

            #region Дескриптор изображения 

            byte[] bytesImage = new byte[10];
            Array.Copy(SourceMass, startDiscImage, bytesImage, 0, 10);
            string separator = Encoding.UTF8.GetString(bytesImage, 0, 1);

            #endregion

            #region Вывод результатов чтения

            MessageBox.Show("Header " + Encoding.UTF8.GetString(DiscScreen) + "  " + "\r\n" +
                             "Ширина области " + BitConverter.ToInt32(W, 0).ToString() + "\r\n" +
                             "Высота области " + BitConverter.ToInt32(H, 0).ToString() + "\r\n" +
                             "Глобальная палитра " + ctIS.ToString() + "\r\n" +
                             "Цветовое разрешение " + valueColor.ToString() + "\r\n" +
                             "Значемость цвета " + signifIS.ToString() + "\r\n" +
                             "Количество цветов " + valueSize.ToString() + "(" + numColor.ToString() + ")" + "\r\n\r\n\r\n" +
                             "");

            #endregion

        }

        #endregion

        #endregion
         
        #region Событие: Нажатие на клавиатуре 

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Control)
            {
                switch (e.KeyCode)
                {
                    case Keys.O:
                        OpenButton.PerformClick();

                        break;
                    case Keys.S:
                        saveButton.PerformClick();
                        break;
                    default:
                        break;
                }
            }
        }

        #endregion

        #region Событие: Загрузки изображения 

        private void OpenButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog openDialog = new OpenFileDialog();
            openDialog.FilterIndex = 1;
            openDialog.AddExtension = true;
            openDialog.RestoreDirectory = false;
            openDialog.Title = "Открытие изображение";
            openDialog.Filter = "Изображения (*.jpg;*.gif;*.bmp;*.png)|*.jpg;*.gif;*.bmp;*.png;|Все файлы(*.*)|*.*";

            switch (openDialog.ShowDialog())
            {
                case DialogResult.OK:

                    //Расширение файла 
                    switch (Path.GetExtension(openDialog.FileName))
                    {
                        case ".gif":

                            Image imgMain = Image.FromFile(openDialog.FileName, true);
                            ImageGIF.SizeMode = PictureBoxSizeMode.Zoom;
                            ImageGIF.Image = imgMain;

                            break;

                        default:
                            break;
                    }
                    break;
            }
        }

        #endregion

        #region Сохранить gif file to hard disk

        private void saveButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog openDialog = new OpenFileDialog();
            openDialog.FilterIndex = 1;
            openDialog.AddExtension = true;
            openDialog.RestoreDirectory = false;
            openDialog.Title = "Открытие изображение";
            openDialog.Filter = "Изображения (*.jpg;*.gif;*.bmp;*.png)|*.jpg;*.gif;*.bmp;*.png;|Все файлы(*.*)|*.*";

            switch (openDialog.ShowDialog())
            {
                case DialogResult.OK:

                    //Расширение файла 
                    switch (Path.GetExtension(openDialog.FileName))
                    {
                        case ".gif":

                            FileStream streamGifFile = File.Open(openDialog.FileName, FileMode.Open);
                            MemoryStream streamGif = new MemoryStream();
                            streamGif.SetLength(streamGifFile.Length);
                            streamGifFile.Read(streamGif.GetBuffer(), 0, (int)streamGifFile.Length);
                            streamGif.Flush();
                            streamGifFile.Close();

                            //Bitmap imgTemp = new Bitmap(1, 1, PixelFormat.Format8bppIndexed);

                            //imgTemp.Save(streamGif, ImageFormat.Gif);
                            //imgTemp.Save(Application.StartupPath + "\\" + "gifOutput.gif", ImageFormat.Gif);

                            ReadGIFFile(streamGif);

                            break;

                        default:
                            break;
                    }
                    break;
            }


            //-------------------------------------------------
            //WriteString("GIF89a",streamGif);

            //WriteFile(Application.StartupPath+"\\"+"gifOutput.gif",

            //Bitmap img = new Bitmap(imgMain.Width, imgMain.Height);
            //img.MakeTransparent();

            //Graphics gr = Graphics.FromImage(img);
            //gr.CompositingQuality = CompositingQuality.HighQuality;
            //gr.InterpolationMode = InterpolationMode.HighQualityBicubic;
            //gr.DrawImage(Image.FromFile(openDialog.FileName), 0, 0);
            //gr.Flush();

            //img.Save(Path.GetDirectoryName(openDialog.FileName) + "\\" + "test.png");
        }

        #endregion

        private void CreateGIF_Click(object sender, EventArgs e)
        {
            Bitmap img = new Bitmap(10, 10, PixelFormat.Format8bppIndexed);
            MemoryStream stream = new MemoryStream();

            img.Save(stream, ImageFormat.Gif);
            img.Save("c:\\output.gif",ImageFormat.Gif);

            Bitmap img2 = new Bitmap(stream);
            img2.MakeTransparent(Color.Black);
            img2.Save("c:\\output2.gif", ImageFormat.Gif);

        }
    }
}
