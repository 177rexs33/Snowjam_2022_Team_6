using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SJ22
{
    [CreateAssetMenu(fileName = "Checkpoints", menuName = "Checkpoints", order = 0)]
    public class Checkpoints : ScriptableObject
    {
        public List<float> Timepoints;
    }
}
