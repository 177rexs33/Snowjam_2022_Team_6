using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SJ22
{
    [System.Serializable]
    public class NamedEnemyProperties {
        public string Name;
        public EnemyProperties Properties;
    }

    [CreateAssetMenu(fileName = "EnemyDatabase", menuName = "Enemy/Database", order = 0)]
    public class EnemyDatabase : ScriptableObject
    {
        public List<NamedEnemyProperties> Enemies;

        public EnemyProperties this[string key]
        {
            get
            {
                var properties = Enemies.Find(e => e.Name == key);
                if (properties == null)
                {
                    return null;
                }
                return properties.Properties;
            }
        }
    }
}
