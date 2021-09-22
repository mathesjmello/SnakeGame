
using UnityEngine;

namespace DefaultNamespace
{
    public class InputSystem: MonoBehaviour
    {
        [SerializeField] private Vector2 direction;
        public float TimeToStart = 2;
        public float TimeToMove = 0.3f;
        private Matrix _m;
        private void Start()
        {
            _m = Bootstrap.Instance.Matrix;
            direction = Vector2.right;
            InvokeRepeating(nameof(ChangePlayerPosition),TimeToStart, TimeToMove);
        }

        private void Update()
        {
            if (Input.GetKey("up"))
            {
                direction = Vector2.down;
            }
            if (Input.GetKey("down"))
            {
                direction = Vector2.up;
            }
            if (Input.GetKey("right"))
            {
                direction = Vector2.right;
            }
            if (Input.GetKey("left"))
            {
                direction = Vector2.left;
            }
        }
        private void ChangePlayerPosition()
        {
            _m.CheckPos(direction);
        }
        
    }
}