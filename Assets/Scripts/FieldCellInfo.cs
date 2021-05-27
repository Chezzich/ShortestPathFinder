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

    public FieldCellType CellType;

    private LabyrinthController labyrinthController;

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

    public void SetCellType(FieldCellType cellType)
    {
        if (CellType == FieldCellType.StartPoint)
            labyrinthController.SetStartCell(null);
        else if (CellType == FieldCellType.EndPoint)
            labyrinthController.SetEndCell(null);

        switch (cellType)
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
}
