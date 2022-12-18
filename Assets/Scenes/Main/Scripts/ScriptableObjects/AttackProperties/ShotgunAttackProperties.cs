using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SJ22
{
    [CreateAssetMenu(fileName = "RepeatAttack", menuName = "Enemy/Attack/Shotgun", order = 0)]
    public class ShotgunAttackProperties : AttackProperties
    {
        public Sprite Sprite;
        public Bounds Hitbox;

        public float ShotDelay;
        public float ShotInterval;
        public float ShotAngle;

        public int NShots;
        public float ShotSpreadAngle;

        public List<ShotPath> ShotPaths;
        public override IEnumerator GenerateShotPattern()
        {
            yield return new Wait(ShotDelay);
            while (true)
            {
                for(int i = 0; i < NShots; ++i)
                {
                    ShotPath shotPath = null;
                    if(ShotPaths != null && ShotPaths.Count > 0)
                    {
                        shotPath = ShotPaths[i % ShotPaths.Count];
                    }
                    float theta = 
                        -ShotSpreadAngle / 2
                        + (float)i/(NShots - 1)
                        * ShotSpreadAngle;
                    yield return new ShotProperties
                    {
                        Sprite = Sprite,
                        Hitbox = Hitbox,
                        Angle = ShotAngle + theta,
                        Path = shotPath,
                    };
                }
                yield return new Wait(ShotInterval);
            }
        }
    }
}
