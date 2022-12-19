using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SJ22
{
    public class Checkpoint : MonoBehaviour
    {
        [SerializeField] Animator animator;
        
        void Reset()
        {
            animator = GetComponentInChildren<Animator>();
        }

        public void SetActive(bool activeState = true)
        {
            animator.SetBool("checkpoint", activeState);
        }

        

        void Awake()
        {
        
        }
    }
}
