using System;
using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace
{
    public class Cell : MonoBehaviour

    {
        public int Id;
        public int col;
        public int row;
        private Image _img;

        public enum MyEnum
        {
            Empty,
            Player,
            Collectable,
            Collider,
            Body
        }

        public MyEnum myType;

        private void Awake()
        {
            _img = GetComponent<Image>();
        }

        public void SetType(Matriz.CellTypes cellTypes)
        {
            myType = (MyEnum) cellTypes;
            SetColor();
        }

        private void SetColor()
        {
            switch (myType)
            {
                case MyEnum.Empty:
                    _img.color = Color.white;
                    break;
                case MyEnum.Player:
                    _img.color = Color.green;
                    break;
                case MyEnum.Collectable:
                    _img.color = Color.red;
                    break;
                case MyEnum.Collider:
                    _img.color = Color.black;
                    break;
                case MyEnum.Body:
                    _img.color = Color.grey;
                    break;
            }
        }
    }
}