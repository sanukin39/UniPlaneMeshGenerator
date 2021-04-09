using UnityEngine;

namespace UPMG
{
    public static class PlaneMeshGenerator
    {
        public static Mesh GenerateHorizontal(int width, int height, float unitLength, bool isHorizontal)
        {
            var direction = isHorizontal ? Vector3.forward : Vector3.up;
            var verticesCount = width * height;
            var triangleCount = (width - 1) * (height - 1) * 2;
            var vertices = new Vector3[verticesCount];
            var triangles = new int[triangleCount * 3];
            var trisIndex = 0;
            for (var w = 0; w < width; w++)
            {
                for (var h = 0; h < height; h++)
                {
                    var vertIndex = h * width + w;
                    var position = Vector3.right * w * unitLength + direction * h * unitLength;
                    vertices[vertIndex] = position;

                    if (w == width - 1 || h == height - 1)
                    {
                        continue;
                    }

                    triangles[trisIndex++] = vertIndex;
                    triangles[trisIndex++] = vertIndex + width;
                    triangles[trisIndex++] = vertIndex + width + 1;
                    triangles[trisIndex++] = vertIndex;
                    triangles[trisIndex++] = vertIndex + width + 1;
                    triangles[trisIndex++] = vertIndex + 1;
                }
            }

            var mesh = new Mesh {vertices = vertices, triangles = triangles};
            return mesh;
        }
    }
}
