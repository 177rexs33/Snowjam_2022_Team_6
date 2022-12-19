using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SJ22
{
    public class PlayerInitializer : MonoBehaviour
    {
        [SerializeField] PlayerContainerSO playerContainer;

        void Awake()
        {
            playerContainer.Player = gameObject;
        }
    }
}
