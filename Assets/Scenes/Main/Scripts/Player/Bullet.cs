using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SJ22
{
    public class Bullet : MonoBehaviour
    {
        [SerializeField] float speed = 10f;
        [SerializeField] GameObject bullet;
        [SerializeField] GameTime time;
        [SerializeField] ScreenProperties screenProperties;
        
        [SerializeField] BoxCollider2D bulletCollider;
        float boundingLengthApprox;

        private void Awake()
        {
            boundingLengthApprox =
                Mathf.Sqrt(2) / 2f
                * Mathf.Max(bulletCollider.bounds.size.x, bulletCollider.bounds.size.y);
        }

        void Update()
        {
            bullet.transform.position += Vector3.up * speed * time.DeltaTime;
            if (IsOutOfBounds)
            {
                Destroy(gameObject);
                return;
            }
        }

        bool IsOutOfBounds
        {
            get
            {
                var centerPos = transform.localPosition;
                return Mathf.Abs(centerPos.x) > (screenProperties.Width + boundingLengthApprox) / 2f
                    || Mathf.Abs(centerPos.y) > (screenProperties.Height + boundingLengthApprox) / 2f;
            }
        }
    }
}
