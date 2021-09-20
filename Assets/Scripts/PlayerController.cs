using System;
using System.Collections;
using UnityEngine;

namespace DefaultNamespace
{
    public class PlayerController: MonoBehaviour
    {
        [SerializeField] private Vector2 direction;
        private Matriz _m;
        private void Start()
        {
            _m = FindObjectOfType<Matriz>();
            direction = Vector2.right;
            InvokeRepeating(nameof(ChangePlayerPosition),2f, 0.5f);
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