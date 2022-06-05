using System;
using System.Xml;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Aspose.Cells;
using System.Threading;

namespace e57xmlUp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox1.Text))
            {                
                MessageBox.Show("Выберите папку назначения", "Ошибка",
    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                //  string[] files= Directory.GetFiles(@"D:\GFU300\E57TestData\11","*.e57");
                var batFilePath= CreateBatFile();
                Process proc = new Process();
                proc.StartInfo.FileName = batFilePath;//@"D:\GFU300\E57TestData\11\e57xmldump.exe";
                proc.Start();
                proc.WaitForExit();
                //Thread.Sleep(3000);
                var pathFileUpload= Upload();
                CollectGarbage(batFilePath);
                MessageBox.Show("Выгрузка успешно завершена!" + Environment.NewLine + "Ссылка на файл: " + pathFileUpload);
                //if (!File.Exists(textBox1.Text + "\\e57xmldump.exe"))
                //{
                //    File.Copy(@"\\it-nesterov\Почта\1\e57xmldump.exe", textBox1.Text);
                //}
                //List<string> newList = new List<string>();
                //var text = File.ReadAllLines(@"D:\GFU300\E57TestData\11\e57con1.bat");
                //text[2]=text[2].Replace("111", "222");
                //File.WriteAllLines(@"D:\GFU300\E57TestData\11\e57con1.bat", text);

                //   proc.StartInfo.FileName = @"D:\GFU300\E57TestData\11\e57con1.bat";   
                // foreach (var file in files)
                //{


                //proc.StartInfo.Arguments = "manitou.e57>manitou.xml";  //String.Format("{0}>{1}", file, file.Replace("e57", "xml"));

                //proc.Start();
                //proc.WaitForInputIdle();
                //proc.WaitForExit();
                //}
                // 
                //MessageBox.Show("Файл успешно создан по след. пути: " + textBox1.Text + "\\e57tools.bat" + Environment.NewLine + "Для продолжения необходимо его запустить." +
                //    Environment.NewLine + "После чего требуется снова вернуться в диалоговое окно и нажать \"Выгрузить xml\""+ Environment.NewLine+
                //    "Примечание: Путь до файла и имена файлов должены состоять из латинских букв, пробелы не допускаются.");
                //"call \"D:\\GFU300\\E57TestData\\11\\e57xmldump.exe\" manitou.e57 ";                                                                                                        //
                //proc.StartInfo.Arguments= "manitou.e57>manitou112.xml";

                //ProcessStartInfo processStartInfo = new ProcessStartInfo();
                //processStartInfo.Arguments = "manitou.e57>111.xml";
                //processStartInfo.FileName = "D:\\GFU300\\E57TestData\\11\\e57xmldump.exe";
                //Process process = new Process();
                //process.StartInfo = processStartInfo;
                //process.Start();
                //process.WaitForInputIdle(10000);
            }
        }

        private void CollectGarbage(string batFile)
        {
            File.Delete(batFile);
            if (deleteFilesXML.Checked)
            {
                string[] files = Directory.GetFiles(textBox1.Text, "*.xml");
                foreach (var file in files)
                {
                    File.Delete(file);
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            if (fbd.ShowDialog() == DialogResult.OK)
            {
                textBox1.Text = fbd.SelectedPath;

            }
        }

        public string CreateBatFile()
        {
            string[] files = Directory.GetFiles(textBox1.Text, "*.e57");
            var batFilePath = String.Format("{0}\\e57tools.bat", textBox1.Text);
            if (File.Exists(batFilePath))
            {
                File.Delete(batFilePath);
            }
            using (var s = File.Create(batFilePath))
            {
                // s.WriteByte(32);
                using (var sw = new StreamWriter(s, Encoding.UTF8))
                {
                    sw.WriteLine("echo on");
                    foreach (var file in files)
                    {
                        var filePath = file.Split('\\');
                        var fileName = filePath.Last();
                        string exePath = @"\\it-nesterov\ProgramShare\ImageE57toXLSX\e57xmldump.exe";
                        //string exeFileDest = textBox1.Text + "\\e57xmldump.exe";
                        //if (!File.Exists(exeFileDest))
                        //{
                        //    File.Copy(exePath, exeFileDest);
                        //}
                        sw.WriteLine(String.Format("call \"{0}\" \"{1}\\{2}\">\"{1}\\{3}\"", exePath, textBox1.Text, fileName, fileName.Replace("e57", "xml")));
                    }
                    //sw.WriteLine("pause");
                    sw.Close();
                }
            }
            return batFilePath;

        }

        public string Upload()
        {
            if (string.IsNullOrEmpty(textBox1.Text))
            {
                MessageBox.Show("Выберите папку назначения", "Ошибка",
    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                Random random = new Random();
                var path = String.Format(textBox1.Text + "\\StationUploadE57_{0}-{1}-{2}_{3}-{4}-{5}.xlsx", DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
                Workbook wb = new Workbook();
                wb.Worksheets.Add("Станций сканирования");
                var ws = wb.Worksheets["Станций сканирования"];

                string[] files = Directory.GetFiles(textBox1.Text, "*.xml");
                int row = 1;
                List<Station> stations = new List<Station>();
                foreach (var file in files)
                {
                    var doc = new XmlDocument();
                    // Загружаем данные из файла.
                    doc.Load(file);
                    // Получаем корневой элемент документа.
                    var root = doc.DocumentElement;
                    var image2D = root.LastChild;
                    if (image2D.Name == "images2D")
                    {
                        List<XmlNode> vectorChildren = new List<XmlNode>();
                        GetElementsByName(image2D, "vectorChild", ref vectorChildren);


                        //int count = 1;
                        foreach (var vectorElem in vectorChildren)
                        {
                            var newStation = new Station();
                            newStation.FileName = file.Split('\\').Last().Replace("xml", "e57");
                            //newStation.Number = count;
                            var pose = GetElementByName(vectorElem, "pose");
                            var translation = GetElementByName(pose, "translation");
                            newStation.X = GetDoubleValue(GetElementByName(translation, "x"));
                            newStation.Y = GetDoubleValue(GetElementByName(translation, "y"));
                            newStation.Z = GetDoubleValue(GetElementByName(translation, "z"));
                            stations.Add(newStation);

                            //var rotation = GetElementByName(pose, "rotation");
                            //newStation.RX = GetDoubleValue(GetElementByName(rotation, "x"));
                            //newStation.RY = GetDoubleValue(GetElementByName(rotation, "y"));
                            //newStation.RZ = GetDoubleValue(GetElementByName(rotation, "z"));
                            //count++;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Xml иерархия имеет различия.");
                    }
                }

                List<Station> stationOr = new List<Station>();
                if (sortCB.Checked)
                {
                    stationOr = stations.OrderBy(x => x.Y).ThenBy(x => x.X).DeleteDuplicates().Enumerate();
                }
                else
                {
                    stationOr = stations.DeleteDuplicates().Enumerate();
                }

                //Переименование файлов
                if (renameCB.Checked) RenameFiles(ref stationOr);

                UploadRowToExcel(ref ws, stationOr, ref row);
                row++;
                AddHeader(ws);
                ws.AutoFitColumns(0, 12);
                wb.Save(path);
                return path;
            }
            return string.Empty;
        }

        public void RenameFiles(ref List<Station> stationOr)
        {
            foreach (var station in stationOr)
            {
                var oldName= String.Format("{0}\\{1}", textBox1.Text, station.FileName);
                var newFileName= String.Format("{0}_{1}", station.Number.ToString(), station.FileName);
                var newName= String.Format("{0}\\{1}", textBox1.Text, newFileName);
                File.Move(oldName, newName);
                station.FileName = newFileName;
            }
        }



        private void button3_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox1.Text))
            {
                MessageBox.Show("Выберите папку назначения", "Ошибка",
    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                Random random = new Random();
                var path = String.Format(textBox1.Text + "\\StationUploadE57_{0}-{1}-{2}_{3}-{4}-{5}.xlsx", DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
                Workbook wb = new Workbook();
                wb.Worksheets.Add("Станций сканирования");
                var ws = wb.Worksheets["Станций сканирования"];

                string[] files = Directory.GetFiles(textBox1.Text, "*.xml");
                int row = 1;
                List<Station> stations = new List<Station>();
                foreach (var file in files)
                {
                    var doc = new XmlDocument();
                    // Загружаем данные из файла.
                    doc.Load(file);
                    // Получаем корневой элемент документа.
                    var root = doc.DocumentElement;
                    var image2D = root.LastChild;
                    if (image2D.Name == "images2D")
                    {
                        List<XmlNode> vectorChildren = new List<XmlNode>();
                        GetElementsByName(image2D, "vectorChild", ref vectorChildren);
                        
                        
                        int count = 1;
                        foreach (var vectorElem in vectorChildren)
                        {
                            var newStation = new Station();
                            newStation.FileName = file.Split('\\').Last().Replace("xml", "e57");
                            newStation.Number = count;
                            var pose = GetElementByName(vectorElem, "pose");
                            var translation = GetElementByName(pose, "translation");
                            newStation.X = GetDoubleValue(GetElementByName(translation, "x"));
                            newStation.Y = GetDoubleValue(GetElementByName(translation, "y"));
                            newStation.Z = GetDoubleValue(GetElementByName(translation, "z"));

                            var rotation = GetElementByName(pose, "rotation");
                            newStation.RX = GetDoubleValue(GetElementByName(rotation, "x"));
                            newStation.RY = GetDoubleValue(GetElementByName(rotation, "y"));
                            newStation.RZ = GetDoubleValue(GetElementByName(rotation, "z"));
                            stations.Add(newStation);
                            count++;
                        }                      
                    }
                    else
                    {
                        MessageBox.Show("Xml иерархия имеет различия.");
                    }
                }
                var stationOr=stations.OrderBy(x => x.X).ThenBy(x => x.Y).ToList();
                UploadRowToExcel(ref ws, stationOr, ref row);
                row++;
                AddHeader(ws);
                ws.AutoFitColumns(0, 12);
                wb.Save(path);
                MessageBox.Show("Выгрузка успешно завершена!" + Environment.NewLine + "Ссылка на файл: " + path);
            }
        }

        public void AddHeader(Worksheet ws)
        {
            Style style = ws.Cells[0, 0].GetStyle();
            style.SetBorder(BorderType.BottomBorder, CellBorderType.Thin, Color.Black);
            style.SetBorder(BorderType.LeftBorder, CellBorderType.Thin, Color.Green);
            style.SetBorder(BorderType.RightBorder, CellBorderType.Thin, Color.Blue);
            style.SetBorder(BorderType.TopBorder, CellBorderType.Thin, Color.Black);
            style.Pattern = BackgroundType.Solid;
            style.ForegroundColor = Color.Yellow;
            style.Font.Size = 12;
            style.Font.IsBold = true;
            style.VerticalAlignment = TextAlignmentType.Center;
            style.HorizontalAlignment = TextAlignmentType.Center;
            ws.Cells[0, 0].Value = "Номер";
            ws.Cells[0, 1].Value = "Наименование файла";
            ws.Cells[0, 2].Value = "X";
            ws.Cells[0, 3].Value = "Y";
            ws.Cells[0, 4].Value = "Z";
            //ws.Cells[0, 5].Value = "RX";
            //ws.Cells[0, 6].Value = "RY";
            //ws.Cells[0, 7].Value = "RZ";

            for (int i = 0; i < 5; i++)
            {
                ws.Cells[0, i].SetStyle(style);
            }
            ws.Cells.SetRowHeight(0, 25);
           
        }

        public void UploadRowToExcel(ref Worksheet ws, List<Station> stations, ref int row)
        {
            foreach (var station in stations)
            {
                ws.Cells[row, 0].Value = station.Number;
                ws.Cells[row, 1].Value = station.FileName;
                ws.Cells[row, 2].Value = station.X;
                ws.Cells[row, 3].Value = station.Y;
                ws.Cells[row, 4].Value = station.Z;
                //ws.Cells[row, 5].Value = station.RX;
                //ws.Cells[row, 6].Value = station.RY;
                //ws.Cells[row, 7].Value = station.RZ;
                row++;
            }
        }

        public double GetDoubleValue(XmlNode xmlNode)
            {
                double val = 0;
                double pow = 0;
                if (xmlNode != null)
                {
                    if (xmlNode.FirstChild != null)
                    {
                        if (xmlNode.FirstChild != null)
                        {
                            string strForParse = xmlNode.FirstChild.InnerText;
                            var strSet = strForParse.Split('e');
                            if (strSet.Count() > 1)
                            {
                                var valStr = strSet[0].Replace(".", ",");
                                var res = Double.TryParse(valStr, out val);
                                var res1 = Double.TryParse(strSet[1], out pow);
                                if (res & res1) val = Math.Pow(10, pow) * val;
                            }
                            else
                            {
                                var valStr = strSet[0].Replace(".", ",");
                                var res = Double.TryParse(valStr, out val);
                            }
                        }
                    }
                }
                return val;
            }

            public XmlNode GetElementByName(XmlNode element, string nameOfXmlNode)
            {
                if (element != null)
                {
                    if (element.ChildNodes.Count > 0)
                    {
                        foreach (XmlNode xmlChild in element.ChildNodes)
                        {
                            if (xmlChild.Name == nameOfXmlNode)
                            {
                                return xmlChild;
                            }

                        }
                    }
                }
                return null;
            }

            public XmlNode GetElementsByName(XmlNode rootElement, string nameOfXmlNode, ref List<XmlNode> vectorChildren)
            {
                if (rootElement.ChildNodes.Count > 0)
                {
                    foreach (XmlNode xmlChild in rootElement.ChildNodes)
                    {
                        if (xmlChild.Name == nameOfXmlNode)
                        {
                            vectorChildren.Add(xmlChild);
                        }
                        if (xmlChild.ChildNodes.Count > 0)
                        {
                            GetElementsByName(xmlChild, nameOfXmlNode, ref vectorChildren);
                        }
                    }
                }
                return null;
            }
            /// <summary>
            /// Метод для отображения содержимого xml элемента.
            /// </summary>
            /// <remarks>
            /// Получает элемент xml, отображает его имя, затем все атрибуты
            /// после этого переходит к зависимым элементам.
            /// Отображает зависимые элементы со смещением вправо от начала строки.
            /// </remarks>
            /// <param name="item"> Элемент Xml. </param>
            /// <param name="indent"> Количество отступов от начала строки. </param>
            private static void PrintItem(XmlElement item, int indent = 0)
            {
                // Если у элемента есть атрибуты, 
                // то выводим их поочередно, каждый в квадратных скобках.
                foreach (XmlAttribute attr in item.Attributes)
                {
                    Console.Write($"[{attr.InnerText}]");
                }

                // Если у элемента есть зависимые элементы, то выводим.
                foreach (var child in item.ChildNodes)
                {
                    if (child is XmlElement node)
                    {
                        // Если зависимый элемент тоже элемент,
                        // то переходим на новую строку 
                        // и рекурсивно вызываем метод.
                        // Следующий элемент будет смещен на один отступ вправо.                   
                        PrintItem(node, indent + 1);
                    }

                    if (child is XmlText text)
                    {
                        // Если зависимый элемент текст,
                        // то выводим его через тире.
                        Console.Write($"- {text.InnerText}");
                    }
                }
            }

        private void button4_Click(object sender, EventArgs e)
        {
            List<Station> stations = new List<Station>();
            stations.Add(new Station() { X = 0, Y = 0 });
            stations.Add(new Station() { X = 10, Y = 10 });
            stations.Add(new Station() { X = 3, Y = 0 });
            stations.Add(new Station() { X = 0, Y = 3 });
            stations.Add(new Station() { X = 4, Y = 3 });
            stations.Add(new Station() { X = 5, Y = 3 });
            stations.Add(new Station() { X = 5, Y = 5 });
            stations.Add(new Station() { X = 4, Y = 5 });
            stations.Add(new Station() { X = 3, Y = 3 });

            stations=stations.OrderBy(x => x.X).ThenBy(y => y.Y).ToList();
            var i = 1;



        }
    }
    }


    

