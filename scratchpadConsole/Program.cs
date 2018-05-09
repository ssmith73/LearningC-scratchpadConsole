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

    //! myDel is a (delegate) type (that holds, and exectutes methods)

    #region Create delegate types
    delegate void myDel(int someNumber);
    delegate void PrintFunction();
    delegate double CalcDel(double num1, double num2);
    delegate int AnonymousDel(int number);
    delegate int DelegateofTheCalculator(int a, int b);
    #endregion

    class Calculator
    {
        public double Add (double num1, double num2) { return num1 + num2; }
        public double Sub (double num1, double num2) { return num1 - num2; }
        public double Mult (double num1, double num2) { return num1 * num2; }
        public static double Div (double num1, double num2) { if (num1 >= num2) return num1 / num2; else return 0; }
    }
    class Test
    {
        public void Print1() { Console.WriteLine("Print1 -- instance"); }
        public static void Print2() { Console.WriteLine("Print2 -- static"); }
    }

    public class Program
    {
        #region Methods to print low/high number
        void LowNumber(int num) { Console.WriteLine($"{num}: Is a Low Value!"); }
        void HighNumber(int num) { Console.WriteLine($"{num}: Is a High Value!"); }
        #endregion  

        static void Main()
        {
            Program program = new Program();
            Test t = new Test();
            Calculator calc = new Calculator();

            // This is a local variable, it will hold a reference
            // to a delegate object of type myDel
            // The type itself (myDel), hold information about the 
            // return type and parameters of the methods it can exectute
            myDel del;
            myDel del2;
            PrintFunction pf;
            CalcDel calcDel;
            

            pf = t.Print1;
            pf += Test.Print2;
            pf += Test.Print2;
            pf -= Test.Print2;

            // If not null, execute delegate
            pf?.Invoke();

            //Calculator delegate
            // Only the last return type will be returned, others are lost
            calcDel = calc.Add;
            calcDel += calc.Sub;
            calcDel += calc.Mult;
            calcDel += Calculator.Div; //static method

            Console.WriteLine($"Using Calculator with 50 and 2 - returns {calcDel(50,2)}");

            //Anonymous method - why create a method explicitly, that is only used for the 
            // delegate instantiation - and pass it to the delegate object, 
            // do it together using an anonymous method
            // It saves having to carry the method somewhere, and keeps things obvious

            AnonymousDel mydel = delegate (int x) { return x + 20; };
            //mydel can be redone with a lambda expression, returns an int, takes an int
            AnonymousDel lambdaDel = x => x + 20;

            Console.WriteLine($"Anonymous method: passing 20 - get: {mydel(20)}");
            Console.WriteLine($"Anonymous method: passing 21 - get: {mydel(21)}");

            // Lambda Expression - essentially, an anonymous method, stripped down
            Console.WriteLine("Using a lambda expr to pass 22 - get: {0} ", lambdaDel(22));

            // Generate a random number
            Random ranNum = new Random();
            int randomValue = ranNum.Next(99);

            // Create the delegate object to hold a reference to 
            // 2 possible methods, decided at run-time
            if(randomValue > 50)
               del = program.HighNumber;
            else
               del = program.LowNumber;

            // Execute the delegate - on whatever method it's pointing to
            del(randomValue);

            randomValue = ranNum.Next(99);
            del2 = randomValue > 50 ?
               new myDel(program.HighNumber) :
               new myDel(program.LowNumber);

            del2(randomValue);

            DelegateHandler();
        }

        static void DelegateHandler()
        {
            StandardCalculator standardCalculator = new StandardCalculator();

            DelegateofTheCalculator delegateofTheCalculator =
                new DelegateofTheCalculator(standardCalculator.Add);
            delegateofTheCalculator += standardCalculator.Sub;
            delegateofTheCalculator -= standardCalculator.Sub;

            // Execute the Add method
            Console.WriteLine("Sum of a and b is:{0}",
                delegateofTheCalculator(10,10));
            
        }
    }
    public class StandardCalculator
    {
        public int Add(int a, int b) { return a + b; }
        public int Sub(int a, int b) { return a > b ? a - b : 0 ; }
        public int Mul(int a, int b) { return a + b; }
    }
}
