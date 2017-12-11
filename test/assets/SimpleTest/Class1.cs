using System;

namespace SimpleTest
{
    // Basic relations between methods
    // Max degree of calls: 1 - 1
    public class Class1
    {
        public void Method1()
        {
            Console.WriteLine("Method 1");
        }

        public void Method2()
        {
            this.PrivateMethod1();
        }

        public void Method3()
        {
            this.Method3();
        }

        private void PrivateMethod1()
        {
            Console.WriteLine("PrivateMethod1");
        }
    }
}
