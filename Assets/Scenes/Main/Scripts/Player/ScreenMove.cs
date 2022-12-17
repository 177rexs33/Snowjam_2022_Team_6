using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SJ22
{
    public class ScreenMove : MonoBehaviour
    {
        [SerializeField] GameTime time;
        [SerializeField] float speed;

        void Update()
        {
            transform.position += speed * time.DeltaTime * Vector3.up;
        }
    }
}
