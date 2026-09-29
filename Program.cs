using CaveSharp.Core;

namespace CaveSharp
{
    class Program
    {
        static void Main(string[] args)
        {
            var engineLoop = new UpdateLoop();
            engineLoop.Start();
        }
    }
}
