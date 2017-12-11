using System;

namespace SimpleTest
{
    // Multiple degree relations between methods
    public class Class2
    {
        public void Method1()
        {
            this.Method2();
            this.PrivateMethod2();
        }

        public void Method2()
        {
            PrivateMethod1();
        }

        private static void PrivateMethod1()
        {
        }

        private void PrivateMethod2()
        {
            Console.WriteLine("Hello world");
        }
    }
}
