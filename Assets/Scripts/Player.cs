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
        public List<Cell> pc = new List<Cell>();

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
                default:
                    _gm.ReloadScene();
                    break;
            }
        }
    
        private void Move(Cell c)
        {
            foreach (var cell in pc)
            {
                cell.SetType(Cell.CellType.Body);
            }
            InsertHead(c);
        }
    
        private void CleanLast()
        {
            var lastBody = pc[pc.Count-1];
            lastBody.SetType(Cell.CellType.Empty);
            pc.RemoveAt(pc.Count-1);
        }

        public Vector2 PlayerPos()
        {
            Vector2 pos = new Vector2(pc[0].row, pc[0].col);
            return pos;
        }

        public void InsertHead(Cell c)
        {
            pc.Insert(0, c);
            pc[0].SetType(Cell.CellType.Player);
        }

        public void AddBody(Cell c)
        {
            pc.Add(c);
            c.SetType(Cell.CellType.Body);
        }
    }

}