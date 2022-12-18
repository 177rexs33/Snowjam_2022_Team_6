using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SJ22
{
    public class Bullet : MonoBehaviour
    {
        private float speed = 10f;
        [SerializeField] GameObject bullet;
        [SerializeField] GameTime time;

        void Update()
        {
            bullet.transform.position += Vector3.up * speed * time.DeltaTime;
        }
    }
}
