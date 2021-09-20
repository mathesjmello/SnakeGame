using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Matriz : MonoBehaviour
{
    public int columns = 30;
    public int rows = 30;
    private Cell[,] _grid;
    public int obstacles;
    private int _rng;
    public List<Cell> listCells = new List<Cell>();
    private List<Cell> _pc = new List<Cell>();
    public int startSize = 0;
    private Vector2 _playerPos;

    public enum CellTypes
    {
        Empty,
        Player,
        Collectable,
        Collider,
        Body
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
        var head = SelectCell();
        _pc.Insert(0,head);
        _pc[0].SetType(CellTypes.Player);
        for (int i = 1; i < startSize; i++)
        {
            var bodyCell = _grid[head.row - i, head.col];
           _pc.Add(bodyCell);
           bodyCell.SetType(CellTypes.Body);
        }
    }

    private Cell SelectCell()
    {
        _rng = Random.Range(0, listCells.Count);
        var chosenCell = listCells[_rng];
        var c = listCells.Find(cell => cell.Id == chosenCell.Id);
        listCells.RemoveAt(_rng);
        return c;
    }

    public void CheckPos(Vector2 dir)
    {
        _playerPos =new Vector2 (_pc[0].row, _pc[0].col);
        var newPlayerPos = _playerPos + dir;
        if ((newPlayerPos.x>=0 && newPlayerPos.x<=9) && (newPlayerPos.y>=0 && newPlayerPos.y<=9))
        {
            var newPLayerCell = _grid[(int)newPlayerPos.x, (int)newPlayerPos.y];
            CheckCollision(newPLayerCell);
        }
        else
        {
            ReloadScene();
        }
    }

    private void CheckCollision(Cell c)
    {
        switch (c.myType)
        {
            case Cell.MyEnum.Empty:
                Move(c);
                CleanLast();
                break;
            case Cell.MyEnum.Collectable:
                Move(c);
                NewCollectable();
                break;
            case Cell.MyEnum.Collider:
                ReloadScene();
                break;
            case Cell.MyEnum.Body:
                ReloadScene();
                break;
        }
    }
    private void NewCollectable()
    {
        var emptyCells = listCells.FindAll(cell => cell.myType == Cell.MyEnum.Empty);
        _rng =  Random.Range(0, emptyCells.Count);
        emptyCells[_rng].SetType(CellTypes.Collectable);
    }

    private void Move(Cell c)
    {
        foreach (var cell in _pc)
        {
            cell.SetType(CellTypes.Body);
        }
        c.SetType(CellTypes.Player);
        _pc.Insert(0,c);
    }

    private void CleanLast()
    {
        var lastBody = _pc[_pc.Count-1];
        lastBody.SetType(CellTypes.Empty);
        _pc.RemoveAt(_pc.Count-1);
    }
    void ReloadScene()
    {
        StopAllCoroutines();
        var scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }
}
