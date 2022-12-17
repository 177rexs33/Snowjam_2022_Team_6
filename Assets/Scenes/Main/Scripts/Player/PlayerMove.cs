using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SJ22
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerMove : MonoBehaviour
    {
        [SerializeField] GameTime time;
        [SerializeField] GameInputReader inputReader;
        [SerializeField] float speed;

        Rigidbody2D rb;
        Vector2 moveDirection;

        void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
            // we move in Update to make movement smoother
            inputReader.Gameplay.Move += dir => moveDirection = dir;
        }

        void Update()
        {
            transform.position += time.DeltaTime * (Vector3) moveDirection * speed;
        }
    }
}
