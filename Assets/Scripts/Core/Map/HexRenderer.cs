using System.Collections.Generic;
using UnityEngine;

namespace Core.Map
{
    [RequireComponent(typeof(MeshFilter), typeof(MeshRenderer), typeof(TileModifiers.TileModifiers))]
    public class HexRenderer : MonoBehaviour
    {

        private Mesh _mesh;

        private MeshFilter _meshFilter;
        private MeshRenderer _meshRenderer;
        private TileModifiers.TileModifiers _tileModifiers;
        
        private List<Face> _faces;
        
        public Material material;
        public float innerSize;
        public float outerSize;
        public float height;
        public bool isFlatTopped;

        private void Awake()
        {
            _meshFilter = GetComponent<MeshFilter>();
            _meshRenderer = GetComponent<MeshRenderer>();
            _tileModifiers = GetComponent<TileModifiers.TileModifiers>();
            _mesh = new Mesh
            {
                name = "Hex"
            };

            _meshFilter.mesh = _mesh;
            _meshRenderer.material = material;
        }

        private void OnEnable()
        {
            DrawMesh();
            CombineFaces();
            
        }

        public void OnValidate()
        {
            if (Application.isPlaying)
            {
                DrawMesh();
                CombineFaces();
            }
        }

        public void DrawMesh()
        {
            _faces = new List<Face>();

            for (int point = 0; point < 6; point++)
            {
                _faces.Add(CreateFace(innerSize, outerSize, height / 2f, height / 2f, point));
            }

            for (int point = 0; point < 6; point++)
            {
                _faces.Add(CreateFace(innerSize, outerSize, -height / 2f, -height / 2f, point, true));
            }

            for (int point = 0; point < 6; point++)
            {
                _faces.Add(CreateFace(outerSize, outerSize, height / 2f, -height / 2f, point, true));
            }

            for (int point = 0; point < 6; point++)
            {
                _faces.Add(CreateFace(innerSize, innerSize, height / 2f, -height / 2f, point, false));
            }
        }

        public void CombineFaces()
        {
            List<Vector3> vertices = new List<Vector3>();
            List<Vector2> uvs = new List<Vector2>();
            List<int> triangles = new List<int>();
            
            for (int i = 0; i < _faces.Count; i++)
            {
                vertices.AddRange(_faces[i].Vertices);
                uvs.AddRange(_faces[i].UVs);
                
                int offset = (4 * i);
                foreach (int triangle in _faces[i].Triangles)
                {
                    triangles.Add(offset + triangle);
                }
            }
            
            _mesh.vertices = vertices.ToArray();
            _mesh.uv = uvs.ToArray();
            _mesh.triangles = triangles.ToArray();
            _mesh.RecalculateNormals();
            
        }
        
        private Face CreateFace(float innerRadius, float outerRadius, float heightA, float heightB, int point,
            bool reverse = false)
        {
            Vector3 pointA = GetPoint(innerRadius, heightB, point);
            Vector3 pointB = GetPoint(innerRadius, heightB, (point < 5) ? point + 1 : 0);
            Vector3 pointC = GetPoint(outerRadius, heightA, (point < 5) ? point + 1 : 0);
            Vector3 pointD = GetPoint(outerRadius, heightA, point);
            
            List<Vector3> vertices = new List<Vector3>() {pointA, pointB, pointC, pointD};
            List<int>  triangles = new List<int>() {0, 1, 2, 2, 3, 0};
            List<Vector2> uvs = new List<Vector2>() {new Vector2(0, 0), new Vector2(1, 0), new Vector2(1, 1), new Vector2(0, 1)};

            if (reverse)
            {
                vertices.Reverse();
            }
            return new Face(vertices, triangles, uvs);
        }

        protected Vector3 GetPoint(float size, float fHight, int index)
        {
            float angleDeg = isFlatTopped ? 60 * index: 60*index-30;
            float angleRad = Mathf.PI / 180f * angleDeg;
            return new Vector3((size *Mathf.Cos(angleRad)), fHight, size *Mathf.Sin(angleRad));
        }

        public void SetMaterial(Material mat)
        {
            material = mat;
            _meshRenderer.material = mat;
        }
    }
}
