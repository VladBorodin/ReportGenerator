using System;
using Console_Func;
using System.Collections.Generic;
using System.Linq;

namespace ReportGenerator{

    /// <summary>
    /// Класс с базовыми ф-циями для сотрудников
    /// </summary>
    internal class Program_Work_Space : Program_Work_Space_guest{
        /// <summary>
        /// Добавление нового извещения
        /// </summary>
        //БЫЛ РЕФ
        public void addDoc(List<Doc>ldoc, List<User>ul, User u){
            Console.Clear();
            ConsoleKeyInfo key;
            Console.WriteLine("\tЕсли желаете добавить новое извещение - нажмите ENTER\n\tИначе любую клавишу!");
            key = Console.ReadKey();
            if(key.Key == ConsoleKey.Enter){
                Console.Write("Введите номер извещения: ");
                ldoc.Add(new Doc(){ date_receive=DateTime.Parse(DateTime.Today.ToShortDateString()),
                                    nomber_doc = Console.ReadLine(),state = "в работе",
                                    state_date = DateTime.Parse(DateTime.Today.ToShortDateString()),
                                    state_confirm = true});
                set_executors(ldoc[ldoc.Count-1],ul,u);
            }
            Console.Clear();
        }
        /// <summary>
        /// добавление даты получения
        /// </summary>
        /// <param name="doc">выбранное извещение</param>
        public void set_date_receive(Doc doc){
            List<string> date_str;
            string date_str_ent;
            bool excep = false;
            ConsoleKeyInfo key;
            while(true){
                do{
                    excep = false;
                    try{
                        Console.Clear();
                        Console.WriteLine($"Введите новую дату, если она отличается от {DateTime.Today.ToShortDateString()}, иначе просто нажмите ENTER");
                        date_str_ent = Console.ReadLine();
                        string[] date_arr_str = date_str_ent.Split(';','.','/',' ','\\',':','\'','*','+','-','|',')','(','"');
                        date_str = date_arr_str.ToList<string>();
                        if(date_str.Count>=2){
                            if(date_str.Count<3){
                                date_str.Add(DateTime.Today.Year.ToString());
                            }else if(Int32.Parse(date_str[2])<100){
                                date_str[2] = "20" + date_str[2];
                            }
                            date_str_ent = date_str[0] + '.' + date_str[1] + '.' + date_str[2];
                            doc.date_receive = DateTime.Parse(date_str_ent);
                        }else{
                            doc.date_receive = DateTime.Parse(DateTime.Today.ToShortDateString().ToString());
                        }
                    }catch(Exception e){
                        excep = true;
                        Console.WriteLine($"Допущена ошибка при вводе данных!\nЛог ошибки: {e.Message}");
                        System.Threading.Thread.Sleep(7000);
                    }
                }while(excep);
                Console.Clear();
                Console.WriteLine($"Вы ввели {doc.date_receive}, если хотите оставить этот вариант - нажмите ENTER, если ввести данные заново - любую клавишу");
                key = Console.ReadKey();
                if(key.Key == ConsoleKey.Enter)break;
            }
            Console.Clear();
        }
        /// <summary>
        /// изменение даты получения
        /// </summary>
        /// <param name="doc">выбранное извещение</param>
        public void change_date_receive(Doc doc){
            List<string> date_str;
            string date_str_ent;
            bool excep = false;
            ConsoleKeyInfo key;
            while(true){
                do{
                    excep = false;
                    try{
                        Console.Clear();
                        Console.WriteLine($"Введите новую дату, если она отличается от {doc.date_receive}, иначе просто нажмите ENTER");
                        date_str_ent = Console.ReadLine();
                        string[] date_arr_str = date_str_ent.Split(';','.','/',' ','\\',':','\'','*','+','-','|',')','(','"');
                        date_str = date_arr_str.ToList<string>();
                        if(date_str.Count>=2){
                            if(date_str.Count<3){
                                date_str.Add(DateTime.Today.Year.ToString());
                            }else if(Int32.Parse(date_str[2])<100){
                                date_str[2] = "20" + date_str[2];
                            }
                            date_str_ent = date_str[0] + '.' + date_str[1] + '.' + date_str[2];
                            doc.date_receive = DateTime.Parse(date_str_ent);
                        }
                    }catch(Exception e){
                        excep = true;
                        Console.WriteLine($"Допущена ошибка при вводе данных!\nЛог ошибки: {e.Message}");
                        System.Threading.Thread.Sleep(7000);
                    }
                }while(excep);
                Console.Clear();
                Console.WriteLine($"Вы ввели {doc.date_receive}, если хотите оставить этот вариант - нажмите ENTER, если ввести данные заново - любую клавишу");
                key = Console.ReadKey();
                if(key.Key == ConsoleKey.Enter)break;
            }
            Console.Clear();
        }
        /// <summary>
        /// изменения номера изв
        /// </summary>
        /// <param name="doc">выбранное извещение</param>
        public void set_nomber_doc(Doc doc){
            string str;
            int key = 13;
            Console.Clear();
            if(doc.nomber_doc.Length>0){
                Console.WriteLine($"Вы желаете заменить номер извещения {doc.nomber_doc} на другой?");
                Console.WriteLine("\tЕсли да - нажмите ENTER\n\tИначе - любую клавишу");
            }
            if(key == 13){
                Console.WriteLine($"\nВведите номер извещения: ");
                str = Console.ReadLine();
                doc.nomber_doc = str;
            }
            Console.Clear();
        }
        /// <summary>
        /// изменение основания извещения
        /// </summary>
        /// <param name="doc">выбранное извещение</param>
        public void set_category(Doc doc){
            string str;
            int key = 13;
            Console.Clear();
            if(doc.category.Length>0){
                Console.WriteLine($"Вы желаете заменить основание {doc.category} на другое?");
                Console.WriteLine("\tЕсли да - нажмите ENTER\n\tИначе - любую клавишу");
            }
            if(key == 13){
                Console.WriteLine($"\nВведите основание извещения: ");
                str = Console.ReadLine();
                doc.category = str;
            }
            Console.Clear();
        }
        /// <summary>
        /// изменение изделия
        /// </summary>
        /// <param name="doc">выбранное извещение</param>
        public void set_product(Doc doc){
            string str;
            int key = 13;
            Console.Clear();
            if(doc.product.Length>0){
                Console.WriteLine($"Вы желаете заменить изделие {doc.product} на другое?");
                Console.WriteLine("\tЕсли да - нажмите ENTER\n\tИначе - любую клавишу");
            }
            if(key == 13){
                Console.WriteLine($"\nВведите изделие: ");
                str = Console.ReadLine();
                doc.product = str;
            }
            Console.Clear();
        }
        /// <summary>
        /// изм приоритета
        /// </summary>
        /// <param name="doc">выбранное извещение</param>
        public void set_priority(Doc doc){
            string str;
            int key = 13;
            Console.Clear();
            if(doc.priority.Length>0){
                Console.WriteLine($"Вы желаете заменить приоритет {doc.priority} на другой?");
                Console.WriteLine("\tЕсли да - нажмите ENTER\n\tИначе - любую клавишу");
            }
            if(key == 13){
                Console.WriteLine($"\nВведите приоритет: ");
                str = Console.ReadLine();
                doc.priority = str;
            }
            Console.Clear();
        }
        /// <summary>
        /// изменение БД
        /// </summary>
        /// <param name="doc">выбранное извещение</param>
        public void set_database(Doc doc){
            string str;
            int key = 13;
            Console.Clear();
            if(doc.priority.Length>0){
                Console.WriteLine($"Вы желаете заменить название базы {doc.database} на другое?");
                Console.WriteLine("\tЕсли да - нажмите ENTER\n\tИначе - любую клавишу");
            }
            if(key == 13){
                Console.Write($"Введите название базы данных: ");
                str = Console.ReadLine();
                doc.database = str;
            }
            Console.Clear();
        }
        /// <summary>
        /// добавление и удаление цехов в расцеховке
        /// </summary>
        /// <param name="doc">выбранное извещение</param>
        public void add_rout(Doc doc){
            string str;
            ConsoleKeyInfo key;
            Console.Clear();
            do{
                Console.SetCursorPosition(0, 0);
                Console.Write("Введите ОДИН цех ЗА РАЗ: ");
                str = Console.ReadLine();
                doc.rout.Add(str);
                x = Console.CursorLeft;
                y = Console.CursorTop;
                while(true){     
                    Console.Clear();
                    Console.SetCursorPosition(x, y);
                    Console.WriteLine("Расцеховка: ");
                    foreach(var n in doc.rout){
                        Console.Write($"{n} ");
                    }
                    Console.WriteLine(  $"\n\n\tЕсли хотите продолжить ввод - нажмите ENTER" +
                                        $"\n\tЕсли хотите удалить последний цех - нажмите BACKSPACE" +
                                        $"\n\tЕсли хотите закончить ввод - нажмите ESC");
                    key = Console.ReadKey();
                    if(key.Key == ConsoleKey.Backspace)doc.rout.RemoveAt(doc.rout.Count-1);
                    if(key.Key == ConsoleKey.Escape || key.Key == ConsoleKey.Enter)break;
                }
                if(key.Key == ConsoleKey.Escape)break;
            }while(key.Key == ConsoleKey.Enter);
            Console.Clear();
        }
        /// <summary>
        /// изменяет статус извещение на собственный
        /// </summary>
        /// <param name="doc">извещение</param>
        /// <param name="u">пользователь</param>
        public void set_state(Doc doc, User u){
            Console.Clear();
            Console.CursorVisible = false;
            ConsoleKeyInfo key;
            Console.Write($"\n\tВведите новый статус для извещения: ");
            doc.state = Console.ReadLine();
            doc.state_confirm = false;
            doc.state_date = DateTime.Parse(DateTime.Today.ToShortDateString().ToString());
            doc.note += $"\n {doc.state} - {u.name} ({DateTime.Today.ToShortDateString().ToString()})";
            Console.WriteLine(  $"\n\t{u.name}, вы успешно изменили статус извещения на {doc.state}!"+
                                $"\n\n\t\t\t> Нажмите любую кнопку, чтобы продолжить <");
            Console.ReadKey();
            Console.Clear();
        }
        /// <summary>
        /// завершить обработку извещения
        /// </summary>
        /// <param name="doc">извещение</param>
        /// <param name="u">пользователь</param>
        public void finish(Doc doc, User u){
            Console.Clear();
            ConsoleKeyInfo key;
            int count = 0;
            bool there = false;
            if(u.VIP){
                Console.WriteLine(  $"{u.name}, т.к. ваш статус: НАЧАЛЬНИК - Вы имеете права:\n" +
                                    $"принудительно изменить статус извещения, а также\n" +
                                    $"{doc.executors.Count} исполнителей и {doc.done_work.Count} закончивших обработку");
                Console.WriteLine(  $"\n\t> Нажите ENTER, чтобы изменить статус исполнителей <"+
                                    $"\n\t> Иначе любую клавишу, чтобы пропустить этот пункт <");
                key = Console.ReadKey();
                if(key.Key == ConsoleKey.Enter){
                    while(doc.executors.Count != 0){
                       doc.done_work.Add(doc.executors[0]);
                       doc.executors.Remove(doc.executors[0]);
                    }
                }
                if(doc.state != "выполнено"){
                    doc.state = "выполнено";
                    doc.note = $"Выполнено\t-{u.name}";
                    doc.state_date = DateTime.Parse(DateTime.Today.ToShortDateString().ToString());
                }
                if(doc.state_confirm == true)doc.state_confirm = false;
            }else{
                Console.WriteLine("\tЕсли вы закончили работу - нажмите ENTER\n\tИначе - нажмите любую клавишу");
                key = Console.ReadKey();
                if(key.Key == ConsoleKey.Enter){
                    foreach(var v in doc.executors){
                        if(v == u.name){there = true; break;}
                        count++;
                    }
                    if(there){
                        doc.executors.RemoveAt(count);
                        doc.done_work.Add(u.name);
                        if(doc.executors.Count == 0){
                            if(doc.state != "выполнено"){
                                doc.state = "выполнено";
                                doc.note = $"Выполнено\t-{u.name}";
                                doc.state_date = DateTime.Parse(DateTime.Today.ToShortDateString().ToString());
                            }
                            if(doc.state_confirm == true)doc.state_confirm = false;
                        }
                        Console.WriteLine(  $"\n\t{u.name}, вы успешно закрыли извещение!");          
                    } else {
                        Console.WriteLine(  $"\n\t{u.name}, Вас не было в списке исполнителей!\n"+
                                            $"\tПроверьте: правильно ли написана ваша фамилия\n"+
                                            $"\tА также если вас нет в списке исполнителей - добавьте себя!");
                    }
                }
            }
            Console.Clear();
        }
        /// <summary>
        /// вернуть извещение
        /// </summary>
        /// <param name="doc">извещение</param>
        /// <param name="u">пользователь</param>
        public void send_back(Doc doc, User u){
            Console.Clear();
            ConsoleKeyInfo key;
            int count = 0;
            bool there = false;
            if(u.VIP){
                Console.WriteLine(  $"{u.name}, т.к. ваш статус: НАЧАЛЬНИК - Вы имеете права:\n" +
                                    $"принудительно изменить статус извещения, а также\n" +
                                    $"{doc.executors.Count} исполнителей и {doc.done_work.Count} закончивших обработку");
                Console.WriteLine(  $"\n\t> Нажите ENTER, чтобы изменить статус исполнителей <"+
                                    $"\n\t> Иначе любую клавишу, чтобы пропустить этот пункт <");
                key = Console.ReadKey();
                if(key.Key == ConsoleKey.Enter){
                    while(doc.executors.Count != 0){
                        doc.done_work.Add(doc.executors[0]);
                        doc.executors.Remove(doc.executors[0]);
                    }
                }
                if(doc.state != "возвращено"){
                    doc.state = "возвращено";
                    doc.note = $"возвращено\t-{u.name}";
                    doc.state_date = DateTime.Parse(DateTime.Today.ToShortDateString().ToString());
                }
                if(doc.state_confirm == true)doc.state_confirm = false;
            }else{
                Console.WriteLine("\tЕсли вы закончили работу - нажмите ENTER\n\tИначе - нажмите любую клавишу");
                key = Console.ReadKey();
                if(key.Key == ConsoleKey.Enter){
                    foreach(var v in doc.executors){
                        count++;
                        if(v == u.name){there = true; break;}
                    }
                    if(there){
                        if(doc.state != "возвращено"){
                            doc.state = "возвращено";
                            doc.note = $"возвращено\t-{u.name}";
                            doc.state_date = DateTime.Parse(DateTime.Today.ToShortDateString().ToString());
                        }
                        if(doc.state_confirm == true)doc.state_confirm = false;
                        Console.WriteLine(  $"\n\t{u.name}, вы успешно вернули извещение!");          
                    } else {
                        Console.WriteLine(  $"\n\t{u.name}, Вас не было в списке исполнителей!\n"+
                                            $"\tПроверьте: правильно ли написана ваша фамилия\n"+
                                            $"\tА также если вас нет в списке исполнителей - добавьте себя!");
                    }
                }
            }
            Console.Clear();
        }
        /// <summary>
        /// взять извещение в работу
        /// </summary>
        /// <param name="doc">извещение</param>
        /// <param name="u">пользователь</param>
        public void take(Doc doc,List<User> ul, User u){
            Console.Clear();
            int count = 0;
            ConsoleKeyInfo key;
            if(u.VIP){
                Console.WriteLine(  $"{u.name}, т.к. ваш статус: НАЧАЛЬНИК - Вы имеете право:\n" +
                                    $"установить исполнителей самостоятельно\n");
                Console.WriteLine(  $"\n\t> Нажите ENTER, если желаете установить исполнителей <"+
                                    $"\n\t > Иначе любую клавишу, чтобы пропустить этот пункт <");
                key = Console.ReadKey();
                if(key.Key == ConsoleKey.Enter)set_executors(doc,ul,u);
                if(doc.state != "в работе"){
                    doc.state = "в работе";
                    doc.note = $"В работе\t-{u.name}";
                    doc.state_date = DateTime.Parse(DateTime.Today.ToShortDateString().ToString());
                }
                if(doc.state_confirm == false)doc.state_confirm = true;
            }else{
                Console.WriteLine("\tЕсли вы берете извещение в работу - нажмите ENTER\nИначе - нажмите любую клавишу");
                key = Console.ReadKey();
                if(key.Key == ConsoleKey.Enter){
                    Console.WriteLine(  $"\n\t{u.name}, вы в списке исполнителей!");
                }
                if(doc.state != "в работе"){
                    doc.state = "в работе";
                    doc.note = $"В работе\t-{u.name}";
                    doc.state_date = DateTime.Parse(DateTime.Today.ToShortDateString().ToString());
                }
                if(doc.state_confirm == false)doc.state_confirm = true;
            }
            Console.Clear();
        }
        /// <summary>
        /// изменение автоматической даты состояния
        /// </summary>
        /// <param name="doc">выбранное извещение</param>
        public void set_state_date(Doc doc){
            List<string> date_str;
            string date_str_ent;
            bool excep = false;
            ConsoleKeyInfo key;
            while(true){
                do{
                    excep = false;
                    try{
                        Console.Clear();
                        Console.WriteLine($"Введите новую дату, если она отличается от {doc.state_date}, иначе просто нажмите ENTER");
                        date_str_ent = Console.ReadLine();
                        string[] date_arr_str = date_str_ent.Split(';','.','/',' ','\\',':','\'','*','+','-','|',')','(','"');
                        date_str = date_arr_str.ToList<string>();
                        if(date_str.Count>=2){
                            if(date_str.Count<3){
                                date_str.Add(DateTime.Today.Year.ToString());
                            }else if(Int32.Parse(date_str[2])<100){
                                date_str[2] = "20" + date_str[2];
                            }
                            date_str_ent = date_str[0] + '.' + date_str[1] + '.' + date_str[2];
                            doc.state_date = DateTime.Parse(date_str_ent);
                        }
                    }catch(Exception e){
                        excep = true;
                        Console.WriteLine($"Допущена ошибка при вводе данных!\nЛог ошибки: {e.Message}");
                        System.Threading.Thread.Sleep(7000);
                    }
                }while(excep);
                Console.Clear();
                Console.WriteLine($"Вы ввели {doc.state_date}, если хотите оставить этот вариант - нажмите ENTER, если ввести данные заново - любую клавишу");
                key = Console.ReadKey();
                if(key.Key == ConsoleKey.Enter)break;
            }
            Console.Clear();
        }
        /// <summary>
        /// Ввод примечания
        /// </summary>
        /// <param name="doc">выбранное извещение</param>
        public void add_note(Doc doc, User u){
            string str;
            ConsoleKeyInfo key;
            string n = "\n\t\t\t";
            int count = 1;
            Console.Clear();
            Console.WriteLine("\tЕсли вы случайно выбрали ф-цию ДОБАВИТЬ ПРИМЕЧАНИЕ - нажмите ESC\n\tИначе - нажмите любую кнопку");
            key = Console.ReadKey();
            if(key.Key != ConsoleKey.Escape){
                Console.WriteLine("Введите примечание: ");
                str = Console.ReadLine();
                for(int i = 0; i < str.Length; i++){
                    if(str[i] == ' ' && i >= 40*count){
                        str = str.Insert(i+1,n.ToString());
                        count++;
                    }
                }
                if(doc.note.Length>0)doc.note+="\n\t\t\t";
                doc.note+=(str + $"\n\t\t\t\t\t\t- {u.name}\n");
            }
            Console.Clear();
        }
        /// <summary>
        /// Ввод кол-ва
        /// </summary>
        /// <param name="doc">выбранное извещение</param>
        public void set_ammount(Doc doc){
            int key = 13;
            Console.Clear();
            if(doc.amount.Length>0){
                Console.WriteLine($"Вы желаете заменить кол-во {doc.amount} на другое?");
                Console.WriteLine("\tЕсли да - нажмите ENTER\n\tИначе - любую клавишу");
            }
            if(key == 13){
                Console.Write("Введите кол-во: ");
                doc.amount = Console.ReadLine();
            }
            Console.Clear();
        }      
        /// <summary>
        /// редактирование списка исполнителей
        /// </summary>
        /// <param name="doc">выбранное извещение</param>
        /// <param name="u">текущий пользователь - проверка прав</param>
        /// <param name="ul">список пользователей - проверка наличия/отсутствия в списке исполнителей</param>
        public void set_executors(Doc doc, List<User> ul, User u){
            ConsoleKeyInfo key;
            int keyPosition = 0;
            bool there = true;
            string name = "";
            if(u.VIP){
                do{
                    Console.Clear();
                    Console.Write("Исполнители: ");
                    foreach(var v in doc.executors){
                        Console.Write($"{v}, ");
                    }
                    Console.WriteLine(  $"\n\tЕсли желаете добавить нового исполнителя      - нажмите ENTER"+
                                        $"\n\tЕсли желаете удалить последнего исполнителя   - нажмите BACKSPACE"+
                                        $"\n\tЕсли желаете закончить редактирование         - нажмите ESC");
                    key = Console.ReadKey();
                    x = Console.CursorLeft;
                    y = Console.CursorTop;
                    if(key.Key==ConsoleKey.Backspace)doc.executors.RemoveAt(doc.executors.Count-1);
                    if(key.Key==ConsoleKey.Escape)break;
                    if(key.Key==ConsoleKey.Enter){
                        do{
                            Console.SetCursorPosition(x,y);
                            Console.WriteLine("\t\nДоступные сотрудники: ");
                            for(int i = 0; i<ul.Count; i++){
                                there = false;
                                foreach(var n in doc.executors){
                                    if(ul[i].name == n)there = true;
                                }
                                if(!there){
                                    if(keyPosition == i){
                                        Console.ForegroundColor = ConsoleColor.Green;
                                        name = ul[i].name;
                                    }
                                    Console.WriteLine($" - {ul[i].name}");
                                    Console.ForegroundColor = ConsoleColor.Gray;
                                }
                            }
                            key = Console.ReadKey();
                            if (key.Key == ConsoleKey.UpArrow && keyPosition > 0) keyPosition--;
                            if (key.Key == ConsoleKey.DownArrow && keyPosition < ul.Count-1) keyPosition++;
                            if (key.Key==ConsoleKey.Enter) {
                                doc.executors.Add(name);
                            }
                        }while(key.Key != ConsoleKey.Enter);
                    }
                }while(key.Key != ConsoleKey.Escape);
            } else {
                Console.Clear();
                Console.WriteLine(  $"\n\tЕсли желаете добавить себя как исполнителя        - нажмите ENTER"+
                                    $"\n\tЕсли желаете удалить себя из списка исполнителей  - нажмите BACKSPACE"+
                                    $"\n\tЕсли желаете закончить редактирование             - нажмите ESC");
                key = Console.ReadKey();
                there = false;
                if(key.Key == ConsoleKey.Enter){
                    foreach(var v in doc.executors){
                        if(v == u.name)there = true;
                    }
                    if(!there){
                        doc.executors.Add(u.name);
                        Console.WriteLine("\n\tВы добавлены!");System.Threading.Thread.Sleep(3000);
                    }else{
                        Console.WriteLine("\n\tВы уже в списке!");System.Threading.Thread.Sleep(3000);
                    }
                }
                if(key.Key == ConsoleKey.Backspace){
                    there = false;
                    for(int i = 0; i<doc.executors.Count; i++){
                        if(doc.executors[i] == u.name){
                            doc.executors.RemoveAt(i);
                            show_ln("\n\tВы удалены из списка!");System.Threading.Thread.Sleep(3000);
                            there = true;
                        }
                    }
                    if(!there){
                        show_ln("\n\tВас не было в списке!");System.Threading.Thread.Sleep(3000);
                    }
                }
            }
            clr();
        }
        /// <summary>
        /// отображение кол-ва изв в работе у каждого сотрудника
        /// </summary>
        /// <param name="ldoc">лист извещений</param>
        /// <param name="ul">лист сотрудников</param>
        public void VIP_info(List<Doc> ldoc, List<User> ul){
            int count = 0;
            int col = 0;
            int col_amount = 3;
            show_ln("");
            foreach(var user in ul){
                count = 0;
                foreach(var doc in ldoc){
                    foreach(var exuc in doc.executors){
                        if(user.name == exuc && doc.state_confirm)count++;
                    }
                }
                col++;
                show($"{user.name}:");
                if(user.name.Length<15){
                    show("\t\t");
                } else {
                    show("\t");
                }
                show($"{count}\t");
                if(col%col_amount == 0)show_ln("");
            }
            show_ln("\n\n");
        }
        public List<Action> doc_func(Doc doc, List<User>ul, User u){
            var func = new List<Action>();
            func.Add(()=>change_date_receive(doc));
            func.Add(()=>set_nomber_doc(doc));
            func.Add(()=>set_category(doc));
            func.Add(()=>set_product(doc));
            func.Add(()=>set_priority(doc));
            func.Add(()=>set_database(doc));
            func.Add(()=>add_rout(doc));
            func.Add(()=>set_executors(doc,ul,u));
            func.Add(()=>finish(doc,u));
            func.Add(()=>set_state(doc,u));
            func.Add(()=>set_state_date(doc));
            func.Add(()=>add_note(doc,u));
            func.Add(()=>set_ammount(doc));
            return func;
        }
    }
 
