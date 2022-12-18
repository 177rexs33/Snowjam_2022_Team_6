using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SJ22
{
    public class PlayerDeath : MonoBehaviour
    {

        [SerializeField] private LayerMask enemyLayer;
        [SerializeField] private EventSO playerDeath;
        [SerializeField] EventSO restartFromCheckpointEvent;


        private void OnTriggerEnter2D(Collider2D collision)
        {
            
            if ((enemyLayer.value & (1 << collision.transform.gameObject.layer)) > 0)
            {
                Die();
                Debug.Log("collision");
            }
        }


        void Die()
        {
            Debug.Log("dieee");
            playerDeath.Event?.Invoke();
            IEnumerator WaitThenRestart()
            {
                yield return new WaitForSeconds(2);
                restartFromCheckpointEvent.Event?.Invoke();
            }

            StartCoroutine(WaitThenRestart());
        }
    }
}
