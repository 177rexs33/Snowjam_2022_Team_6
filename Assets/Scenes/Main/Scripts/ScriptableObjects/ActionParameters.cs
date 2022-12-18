using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SJ22
{
    [CreateAssetMenu(fileName = "ActionParameters", menuName = "Action Parameters", order = 0)]
    public class ActionParameters : ScriptableObject
    {
        public float ActiveDuration;
        public float RechargeDuration;
    }
}
