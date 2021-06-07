using System.Collections.Generic;
using UnityEngine;

public class SquareGrid
{
    public static readonly Vector2Int[] DIRS = new[] {
    new Vector2Int(1, 1),
    new Vector2Int(-1, 1),
    new Vector2Int(1, -1),
    new Vector2Int(-1, -1),
    new Vector2Int(1, 0),
    new Vector2Int(-1, 0),
    new Vector2Int(0, -1),
    new Vector2Int(0, 1)
    };

    public SquareGrid(int width, int height, List<FieldCellInfo> cellsList)
    {
        this.width = width;
        this.height = height;

        field = new FieldCellInfo[width, height];

        for (int i = 0; i < field.GetLength(0); i++)
        {
            for (int j = 0; j < field.GetLength(1); j++)
            {
                field[i, j] = cellsList.Find((cell) => cell.Position.x == i && cell.Position.y == j);
            }
        }
    }

    private int width, height;

    private FieldCellInfo[,] field;

    public bool InBounds(FieldCellInfo cell)
    {
        return (cell.Position.x >= 0) && (cell.Position.x < width) && (cell.Position.y >= 0) && (cell.Position.y < height);
    }

    public bool Passable(FieldCellInfo cell)
    {
        return field[cell.Position.x, cell.Position.y].GetCellType() != FieldCellInfo.FieldCellType.Wall;
    }

    public float Cost(Vector2Int beginCellPosition, Vector2Int endCellPosition)
    {
        if (AStarSearch.Heuristic(beginCellPosition, endCellPosition) == 2f) //diagonal movement
        {
            return field[endCellPosition.x, endCellPosition.y].GetCellMovePrice() * Mathf.Sqrt(2f);
        }
        return field[endCellPosition.x, endCellPosition.y].GetCellMovePrice();
    }

    public IEnumerable<Vector2Int> GetNeighbors(Vector2Int cellPosition)
    {
        foreach (var dir in DIRS)
        {
            FieldCellInfo neighbourCell = GetCellAtPoint(cellPosition.x + dir.x, cellPosition.y + dir.y);
            if (neighbourCell && InBounds(neighbourCell) && Passable(neighbourCell))
            {
                yield return neighbourCell.Position;
            }
        }
    }

    public FieldCellInfo GetCellAtPoint(int x, int y)
    {
        for (int i = 0; i < field.GetLength(0); i++)
        {
            for (int j = 0; j < field.GetLength(1); j++)
            {
                if (x == i && y == j)
                    return field[i, j];
            }
        }
        return null;
    }
}

public class PriorityQueue<T>
{
    private List<KeyValuePair<T, float>> elements = new List<KeyValuePair<T, float>>();

    public int Count
    {
        get { return elements.Count; }
    }

    public void Enqueue(T item, float priority)
    {
        elements.Add(new KeyValuePair<T, float>(item, priority));
    }

    public T Dequeue()
    {
        int bestIndex = 0;

        for (int i = 0; i < elements.Count; i++)
        {
            if (elements[i].Value < elements[bestIndex].Value)
            {
                bestIndex = i;
            }
        }

        T bestItem = elements[bestIndex].Key;
        elements.RemoveAt(bestIndex);
        return bestItem;
    }
}

public class AStarSearch
{
    private Dictionary<Vector2Int, Vector2Int> pathPossiblePositions = new Dictionary<Vector2Int, Vector2Int>();
    private Dictionary<Vector2Int, float> costs = new Dictionary<Vector2Int, float>();

    private Vector2Int startPointPosition;
    private Vector2Int endPointPosition;

    static public float Heuristic(Vector2Int a, Vector2Int b)
    {
        return Mathf.Abs(a.x - b.x) + Mathf.Abs(a.y - b.y);
    }

    public AStarSearch(SquareGrid fieldGrid, Vector2Int startPos, Vector2Int endPos)
    {
        startPointPosition = startPos;
        endPointPosition = endPos;

        FindPath(fieldGrid);
    }

    private void FindPath(SquareGrid fieldGrid)
    {
        var cellsQueue = new PriorityQueue<Vector2Int>();
        cellsQueue.Enqueue(startPointPosition, 0f);

        pathPossiblePositions.Add(startPointPosition, startPointPosition);
        costs.Add(startPointPosition, 0f);

        while (cellsQueue.Count > 0)
        {
            Vector2Int currentPos = cellsQueue.Dequeue();

            if (currentPos.Equals(endPointPosition))
                break;

            foreach (var neighbor in fieldGrid.GetNeighbors(currentPos))
            {
                float newCost = costs[currentPos] + fieldGrid.Cost(currentPos, neighbor);

                if (!costs.ContainsKey(neighbor) || newCost < costs[neighbor])
                {
                    if (costs.ContainsKey(neighbor))
                    {
                        costs.Remove(neighbor);
                        pathPossiblePositions.Remove(neighbor);
                    }

                    costs.Add(neighbor, newCost);
                    pathPossiblePositions.Add(neighbor, currentPos);
                    fieldGrid.GetCellAtPoint(neighbor.x, neighbor.y).ShowPathPossiblePoint();
                    float priority = newCost + Heuristic(neighbor, endPointPosition);
                    cellsQueue.Enqueue(neighbor, priority);
                }
            }
        }
    }

    public List<Vector2Int> GetPath()
    {
        List<Vector2Int> path = new List<Vector2Int>();
        Vector2Int currentPos = endPointPosition;

        while (!currentPos.Equals(startPointPosition))
        {
            if (!pathPossiblePositions.ContainsKey(currentPos))
            {
                return new List<Vector2Int>();
            }
            path.Add(currentPos);
            currentPos = pathPossiblePositions[currentPos];
        }

        path.Reverse();
        return path;
    }
}

