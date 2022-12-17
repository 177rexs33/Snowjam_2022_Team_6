using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SJ22
{
    [CreateAssetMenu(fileName = "NormalShotPath", menuName = "Enemy/Path/Normal", order = 0)]
    public class NormalShotPath : ShotPath
    {
        public enum PathType
        {
            Straight,
            Sin,
        };
        [SerializeField] PathType pathType;

        public override Vector2 GetPosition(float t)
        {
            switch (pathType)
            {
                case PathType.Straight:
                    return new Vector2(t, 0);
                case PathType.Sin:
                    return new Vector2(t, Mathf.Sin(t));
                default:
                    throw new NotImplementedException();
            }
        }
    }
}
