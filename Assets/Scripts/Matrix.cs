using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class Matrix : MonoBehaviour
{
    public int columns = 30;
    public int rows = 30;
    private Cell[,] _grid;
    public int obstacles;
    private int _rng;
    private List<Cell> _listCells = new List<Cell>();
    public int startSize = 0;
    private Vector2 _playerPos;
    private readonly Player _player;

    public Matrix()
    {
        _player = new Player(this);
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
                cell.myType = Cell.CellType.Empty;
                cell.col = i;
                cell.row = j;
                cell.Id = id;
                id++;
                _grid[j, i] = cell;
                _listCells.Add(cell);
            }
        }
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
        SelectCell().SetType(Cell.CellType.Collectable);
    }

    private void DefineCollider()
    {
        for (int i = 0; i < obstacles; i++)
        {
            SelectCell().SetType(Cell.CellType.Collider);
        }
    }
    
    private void DefinePlayer()
    {
        var head = SelectCell();
        _player._pc.Insert(0,head);
        _player._pc[0].SetType(Cell.CellType.Player);
        for (int i = 1; i < startSize; i++)
        {
            var bodyCell = _grid[head.row - i, head.col];
            _player._pc.Add(bodyCell);
           bodyCell.SetType(Cell.CellType.Body);
        }
    }

    private Cell SelectCell()
    {
        _rng = Random.Range(0, _listCells.Count);
        var chosenCell = _listCells[_rng];
        var c = _listCells.Find(cell => cell.Id == chosenCell.Id);
        _listCells.RemoveAt(_rng);
        return c;
    }

    public void CheckPos(Vector2 dir)
    {
        _playerPos =new Vector2 (_player._pc[0].row, _player._pc[0].col);
        var newPlayerPos = _playerPos + dir;
        if ((newPlayerPos.x>=0 && newPlayerPos.x<=9) && (newPlayerPos.y>=0 && newPlayerPos.y<=9))
        {
            var newPLayerCell = _grid[(int)newPlayerPos.x, (int)newPlayerPos.y];
            _player.CheckCollision(newPLayerCell);
        }
        else
        {
            _player.ReloadScene();
        }
    }

    public void NewCollectable()
    {
        var emptyCells = _listCells.FindAll(cell => cell.myType == Cell.CellType.Empty);
        _rng =  Random.Range(0, emptyCells.Count);
        emptyCells[_rng].SetType(Cell.CellType.Collectable);
    }
}
