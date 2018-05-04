using System;

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
    public delegate void myDel(int num);
    public class Program
    {
        internal void PrintHigh(int value) { Console.WriteLine($"High Number {value}"); }
        internal void PrintLow(int value) { Console.WriteLine($"Low Number {value}"); }

        static void Main()
        {
            Program program = new Program();
            Random rand = new Random();
            int randNum = rand.Next(99);
            //myDel is a (delegate) type that holds a method, one that returns void, and takes an int parameter
                                                    // holds this method
                   //simpleDel holds a reference to a
                   //delegate type myDel
            myDel simpleDel = (randNum > 50) ? new myDel(program.PrintHigh) : 
                         // or this method
                new myDel( program.PrintLow);

            //execute the simpleDel delegate object, which executes whatever
            //method the it's holding
            simpleDel(randNum);
        }
    }
}
