using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SJ22
{
    [CreateAssetMenu(fileName = "MultiShotgunAttackProperties", menuName = "Enemy/Attack/MultiShotgun", order = 0)]
    public class MultiShotgunAttackProperties : AttackProperties
    {
        public Sprite Sprite;
        public Bounds Hitbox;

        public float AttackDelay;
        public float AttackInterval;
        public float NetAttackAngle;

        public float ShotInterval;
        public List<float> ShotAngles;

        public int NShots;
        public float ShotSpreadAngle;

        public List<Vector2> ShotOffsets = new List<Vector2>();

        public List<ShotPath> ShotPaths;
        public override IEnumerator GenerateShotPattern()
        {
            yield return new Wait(AttackDelay);
            while (true)
            {
                foreach(var shotAngle in ShotAngles)
                {
                    for (int i = 0; i < NShots; ++i)
                    {
                        ShotPath shotPath = null;
                        if (ShotPaths != null && ShotPaths.Count > 0)
                        {
                            shotPath = ShotPaths[i % ShotPaths.Count];
                        }
                        Vector2 shotOffset = Vector2.zero;
                        if(ShotOffsets.Count > 0)
                        {
                            shotOffset = ShotOffsets[i % ShotOffsets.Count];
                        }
                        float theta =
                            -ShotSpreadAngle / 2
                            + (float)i / (NShots - 1)
                            * ShotSpreadAngle;
                        yield return new ShotProperties
                        {
                            Sprite = Sprite,
                            Hitbox = Hitbox,
                            Angle = NetAttackAngle + theta + shotAngle,
                            Path = shotPath,
                            Offset = shotOffset,
                        };
                    }
                    yield return new Wait(ShotInterval);

                }
                yield return new Wait(AttackInterval);
            }
        }
    }
}
