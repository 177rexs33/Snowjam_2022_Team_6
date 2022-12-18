using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SJ22
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerMove : MonoBehaviour
    {
        [SerializeField] GameTime time;
        [SerializeField] EventSO restartFromCheckpointEvent;
        [SerializeField] GameInputReader inputReader;
        [SerializeField] ScreenProperties screenProperties;
        [SerializeField] float speed;

        Rigidbody2D rb;
        Vector2 moveDirection;
        Vector3 startPosition;

        void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
            // we move in Update to make movement smoother
            inputReader.Gameplay.Move += dir => moveDirection = dir;

            startPosition = transform.localPosition;
            restartFromCheckpointEvent.Event += ResetLocalPosition;
        }

        void ResetLocalPosition()
        {
            transform.localPosition = startPosition;
        }

        void Update()
        {
            transform.localPosition += time.DeltaTime * (Vector3) moveDirection * speed;

            // clamp transform so that it does not go outside of screen
            // we subtract 1 because that is the width/height of the player
            transform.localPosition = new Vector3(
                Mathf.Clamp(
                    transform.localPosition.x, 
                    -(screenProperties.Width - 1)/2, 
                    (screenProperties.Width - 1)/2
                ),
                Mathf.Clamp(
                    transform.localPosition.y, 
                    -(screenProperties.Height - 1)/2, 
                    (screenProperties.Height - 1)/2
                ),
                transform.localPosition.z
            );
        }
    }
}