    /// <summary>
    /// класс с минимальными ф-циям для гостя
    /// </summary>
    internal class Program_Work_Space_guest:Console_Func_Program{
        public int x;
        public int y;
        ///наименование стобцов
        public List<string> name_col = new List<string>(){
            "Дата получения", "№ извещения", "Основание", "Изделие", "Приоритет", "БД", "Состояние", "Дата изменения", "Кол-во"
            };
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public File_Doc testing_table(){
            File_Doc FD = new File_Doc();
            FD.file_doc.Add(new Doc{date_receive = new DateTime(2021,03,05),
                                    nomber_doc = "0417-0350", category = "заказ",
                                    product = "Пилот", priority = "1",
                                    database = "ДО",
                                    rout = {"МетОП","МехОП","64"},
                                    amount = "24",
                                    state = "в работе",
                                    state_confirm = true,
                                    note = "",
                                    state_date = new DateTime(2021,03,17),
                                    executors={"Пчелинцева А.Я.", "Казаев В.А.","Пугина Н.В.", "Воробьева Е.А.","Бубнов В.Б.","Пискунова Г.А."}});
            
            FD.file_doc.Add(new Doc{date_receive = new DateTime(2021,04,20),
                                    nomber_doc = "0424-2350",
                                    category = "заказ",
                                    product = "blokset",
                                    priority = "1",
                                    database = "ДО",
                                    rout = {"МетОП","МехОП"},
                                    amount = "34",
                                    state = "выполнено",
                                    state_confirm = false,
                                    note = "вопрос к КС",
                                    state_date = new DateTime(2021,04,27),
                                    done_work={"Бородин В.А.", "Казаев В.А.","Данодина М.А", "Ледовских Н.Г.","Байкова Е.А.","Пискунова Г.А."}});

            FD.file_doc.Add(new Doc{date_receive = new DateTime(2021,05,10),
                                    nomber_doc = "0434-4512",
                                    category = "улучшение",
                                    product = "НКУ-СЭЩ-М",
                                    priority = "2",
                                    database = "ЛОЦМАН",
                                    rout = {"МетОП","МехОП","ПГП"},
                                    amount = "43",
                                    state = "возращено",
                                    state_confirm = false,
                                    note = "корректировка листа извещения",
                                    state_date = new DateTime(2021,05,29),
                                    executors={"Бородин В.А.", "Казаев В.А.","Кондрат В.В.","Данодина М.А", "Ледовских Н.Г.","Байкова Е.А.","Пискунова Г.А.","Самсонова Е.А."}});
            return FD;
        }
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Вход в систему, выбор прав при вводе табельного номера
        /// </summary>
        /// <param name="user">пользователь из файла CSV</param>
        public void testing(User user){
            if(user.login==0)show($"Добро пожаловать в систему {user.name}!\n\nВаш статус: Гость\n\n> Нажмите любую клавишу <");
            if(user.login>0 && !user.VIP)show($"Добро пожаловать в систему {user.name}!\n\nВаш статус: Сотрудник\n\n> Нажмите любую клавишу <");
            if(user.login>0 && user.VIP)show($"Добро пожаловать в систему {user.name}!\n\nВаш статус: Начальник\n\n> Нажмите любую клавишу <");
            key();
        }
        /// <summary>
        /// Костыль для выравнивания текста в таблице
        /// </summary>
        /// <param name="i">Кол-во пробелов для цикла</param>
        /// <returns></returns>
        public string tab(int i){
            string str = "";
            for(;i!=0;i--){
                str += ' ';
            }
            return str;
        }
        /// <summary>
        /// Получения максимальной длины одного поля в столбце, для высчета кол-ва пробелов
        /// </summary>
        /// <param name="ld">Лист извещений</param>
        /// <returns></returns>
        public List<int> getMaxLength(List<Doc> ld){
            ///ВОЗМОЖНО нужно создать 2 класса ТАБЛИЦА и ДОП ИНФО - веместо одного DOC, чтобы вести перебор через item в цикле
            ///устанавливаем самую длинную строку в таблице
            List<int> max_length = new List<int>();
            foreach(var n in name_col){
                max_length.Add(n.Length+1);          //задаем максимальную длину слова в столбце +1 пробел для проствета между символоми;
            }
            foreach(var n in ld){
                if(n.date_receive != null){
                    if(max_length[0]<=n.date_receive.ToString().Length)max_length[0]=n.date_receive.ToString().Length+1;
                }
                if(n.nomber_doc != null){
                    if(max_length[1]<=n.nomber_doc.Length)max_length[1]=n.nomber_doc.Length+1;
                }
                if(n.category != null){
                    if(max_length[2]<=n.category.Length)max_length[2]=n.category.Length+1;
                }
                if(n.product != null){
                    if(max_length[3]<=n.product.Length)max_length[3]=n.product.Length+1;
                }
                if(n.priority != null){
                    if(max_length[4]<=n.priority.Length)max_length[4]=n.priority.Length+1;
                }
                if(n.database != null){
                    if(max_length[5]<=n.database.Length)max_length[5]=n.database.Length+1;
                }
                if(n.state != null){
                    if(max_length[6]<=n.state.Length)max_length[6]=n.state.Length+1;
                }
                if(n.state_date != null){
                    if(max_length[7]<=n.state_date.ToString().Length)max_length[7]=n.state_date.ToString().Length+1;
                }
                if(n.amount != null){
                    if(max_length[8]<=n.amount.Length)max_length[8]=n.amount.Length+1;
                }
            }
            return max_length;
        }
        /// <summary>
        /// создает матрицу чисел, в которой хранится кол-во пробелов для каждой ячейки в таблице
        /// </summary>
        /// <param name="ld">лист извещений</param>
        /// <returns></returns>
        public List<List<int>> getTabs(List<Doc> ld){
            ///выравнивание
            int count_of_col = name_col.Count;
            int count_of_str = ld.Count;
            ///создаю матрицу отступов для таблицы
            List<List<int>> tabs = new List<List<int>>();
            List<int> max_length = getMaxLength(ld);
            for(int i = 0; i < count_of_str; i++){
                List<int> col = new List<int>();
                for(int j = 0; j < count_of_col; j++){
                    col.Add(0);
                }
                tabs.Add(col);
            }
            ///заполняем матрицу
            for(int i = 0; i<ld.Count;i++){
                if(ld[i].date_receive != null){tabs[i][0] = max_length[0] - ld[i].date_receive.ToString().Length;}
                else{tabs[i][0] = max_length[0];}
                if(ld[i].nomber_doc != null){tabs[i][1] = max_length[1] - ld[i].nomber_doc.Length;}
                else{tabs[i][1] = max_length[1];}
                if(ld[i].category != null){tabs[i][2] = max_length[2] - ld[i].category.Length;}
                else{tabs[i][2] = max_length[2];}
                if(ld[i].product != null){tabs[i][3] = max_length[3] - ld[i].product.Length;}
                else{tabs[i][3] = max_length[3];}
                if(ld[i].priority != null){tabs[i][4] = max_length[4] - ld[i].priority.Length;}
                else{tabs[i][4] = max_length[4];}
                if(ld[i].database != null){tabs[i][5] = max_length[5] - ld[i].database.Length;}
                else{tabs[i][5] = max_length[5];}
                if(ld[i].state != null){tabs[i][6] = max_length[6] - ld[i].state.Length;}
                else{tabs[i][6] = max_length[6];}
                if(ld[i].state_date != null){tabs[i][7] = max_length[7] - ld[i].state_date.ToString().Length;}
                else{tabs[i][7] = max_length[7];}
                if(ld[i].amount != null){tabs[i][8] = max_length[8] - ld[i].amount.Length;}
                else{tabs[i][8] = max_length[8];}
            }
            ///
            return tabs;
        }
        /// <summary>
        /// Создает лист чисел, в котором хранятся кол-во пробелов для каждого столбца одного извещения
        /// </summary>
        /// <param name="ld">лист извещений</param>
        /// <param name="nomber_doc">номер извещения</param>
        /// <returns></returns>
        public List<int> getColTabs(List<Doc> ld, string nomber_doc){
            ///выравнивание
            int n = 0;
            int count_of_col = name_col.Count;
            int count_of_str = ld.Count;
            ///создаю матрицу отступов для таблицы
            List<List<int>> tabs = new List<List<int>>();
            List<int> max_length = getMaxLength(ld);
            for(int i = 0; i < count_of_str; i++){
                List<int> col = new List<int>();
                for(int j = 0; j < count_of_col; j++){
                    col.Add(0);
                }
                tabs.Add(col);
            }
            ///заполняем матрицу - считаю это обязательным, т.к. возможно сортировка может изменить положение извещений в списке
            for(int i = 0; i<ld.Count;i++){
                if(ld[i].date_receive != null){tabs[i][0] = max_length[0] - ld[i].date_receive.ToString().Length;}
                else{tabs[i][0] = max_length[0];}
                if(ld[i].nomber_doc != null){tabs[i][1] = max_length[1] - ld[i].nomber_doc.Length;}
                else{tabs[i][1] = max_length[1];}
                if(ld[i].category != null){tabs[i][2] = max_length[2] - ld[i].category.Length;}
                else{tabs[i][2] = max_length[2];}
                if(ld[i].product != null){tabs[i][3] = max_length[3] - ld[i].product.Length;}
                else{tabs[i][3] = max_length[3];}
                if(ld[i].priority != null){tabs[i][4] = max_length[4] - ld[i].priority.Length;}
                else{tabs[i][4] = max_length[4];}
                if(ld[i].database != null){tabs[i][5] = max_length[5] - ld[i].database.Length;}
                else{tabs[i][5] = max_length[5];}
                if(ld[i].state != null){tabs[i][6] = max_length[6] - ld[i].state.Length;}
                else{tabs[i][6] = max_length[6];}
                if(ld[i].state_date != null){tabs[i][7] = max_length[7] - ld[i].state_date.ToString().Length;}
                else{tabs[i][7] = max_length[7];}
                if(ld[i].amount != null){tabs[i][8] = max_length[8] - ld[i].amount.Length;}
                else{tabs[i][8] = max_length[8];}
            }
            ///
            foreach(var v in ld){
                if(v.nomber_doc == nomber_doc)break;
                n++;
            }
            return tabs[n];
        }
        /// <summary>
        /// создает отступы в заголовке таблицы, в названиях столбцов
        /// </summary>
        /// <param name="ld">лист извещений</param>
        /// <returns></returns>
        public List<int> getNameColTabs(List<Doc> ld){
            ///выравнивание
            int count_of_col = name_col.Count;
            List<int> max_length = getMaxLength(ld);
            List<int> col = new List<int>();
            for(int i = 0; i<count_of_col;i++){
                col.Add(max_length[i] - name_col[i].Length);
            }
            ///
            return col;
        }
        /// <summary>
        /// создает границу из символов, из расчета ширины таблицы, для лучшего чтения данных пользователем
        /// </summary>
        /// <param name="ld">лист извещений</param>
        /// <param name="n">разделяющий символ</param>
        public void showEdge(List<Doc> ld, char n){
            List<int> max_length = getMaxLength(ld);
            int line_length = 0;
            foreach(var v in max_length){
                line_length += v;
            }
            show_ln("");
            for(int i = 0; i<line_length; i++){
                show(n.ToString());
            }
            show_ln("");
        }
        /// <summary>
        /// Вывод таблицы
        /// </summary>
        /// <param name="ordered_data">отсортированный лист извещений</param>
        public void showTable(List<Doc> ordered_data){
            List<List<int>> t = getTabs(ordered_data);
            for(int i = 0; i < ordered_data.Count; i++){
                show($"{ordered_data[i].date_receive}{tab(t[i][0])}{ordered_data[i].nomber_doc}{tab(t[i][1])}{ordered_data[i].category}{tab(t[i][2])}{ordered_data[i].product}{tab(t[i][3])}{ordered_data[i].priority}{tab(t[i][4])}");
                show($"{ordered_data[i].database}{tab(t[i][5])}{ordered_data[i].state}{tab(t[i][6])}{ordered_data[i].state_date}{tab(t[i][7])}{ordered_data[i].amount}{tab(t[i][8])}");               
                showEdge(ordered_data, '-');
            }
        }
        /// <summary>
        /// Вывод одного извещения
        /// </summary>
        /// <param name="ordered_data">отсортированный лист извещений</param>
        /// <param name="document">извещение</param>
        public void showDoc(List<Doc> ordered_data, Doc document){
            List<int> t = getColTabs(ordered_data, document.nomber_doc);
            show($"{document.date_receive}{tab(t[0])}{document.nomber_doc}{tab(t[1])}{document.category}{tab(t[2])}{document.product}{tab(t[3])}{document.priority}{tab(t[4])}");
            show($"{document.database}{tab(t[5])}{document.state}{tab(t[6])}{document.state_date}{tab(t[7])}{document.amount}{tab(t[8])}");
            showEdge(ordered_data, '-');
        }
        /// <summary>
        /// сортировка по дате получения
        /// </summary>
        /// <param name="ld">лист извещений</param>
        /// <returns></returns>
        public List<Doc> sortTable_date_receive(List<Doc> ld){
            /// по дате выдачи
            var ordered_data = from n in ld orderby n.date_receive descending select n;
            if(ld.SequenceEqual(ordered_data)){
                ordered_data = from n in ld orderby n.date_receive ascending select n;
            };
            return ordered_data.ToList<Doc>();
        }
        /// <summary>
        /// сортировка по номеру извещения
        /// </summary>
        /// <param name="ld">лист извещений</param>
        /// <returns></returns>
        public List<Doc> sortTable_nomber_doc(List<Doc> ld){
            /// по номеру изв
            var ordered_data = from n in ld orderby n.nomber_doc descending select n;
            if(ld.SequenceEqual(ordered_data)){
                ordered_data = from n in ld orderby n.nomber_doc ascending select n;
            };
            return ordered_data.ToList<Doc>();
        }
        /// <summary>
        /// сортировка по основанию
        /// </summary>
        /// <param name="ld">лист извещений</param>
        /// <returns></returns>
        public List<Doc> sortTable_category(List<Doc> ld){
            /// по основанию
            var ordered_data = from n in ld orderby n.category descending select n;
            if(ld.SequenceEqual(ordered_data)){
                ordered_data = from n in ld orderby n.category ascending select n;
            };
            return ordered_data.ToList<Doc>();
        }
        /// <summary>
        /// сортировка по приоритету
        /// </summary>
        /// <param name="ld">лист извещений</param>
        /// <returns></returns>
        public List<Doc> sortTable_priority(List<Doc> ld){
            /// по изделию
            var ordered_data = from n in ld orderby n.priority descending select n;
            if(ld.SequenceEqual(ordered_data)){
                ordered_data = from n in ld orderby n.priority ascending select n;
            };
            return ordered_data.ToList<Doc>();
        }
        /// <summary>
        /// сортировка по изделию
        /// </summary>
        /// <param name="ld">лист извещений</param>
        /// <returns></returns>
        public List<Doc> sortTable_product(List<Doc> ld){
            /// по изделию
            var ordered_data = from n in ld orderby n.product descending select n;
            if(ld.SequenceEqual(ordered_data)){
                ordered_data = from n in ld orderby n.product ascending select n;
            };
            return ordered_data.ToList<Doc>();
        }
        /// <summary>
        /// сортировка по БД
        /// </summary>
        /// <param name="ld">лист извещений</param>
        /// <returns></returns>
        public List<Doc> sortTable_database(List<Doc> ld){
            /// по изделию
            var ordered_data = from n in ld orderby n.database descending select n;
            if(ld.SequenceEqual(ordered_data)){
                ordered_data = from n in ld orderby n.database ascending select n;
            };
            return ordered_data.ToList<Doc>();
        }
        /// <summary>
        /// сортировка по состоянию
        /// </summary>
        /// <param name="ld">лист извещений</param>
        /// <returns></returns>
        public List<Doc> sortTable_state(List<Doc> ld){
            /// по изделию
            var ordered_data = from n in ld orderby n.state descending select n;
            if(ld.SequenceEqual(ordered_data)){
                ordered_data = from n in ld orderby n.state ascending select n;
            };
            return ordered_data.ToList<Doc>();
        }
        /// <summary>
        /// сортировка по дате изменения состояния
        /// </summary>
        /// <param name="ld">лист извещений</param>
        /// <returns></returns>
        public List<Doc> sortTable_state_date(List<Doc> ld){
            /// по изделию
            var ordered_data = from n in ld orderby n.state_date descending select n;
            if(ld.SequenceEqual(ordered_data)){
                ordered_data = from n in ld orderby n.state_date ascending select n;
            };
            return ordered_data.ToList<Doc>();
        }
        /// <summary>
        /// сортировка по колличеству
        /// </summary>
        /// <param name="ld">лист извещений</param>
        /// <returns></returns>
        public List<Doc> sortTable_amount(List<Doc> ld){
            /// по изделию
            var ordered_data = from n in ld orderby n.amount descending select n;
            if(ld.SequenceEqual(ordered_data)){
                ordered_data = from n in ld orderby n.amount ascending select n;
            };
            return ordered_data.ToList<Doc>();
        }
        /// <summary>
        /// выбор извещения из списка
        /// </summary>
        /// <param name="ordered_doc">отсортированный лист извещений</param>
        /// <returns></returns>
        public string select_curent_doc(List<Doc> ordered_doc){
            cursor_show(false);
            x = cursor_x();
            y = cursor_y();
            int keyPosition = 0;
            List<int> t = getNameColTabs(ordered_doc);
            ConsoleKeyInfo key;
            string return_str = "";
            do{
                cursor_move(x,y);
                for(int i = 0;i<name_col.Count;i++){
                    Console.Write($"{name_col[i]}{tab(t[i])}");
                }
                showEdge(ordered_doc,'=');
                for(int i = 0; i < ordered_doc.Count; i++){                                  //вопрос - как тут фор заменить на форыч
                    if(keyPosition == i)Console.ForegroundColor = ConsoleColor.Green;
                    showDoc(ordered_doc, ordered_doc[i]);
                    Console.ForegroundColor = ConsoleColor.Gray;
                }
                key = Console.ReadKey();
                if (key.Key == ConsoleKey.UpArrow && keyPosition > 0) keyPosition--;
                if (key.Key == ConsoleKey.DownArrow && keyPosition < ordered_doc.Count-1) keyPosition++;
                if (key.Key == ConsoleKey.Enter) {
                    for(int i = 0; i < ordered_doc.Count; i++){
                        if(i == keyPosition){
                            return_str = ordered_doc[i].nomber_doc;
                        }
                    }
                }
                if (key.Key == ConsoleKey.Escape)break;
            }while(key.Key != ConsoleKey.Enter);
            return return_str;
        }
        /// <summary>
        /// ф-ция - костыль, для ввода цикла в лист
        /// </summary>
        /// <param name="arg">одно из полей DOC, которое представляет из себя List</param>
        /// <param name="name_arg">наименование поля для вывода</param>
        public void cycle_doc(List<string> arg, string name_arg){
            show($"{name_arg}:\n");
            foreach(var v in arg){
                show_ln($"\t\t\t{v}\n");
            }
            show_ln("");
        }
        /// <summary>
        /// Вывод подробной информации извещения в List
        /// </summary>
        /// <param name="curent_doc">выбранный документ</param>
        /// <returns>Возвращает лист</returns>
        public List<Action> doc_info(Doc curent_doc){
            var list = new List<Action>();
            list.Add(()=>show_ln($"Дата получения изв.:\t{curent_doc.date_receive}\n"));
            list.Add(()=>show_ln($"Номер изв.:\t\t{curent_doc.nomber_doc}\n"));
            list.Add(()=>show_ln($"Основание:\t\t{curent_doc.category}\n"));
            list.Add(()=>show_ln($"Изделие:\t\t{curent_doc.product}\n"));
            list.Add(()=>show_ln($"Приоритет:\t\t{curent_doc.priority}\n"));
            list.Add(()=>show_ln($"БД:\t\t\t{curent_doc.database}\n"));
            list.Add(()=>cycle_doc(curent_doc.rout,"Маршрут"));
            list.Add(()=>cycle_doc(curent_doc.executors,"В работе"));
            list.Add(()=>cycle_doc(curent_doc.done_work,"Закончили работу"));
            list.Add(()=>show_ln($"Состояние изв.:\t\t{curent_doc.state}\n"));
            list.Add(()=>show_ln($"Дата изм. состояния:\t{curent_doc.state_date}\n"));
            list.Add(()=>show_ln($"Примечание:\t\t{curent_doc.note}\n"));
            list.Add(()=>show_ln($"Количество:\t\t{curent_doc.amount}\n"));
            return list;
        }
        /// <summary>
        /// открыть подробную информацию выбранного извещения
        /// </summary>
        /// <param name="ld">лист извещений</param>
        /// <param name="s_name">переменная для хранения номера извещения</param>
        public void open_curent_doc(List<Doc>ld, string s_name){
            int n = 0;
            Doc curent_doc = new Doc();
            var info = new List<Action>();
            clr();
            for (;n<ld.Count;n++){
                if(ld[n].nomber_doc == s_name){
                    curent_doc = ld[n];
                    break;
                }
            }
            info = doc_info(curent_doc);
            foreach(var v in info){
                v();
            }
            show_ln("\n\t\t>Нажмите любую клавишу<");
            key();
        }
        /// <summary>
        /// вернуть извещение по номеру
        /// </summary>
        /// <param name="ld">лист извещений</param>
        /// <param name="s_name">номер извещения</param>
        /// <returns></returns>
        public Doc get_curent_doc(List<Doc>ld, string s_name){
            Doc curent_doc = new Doc();
            foreach(var n in ld){
                if(n.nomber_doc == s_name){
                    curent_doc = n;
                    break;
                }
            }
            return curent_doc;
        }
    }
}