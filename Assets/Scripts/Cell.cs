
using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace
{
    public class Cell : MonoBehaviour

    {
        public int id;
        public int col;
        public int row;
        private Image _img;

        public enum CellType
        {
            Empty,
            Player,
            Collectable,
            Collider,
            Body
        }

        public CellType myType;

        private void Awake()
        {
            _img = GetComponent<Image>();
        }

        public void SetCellInfo(int i, int c, int r)
        {
            id = i;
            col = c;
            row = r;
        }

        public void SetType(CellType cellTypes)
        {
            myType = cellTypes;
            SetColor();
        }

        private void SetColor()
        {
            switch (myType)
            {
                case CellType.Empty:
                    _img.color = Color.white;
                    break;
                case CellType.Player:
                    _img.color = Color.green;
                    break;
                case CellType.Collectable:
                    _img.color = Color.red;
                    break;
                case CellType.Collider:
                    _img.color = Color.black;
                    break;
                case CellType.Body:
                    _img.color = Color.grey;
                    break;
            }
        }
    }
}