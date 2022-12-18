using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SJ22
{
    [CreateAssetMenu(fileName = "RepeatAttack", menuName = "Enemy/Attack/Repeat", order = 0)]
    public class RepeatAttackProperties : AttackProperties
    {
        public Sprite Sprite;
        public Bounds Hitbox;

        public float ShotDelay;
        public float ShotInterval;
        public float ShotAngle;
        public ShotPath ShotPath;

        public override IEnumerator GenerateShotPattern()
        {
            yield return new Wait(ShotDelay);
            while (true)
            {
                yield return new ShotProperties
                {
                    Sprite = Sprite,
                    Hitbox = Hitbox,
                    Angle = ShotAngle,
                    Path = ShotPath,
                };
                yield return new Wait(ShotInterval);
            }
        }
    }
}
