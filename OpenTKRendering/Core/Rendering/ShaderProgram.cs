using OpenTK.Graphics.OpenGL4;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenTKRendering.Core.Rendering
{
    public class ShaderProgram : IDisposable
    {

        private int _programHandle;
        private List<int> _shaderHandles = new List<int>();

        public int ProgramHandle => _programHandle;

        public ShaderProgram(String vertexShaderSource, string fragmentShaderSource)
        {
            // Create and compile shader
            int vertexShader = CompileShader(vertexShaderSource, ShaderType.VertexShader);
            int fragmentShader = CompileShader(fragmentShaderSource, ShaderType.FragmentShader);

            // Create and link program
            _programHandle = GL.CreateProgram();
            GL.AttachShader(_programHandle, vertexShader);
            GL.AttachShader(_programHandle, fragmentShader);
            GL.LinkProgram(_programHandle);

            // Check link status
            GL.GetProgram(_programHandle, GetProgramParameterName.LinkStatus, out int success);
            if (success == 0)
            {
                string infoLog = GL.GetProgramInfoLog(_programHandle);
                throw new Exception($"Error linking program: {infoLog}");
            }

            // Clean up
            GL.DetachShader(_programHandle, vertexShader);
            GL.DetachShader(_programHandle, fragmentShader);
            GL.DeleteShader(vertexShader);
            GL.DeleteShader(fragmentShader);

        }

        private int CompileShader(string shaderSource, ShaderType shaderType)
        {
            int handle = GL.CreateShader(shaderType);
            GL.ShaderSource(handle, shaderSource);
            GL.CompileShader(handle);

            // Check for errors
            GL.GetShader(handle, ShaderParameter.CompileStatus, out int success);
            if (success == 0)
            {
                string infoLog = GL.GetShaderInfoLog(handle);
                throw new Exception($"Error compiling {shaderType} shader: {infoLog}");
            }

            _shaderHandles.Add(handle);

            return handle;
        }

        public void Use()
        {
            GL.UseProgram(_programHandle);
        }

        public void Dispose()
        {
            GL.UseProgram(0);
            GL.DeleteProgram(_programHandle);
        }
    }
}
