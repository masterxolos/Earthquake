using UnityEngine;

public class GridManager : MonoBehaviour
{
    public int gridSizeX = 10;
    public int gridSizeY = 5;
    public float cellSize = 1f;

    void Start()
    {
        GenerateGrid();
    }

    void GenerateGrid()
    {
        for (int x = 0; x < gridSizeX; x++)
        {
            for (int y = 0; y < gridSizeY; y++)
            {
                Vector3 cellPosition = new Vector3(x * cellSize, y * cellSize, 0f);
                GameObject cell = new GameObject("Cell_" + x + "_" + y);
                cell.transform.position = cellPosition;
                cell.transform.parent = transform; // Set the grid as the parent
            }
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.gray;

        for (float x = 0; x < gridSizeX * cellSize; x += cellSize)
        {
            for (float y = 0; y < gridSizeY * cellSize; y += cellSize)
            {
                Vector3 cellPosition = new Vector3(x, y, 0f) + transform.position;
                Gizmos.DrawWireCube(cellPosition, new Vector3(cellSize, cellSize, 0f));
            }
        }
    }
    
    public bool IsInsideGrid(Vector3 position)
    {
        float minX = transform.position.x-1;
        float minY = transform.position.y-1;
        float maxX = minX + gridSizeX * cellSize+1;
        float maxY = minY + gridSizeY * cellSize+1;

        return (position.x >= minX && position.x <= maxX && position.y >= minY && position.y <= maxY);
    }

    public Vector3 GetNearestGridPosition(Vector3 position)
    {
        float x = Mathf.Floor((position.x - transform.position.x) / cellSize + 0.5f);
        float y = Mathf.Floor((position.y - transform.position.y) / cellSize + 0.5f);

        Vector3 nearestPosition = new Vector3(x * cellSize + transform.position.x, y * cellSize + transform.position.y, 0f);

        Debug.Log("Input Position: " + position);
        Debug.Log("Nearest Position: " + nearestPosition);
        
        return nearestPosition;
    }

}