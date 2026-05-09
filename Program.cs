using System;

namespace Composer
{
    public static class Program
    {
        [STAThread]
        static void Main()
        {
            using (var game = new SynthGame())
                game.Run();
        }
    }
}
