using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SJ22
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerMove : MonoBehaviour
    {
        [SerializeField] GameInputReader inputReader;
        [SerializeField] float speed;

        Rigidbody2D rb;
        Vector2 moveDirection;

        void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
            //inputReader.Gameplay.Move += dir => rb.velocity = dir * speed;
            inputReader.Gameplay.Move += dir => moveDirection = dir;
        }

        void Update()
        {
            transform.position += Time.deltaTime * (Vector3) moveDirection * speed;
        }
    }
}
