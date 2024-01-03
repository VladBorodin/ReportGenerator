using System;
using System.Collections.Generic;
using System.Linq;

namespace ReportGenerator
{
    internal class Interface : Program_Work_Space{
        /// <summary>
        /// Главное окно в котором находится панель управления и вывод таблицы
        /// </summary>
        /// <param name="u">пользователь</param>
        /// <param name="ldoc">лист извещений</param>
        /// <param name="ul">лист сотрудников</param>
        public void main_window(List<Doc> ldoc, List<User> ul, User u){
            bool exit = false;
            Console.CursorVisible = false;
            do{
                Console.Clear();
                if(u.VIP)VIP_info(ldoc, ul);
                exit = menu_main(ldoc,ul,u);
            }while(!exit);
        }
        /// <summary>
        /// главная панель управления таблицей
        /// </summary>
        /// <param name="ldoc">лист извещений</param>
        /// <param name="ul">лист сотрудников</param>
        /// <param name="u">пользователь</param>
        /// <returns>возвращает булевую, для окончания/продолжения работы</returns>
        public bool menu_main(List<Doc> ldoc, List<User> ul, User u){
            int keyPosition = 1;                                                        //позиция для выбора стрелочками
            //int n_m = 0;                                                              //создать словарь или лист для перечисления ф-ций меню
            int x1 = 0; int y1 = 0; //выравнивание курсора
            bool exit = false;
            ConsoleKeyInfo key;
            List<string> menu_main_name = new List<string>(){"добавить извещение","сортировать","изменить состояние извещения","открыть","редактировать"};
            var list = new List<Action>();
            List<int> t = getNameColTabs(ldoc);
            list.Add(()=>addDoc(ldoc,ul,u));
            if(u.VIP){list.Add(() => menu_sort(ldoc, ul, u));}
            else{list.Add(()=>menu_sortByCol(ldoc,ul,u));}
            list.Add(()=>menu_state(get_curent_doc(ldoc,select_curent_doc(ldoc)), ul, u));
            list.Add(()=>open_curent_doc(ldoc,select_curent_doc(ldoc)));
            list.Add(()=>menu_change(get_curent_doc(ldoc,select_curent_doc(ldoc)),ul,u));
            x = Console.CursorLeft; y = Console.CursorTop;
            do {
                Console.SetCursorPosition(x,y);
                for(int i = 0; i < menu_main_name.Count; i++) {
                    Console.Write("  ");
                    if (keyPosition == i + 1) {
                        Console.BackgroundColor = ConsoleColor.Gray;
                        Console.ForegroundColor = ConsoleColor.Black;
                    }
                    Console.Write(menu_main_name[i]);
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.ForegroundColor = ConsoleColor.Gray;
                }
                Console.ResetColor();
                Console.WriteLine("\n");
                x1 = Console.CursorLeft; y1 = Console.CursorTop;
                for(int i = 0;i<name_col.Count;i++){
                    Console.Write($"{name_col[i]}{tab(t[i])}");
                }
                showEdge(ldoc,'=');
                showTable(ldoc);
                key = Console.ReadKey();
                if(key.Key == ConsoleKey.LeftArrow) {
                    if (keyPosition > 1) keyPosition--;
                }else if(key.Key == ConsoleKey.RightArrow) {
                    if (keyPosition < menu_main_name.Count) keyPosition++;
                }
                if (key.Key == ConsoleKey.Enter) {
                    if(keyPosition>0)Console.SetCursorPosition(x1,y1);
                    list[keyPosition-1]();
                }
                if(key.Key == ConsoleKey.Escape)break;
            } while(key.Key != ConsoleKey.Enter);
            if(key.Key == ConsoleKey.Escape)exit = true;
            return exit;
        }
        /// <summary>
        /// Меню выбора сортировок, доступно только для начальников
        /// </summary>
        /// <param name="ldoc">лист извещений</param>
        /// <param name="ul">лист сотрудников</param>
        /// <param name="u">пользователь</param>
        //БЫЛ РЕФ
        public void menu_sort(List<Doc> ldoc, List<User> ul, User u){
            Console.Clear();
            Console.CursorVisible = false;
            int keyPosition = 0;
            ConsoleKeyInfo key;
            if(u.VIP)VIP_info(ldoc, ul);
            Console.WriteLine();
            x = Console.CursorLeft; y = Console.CursorTop;
            do{
                Console.SetCursorPosition(x,y);
                Console.Write("Сортировать:\t");
                if(keyPosition == 0){
                    Console.BackgroundColor = ConsoleColor.Gray;
                    Console.ForegroundColor = ConsoleColor.Black;
                }
                Console.Write("по столбцам таблицы");
                Console.BackgroundColor = ConsoleColor.Black;
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.Write("\t");
                if(keyPosition == 1){
                    Console.BackgroundColor = ConsoleColor.Gray;
                    Console.ForegroundColor = ConsoleColor.Black;
                }
                Console.Write("по сотрудникам");
                Console.BackgroundColor = ConsoleColor.Black;
                Console.ForegroundColor = ConsoleColor.Gray;
                key = Console.ReadKey();
                if(key.Key == ConsoleKey.LeftArrow && keyPosition > 0)keyPosition--;
                if(key.Key == ConsoleKey.RightArrow && keyPosition < 1)keyPosition++;
                if(key.Key == ConsoleKey.Escape)break;
            }while(key.Key != ConsoleKey.Enter);
            if(key.Key != ConsoleKey.Escape){
                if(keyPosition==0)menu_sortByCol(ldoc, ul, u);
                if(keyPosition==1)menu_sortByUser(ldoc, ul);
            }
        }
        /// <summary>
        /// Делегат всех доступных сортировок по столбцам
        /// </summary>
        /// <param name="ldoc">лист извещений</param>
        /// <returns>возвращает отсортированные данные</returns>
        delegate List<Doc> MenuSort(List<Doc> ldoc);
        /// <summary>
        /// сортировка по столбцу
        /// </summary>
        /// <param name="ldoc">лист извещений</param>
        /// <param name="ul">лист сотрудников</param>
        /// <param name="u">пользователь</param>
        //БЫЛ РЕФ
        public void menu_sortByCol(List<Doc> ldoc, List<User> ul, User u){
            ConsoleKeyInfo key;
            Console.CursorVisible = false;
            int keyPosition = 0;
            Console.Clear();
            x = 0; y = 0;
            List<int> t = getNameColTabs(ldoc);
            Dictionary<int,MenuSort> menu_sort = new Dictionary<int, MenuSort>();
            int n_sort = 0;
            menu_sort.Add(n_sort++,sortTable_date_receive);
            menu_sort.Add(n_sort++,sortTable_nomber_doc);
            menu_sort.Add(n_sort++,sortTable_category);
            menu_sort.Add(n_sort++,sortTable_product);
            menu_sort.Add(n_sort++,sortTable_priority);
            menu_sort.Add(n_sort++,sortTable_database);
            menu_sort.Add(n_sort++,sortTable_state);
            menu_sort.Add(n_sort++,sortTable_state_date);
            menu_sort.Add(n_sort++,sortTable_amount);
            do{
                Console.SetCursorPosition(x, y);
                if(u.VIP)VIP_info(ldoc,ul);
                Console.WriteLine("\n\t\t\t> Для завершения сортировки нажмите ESC <\n");
                for(int i = 0;i<name_col.Count;i++){
                    if(keyPosition == i){
                        Console.BackgroundColor = ConsoleColor.Gray; 
                        Console.ForegroundColor = ConsoleColor.Black; 
                    }
                    Console.Write($"{name_col[i]}{tab(t[i])}");
                    Console.BackgroundColor = ConsoleColor.Black; 
                    Console.ForegroundColor = ConsoleColor.Gray; 
                }
                showEdge(ldoc, '=');
                showTable(ldoc);
                key = Console.ReadKey();
                if(key.Key == ConsoleKey.LeftArrow){
                    if(keyPosition>0) keyPosition--;
                } else if(key.Key == ConsoleKey.RightArrow){
                    if(keyPosition<name_col.Count-1) keyPosition++;
                }
                if(key.Key == ConsoleKey.Enter){
                    MenuSort MS = menu_sort.ElementAt(keyPosition).Value;
                    ldoc = MS(ldoc);
                }
            }while(key.Key != ConsoleKey.Escape);
        }        
        /// <summary>
        /// вывод всех докуметов, что в работе у выбранного сотрудника
        /// </summary>
        /// <param name="ldoc">лист извещений</param>
        /// <param name="ul">лист сотрудников</param>
        public void menu_sortByUser(List<Doc> ldoc, List<User> ul){
            int count = 0;
            int col = 0;
            int col_amount = 3;
            bool there = false;
            List<int> t = getNameColTabs(ldoc);
            ConsoleKeyInfo key;
            Console.CursorVisible = false;
            int keyPosition = 0;
            do{
                do{
                    Console.Clear();
                    Console.WriteLine();
                    for(int i = 0; i < ul.Count; i++){
                        count = 0;
                        foreach(var doc in ldoc){
                            foreach(var exuc in doc.executors){
                                if(ul[i].name == exuc && doc.state_confirm)count++;
                            }
                        }
                        col++;
                        if(keyPosition==i){
                            Console.BackgroundColor = ConsoleColor.Gray;
                            Console.ForegroundColor = ConsoleColor.Black; 
                        }
                        Console.Write($"{ul[i].name}:");
                        Console.BackgroundColor = ConsoleColor.Black; 
                        Console.ForegroundColor = ConsoleColor.Gray;
                        if(ul[i].name.Length<15){
                            Console.Write("\t\t");
                        } else {
                            Console.Write("\t");
                        }
                        Console.Write($"{count}\t");
                        if(col%col_amount == 0)Console.WriteLine();
                    }
                    key = Console.ReadKey();
                    if(key.Key == ConsoleKey.UpArrow){
                        if(keyPosition>col_amount-1)keyPosition-=col_amount;
                    }
                    if(key.Key == ConsoleKey.DownArrow){
                        if(keyPosition<(ul.Count-col_amount))keyPosition+=col_amount;
                    }
                    if(key.Key == ConsoleKey.LeftArrow){
                        if(keyPosition>0)keyPosition--;
                    }
                    if(key.Key == ConsoleKey.RightArrow){
                        if(keyPosition<ul.Count-1)keyPosition++;
                    }
                    if(key.Key == ConsoleKey.Escape)break;
                    Console.WriteLine("\n\t\t\t> Для завершения сортировки нажмите ESC <\n");
                }while(key.Key != ConsoleKey.Enter);
                if(key.Key != ConsoleKey.Escape){
                for(int i = 0;i<name_col.Count;i++){
                    Console.Write($"{name_col[i]}{tab(t[i])}");
                }
                showEdge(ldoc,'=');
                    foreach(var n in ldoc){
                        there = false;
                        foreach(var v in n.executors){
                            if(ul[keyPosition].name==v && n.state_confirm){
                                there = true;
                                break;
                            }
                        }
                        if(there)showDoc(ldoc, n);
                    }
                    key = Console.ReadKey();
                }
            }while(key.Key != ConsoleKey.Escape);
        }
        /// <summary>
        /// меню выбора стандартного состояния
        /// </summary>
        /// <param name="doc">извещение</param>
        /// <param name="ul">лист сотрудников</param>
        /// <param name="u">пользователь</param>
        public void menu_state(Doc doc, List<User>ul, User u){
            Console.Clear();
            Console.WriteLine("\n\n");
            x = Console.CursorLeft; y = Console.CursorTop;
            Console.CursorVisible = false;
            int keyPosition = 1;
            ConsoleKeyInfo key;
            do {
                if(doc.nomber_doc == null)break;
                while(Console.KeyAvailable == false){
                    Console.SetCursorPosition(x, y);
                    Console.WriteLine("\tВыбор состояния извещения: ");
                    if (keyPosition == 1)Console.ForegroundColor = ConsoleColor.DarkCyan;
                    Console.WriteLine("\t\tВыполнено");
                    Console.ForegroundColor = ConsoleColor.Gray;
                    if (keyPosition == 2)Console.ForegroundColor = ConsoleColor.DarkCyan;
                    Console.WriteLine("\t\tВозвращено");
                    Console.ForegroundColor = ConsoleColor.Gray;
                    if (keyPosition == 3)Console.ForegroundColor = ConsoleColor.DarkCyan;
                    Console.WriteLine("\t\tВ работе");
                    Console.ForegroundColor = ConsoleColor.Gray;
                }
                key = Console.ReadKey();
                if (key.Key == ConsoleKey.Escape)break;
                if (key.Key == ConsoleKey.UpArrow && keyPosition > 1) keyPosition--;
                if (key.Key == ConsoleKey.DownArrow && keyPosition < 3) keyPosition++;
                if (key.Key == ConsoleKey.Enter) {
                    switch (keyPosition) {
                    case 1:
                        finish(doc,u);
                        break;
                    case 2:
                        send_back(doc,u);
                        break;
                    case 3:
                        take(doc,ul,u);
                        break;
                    }
                }
            }while(key.Key != ConsoleKey.Enter);
        }
        /// <summary>
        /// меню редактора извещения
        /// </summary>
        /// <param name="doc">извещение</param>
        /// <param name="ul">список сотрудников</param>
        /// <param name="u">пользователь</param>
        public void menu_change(Doc doc, List<User>ul, User u){
            Console.Clear();
            x = Console.CursorLeft; y = Console.CursorTop;
            ConsoleKeyInfo key;
            var info = new List<Action>();
            var func = new List<Action>();
            info = doc_info(doc);
            func = doc_func(doc, ul, u);
            int keyPosition = 0;
            do{
                do{
                    Console.SetCursorPosition(x,y);
                    for(int i = 0; i < info.Count; i++){
                        if(keyPosition == i)Console.ForegroundColor = ConsoleColor.Green;
                        info[i]();
                        Console.ForegroundColor = ConsoleColor.Gray;
                    }
                    Console.WriteLine("\n\n\t\t> Нажмите ESC для выхода из редактирования <");
                    key = Console.ReadKey();
                    if(key.Key == ConsoleKey.UpArrow && keyPosition > 0)keyPosition--;
                    if(key.Key == ConsoleKey.DownArrow && keyPosition < info.Count-1)keyPosition++;
                    if(key.Key == ConsoleKey.Escape)break;
                    if(key.Key == ConsoleKey.Enter){
                        func[keyPosition]();
                    }
                }while(key.Key != ConsoleKey.Enter);
            }while(key.Key != ConsoleKey.Escape);
        }
    }
}