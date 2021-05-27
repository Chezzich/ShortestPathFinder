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

    private const int DEFAULT_FIELD_SIZE = 10;

    private void Awake()
    {
        LabyrinthWidth = DEFAULT_FIELD_SIZE;
        LabyrinthHeight = DEFAULT_FIELD_SIZE;
    }

    public void GenerateField()
    {
        for (int i = 0; i < LabyrinthWidth; i++)
        {
            for (int j = 0; j < LabyrinthHeight; j++)
            {
                Instantiate(fieldCellPrefab, new Vector3(i, j), Quaternion.identity, fieldTransform);
            }
        }
    }
}
