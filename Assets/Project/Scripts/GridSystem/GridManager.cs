using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    public int width, height;
    public float cellSize;

    private GridCell[,] _grid;

    private GameObject _primitive;

    private Vector3 _startPosition;

    private Camera _camera;

    public levelObjectData levelObject;

    private void Start()
    {
        _camera = Camera.main ?? FindObjectOfType<Camera>();
        
        _startPosition = transform.position;
        
        _grid = new GridCell[width, height];

        _primitive = GameObject.CreatePrimitive(PrimitiveType.Cube);
        _primitive.layer = LayerMask.NameToLayer("BuildableArea");
        Destroy(_primitive);
        
        CreateGrid();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            var pos = GetGridPos(GetMousePos());
            var index = (pos.x * height) + pos.y;
            transform.GetChild(index).GetComponent<MeshRenderer>().enabled = false;

            if (!_grid[pos.x, pos.y].cellPopulated)
            {
                _grid[pos.x, pos.y].cellPopulated = true;
                Instantiate(levelObject.model, _grid[pos.x, pos.y].worldPosition, Quaternion.identity);
            }
        }
    }

    #region GridCreation

    void CreateGrid()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                _grid[x, y].cellPopulated = false;
                _grid[x, y].worldPosition = GetPositionInWorldPos(x, y);

                var visualCell = Instantiate(_primitive, _grid[x, y].worldPosition, Quaternion.identity);
                visualCell.transform.parent = transform;
                visualCell.transform.localScale = new Vector3(cellSize * .8f, .2f, cellSize * .8f);
                visualCell.GetComponent<MeshRenderer>().material.color = new Color(0.96f, 0.97f, 0.42f, 0.53f);
            }
        }
    }

    Vector3 GetPositionInWorldPos(int x, int y)
    {
        var center = new Vector3((width - 1) * cellSize, 0, (height - 1) * cellSize) * .5f;
        var setGridPos = _startPosition - center;
        var pos = new Vector3(x * cellSize, 0, y * cellSize) + setGridPos;

        return pos;
    }

    private struct GridCell
    {
        public Vector3 worldPosition;
        public bool cellPopulated;
    }

    #endregion
    
    
    #region PopulateGrid

    private Vector3 GetMousePos()
    {
        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit raycastHit, Mathf.Infinity, 1 << LayerMask.NameToLayer("BuildableArea")))
        {
            var point = new Vector3(raycastHit.point.x, transform.position.y, raycastHit.point.z);
            print(point);
            return point;
        }
        else
        {
            return Vector3.zero;
        }
    }

    private Vector2Int GetGridPos(Vector3 worldPos)
    {
        var diffX = (worldPos.x) + ((width * cellSize) * .5f);
        var diffY = (worldPos.z) + ((height * cellSize) * .5f);
        int x = Mathf.FloorToInt((diffX) / (cellSize));
        int y = Mathf.FloorToInt((diffY) / (cellSize));
        
        print($"{x} / {y}");

        return new Vector2Int(x, y);
    }
    
    #endregion
    
}
