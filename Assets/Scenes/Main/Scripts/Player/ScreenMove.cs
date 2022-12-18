using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SJ22
{
    public class ScreenMove : MonoBehaviour
    {
        [SerializeField] GameTime time;
        [SerializeField] ScreenProperties properties;

        void Update()
        {
            transform.position += properties.Speed * time.DeltaTime * Vector3.up;
        }


    }
}
