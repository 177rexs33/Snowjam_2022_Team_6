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


        private void OnTriggerEnter2D(Collider2D collision)
        {
            if ((enemyLayer.value & (1 << collision.transform.gameObject.layer)) > 0)
            {
                Die();
            }
        }


        void Die()
        {
            Debug.Log("death");
            playerDeath.Event?.Invoke();
        }
    }
}
