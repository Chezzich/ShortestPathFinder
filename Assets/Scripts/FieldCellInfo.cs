using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldCellInfo : MonoBehaviour
{
    [SerializeField] private MeshRenderer meshRenderer;
    [SerializeField] private Material emptyMaterial;
    [SerializeField] private Material wallMaterial;
    public FieldCellType CellType;

    public enum FieldCellType
    {
        Empty,
        Wall
    }

    private void OnMouseDown()
    {
        if (LabyrinthController.IsActiveBrushes)
        {
            CellType = LabyrinthController.ActiveBrushType;
            switch (CellType)
            {
                case FieldCellType.Empty:
                    meshRenderer.material = emptyMaterial;
                    break;
                case FieldCellType.Wall:
                    meshRenderer.material = wallMaterial;
                    break;
                default:
                    break;
            }
        }
    }
}
