using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SJ22
{
    public class Explosion : MonoBehaviour
    {
        public float explodeTime = 1f;
        private float startTime;
        void Awake()
        {
            startTime = Time.time;
        }

        void Update()
        {
            if(Time.time > startTime + explodeTime)
            {
                Debug.Log("destroy");
                Destroy(this.gameObject);
            }
        }
    }
}
