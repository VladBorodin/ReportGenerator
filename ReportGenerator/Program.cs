using System;
using System.Text;
using System.IO;
using System.Xml.Serialization;
using System.Collections.Generic;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System.Linq;

namespace ReportGenerator{
    internal class Program{
        /// <summary>
        /// Сериалицация строки документа
        /// </summary>
        static void XmlSerialize(){
            Doc doc = new Doc();
            Console.WriteLine(doc);
            XmlSerializer xml = new XmlSerializer(typeof(Doc));
            string filename = "test.xml";
            using (Stream stream = File.Create(filename)){                             //нужно добавлять, а не пересоздавать
                xml.Serialize(stream, doc);
            }
            Doc d;
            using (Stream stream = File.OpenRead(filename)){
                d = (Doc)xml.Deserialize(stream);
            }
            Console.WriteLine(d);
        }
        /// <summary>
        /// Сериализация всего докумета
        /// </summary>
        static void XmlListSerialize(){
            List<Doc> list = new List<Doc>();
            XmlSerializer xml = new XmlSerializer(typeof(List<Doc>));
            string filename = "test.xml";
            using (Stream stream = File.Create(filename)){                             //нужно добавлять, а не пересоздавать
                xml.Serialize(stream, list);
            }
        }
        /// <summary>
        /// Создание и перезапись списка табельных номеров
        /// </summary>
        /// <param name="path_dir_CSV">Путь к папке со списком табельных номеров</param>
        /// <param name="path_file_CSV">Путь к файлу со списком табельных номеров</param>
        static void WriteCSVFile(string path_dir_CSV, string path_file_CSV){
            if (!Directory.Exists(path_dir_CSV)){
                Directory.CreateDirectory(path_dir_CSV);
            }
            if (!File.Exists(path_file_CSV)){
                using (FileStream fs = new FileStream(path_file_CSV, FileMode.Create, FileAccess.Write, FileShare.None)){
                    string outData = "Бисерова И.Г." + ";" + "2518" + ";" + true + "\n" +
                                        "Бородин В.А." + ";" + "5837" + ";" + false + "\n" +
                                        "Бубнов В.Б." + ";" + "7186" + ";" + false + "\n" +
                                        "Воробьева Е.А." + ";" + "0626" + ";" + false + "\n" +
                                        "Данодина М.А." + ";" + "2529" + ";" + false + "\n" +
                                        "Казаев В.А." + ";" + "4677" + ";" + false + "\n" +
                                        "Князева А.А." + ";" + "1649" + ";" + false + "\n" +
                                        "Кондрат В.В." + ";" + "8761" + ";" + false + "\n" +
                                        "Ледовских Н.Г." + ";" + "8160" + ";" + false + "\n" +
                                        "Макарова М.В." + ";" + "2116" + ";" + false + "\n" +
                                        "Маслякова А.А." + ";" + "9444" + ";" + false + "\n" +
                                        "Оберт В.Е" + ";" + "3073" + ";" + true + "\n" +
                                        "Пискунова Г.А." + ";" + "6294" + ";" + false + "\n" +
                                        "Пугина Н.В." + ";" + "6984" + ";" + false + "\n" +
                                        "Пчелинцева А.Я." + ";" + "1011" + ";" + false + "\n" +
                                        "Рассейкина О.В." + ";" + "7949" + ";" + false + "\n" +
                                        "Самсонова Е.А." + ";" + "3180" + ";" + false;
                    byte[] data = Encoding.Default.GetBytes(outData);
                    fs.Write(data, 0, data.Length);
                }
            }
        }
        static List<User> readCSVFile(string path_file_CSV){
            List<User> users = new List<User>();
            using (FileStream fs = new FileStream(path_file_CSV, FileMode.Open, FileAccess.Read, FileShare.Read)){
                byte[] inData = new byte[fs.Length];                                                            //хардкор - дали весь вес файла
                fs.Read(inData, 0, inData.Length);
                string usr = Encoding.Default.GetString(inData);
                string[] userData = usr.Split('\n');
                foreach (string u in userData)
                {
                    if (u.Length == 0) break;
                    string[] user = u.Split(';');
                    users.Add(new User
                    {
                        name = user[0],
                        login = Int32.Parse(user[1]),
                        VIP = bool.Parse(user[2]),
                    });
                }
            }
            return users;
        }
        static User userSelect(List<User> users, int ID)
        {
            User user_return = new User();
            try{
                foreach (var u in users)
                {
                    if (u.login == ID)
                    {
                        user_return = u;
                        break;
                    }
                }
                if (user_return.login == 0)
                {
                    Console.WriteLine("Вашего табельного нет в списке!\nПроверьте правильно ли его ввели или работайте в гостевом режиме!\nЕсли номер введен корректно обратитесь к начальнику, чтобы вас добавили в список!");
                    User Guest = new User() { name = "Гость", VIP = false, login = 0 };
                    user_return = Guest;
                    System.Threading.Thread.Sleep(5000);
                }
            }
            catch (Exception e){
                Console.WriteLine($"Похоже файл со списком сотрудников открыт другим пользователем!\nЛог ошибки:{e.Message}");
                System.Threading.Thread.Sleep(7000);
            }
            Console.Clear();
            return user_return;
        }
        static void ExcelOutput(List<Doc> ld){
            // Creating an instance 
            // of ExcelPackage 
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            ExcelPackage excel = new ExcelPackage();
            
            // name of the sheet 
            var workSheet = excel.Workbook.Worksheets.Add("Извещения");

            // setting the properties 
            // of the work sheet  
            workSheet.TabColor = System.Drawing.Color.Black;
            workSheet.DefaultRowHeight = 12;

            // Setting the properties 
            // of the first row 
            workSheet.Row(1).Height = 20;
            workSheet.Row(1).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            workSheet.Row(1).Style.Font.Bold = true;

            // Header of the Excel sheet 
            workSheet.Cells[1, 1].Value = "Дата получения";
            workSheet.Cells[1, 2].Value = "№ извещения";
            workSheet.Cells[1, 3].Value = "Основание";
            workSheet.Cells[1, 4].Value = "Изделие";
            workSheet.Cells[1, 5].Value = "Приоритет";
            workSheet.Cells[1, 6].Value = "БД";
            workSheet.Cells[1, 7].Value = "Исполнители";
            workSheet.Cells[1, 8].Value = "Состояние";
            workSheet.Cells[1, 9].Value = "Дата изменения";
            workSheet.Cells[1, 10].Value = "Завершили работу";
            workSheet.Cells[1, 11].Value = "Расцеховка";
            workSheet.Cells[1, 12].Value = "Кол-во";
            workSheet.Cells[1, 13].Value = "Примечание";

            // Inserting the article data into excel 
            // sheet by using the for each loop 
            // As we have values to the first row  
            // we will start with second row 
            int recordIndex = 2;

            foreach (var v in ld){
                workSheet.Cells[recordIndex, 1].Value = v.date_receive.ToShortDateString();
                workSheet.Cells[recordIndex, 2].Value = v.nomber_doc;
                workSheet.Cells[recordIndex, 3].Value = v.category;
                workSheet.Cells[recordIndex, 4].Value = v.product;
                workSheet.Cells[recordIndex, 5].Value = v.priority;
                workSheet.Cells[recordIndex, 6].Value = v.database;
                for (int i = 0; i < v.executors.Count; i++) {
                    workSheet.Cells[recordIndex, 7].Value += $"{v.executors[i]}";
                    if (i != v.executors.Count - 1) workSheet.Cells[recordIndex, 7].Value += ",";
                }
                if (workSheet.Cells[recordIndex, 7].Value == null) { 
                    workSheet.Cells[recordIndex, 7].Value += "-"; 
                } else if(workSheet.Cells[recordIndex, 7].Value.ToString() == ""){
                    workSheet.Cells[recordIndex, 7].Value += "-";
                }
                workSheet.Cells[recordIndex, 8].Value = v.state;
                workSheet.Cells[recordIndex, 9].Value = v.state_date.ToShortDateString();
                for (int i = 0; i < v.done_work.Count; i++) {
                    workSheet.Cells[recordIndex, 10].Value += $"{v.done_work[i]}";
                    if (i != v.done_work.Count - 1) workSheet.Cells[recordIndex, 10].Value += ",";
                }
                if (workSheet.Cells[recordIndex, 10].Value == null) {
                    workSheet.Cells[recordIndex, 10].Value += "-";
                } else if (workSheet.Cells[recordIndex, 10].Value.ToString() == "") {
                    workSheet.Cells[recordIndex, 10].Value += "-";
                }
                for (int i = 0; i < v.rout.Count; i++) {
                    workSheet.Cells[recordIndex, 11].Value += $"{v.rout[i]}";
                    if (i != v.rout.Count - 1) workSheet.Cells[recordIndex, 11].Value += ",";
                }
                if (workSheet.Cells[recordIndex, 11].Value == null) {
                    workSheet.Cells[recordIndex, 11].Value += "-";
                } else if (workSheet.Cells[recordIndex, 11].Value.ToString() == "") {
                    workSheet.Cells[recordIndex, 11].Value += "-";
                }
                workSheet.Cells[recordIndex, 12].Value = v.amount;
                workSheet.Cells[recordIndex, 13].Value = v.note;
                recordIndex++;
            }

            // By default, the column width is not  
            // set to auto fit for the content 
            // of the range, so we are using 
            // AutoFit() method here.  
            workSheet.Column(1).AutoFit();
            workSheet.Column(2).AutoFit();
            workSheet.Column(3).AutoFit();
            workSheet.Column(4).AutoFit();
            workSheet.Column(5).AutoFit();
            workSheet.Column(6).AutoFit();
            workSheet.Column(7).AutoFit();
            workSheet.Column(8).AutoFit();
            workSheet.Column(9).AutoFit();
            workSheet.Column(10).AutoFit();
            workSheet.Column(11).AutoFit();
            workSheet.Column(12).AutoFit();
            workSheet.Column(13).AutoFit();

            // file name with .xlsx extension  
            string p_strPath = "source\\report.xlsx";

            if (File.Exists(p_strPath))
                File.Delete(p_strPath);

            // Create excel file on physical disk  
            FileStream objFileStrm = File.Create(p_strPath);
            objFileStrm.Close();

            // Write content to excel file  
            File.WriteAllBytes(p_strPath, excel.GetAsByteArray());
            //Close Excel package 
            excel.Dispose();
        }
        static List<Doc> ExcelInput(){

            List<Doc> ld = new List<Doc>();
            // file name with .xlsx extension  
            string p_strPath = "source\\report.xlsx";

            // Creating an instance 
            // of ExcelPackage
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            using (ExcelPackage excel = new ExcelPackage(p_strPath)){
                //get the first worksheet in the workbook
                ExcelWorksheet worksheet = excel.Workbook.Worksheets[0];
                int colCount = worksheet.Dimension.End.Column;  //get Column Count
                int rowCount = worksheet.Dimension.End.Row;     //get row count

                for (int row = 2; row <= rowCount; row++){
                    ld.Add(new Doc {
                        date_receive = DateTime.Parse(worksheet.Cells[row, 1].Value.ToString()),
                        nomber_doc = worksheet.Cells[row, 2].Value.ToString(),
                        category = worksheet.Cells[row, 3].Value.ToString(),
                        product = worksheet.Cells[row, 4].Value.ToString(),
                        priority = worksheet.Cells[row, 5].Value.ToString(),
                        database = worksheet.Cells[row, 6].Value.ToString(),
                        executors = worksheet.Cells[row, 7].Value.ToString().Split(',').ToList<string>(),
                        state = worksheet.Cells[row, 8].Value.ToString(),
                        state_date = DateTime.Parse(worksheet.Cells[row, 9].Value.ToString()),
                        done_work = worksheet.Cells[row, 10].Value.ToString().Split(',').ToList<string>(),
                        rout = worksheet.Cells[row, 11].Value.ToString().Split(',').ToList<string>(),
                        amount = worksheet.Cells[row, 12].Value.ToString(),
                        note = worksheet.Cells[row, 12].Value.ToString()
                    });
                }
            excel.Dispose();
            }
            //Close Excel package
            return ld;
        }
        static void Main(string[] args){
            string path_file_CSV = "source\\list\\list_of_employees.csv";
            string path_dir_CSV = "source\\list";
            WriteCSVFile(path_dir_CSV, path_file_CSV);
            List<User> users = readCSVFile(path_file_CSV);
            User user = new User();
            user = userSelect(users, user.Log_In());
            if (user.login > 0){
                Program_Work_Space wp = new Program_Work_Space();
                wp.testing(user);
            }
            else{
                Program_Work_Space_guest wp_g = new Program_Work_Space_guest();
                wp_g.testing(user);
            }
            Program_Work_Space test_ws = new Program_Work_Space();
            List<Doc> ld = ExcelInput();
            Interface test_i = new Interface();
            test_i.main_window(ld, users, user);
            ExcelOutput(ld);
            Console.Clear();
            Console.WriteLine("Работа окончена");
            Console.ReadKey();
        }
    }
    /// <summary>
    /// Класс для хранения всех данных из файла
    /// </summary>
    [Serializable]
    public class File_Doc
    {
        public List<Doc> file_doc = new List<Doc>();
        public List<Doc> getLD()
        {
            return file_doc;
        }
    }
    /// <summary>
    /// Класс для хранения строки (одно изв) из документа
    /// </summary>
    public class Doc
    {
        public DateTime date_receive { get; set; }                          //Дата получения
        public string nomber_doc { get; set; }                              //Номер изв.
        public string category { get; set; }                                //Основание
        public string product { get; set; }                                 //Изделие
        public string priority { get; set; }                                //Приоритет
        public string database { get; set; }                                //Примечание - переименовать в базу данных - ДО/Лоцман
        public List<string> rout = new List<string>();                      //Расцеховка
        public string state { get; set; }                                   //Состояние изв
        public bool state_confirm { get; set; }                             //Необходимая переменная, для более успешного подсчета
        public DateTime state_date { get; set; }                            //Дата изменения состояния
        public string note { get; set; }                                    //Примечание к состоянию
        public string amount { get; set; }                                  //Кол-во деталей
        public List<string> executors = new List<string>();                 //Исполнители
        public List<string> done_work = new List<string>();                 //Закончили работу
    }
}