
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.UI;


public class Matrix : MonoBehaviour
{
    public int columns = 10;
    public int rows = 10;
    private Cell[,] _grid;
    public int obstacles;
    private int _rng;
    private List<Cell> _listCells = new List<Cell>();
    public int startSize = 0;
    private Vector2 _playerPos;
    private Player _player;
    private GameManager _gm;
    private int limitSize = 4;
    private Rect _rect;
    private GridLayoutGroup  _gg;
    
    void Start()
    {
        _gg = GetComponent<GridLayoutGroup>();
        _gg.constraintCount = columns;
        _rect = new Rect(0, 0, rows, columns);
        _player = Bootstrap.Instance.Player;
        _gm = Bootstrap.Instance.GM;
        CreateGrid();
    }
    private void CreateGrid()
    {
        _grid = new Cell[rows, columns];
        var id = 0;
        for (int c = 0; c < columns; c++)
        {
            for (int r = 0; r < rows; r++)
            {
                var cell = Instantiate(Resources.Load<Cell>("cell"),transform);
                cell.myType = Cell.CellType.Empty;
                cell.SetCellInfo(id, c , r);
                id++;
                _grid[r, c] = cell;
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
        _listCells.Remove(head);
        _player.InsertHead(head);
        if (startSize>limitSize || startSize<0)
        {
            startSize = 3;
        }
        for (int i = 1; i < startSize; i++)
        {
            var bodyCell = _grid[head.row - i, head.col];
            _listCells.Remove(_listCells.Find(cell => cell.id == bodyCell.id));
            _player.AddBody(bodyCell);
           
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
        _listCells.Remove(chosenCell);
        return chosenCell;
    }

    public void CheckPos(Vector2 dir)
    {
        _playerPos = _player.PlayerPos();
        var newPlayerPos = _playerPos + dir;
        if (_rect.Contains(newPlayerPos))
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
