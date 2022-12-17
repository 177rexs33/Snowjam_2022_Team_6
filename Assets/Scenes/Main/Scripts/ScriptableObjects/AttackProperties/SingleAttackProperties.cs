using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SJ22
{
    [CreateAssetMenu(fileName = "SingleAttack", menuName = "Enemy/Attack/Single", order = 0)]

    public class SingleAttackProperties : AttackProperties
    {
        public float ShotInterval;
        public float ShotAngle;
        public ShotPath ShotPath;

        public override IEnumerator GenerateShotPattern()
        {
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
