using System;
using System.Numerics;
using System.Text.RegularExpressions;


/*
 * Задание:
1) Дана строка S и символ C Посчитать, сколько раз в строке S встречается символ C.
2) Из строки S удалить все цифры.
3) Выяснить, все ли буквы из слова "кеу”, встречаются в заданном тексте.
4) Проверить, правильно ли в заданном тексте расставлены круглые скобки.
 */

namespace StringRegex
{
    class Programm
    {
        static int CountChar(string str, char c, bool flag = false) // флаг если true, то регистр не учитывается
        {
            if (flag)
            {
                str = str.ToLower();
                c = char.ToLower(c);
            }

            int count = 0;
            foreach(char ch in str)
            {
                if(ch == c) count++;
            }
            return count;
        }
        static int CountCharRegex(string str, char c, bool flag = false) // флаг если true, то регистр не учитывается
        {
            if (flag)
            {
                str = str.ToLower();
                c = char.ToLower(c);
            }

            Regex regex = new Regex(c.ToString());
            MatchCollection g = regex.Matches(str);
            return g.Count();
        }
        static int CountSubstringRegex(string str, string c, bool flag = false)
        {
            if (flag)
            {
                str = str.ToLower();
                c = c.ToLower();
            }

            Regex regex = new Regex($@"{c}");
            //Regex regex = new Regex($@"(\w*){c}(\w*)"); //отдельные слова
            MatchCollection g = regex.Matches(str);
            return g.Count();
        }
        static string DeleteNumbersRegex(string str)
        {
            Regex regex = new Regex(@"\d");
            str = regex.Replace(str, string.Empty);
            return str;
        }
        static string DeleteNumbers(string str)
        {
            foreach(char ch in str)
            {
                if(char.IsDigit(ch))
                {
                    str = str.Replace(ch,'\0');
                }
            }
            return str;
        }
        static bool FindCharsFromString(string str, string key)
        {            
            char[] letters = key.ToCharArray();
            Regex[] reg = new Regex[letters.Length];
            MatchCollection[] g = new MatchCollection[letters.Length];
            for (int i = 0; i < letters.Length; i++)
            {
                reg[i] = new Regex(letters[i].ToString());
                g[i] = reg[i].Matches(str);
            }
            foreach(var item in g)
            {
                if(item.Count() == 0) return false;
            }          
            return true;
        }
        static bool CheckBracketsRegex(string str)
        {
            Regex leftReg = new Regex("[(]");
            Regex rightReg = new Regex("[)]");
            MatchCollection leftMatch = leftReg.Matches(str);
            MatchCollection rightMatch = rightReg.Matches(str);
            if (leftMatch.Count != rightMatch.Count) return false;
            else
            {
                Regex extra = new Regex(@"^[)]|[(]$");
                Match match = extra.Match(str);
                if (match.Success) return false;
            }

            Regex b = new Regex(@"\w*[(]*\w*[)]\w*");
            MatchCollection matchCol = b.Matches(str);

            foreach (Match match in matchCol)
                Console.WriteLine(match.Value);

            if (matchCol.Count == leftMatch.Count) return true;
            else return false;
        }
        static bool CheckBrackets(string str)
        {
            List<int> left = new List<int>();
            List<int> rigth = new List<int>();
            for(int i = 0; i < str.Length; i++)
            {
                if (str[i] == '(')
                    left.Add(i);
                if (str[i] == ')')
                    rigth.Add(i);
            }

            foreach (var item in left)
            {
                Console.Write(item);
                Console.Write(' ');
            }
            Console.WriteLine();
            foreach (var item in rigth)
            {                
                Console.Write(item);
                Console.Write(' ');
            }
            Console.WriteLine();

            if (left.Count != rigth.Count) return false;           
            if (left.Last() == str.Length-1)    return false;
            if (rigth.First() == 0)    return false;

           return true;

        }
        static bool CheckBracketsBool(string str)
        {
            string vector = string.Empty;
            foreach(char c in str)
            {
                if (c == '(') vector += "0";
                if (c == ')') vector += "1";
            }         
            int left = 0, right = 0; // Количество скобок  left - (,   right - )
            foreach(char c in vector)    // Слева правых скобок(1) должно быть всегда меньше или равно левым(0)
            {
                if (right > left) return false; 
                if (c == '0') left++;
                if (c == '1') right++;                
            }
            if (right != left) return false;
            return true;
        }
        
        static void BoolTable(int size)
        {
            byte b = 0;
            for (int i = 0; i < Math.Pow(2, size); i++)
            {
                string s = Convert.ToString(b, 2);

                while (s.Length < size) // заполняем незначащими нулями
                {
                    s = s.Insert(0, "0");
                }

                //if (CountChar(s, '0') == CountChar(s, '1'))
                //{
                //    if (s[0] != '1' && s[s.Length-1] != '0')
                //        Console.WriteLine(s);
                //}    
                Console.WriteLine(s);
                b++;
            }
        }
        static void Main()
        {
            string str = "(k(2ySkmee Mic(rol ))1teext(( some4k) SS)OMEsomesome 5(3 02))";
            string s1 = "( (  ) )";
            string s2 = "( )  ( )";
            string s3 = ") )  ( (";
            string s4 = ") (  ) (";

            Console.WriteLine(CountChar(str, 'm'));
            Console.WriteLine(CountCharRegex(str, 'm'));
            Console.WriteLine(CountSubstringRegex(str, "some", true));

            Console.WriteLine(DeleteNumbers(str));

            Console.WriteLine(FindCharsFromString(str, "smy"));

            Console.WriteLine(s1 + '\n' + CheckBrackets(s1));
            Console.WriteLine(s2 + '\n' + CheckBrackets(s2));
            Console.WriteLine(s3 + '\n' + CheckBrackets(s3));
            Console.WriteLine(s4 + '\n' + CheckBrackets(s4));

            Console.WriteLine(CheckBracketsBool(str));
            //BoolTable(8);
        }
    }
}