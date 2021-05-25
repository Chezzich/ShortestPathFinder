using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldCellInfo : MonoBehaviour
{
    public FieldCellType CellType; 

    public enum FieldCellType
    {
        Empty,
        Wall
    }

    private void OnMouseDown()
    {
        
    }
}
