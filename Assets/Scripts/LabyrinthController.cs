using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LabyrinthController : MonoBehaviour
{
    [SerializeField] private GameObject fieldCellPrefab;
    [SerializeField] private Transform fieldTransform;

    private int labyrinthWidth, labyrinthHeight;

    private void Awake()
    {
        GenerateLabyrinth();
    }

    private void GenerateLabyrinth()
    {
        GetLabyrinthWidthAndHeight();
        for (int i = 0; i < labyrinthWidth; i++)
        {
            for (int j = 0; j < labyrinthHeight; j++)
            {
                Instantiate(fieldCellPrefab, new Vector3(i, j), Quaternion.identity, fieldTransform);
            }
        }
    }

    private void GetLabyrinthWidthAndHeight()
    {
        labyrinthWidth = 10;
        labyrinthHeight = 10;
    }
}
