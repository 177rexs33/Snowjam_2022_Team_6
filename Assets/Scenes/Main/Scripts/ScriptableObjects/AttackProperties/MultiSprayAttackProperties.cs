using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SJ22
{
    [System.Serializable]
    public class SprayRange
    {
        public float StartAngle, EndAngle;
    }

    [CreateAssetMenu(fileName = "MultiSprayAttackProperties", menuName = "Enemy/Attack/MultiSpray", order = 0)]
    public class MultiSprayAttackProperties : AttackProperties
    {
        public Sprite Sprite;
        public Bounds Hitbox;

        public float AttackDelay;
        public float AttackInterval;
        public float NetAttackAngle;

        public float SprayInterval;
        public List<SprayRange> SprayRanges;

        public float ShotInterval;
        public int NShots;

        public List<ShotPath> ShotPaths;

        public override IEnumerator GenerateShotPattern()
        {
            yield return new Wait(AttackDelay);
            while (true)
            {
                foreach (var sprayRange in SprayRanges)
                {
                    for (int i = 0; i < NShots; ++i)
                    {
                        ShotPath shotPath = null;
                        if (ShotPaths != null && ShotPaths.Count > 0)
                        {
                            shotPath = ShotPaths[i % ShotPaths.Count];
                        }
                        float theta =
                            sprayRange.StartAngle
                            + (sprayRange.EndAngle - sprayRange.StartAngle)
                            * i / (NShots - 1);
                        yield return new ShotProperties
                        {
                            Sprite = Sprite,
                            Hitbox = Hitbox,
                            Angle = NetAttackAngle + theta,
                            Path = shotPath,
                        };
                        yield return new Wait(ShotInterval);
                    }
                    yield return new Wait(SprayInterval);
                }
                yield return new Wait(AttackInterval);
            }
        }
    }
}
