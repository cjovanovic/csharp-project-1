using System;
using System.Collections;

ArrayList a = new ArrayList();
a.Add("hello");
a.Add("bye");
a.Add("Aleksandar");
a.Add("Jovanovic");

Console.WriteLine(a[1]);

foreach(String item in a)
{
    Console.WriteLine(item);
}

Console.WriteLine(a.Contains("Jovanovic"));

Console.WriteLine("After sorting!");
a.Sort();
foreach (String item in a)
{
    Console.WriteLine(item);
}