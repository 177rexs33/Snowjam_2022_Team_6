using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SJ22
{


    [CreateAssetMenu(fileName = "Event", menuName = "Event", order = 0)]
    public class EventSO : ScriptableObject
    {
        public System.Action Event;
    }
}
