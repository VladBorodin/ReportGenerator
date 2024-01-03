using System;

namespace Console_Func
{
    internal class Console_Func_Program{
        public void show_ln(string str){
            Console.WriteLine($"{str}");
        }
        public void show(string str){
            Console.Write($"{str}");
        }
        public string input(string str){
            return str;
        }
        public ConsoleKeyInfo key(){
            return Console.ReadKey();
        }
        public string line(){
            return Console.ReadLine();
        }
        public void color_text_green(){
            Console.ForegroundColor = ConsoleColor.Green;
        }
        public void color_BW_reverse(){
            Console.ForegroundColor = ConsoleColor.Black;
            Console.BackgroundColor = ConsoleColor.Gray;
        }
        public void color_OFF(){
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.BackgroundColor = ConsoleColor.Black;
        }
        public void clr(){
            Console.Clear();
        }
        public void cursor_move(int x, int y){
            Console.SetCursorPosition(x,y);
        }
        public int cursor_x(){
            return Console.CursorLeft;
        }
        public int cursor_y(){
            return Console.CursorTop;
        }
        public void cursor_show(bool yn){
            Console.CursorVisible = yn;
        }
    }
}