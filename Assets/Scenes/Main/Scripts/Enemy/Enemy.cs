using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SJ22
{
    public class Enemy : MonoBehaviour
    {
        [SerializeField] GameTime time;
        [SerializeField] EventSO restartFromCheckpointEvent;

        [SerializeField] new BoxCollider2D collider;

        [SerializeField] GameObject shotPrefab;

        [SerializeField] public EnemyProperties Properties;
        [SerializeField] public GameObject ShotContainer;

        IEnumerator shotEnumerator;

        bool waiting = false;
        float waitEndTime;

        void Awake()
        {
            restartFromCheckpointEvent.Event += OnRestart;
        }

        void OnDestroy()
        {
            restartFromCheckpointEvent.Event -= OnRestart;
        }

        void OnRestart()
        {
            Destroy(gameObject);
        }

        void Start()
        {
            collider.size = Properties.Hitbox.size;
            collider.offset = Properties.Hitbox.center;

            shotEnumerator = Properties.AttackProperties.GenerateShotPattern();

            if (Properties.SpritePrefab != null)
            {
                var spritePrefab = Instantiate(Properties.SpritePrefab);
                spritePrefab.transform.SetParent(transform);
                spritePrefab.transform.localPosition = Vector3.zero;
            }
        }

        private void Update()
        {
            if (waiting && time.Time >= waitEndTime)
            {
                waiting = false;
            }
            if (!waiting)
            {
                GenerateShots();
            }

        }

        void GenerateShots()
        {
            // try to get the next shot from the enumerator
            // returns false if end of enumerator
            while (shotEnumerator.MoveNext())
            {
                object obj = shotEnumerator.Current;
                if (obj is ShotProperties shotProperties)
                {
                    InstantiateShot(shotProperties);
                }
                else if (obj is AttackProperties.Wait wait)
                {
                    waitEndTime = time.Time + wait.Seconds;
                    waiting = true;
                    break;
                }
                else
                {
                    // just in case ;)
                    break;
                }
            }
        }

        void InstantiateShot(ShotProperties shotProperties)
        {
            var shotGO = Instantiate(shotPrefab);
            shotGO.transform.position = transform.position + (Vector3)shotProperties.Offset;

            var shot = shotGO.GetComponent<Shot>();
            shot.Properties = shotProperties;
            shot.transform.SetParent(ShotContainer.transform, true);
        }

    }
}
