using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SJ22
{
    [CreateAssetMenu(fileName = "NormalShotPath", menuName = "Enemy/Shot Path/Normal", order = 0)]
    public class NormalShotPath : ShotPath
    {
        public enum PathType
        {
            Straight,
            Sin,
        };
        [SerializeField] PathType pathType;

        public override Vector2 F(float t)
        {
            switch (pathType)
            {
                case PathType.Straight:
                    return new Vector2(0, -t);
                case PathType.Sin:
                    return new Vector2(Mathf.Sin(t), -t);
                default:
                    throw new NotImplementedException();
            }
        }
    }
}
