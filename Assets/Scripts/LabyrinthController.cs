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

    private void Awake()
    {
        LabyrinthWidth = DEFAULT_FIELD_SIZE;
        LabyrinthHeight = DEFAULT_FIELD_SIZE;
        IsActiveBrushes = false;
        fieldCells = new List<FieldCellInfo>();
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
                fieldCells.Add(currentCell);
            }
        }
    }
}
