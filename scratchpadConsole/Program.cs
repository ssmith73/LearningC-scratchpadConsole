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

    //!     an object      myDel is a (delegate) type (that holds, and exectutes methods)

    delegate void myDel(int someNumber);


    public class Program
    {
        void LowNumber(int num) { Console.WriteLine($"{num}: Is a Low Value!"); }
        void HighNumber(int num) { Console.WriteLine($"{num}: Is a High Value!"); }

        static void Main()
        {
            Program program = new Program();

            myDel del;
            Random ranNum = new Random();
            int randomValue = ranNum.Next(99);

            // Use the delegate object to hold one of 
            // 2 possible methods
            del = randomValue > 50 ? 
               new myDel(program.HighNumber) :
               new myDel(program.LowNumber);

            del(randomValue);



        }
    }
}
