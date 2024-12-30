using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenTKRendering.Core.Rendering
{
    public class VertexBuffer : IDisposable
    {
        private int _vboHandle;
        private int _vaoHandle;
        private int _currentBindVAO = 0;

        public VertexBuffer(float[] vertices)
        {
            // Create and bind VBO
            _vboHandle = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, _vboHandle);
            GL.BufferData(BufferTarget.ArrayBuffer, vertices.Length * sizeof(float), vertices, BufferUsageHint.StaticDraw);

            // Create and bind VAO
            _vaoHandle = GL.GenBuffer();
            BindVAO(_vaoHandle);

            // Position attribute
            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 6 * sizeof(float), 0);
            GL.EnableVertexAttribArray(0);

            // Color attribute
            GL.VertexAttribPointer(1, 3, VertexAttribPointerType.Float, false, 6 * sizeof(float), 3 * sizeof(float));
            GL.EnableVertexAttribArray(1);
        }

        private void BindVAO(int vao)
        {
            if (_currentBindVAO != vao)
            {
                GL.BindVertexArray(vao);
                _currentBindVAO = vao;
            }
        }

        public void Bind()
        {
            BindVAO(_vaoHandle);
        }

        public void Dispose()
        {
            // Clean up VAO and VBO
            GL.BindVertexArray(0);
            GL.DeleteVertexArray(_vaoHandle);

            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            GL.DeleteBuffer(_vboHandle);
        }
    }
}
