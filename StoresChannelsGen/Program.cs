// See https://aka.ms/new-console-template for more information

List<string> topics = new List<string>();
int chain = 2;
 



for (int i = 1; i < 10; i++)
{
    string s = $"posretail_c{chain}_s{i}:1:1";
    topics.Add(s);
    for (int j = 1; j < 10; j++ )
    {
        s = $"posretail_c{chain}_s{i}_reg{j}:1:1";
        topics.Add(s);
    } 
}
Console.WriteLine(String.Join(",",topics));
Console.WriteLine("Hello, World!");