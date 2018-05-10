using System;
using System.Collections.Generic;

namespace scratchpadConsole
{
    /*!     
     * myDel is a type of the delegate object, it holds 3 things
       The address of the method on which it makes calls
       Return type
       Parameters (if any)
     */

    //! the delegate object can hold one or more methods

    //!     an object      myDel is a (delegate) type
    //? like a class, so it cannot be put inside another class
    delegate int myDel(string str);
    delegate void voidStrInt(string a, int b);

    public class Program
    {
        static void Main()
        {

            Program program = new Program();

            int ConvertStrToInt(string str)
            {
                return Convert.ToInt16(str);
            }
            myDel del = ConvertStrToInt;
            myDel anonMethodDel = delegate (string str) { return str.Length; };
            myDel lamdaExpr = s => Convert.ToInt16(s);

            //Instead of a delegate type, a Func can be used, which is 
            // just a delegate type that takes n, inputs and returns a single
            // value Func<in1, in2, in..15, out> myFunc
            // An Action<n1,n2> is like a func that does not return anything
            Func<int, int> myFunc,myFunc2;
            myFunc = delegate (int num) { return num * num; };
            myFunc2 = num => num * num;

            Console.WriteLine($"50 using a delegate: {del("50")}");
            Console.WriteLine($"Length of 'Sean' using an anonymous method: {anonMethodDel("Sean")}");
            Console.WriteLine($"50 using a lamda expression: {lamdaExpr("50")}");
            Console.WriteLine($"myFunc: Anonymous method: 4 squared is: {myFunc(4)}");
            Console.WriteLine($"myFunc: Lambda expression: 5 squared is: {myFunc2(5)}");

            var Films = new List<Film>
            {
                new Film { Name = "Jaws", Year = 1975 },
                new Film { Name = "Singing in the Rain", Year = 1952 },
                new Film { Name = "Some like it Hot", Year = 1959 },
                new Film { Name = "The Wizard of Oz", Year = 1939 },
                new Film { Name = "It's a Wonderful Life", Year = 1946 },
                new Film { Name = "American Beauty", Year = 1999 },
                new Film { Name = "High Fidelity", Year = 2000 },
                new Film { Name = "The Usual Suspects", Year = 1995 }
            };

            //voidStrInt myPrintDel = null;

            voidStrInt myPrintDel = delegate (string name, int year)
                { Console.WriteLine($"myPringDel:  Name = {name}, Year={year}"); };


            foreach (var item in Films)
            {
                 myPrintDel(item.Name, item.Year);
            }
            
            

            Action<Film> print = 
                film=> Console.WriteLine($"Name = {film.Name}, Year={film.Year}");
               
            foreach (var film in Films)
            {
                print(film);
            }
            Console.WriteLine("Use Foreach extension method of a list");
            Films.ForEach(print);
            Console.WriteLine("Films before 1999");
            Films.FindAll(film => film.Year < 1999).ForEach(print);

            Console.WriteLine();
            Console.WriteLine("Sorted list");
            Films.Sort((f1, f2) => f1.Name.CompareTo(f2.Name));
            Films.ForEach(print);

            
        }
    }
}
