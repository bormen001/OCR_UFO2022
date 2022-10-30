using System;
using System.Drawing;
using System.Windows.Forms;
using IronOcr;


namespace WindowsForms_OCR
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();

            open.Filter = "Image Files(*.jpg; *.jpeg; *.gif; *.bmp; *.png)|*.jpg; *.jpeg; *.gif; *.bmp; *.png";
            if (open.ShowDialog() == DialogResult.OK)
            {

              ImagePath.Text = open.FileName;
            }
        }

        /*
        СЕГМЕНТАЦИЯ
        Auto	Полностью автоматическая сегментация страниц, но без экранного меню.
        AutoOnly	Автоматическая сегментация страницы, но без экранного меню или распознавания текста.
        AutoOsd	Автоматическая сегментация страницы с ориентацией и определением сценария (OSD).
        CircleWord	Обрабатывайте изображение как одно слово в круге.
        OsdOnly	Только для ориентации и обнаружения скриптов (OSD).
        RawLine	Обрабатывайте изображение как единую текстовую строку, минуя хаки, характерные для Tesseract.
        SingleBlock	Предположим, что это один однородный блок текста.
        SingleBlockVertText	Предположим, что это один однородный блок текста, выровненный по вертикали.
        SingleChar	Обрабатывайте изображение как один символ.
        SingleColumn	Предположим, что текст состоит из одного столбца переменного размера.
        SingleLine	Обрабатывайте изображение как одну текстовую строку.
        SingleWord	Обрабатывайте изображение как одно слово.
        SparseText	Найдите как можно больше текста в произвольном порядке.
        SparseTextOsd	Разреженный текст с ориентацией и определением скрипта.

        * ФИЛЬТРЫ
        Input.Binarize() - этот фильтр изображения превращает каждый пиксель в черный или белый без промежуточной точки.Может улучшить производительность распознавания в случаях очень низкого контраста текста с фоном.
        Input.ToGrayScale () - этот фильтр изображения превращает каждый пиксель в оттенок оттенков серого. Вряд ли это улучшит точность распознавания, но может повысить скорость
        Input.Контрастность () - автоматически увеличивает контрастность.Этот фильтр часто повышает скорость распознавания и точность при сканировании с низкой контрастностью.
        Input.DeNoise () - удаляет цифровой шум.Этот фильтр следует использовать только там, где ожидается шум.
        Input.Инвертировать () - инвертирует каждый цвет.Например.Белый становится черным: черный становится белым.
        Input.Dilate() - расширенная морфология. Расширение добавляет пиксели к границам объектов на изображении
        Input.Erode() - расширенная морфология. Эрозия удаляет пиксели на границах объекта
        Input.Deskew() - поворачивает изображение так, чтобы оно было правильным и ортогональным. Это очень полезно для распознавания, поскольку допуск тессеракта для искаженных сканирований может составлять всего 5 градусов.
        Input.DeepCleanBackgroundNoise() - удаление сильного фонового шума. Используйте этот фильтр только в том случае, если известен сильный фоновый шум документа, поскольку этот фильтр также может снизить точность распознавания чистых документов и требует больших затрат процессора.
        Input.EnhanceResolution - повышает разрешение изображений низкого качества.Этот фильтр не часто требуется из-за OcrInput.Минимальный DPI и OcrInput.TargetDPI автоматически распознает и разрешает входные данные с низким разрешением.
               */

        private void button3_Click(object sender, EventArgs e)
        {


           var Ocr = new IronTesseract();
            Ocr.Configuration.BlackListCharacters = "~`$#^*_}{][|\\";
            Ocr.Configuration.PageSegmentationMode = TesseractPageSegmentationMode.Auto;
            Ocr.Language = OcrLanguage.Russian;
            //
            pictureBox1.Image = Image.FromFile(ImagePath.Text);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            using (var Input = new OcrInput(ImagePath.Text))
            {
                //Input.FindMultipleTextRegions();
                Input.TargetDPI = null;

                //Input.ToGrayScale();
                //Input.Contrast(); 
               //Input.Binarize();
                //Input.Deskew();
               // Input.DeNoise();
               //Input.Dilate();
                //Input.Erode();
                //Input.EnhanceResolution(200);
               // Input.Invert();
                // Input.Rotate(-359.58);
                //Input.DeepCleanBackgroundNoise();
                //Input.Scale(100); // or Input.Scale(3000, 2000);
                //Input.Sharpen();
                //Input.ReplaceColor(Color.FromArgb(185, 163, 143), Color.FromArgb(235, 226, 216), 25);

                foreach (var page in Input.Pages)
                {
                    page.SaveAsImage(@"D:\OCR111.png");
                }
                pictureBox3.SizeMode = PictureBoxSizeMode.StretchImage;
                pictureBox3.Image = Image.FromFile(@"F:\OCR111.png");

                var Result = Ocr.Read(Input);
                richTextBox1.Text = Result.Text;
            }
            /*
            System.Drawing.Bitmap image = new Bitmap(ImagePath.Text);
            Color c1 = Color.FromArgb(234, 21, 21); //lower color
            Color c2 = Color.FromArgb(0, 0, 0); //upper color
            Color bkColor = Color.FromArgb(0, 0, 0); //background
            for (int x = 0; x < image.Width; x++)
                for (int y = 0; y < image.Height; y++)
                {
                    Color c = image.GetPixel(x, y);
                    if (c.R >= c1.R && c.R <= c2.R && c.G >= c1.G && c.G <= c2.G && c.B >= c1.B && c.B <= c2.B)
                        image.SetPixel(x, y, bkColor);
                }
            image.Save(@"D:\OCR2.png", System.Drawing.Imaging.ImageFormat.Png);
            */
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {

        }
    }
}
