using OpenTK;
using OpenTK.Graphics.OpenGL;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTKRendering;

namespace OpenTKBasics
{
    class Program
    {
        public static void Main(string[] args)
        {
            using (Game game = new Game())
            {
                game.Run();
            }

        }

        

    }
}