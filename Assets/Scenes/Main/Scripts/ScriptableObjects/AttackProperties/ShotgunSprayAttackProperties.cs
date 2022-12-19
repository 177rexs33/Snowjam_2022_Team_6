using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SJ22
{
    [CreateAssetMenu(fileName = "ShotgunSprayAttack", menuName = "Enemy/Attack/ShotgunSpray", order = 0)]
    public class ShotgunSprayAttackProperties : AttackProperties
    {
        public Sprite Sprite;
        public Bounds Hitbox;

        public float ShotDelay;
        public float ShotInterval;
        public float ShotAngle;

        public List<float> BlastAngles;

        public int NShots;
        public float ShotSpreadAngle;

        public List<ShotPath> ShotPaths;
        public override IEnumerator GenerateShotPattern()
        {
            yield return new Wait(ShotDelay);
            while (true)
            {
                foreach(var blastAngle in BlastAngles)
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
                            Angle = ShotAngle + theta + blastAngle,
                            Path = shotPath,
                        };
                    }
                }
                yield return new Wait(ShotInterval);
            }
        }
    }
}
