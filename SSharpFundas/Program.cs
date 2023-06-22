
using System;

namespace CSharpFundas
{
    internal class Program : Program4
    {

        String name;
        String firstName;
        String lastName;

        // method default constructor!

        public Program(String firstName, String lastName)
        {
            this.firstName = firstName;
            this.lastName = lastName;
        }

        public void getName()
        {
            Console.WriteLine("My first name is " + this.firstName + " while last name is " + this.lastName);
        }

        public void getData()
        {
            Console.WriteLine("Printing my first methods!!!");
        }


        static void Main(string[] args)
        {
            Program p = new Program("Aleksandar", "Jovanovic");
            p.getName();
            p.getData();
            p.SetData();
            

            Console.WriteLine("Hello World!");
            int a = 4;
            //Double = 13.14;

            Console.WriteLine("My number is " + a);
            String name = "Pera";
            Console.WriteLine("My nickname is " + name);
            Console.WriteLine($"My nickname is {name}");

            var age = 24;
            Console.WriteLine($"Age is {age}");
            //age = "hello";

            dynamic height = 13.14;
            Console.WriteLine($"My height is {height}");

            height = "hellooooo!!!";
            Console.WriteLine($"And again, my height is {height}");

        }
    }
}