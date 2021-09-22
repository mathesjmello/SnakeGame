
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;


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
    private Player _player;
    private GameManager _gm;
    private int limitSize = 4;
    
    
    void Start()
    {
        _player = Bootstrap.Instance.Player;
        _gm = Bootstrap.Instance.GM;
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
                cell.id = id;
                id++;
                _grid[j, i] = cell;
                _listCells.Add(cell);
            }
        }
        SetBoard();
    }

    private void SetBoard()
    {
        DefinePlayer();
        DefineCollider();
        DefineCollectable();
    }
    
    private void DefinePlayer()
    {
        var head = _listCells[55];
        _player._pc.Insert(0,head);
        _player._pc[0].SetType(Cell.CellType.Player);
        if (startSize>limitSize || startSize<0)
        {
            startSize = 3;
        }
        for (int i = 1; i < startSize; i++)
        {
            var bodyCell = _grid[head.row - i, head.col];
            _listCells.RemoveAt(_listCells.Find(cell => cell.id == bodyCell.id).id);
            _player._pc.Add(bodyCell);
            bodyCell.SetType(Cell.CellType.Body);
        }
    }
    
    private void DefineCollectable()
    {
        PeekRandomCell().SetType(Cell.CellType.Collectable);
    }

    private void DefineCollider()
    {
        for (int i = 0; i < obstacles; i++)
        {
            PeekRandomCell().SetType(Cell.CellType.Collider);
        }
    }
    
    private Cell PeekRandomCell()
    {
        _rng = Random.Range(0, _listCells.Count);
        var chosenCell = _listCells[_rng];
        _listCells.RemoveAt(_rng);
        return chosenCell;
    }

    public void CheckPos(Vector2 dir)
    {
        _playerPos = _player.PlayerPos();
        var newPlayerPos = _playerPos + dir;
        if (newPlayerPos.x>=0 && newPlayerPos.x<=9 && newPlayerPos.y>=0 && newPlayerPos.y<=9)
        {
            var newPLayerCell = _grid[(int)newPlayerPos.x, (int)newPlayerPos.y];
            _player.CheckCollision(newPLayerCell);
        }
        else
        {
            _gm.ReloadScene();
        }
    }

    public void NewCollectable()
    {
        var emptyCells = _listCells.FindAll(cell => cell.myType == Cell.CellType.Empty);
        _rng =  Random.Range(0, emptyCells.Count);
        emptyCells[_rng].SetType(Cell.CellType.Collectable);
    }
}
