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

        public float ShotDelay;
        public float BurstInterval;
        public float NetBurstAngle;

        public float ShotInterval;
        public List<float> ShotAngles;

        public int NShots;
        public float ShotSpreadAngle;

        public List<ShotPath> ShotPaths;
        public override IEnumerator GenerateShotPattern()
        {
            yield return new Wait(ShotDelay);
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
                        float theta =
                            -ShotSpreadAngle / 2
                            + (float)i / (NShots - 1)
                            * ShotSpreadAngle;
                        yield return new ShotProperties
                        {
                            Sprite = Sprite,
                            Hitbox = Hitbox,
                            Angle = NetBurstAngle + theta + shotAngle,
                            Path = shotPath,
                        };
                    }
                    yield return new Wait(ShotInterval);

                }
                yield return new Wait(BurstInterval);
            }
        }
    }
}
