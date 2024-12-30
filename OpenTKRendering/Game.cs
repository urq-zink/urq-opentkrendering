using OpenTK.Graphics.OpenGL;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;

namespace OpenTKRendering
{
    public class Game : GameWindow
    {
        public Game() : base(GameWindowSettings.Default, NativeWindowSettings.Default)
        {
        }

        protected override void OnLoad()
        {
            GL.ClearColor(0.2f, 0.3f, 0.3f, 1.0f);
        }

        protected override void OnRenderFrame(FrameEventArgs args)
        {
            GL.Clear(ClearBufferMask.ColorBufferBit);
            SwapBuffers();
        }
    }
}