using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vector3 = OpenTK.Mathematics.Vector3;

namespace OpenTKRendering.Core.Rendering
{
    public static class VertexGenerator
    {
        // Basic triangle generator with individual color
        public static float[] CreateTriangle(
            Vector3 position,
            float size,
            Vector3 color)
        {
            return new float[] {
            position.X - size/2, position.Y - size/2, position.Z, color.X, color.Y, color.Z,
            position.X + size/2, position.Y - size/2, position.Z, color.X, color.Y, color.Z,
            position.X, position.Y + size/2, position.Z, color.X, color.Y, color.Z
        };
        }

        // Multi-triangle generator with different colors
        public static float[] CreateMultiColorTriangles(int count)
        {
            float[] vertices = new float[count * 18]; // 3 vertices per triangle, 6 floats per vertex
            Random random = new Random();

            for (int i = 0; i < count; i++)
            {
                // Randomize position and colors
                float x = (float)(random.NextDouble() * 2 - 1); // -1 to 1
                float y = (float)(random.NextDouble() * 2 - 1);
                float size = (float)(0.1 + random.NextDouble() * 0.3); // 0.1 to 0.4

                Vector3 color1 = new Vector3(
                    (float)random.NextDouble(),
                    (float)random.NextDouble(),
                    (float)random.NextDouble()
                );

                int baseIndex = i * 18;
                vertices[baseIndex + 0] = x - size / 2;
                vertices[baseIndex + 1] = y - size / 2;
                vertices[baseIndex + 2] = 0.0f;
                vertices[baseIndex + 3] = color1.X;
                vertices[baseIndex + 4] = color1.Y;
                vertices[baseIndex + 5] = color1.Z;

                vertices[baseIndex + 6] = x + size / 2;
                vertices[baseIndex + 7] = y - size / 2;
                vertices[baseIndex + 8] = 0.0f;
                vertices[baseIndex + 9] = color1.X;
                vertices[baseIndex + 10] = color1.Y;
                vertices[baseIndex + 11] = color1.Z;

                vertices[baseIndex + 12] = x;
                vertices[baseIndex + 13] = y + size / 2;
                vertices[baseIndex + 14] = 0.0f;
                vertices[baseIndex + 15] = color1.X;
                vertices[baseIndex + 16] = color1.Y;
                vertices[baseIndex + 17] = color1.Z;
            }

            return vertices;
        }

        // Grid of triangles generator
        public static float[] CreateTriangleGrid(int rows, int cols)
        {
            float[] vertices = new float[rows * cols * 18];
            float gridSize = 2.0f; // Total grid size
            float triangleSize = gridSize / Math.Max(rows, cols);

            for (int row = 0; row < rows; row++)
            {
                for (int col = 0; col < cols; col++)
                {
                    // Calculate base position
                    float x = -1.0f + (col + 0.5f) * (gridSize / cols);
                    float y = -1.0f + (row + 0.5f) * (gridSize / rows);

                    // Generate random color
                    OpenTK.Mathematics.Vector3 color = new Vector3(
                        (float)(row / (float)rows),
                        (float)(col / (float)cols),
                        0.5f
                    );

                    int baseIndex = (row * cols + col) * 18;
                    vertices[baseIndex + 0] = x - triangleSize / 2;
                    vertices[baseIndex + 1] = y - triangleSize / 2;
                    vertices[baseIndex + 2] = 0.0f;
                    vertices[baseIndex + 3] = color.X;
                    vertices[baseIndex + 4] = color.Y;
                    vertices[baseIndex + 5] = color.Z;

                    vertices[baseIndex + 6] = x + triangleSize / 2;
                    vertices[baseIndex + 7] = y - triangleSize / 2;
                    vertices[baseIndex + 8] = 0.0f;
                    vertices[baseIndex + 9] = color.X;
                    vertices[baseIndex + 10] = color.Y;
                    vertices[baseIndex + 11] = color.Z;

                    vertices[baseIndex + 12] = x;
                    vertices[baseIndex + 13] = y + triangleSize / 2;
                    vertices[baseIndex + 14] = 0.0f;
                    vertices[baseIndex + 15] = color.X;
                    vertices[baseIndex + 16] = color.Y;
                    vertices[baseIndex + 17] = color.Z;
                }
            }

            return vertices;
        }

        // Custom shape generator with gradient coloring
        public static float[] CreateCustomShape(Vector3 center, float[] shapeVertices, Vector3 startColor, Vector3 endColor)
        {
            float[] vertices = new float[shapeVertices.Length * 2]; // 3 positions + 3 colors per vertex

            for (int i = 0; i < shapeVertices.Length; i += 2)
            {
                // Position
                vertices[i * 6] = center.X + shapeVertices[i];
                vertices[i * 6 + 1] = center.Y + shapeVertices[i + 1];
                vertices[i * 6 + 2] = center.Z;

                // Interpolate color
                float t = i / (float)(shapeVertices.Length - 2);
                Vector3 interpolatedColor = Vector3.Lerp(startColor, endColor, t);

                // Color
                vertices[i * 6 + 3] = interpolatedColor.X;
                vertices[i * 6 + 4] = interpolatedColor.Y;
                vertices[i * 6 + 5] = interpolatedColor.Z;
            }

            return vertices;
        }
    }

}   
