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
        [SerializeField] private GameObject explosion;


        private void OnTriggerEnter2D(Collider2D collision)
        {
            
            if ((enemyLayer.value & (1 << collision.transform.gameObject.layer)) > 0)
            {
                Die();
                
            }
        }


        void Die()
        {
            this.GetComponent<SpriteRenderer>().enabled = false;
            var explosionGO = Instantiate(explosion);
            explosionGO.transform.parent = this.transform.parent;
            explosionGO.transform.position = this.transform.position;
            playerDeath.Event?.Invoke();
        }
    }
}
