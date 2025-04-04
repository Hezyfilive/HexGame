using UnityEngine;

namespace Core.Map
{
    public class HexGrid : MonoBehaviour
    {
        [Header("Grid Settings")]
        public Vector2Int gridSize;

        [Header("Tile Settings")] 
        public float outerSize = 1f;
        public float innerSize = 0f;
        public float height = 1f;
        public bool isFlatTopped;
        public Material tileMaterial;


        private void OnEnable()
        {
            DestroyAllChildren();
            LayoutGrid();
        }

        private void OnValidate()
        {
        }

        private void LayoutGrid()
        {
            for (int y = 0; y < gridSize.y; y++)
            {
            
                for (int x = 0; x < gridSize.x; x++)
                {
                    GameObject tile = new GameObject($"Tile {x}, {y}", typeof(HexRenderer))
                    {
                        transform =
                        {
                            position = GetPositionForHexFromCoordinates(new Vector2Int(x, y))
                        }
                    };

                    HexRenderer hexRenderer = tile.GetComponent<HexRenderer>();
                    hexRenderer.isFlatTopped = isFlatTopped;
                    hexRenderer.outerSize = outerSize;
                    hexRenderer.innerSize = innerSize;
                    hexRenderer.height = height;
                    hexRenderer.SetMaterial(tileMaterial);
                    hexRenderer.DrawMesh();
                    hexRenderer.CombineFaces();
                
                    tile.transform.SetParent(transform, true);
                
                
                }
            }
        }

        public Vector3 GetPositionForHexFromCoordinates(Vector2Int coordinates)
        {
            int column = coordinates.x;
            int row = coordinates.y;
            float width, distance, xPos, yPos;
            bool shouldOffset;
            float horizontalDistance, verticalDistance, offset;
            float size = outerSize;

            if (!isFlatTopped)
            {
                shouldOffset = (row % 2) == 0;
                width = Mathf.Sqrt(3) * size;
                distance = 2f * size;
            
                horizontalDistance = width;
                verticalDistance = distance * (3f/4f);
            
                offset = (shouldOffset) ? width/2 : 0;
            
                xPos = (column * (horizontalDistance)) + offset;
                yPos = (row * (verticalDistance));
            }
            else
            {
                shouldOffset = (column % 2) == 0;
                width = 2f * size;
                distance = Mathf.Sqrt(3f) * size;
            
                horizontalDistance = width * (3f/4f);
                verticalDistance = distance;
            
                offset = (shouldOffset) ? distance/2 : 0;
            
                xPos = (column * (horizontalDistance)) ;
                yPos = (row * (verticalDistance)) + offset;
            }
            return new Vector3(xPos, 0, yPos);
        }

        void DestroyAllChildren()
        {
            foreach (Transform child in transform)
            {
                Destroy(child.gameObject);
            }
        }
    }
}
