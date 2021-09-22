using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace DefaultNamespace
{
    public class Player: MonoBehaviour
    {
        private Matrix _matrix;
        private GameManager _gm;
        public List<Cell> _pc = new List<Cell>();

        private void Start()
        {
            _matrix = Bootstrap.Instance.Matrix;
            _gm = Bootstrap.Instance.GM;
        }

        public void CheckCollision(Cell c)
        {
            switch (c.myType)
            {
                case Cell.CellType.Empty:
                    Move(c);
                    CleanLast();
                    break;
                case Cell.CellType.Collectable:
                    Move(c);
                    _matrix.NewCollectable();
                    break;
                case Cell.CellType.Collider:
                    _gm.ReloadScene();
                    break;
                case Cell.CellType.Body:
                    _gm.ReloadScene();
                    break;
            }
        }
    
        private void Move(Cell c)
        {
            foreach (var cell in _pc)
            {
                cell.SetType(Cell.CellType.Body);
            }
            c.SetType(Cell.CellType.Player);
            _pc.Insert(0,c);
        }
    
        private void CleanLast()
        {
            var lastBody = _pc[_pc.Count-1];
            lastBody.SetType(Cell.CellType.Empty);
            _pc.RemoveAt(_pc.Count-1);
        }

        public Vector2 PlayerPos()
        {
            Vector2 pos = new Vector2(_pc[0].row, _pc[0].col);
            return pos;
        }
    }

}