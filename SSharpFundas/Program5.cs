using System;

String[] a = {"hello", "bye", "Aleksandar", "Jovanovic"};
int[] b = { 1, 2, 3, 4, 5 };

String[] a1 = new String[4];
a1[0] = "Howdie!!!";
a1[3] = "nah";

Console.WriteLine(a[3]);

for(int i = 0; i<a.Length; i++)
{
    Console.WriteLine(a[i]);
    if (a[i] == "Aleksandar")
    {
        Console.WriteLine("Match found!!!!");
        break;
    }
}

