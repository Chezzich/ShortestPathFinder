using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LabyrinthController : MonoBehaviour
{
    [SerializeField] private GameObject fieldCellPrefab;
    [SerializeField] private Transform fieldTransform;

    public int LabyrinthWidth { get; set; }
    public int LabyrinthHeight { get; set; }
    
    public static bool IsActiveBrushes { get; set; }
    public static FieldCellInfo.FieldCellType ActiveBrushType { get; set; }

    private const int DEFAULT_FIELD_SIZE = 10;

    private List<FieldCellInfo> fieldCells;

    private FieldCellInfo startCell;
    private FieldCellInfo endCell;

    private void Awake()
    {
        LabyrinthWidth = DEFAULT_FIELD_SIZE;
        LabyrinthHeight = DEFAULT_FIELD_SIZE;
        IsActiveBrushes = false;
        fieldCells = new List<FieldCellInfo>();
        startCell = null;
        endCell = null;
    }

    public void GenerateField()
    {
        foreach (var cell in fieldCells)
        {
            Destroy(cell.gameObject);
        }
        fieldCells.Clear();

        FieldCellInfo currentCell = null;
        for (int i = 0; i < LabyrinthWidth; i++)
        {
            for (int j = 0; j < LabyrinthHeight; j++)
            {
                currentCell = Instantiate(fieldCellPrefab, new Vector3(i, j), Quaternion.identity, fieldTransform).GetComponent<FieldCellInfo>();
                currentCell.SetLabyrinthController(this);
                currentCell.Position = new Vector2Int(i, j);
                fieldCells.Add(currentCell);
            }
        }
    }

    public void SetStartCell(FieldCellInfo cell)
    {
        if (startCell != null)
            startCell.SetCellType(FieldCellInfo.FieldCellType.Empty);
        startCell = cell;
    }

    public void SetEndCell(FieldCellInfo cell)
    {
        if (endCell)
            endCell.SetCellType(FieldCellInfo.FieldCellType.Empty);
        endCell = cell;
    }

    public void FindPath()
    {
        if (startCell && endCell)
        {
            SquareGrid grid = new SquareGrid(LabyrinthWidth, LabyrinthHeight, fieldCells);
            AStarSearch aStarSearch = new AStarSearch(grid, startCell.Position, endCell.Position);
            List<Vector2Int> path = aStarSearch.GetPath();
            foreach (var item in path)
            {
                GetCellAtPoint(new Vector2Int(item.x, item.y)).ShowPathPoint();
            }
        }
    }


    public void GenerateWalls()
    {
        FieldCellInfo cell;
        for (int j = LabyrinthHeight - 1; j >= 0; j--)
        {
            for (int i = 0; i < LabyrinthWidth; i++)
            {
                cell = GetCellAtPoint(new Vector2Int(i, j));
                if (cell.GetCellType() == FieldCellInfo.FieldCellType.Empty)
                {
                    if (cell.Position.x == 0 || cell.Position.x == LabyrinthWidth - 1
                        || cell.Position.y == 0 || cell.Position.y == LabyrinthHeight - 1)
                        cell.SetCellType(FieldCellInfo.FieldCellType.Wall);
                    else
                    {
                        bool isUpperWall = GetCellAtPoint(new Vector2Int(cell.Position.x, cell.Position.y + 1))?.GetCellType() == FieldCellInfo.FieldCellType.Wall;
                        bool isUpperLeftCorner = GetCellAtPoint(new Vector2Int(cell.Position.x + 1, cell.Position.y + 1))?.GetCellType() == FieldCellInfo.FieldCellType.Wall;
                        bool isLeftRightWalls = (GetCellAtPoint(new Vector2Int(cell.Position.x - 1, cell.Position.y))?.GetCellType() == FieldCellInfo.FieldCellType.Wall
                                            || GetCellAtPoint(new Vector2Int(cell.Position.x + 1, cell.Position.y))?.GetCellType() == FieldCellInfo.FieldCellType.Wall);


                        if ((isUpperWall && isLeftRightWalls) || isUpperLeftCorner)
                            cell.SetCellType(FieldCellInfo.FieldCellType.Empty);
                        else
                            cell.SetCellType(UnityEngine.Random.Range(0, 2) == 0 ? FieldCellInfo.FieldCellType.Wall : FieldCellInfo.FieldCellType.Empty);
                    }
                }
            }
        }

        fieldCells.Find((el) => el.GetCellType() == FieldCellInfo.FieldCellType.Empty).SetCellType(FieldCellInfo.FieldCellType.StartPoint);
        fieldCells.FindLast((el) => el.GetCellType() == FieldCellInfo.FieldCellType.Empty).SetCellType(FieldCellInfo.FieldCellType.EndPoint);
    }

    private FieldCellInfo GetCellAtPoint(Vector2Int point)
    {
        return fieldCells.Find((cell) => cell.Position == point);
    }
}