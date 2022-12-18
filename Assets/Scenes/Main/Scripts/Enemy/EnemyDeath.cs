using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SJ22
{
    public class EnemyDeath : MonoBehaviour
    {
        [SerializeField] LayerMask playerBullets;
        [SerializeField] GameObject explosion;
        

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if ((playerBullets.value & (1 << collision.transform.gameObject.layer)) > 0)
            {
                Die();
                
            }
        }

        void Die()
        {
            var explosionGO = Instantiate(explosion);
            explosionGO.transform.parent = this.transform.parent;
            explosionGO.transform.position = this.transform.position;
            Destroy(this.gameObject);
            

        }
    }
}
