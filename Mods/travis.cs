using System;
using System.Collections.Generic;
using System.Text;

namespace Void.Mods
{
    internal class travis
    {
        public abstract class travisBase
        {
            public abstract void travisTest(float value);
        }
        public void travisconstructor()
        {
            travisTest(); // removed the float argument
        }
        public static void Main()
        {
            travisTest();
        }
        public static void travisTest()
        {
            global::System.Console.WriteLine("Travis test successful!");
        }
    }
}
