// See https://aka.ms/new-console-template for more information


public static class Program
{
    public static void Main()
    {
        List<string> topics = new List<string>();
         

        int[] stores = { 1, 2, 3 };
        int[] terminals = { 4, 5, 6 };


        string data = $"retail_sever_c{299}_s{9999}_txout:1:1";
        string cmd = $"retail_sever_c{299}_s{9999}_cmd:1:1";
        string info = $"retail_sever_c{299}_s{9999}_info:1:1";


        topics.Add(data);
        for (int j = 1; j < 10; j++)
        {
            s = $"posretail_c{299}_s{299}_reg{j}:1:1";
            topics.Add(s);
        }
        Console.WriteLine(String.Join(",", topics));
        Console.WriteLine("Hello, World!");

        // Write your program code here. Make sure you call Console.ReadLine() at the end of the program to ensure that the console window pauses execution until a key press is received.
        Console.WriteLine("Hello World!");
        Console.ReadLine();
    }
}

