using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

namespace SJ22
{
    [CreateAssetMenu(fileName = "EnemyProperties", menuName = "Enemy/Enemy Properties", order = 0)]
    public class EnemyProperties : ScriptableObject
    {
        public Sprite Sprite;
        public float Scale = 1f;
        public AnimatorController AnimatorController;
        public Bounds Hitbox;
        public AttackProperties AttackProperties;
    }
}
