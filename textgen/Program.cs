/*
 * By InnieSharp
 * Created in SharpDevelop 5.1
 */
using System;
using System.IO;

namespace textgen
{
	class Program
	{
		private static readonly Random random = new Random();
        private static readonly string alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";

        public static string Generate(int len)
        {
            var buffer = new char[len];
            for (int i = 0; i < buffer.Length; i++)
            {
                buffer[i] = alphabet[random.Next(alphabet.Length)];
            }
            return new string(buffer);
        }
		
		public static void Main(string[] args)
		{
			string outputfile = "textgen_output.txt";
			string start = "";
			string result = "";
			string end = "";
			string bas = "";
			int count = 0;
			foreach(string arg in args)
			{
				if(arg == "/help" || arg == "/?" || arg == "/h")
				{
					Console.WriteLine("/help /? /h   Выводит все доступные аргументы\n/outputfile /of   Задаёт путь к файлу с результатом\n/start /s   Задаёт начальный символ или строку\n            $n - Новая строка\n            $t - Таб\n/end /e   Задаёт конечный символ или строку\n          $n - Новая строка\n          $t - Таб\n/base /b /mid   Задаёт основной символ или строку\n                $i - Число, которое увеличевается на единицу при повторном использовании\n                $n - Новая строка\n                $t - Таб\n                $k - Steam-ключ\n                $p - Пароль\n/count /c   Задаёт кол-во написаний\n\nПример: \"/s=int[] a =$n{$n\" /e=} /b=$t$i /c=12 /of=result.txt\n\nДанная справка сделана для версии ALPHA3.\nСделал etar125.");
				}
				else if(arg.StartsWith("/outputfile=") || arg.StartsWith("/of="))
				{
					outputfile = arg.Substring(arg.IndexOf("=") + 1);
				}
				else if(arg.StartsWith("/start=") || arg.StartsWith("/s="))
				{
					start = arg.Substring(arg.IndexOf("=") + 1);
				}
				else if(arg.StartsWith("/end=") || arg.StartsWith("/e="))
				{
					end = arg.Substring(arg.IndexOf("=") + 1);
				}
				else if(arg.StartsWith("/base=") || arg.StartsWith("/b=") || arg.StartsWith("/mid="))
				{
					bas = arg.Substring(arg.IndexOf("=") + 1);
				}
				else if(arg.StartsWith("/count=") || arg.StartsWith("/c="))
				{
					try
					{
						count = int.Parse(arg.Substring(arg.IndexOf("=") + 1));
					}
					catch
					{
						Console.WriteLine("Кол-во было выражено не числом.");
						Environment.Exit(0);
					}
				}
			}
			result += ConvertB(start);
			for(int i = 0; i < count; i++)
			{
				result += ConvertA(bas, i);
			}
			result += ConvertB(end);
			File.WriteAllText(outputfile, result);
		}
		
		public static string ConvertA(string a, int m)
		{
			string res = "";
			for(int ii = 0; ii < a.Length; ii++)
			{
				if(a[ii] == '$')
				{
					if(a[ii+1] == 'i')
					{
						res += m;
					}
					else if(a[ii+1] == 'n')
					{
						res += Environment.NewLine;
					}
					else if(a[ii+1] == 't')
					{
						res += "\t";
					}
					else if(a[ii+1] == 'k')
					{
						res += Generate(5) + "-" + Generate(5) + "-" + Generate(5) + "-" + Generate(5) + "-" + Generate(5);
					}
					else if(a[ii+1] == 'p')
					{
						res += Generate(random.Next(101));
					}
					ii++;
				}
				else { res += a[ii]; }
			}
			return res;
		}
		
		public static string ConvertB(string a)
		{
			string res = "";
			for(int ii = 0; ii < a.Length; ii++)
			{
				if(a[ii] == '$')
				{
					if(a[ii+1] == 'n')
					{
						res += Environment.NewLine;
					}
					else if(a[ii+1] == 't')
					{
						res += "\t";
					}
					ii++;
				}
				else { res += a[ii]; }
			}
			return res;
		}
	}
}