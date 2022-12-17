using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SJ22
{
    public class Shot : MonoBehaviour
    {
        [System.NonSerialized] public ShotProperties Properties;

        [SerializeField] ScreenProperties screenProperties;
        [SerializeField] GameTime time;
        [SerializeField] SpriteRenderer spriteRenderer;
        [SerializeField] BoxCollider2D boxCollider;

        float startTime;
        Vector3 startPosition;
        Quaternion rotation;

        // we approximate the hitbox as a square with length
        // sqrt(2)/2 * max(hitBox.size.x, hitBox.size.y)
        // to account for angle (sqrt(2)/2 is if 45 degrees)
        float boundingLengthApprox;

        bool IsOutOfBounds
        {
            get
            {
                var centerPos = transform.localPosition + Properties.Hitbox.center;
                return Mathf.Abs(centerPos.x + boundingLengthApprox/2) > screenProperties.Width / 2f
                    || Mathf.Abs(centerPos.y + boundingLengthApprox/2) > screenProperties.Height / 2f;
            }
        }

        void Start()
        {
            if(spriteRenderer != null && Properties.Sprite != null)
            {
                spriteRenderer.sprite = Properties.Sprite;
            }
            boxCollider.size = Properties.Hitbox.size;
            boxCollider.offset = Properties.Hitbox.center;
            boundingLengthApprox = 
                Mathf.Sqrt(2)/2f 
                * Mathf.Max(Properties.Hitbox.size.x, Properties.Hitbox.size.y);

            startTime = time.Time;
            startPosition = transform.localPosition;
            rotation = Quaternion.AngleAxis(Properties.Angle, Vector3.up);
            transform.rotation = rotation;
        }

        void Update()
        {
            if (IsOutOfBounds)
            {
                Destroy(gameObject);
            }
            transform.localPosition =
                rotation
                * ((Vector3)Properties.Path.GetPosition(time.Time - startTime) - startPosition)
                + startPosition;
        }
    }
}
