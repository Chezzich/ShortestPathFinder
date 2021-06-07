using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldCellInfo : MonoBehaviour
{
    [SerializeField] private MeshRenderer meshRenderer;
    [SerializeField] private Material emptyMaterial;
    [SerializeField] private Material wallMaterial;
    [SerializeField] private Material startPointMaterial;
    [SerializeField] private Material endPointMaterial;
    [SerializeField] private Material possiblePathPointMaterial;

    private FieldCellType cellType;

    private LabyrinthController labyrinthController;

    public Vector2Int Position { get; set; }

    public enum FieldCellType
    {
        Empty,
        Wall,
        StartPoint,
        EndPoint
    }

    private void OnMouseDown()
    {
        if (LabyrinthController.IsActiveBrushes)
        {
            SetCellType(LabyrinthController.ActiveBrushType);
        }
    }

    public void SetLabyrinthController(LabyrinthController controller)
    {
        labyrinthController = controller;
    }

    public void SetCellType(FieldCellType newCellType)
    {
        if (newCellType == FieldCellType.StartPoint)
            labyrinthController.SetStartCell(null);
        else if (newCellType == FieldCellType.EndPoint)
            labyrinthController.SetEndCell(null);

        cellType = newCellType;

        switch (newCellType)
        {
            case FieldCellType.Empty:
                meshRenderer.material = emptyMaterial;
                break;
            case FieldCellType.Wall:
                meshRenderer.material = wallMaterial;
                break;
            case FieldCellType.StartPoint:
                meshRenderer.material = startPointMaterial;
                labyrinthController.SetStartCell(this);
                break;
            case FieldCellType.EndPoint:
                meshRenderer.material = endPointMaterial;
                labyrinthController.SetEndCell(this);
                break;
            default:
                break;
        }
    }

    public FieldCellType GetCellType()
    {
        return cellType;
    }

    public int GetCellMovePrice()
    {
        int price = 0;
        switch (cellType)
        {
            case FieldCellType.Empty:
                price = 1;
                break;
            case FieldCellType.Wall:
                price = int.MaxValue;
                break;
            case FieldCellType.StartPoint:
                price = 1;
                break;
            case FieldCellType.EndPoint:
                price = 1;
                break;
        }
        return price;
    }

    public void ShowPathPoint()
    {
        meshRenderer.material = startPointMaterial;
    }

    public void ShowPathPossiblePoint()
    {
        meshRenderer.material = possiblePathPointMaterial;
    }
}
