using UnityEngine;

namespace DafEditor.Editor.Common
{
    public class DirectionArrow
    {
        private Material m_linkArrowMaterial;
        
        public void DrawArrow(Vector3 _cross, Vector3 _direction, Vector3 _center, Color _color)
        {
            const float sideLength = 6f;
            Vector3[] vertices = {
                _center + (_direction * sideLength),
                (_center - (_direction * sideLength)) + (_cross * sideLength),
                (_center - (_direction * sideLength)) - (_cross * sideLength)
            };
            UseLinkArrowMaterial();
            GL.Begin(vertices.Length + 1);
            GL.Color(_color);
            for (int i = 0; i < vertices.Length; i++)
            {
                GL.Vertex(vertices[i]);
            }
            GL.End();
        }
        
        private void UseLinkArrowMaterial()
        {
            if (m_linkArrowMaterial == null)
            {
                var shader = Shader.Find("Lines/Colored Blended") ?? Shader.Find("Legacy Shaders/Transparent/Diffuse") ?? Shader.Find("Transparent/Diffuse");
                if (shader == null) return;
                m_linkArrowMaterial = new Material(shader);
            }
            m_linkArrowMaterial.SetPass(0);
        }
    }
}