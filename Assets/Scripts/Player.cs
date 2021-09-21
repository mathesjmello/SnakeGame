using System.Collections.Generic;
using UnityEngine.SceneManagement;

namespace DefaultNamespace
{
    public class Player
    {
        private Matrix _matrix;
        public List<Cell> _pc = new List<Cell>();
    
        public Player(Matrix matrix)
        {
            _matrix = matrix;
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
                    ReloadScene();
                    break;
                case Cell.CellType.Body:
                    ReloadScene();
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
    
        public void ReloadScene()
        {
            var scene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(scene.name);
        }
    }

}