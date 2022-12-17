using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SJ22
{
    [CreateAssetMenu(fileName = "EnemyProperties", menuName = "Enemy/Enemy Properties", order = 0)]
    public class EnemyProperties : ScriptableObject
    {
        public Sprite Sprite;
        public Bounds Hitbox;
        public AttackProperties AttackProperties;
    }
}
