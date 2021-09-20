using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.UI;

public class Matriz : MonoBehaviour
{
    public int columns = 30;
    public int rows = 30;
    private Cell[,] _grid;
    public int obstacles;
    private int _rng;
    public List<Cell> listCells = new List<Cell>();
    private List<Cell> _displayedList = new List<Cell>();
    public Cell playerCell;
    private Vector2 _playerPos;

    public enum CellTypes
    {
        Empty,
        Player,
        Collectable,
        Collider
    }
    void Start()
    {
        CreateGrid();
    }
    private void CreateGrid()
    {
        _grid = new Cell[rows, columns];
        var id = 0;
        for (int i = 0; i < columns; i++)
        {
            for (int j = 0; j < rows; j++)
            {
                var cell = Instantiate(Resources.Load<Cell>("cell"),transform);
                cell.myType = (Cell.MyEnum) CellTypes.Empty;
                cell.col = i;
                cell.row = j;
                cell.Id = id;
                id++;
                _grid[j, i] = cell;
                listCells.Add(cell);
            }
        }
        _displayedList = listCells;
        SetBoard();
    }

    private void SetBoard()
    {
        DefineCollider();
        DefinePlayer();
        DefineCollectable();
    }

    private void DefineCollectable()
    {
        SelectCell().SetType(CellTypes.Collectable);
    }

    private void DefineCollider()
    {
        for (int i = 0; i < obstacles; i++)
        {
            SelectCell().SetType(CellTypes.Collider);
        }
    }
    
    private void DefinePlayer()
    {
        playerCell = SelectCell();
        playerCell.SetType(CellTypes.Player);
    }

    private Cell SelectCell()
    {
        _rng = Random.Range(0, _displayedList.Count);
        var chosenCell = _displayedList[_rng];
        var c = listCells.Find(cell => cell.Id == chosenCell.Id);
        _displayedList.RemoveAt(_rng);
        return c;
    }

    public void MovePlayer(Vector2 dir)
    {
        playerCell.SetType(CellTypes.Empty);
        _playerPos =new Vector2 (playerCell.row, playerCell.col);
        var newplayerpos = _playerPos + dir;
        var newPLayerCell = _grid[(int)newplayerpos.x, (int)newplayerpos.y];
        newPLayerCell.SetType(CellTypes.Player);
        playerCell = newPLayerCell;
        
    }
}
