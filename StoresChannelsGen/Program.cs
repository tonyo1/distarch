// See https://aka.ms/new-console-template for more information

namespace Utils;
public static class Program
{
    public static void Main()
    {
        List<string> topics = new List<string>();
         

        int[] stores = { 1, 2, 3 };
        int[] terminals = { 4, 5, 6 };


        string txout = $"retail_sever_c{299}_s{9999}_txout:1:1";
        string cmd = $"retail_sever_c{299}_s{9999}_cmd:1:1";
        string info = $"retail_sever_c{299}_s{9999}_info:1:1";


        topics.Add(txout);
        topics.Add(cmd);
        topics.Add(info);

        Console.WriteLine(String.Join(",", topics));
        Console.WriteLine("Hello World!");
        Console.ReadLine();
    }
}

