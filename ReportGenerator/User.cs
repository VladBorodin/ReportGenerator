using System;
using Console_Func;

namespace ReportGenerator{
    /// <summary>
    /// Класс пользователь, в котором хранится информация из файла со списком табельных номеров
    /// </summary>
    internal class User{
        public bool VIP = false;
        public int login{get;set;}
        public string name {get;set;}
        /// <summary>s
        /// Проверка прав введенного табельного номера
        /// </summary>
        /// <returns>Возвращает введенный пользователем номер для ф-ции readCSVFile</returns>
        public int Log_In(){
            bool excep;
            do{
                Console.Clear();
                Console.WriteLine("Введите свой табельный номер: ");
                    try{
                        login = Int32.Parse(Console.ReadLine());
                        excep = false;
                    } catch(Exception e){
                        excep = true;
                    Console.WriteLine($"Введены некорректные символы!\nЛог ошибки: {e.Message}");
                        System.Threading.Thread.Sleep(7000);
                    }
            }while(excep);
            Console.Clear();
            return login;
        }
    }
}