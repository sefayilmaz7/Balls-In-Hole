using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    public static GridManager instance = null;

    public GameObject target;
    public GameObject structure;

    Vector3 truePos;
    Vector3 structPos;

    public float gridSize;


    private void Start()
    {
        structPos = structure.transform.position;
        generateMap(20, 11);
    }

    void LateUpdate()
    {
        generateGrid();
    }

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }

    // This method allows us to prepare our map with blocks
    void generateMap(int rows, int cols)

    {
        GameObject cloneStruct;
        for (int row = 0; row <= rows; row++)
        {
            cloneStruct = Instantiate(structure);
            cloneStruct.transform.position = structPos -= new Vector3(0, 0, 0.5f);
        }
        for (int col = 0; col < cols; col++)
        {
            cloneStruct = Instantiate(structure);
            cloneStruct.transform.position = structPos += new Vector3(0.5f, 0, 0);
        }
        for (int row = 0; row <= rows; row++)
        {
            cloneStruct = Instantiate(structure);
            cloneStruct.transform.position = structPos += new Vector3(0, 0, 0.5f);
        }
        for (int col = 0; col < cols; col++)
        {
            cloneStruct = Instantiate(structure);
            cloneStruct.transform.position = structPos -= new Vector3(0.5f, 0, 0);
        }
        // creating inner walls
        for (int i = 0; i < 8; i++)
        {
            cloneStruct = Instantiate(structure);
            cloneStruct.transform.position = truePos -= new Vector3(0, 0, 0.5f);
        }
        for (int j = 0; j < 3; j++)
        {
            cloneStruct = Instantiate(structure);
            cloneStruct.transform.position = truePos -= new Vector3(0.5f, 0, 0);
        }
    }
    // This method created our grid system and positioned the first block
    void generateGrid()
    {
        truePos.x = Mathf.Floor(target.transform.position.x / gridSize) * gridSize;
        truePos.y = Mathf.Floor(target.transform.position.y / gridSize) * gridSize;
        truePos.z = Mathf.Floor(target.transform.position.z / gridSize) * gridSize;
        structure.transform.position = truePos;

    }
}
