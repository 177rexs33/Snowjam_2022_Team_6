using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SJ22
{
    public class EnemyDeath : MonoBehaviour
    {
        [SerializeField] LayerMask playerBullets;


        private void OnTriggerEnter2D(Collider2D collision)
        {
            if ((playerBullets.value & (1 << collision.transform.gameObject.layer)) > 0)
            {
                Die();
                
            }
        }

        void Die()
        {
            
            Destroy(this.gameObject);
        }
    }
}
