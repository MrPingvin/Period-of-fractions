using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace lab5
{
    class Program
    {
        static void Main(string[] args)
        {
            String input_number, input_base;
            Console.WriteLine("Ввод данных производится из файла? Y(да)/*Any_Symbol");
            if(Console.ReadKey().Key == ConsoleKey.Y)
                try
                {
                    Console.WriteLine("Поместите файл в папку с проектом и введите его название. Пример : name.txt");
                    using (StreamReader str = new StreamReader(Console.ReadLine()))
                    {
                        input_number = str.ReadLine();
                        input_base = str.ReadLine();
                        if(input_base == null || input_number == null)
                        {
                            Console.WriteLine("Содержимое файла не соответствует формату входных данных.");
                            Console.ReadKey();
                            return;
                        }
                    }
                    
                }
                catch(FileNotFoundException e)
                {
                    Console.WriteLine("Не удалось открыть файл.");
                    Console.ReadKey();
                    return;
                }
            else
            {
                input_number = Console.ReadLine();
                input_base = Console.ReadLine();
            }
            try
            {
                String convert_valid = "", convert_decimal = "";
                String[] valid_decimal = input_number.Split(',');
                long valid_number = Convert.ToInt64(valid_decimal[0]),  syst = Convert.ToInt64(input_base);
                double decimal_number = Convert.ToDouble("0," + valid_decimal[1]);

                while (valid_number != 0)
                {
                    convert_valid += Convert.ToChar((valid_number % syst <= 9) ? (valid_number % syst + '0') : (valid_number % syst + 'A' - 10));
                    valid_number /= syst;
                }
                convert_valid = (convert_valid.Length == 0)?"0":Revers(convert_valid);

                while (decimal_number != 0)
                {
                    decimal_number = decimal_number * syst;
                    convert_decimal += Convert.ToChar(((int)decimal_number % syst <= 9) ? ((int)decimal_number % syst + '0') : ((int)decimal_number % syst + 'A' - 10));
                    string[] spliter = Convert.ToString(decimal_number).Split(',');

                    decimal_number = (spliter.Length == 1) ? (0) : (Convert.ToDouble("0," + spliter[1]));
                    if (convert_decimal.Length > 40) break;
                }
                convert_decimal = FindPeriod(convert_decimal);
                Console.WriteLine(convert_valid  + "," + convert_decimal);
                Console.ReadKey();
            }
            catch (FormatException e)
            {
                Console.WriteLine("Обнаружена не десятичная дробь. " + e.Message);
                Console.ReadKey();
                return;
            }
        }

        public static String Revers(String str)
        {
            char[] arr = str.ToCharArray();
            Array.Reverse(arr);
            return new String(arr);
        }

        public static String FindPeriod(String str)
        {
            string period = "-1";
            for (int per = 0; per < str.Length; per++) {
                string[] spliter = str.Split(str[per]);
                for (int i = 0; i < spliter.Length; i++)
                {
                    for (int j = i + 1; j < spliter.Length-1; j++)
                        if (spliter[i] != spliter[j])
                        {
                            period = "-1";
                            break;
                        }
                        else
                            period = spliter[i];
                    if (period != "-1") {
                        string result = "";
                        for (int k = 1; k < i; k++)
                            result += str[per] + spliter[k];
                        return new String((result + "(" + str[per] + period + ")").ToCharArray());
                    }
                }
            }
            return str;
        }

    }
}
