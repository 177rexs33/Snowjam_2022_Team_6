using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SJ22
{
    [CreateAssetMenu(fileName = "PlayerContainer", menuName = "Player Container", order = 0)]
    public class PlayerContainerSO : ScriptableObject
    {
        [System.NonSerialized]
        public GameObject Player;
    }
}
