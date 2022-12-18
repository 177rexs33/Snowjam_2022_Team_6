using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SJ22
{
    public class PlayerShoot : MonoBehaviour

    {
        [SerializeField] GameInputReader inputReader;
        [SerializeField] GameObject bullet;
        [SerializeField] GameTime time;
        [SerializeField] GameObject shotContainer;

        [SerializeField] float shotIntreval = 0.5f;
        float nextTimeToShoot = 0f;
        private bool pressed;

        private void Awake()
        {
            inputReader.Gameplay.Shoot += pressed => this.pressed = pressed;
        }
        void Update()
        {
            if(pressed && nextTimeToShoot < time.Time)
            {
                Shoot();
                nextTimeToShoot = time.Time + shotIntreval;

            }

        }

        void Shoot()
        {
            var bulletGO = Instantiate(bullet);
            bulletGO.transform.parent = shotContainer.transform;
            bulletGO.transform.position = transform.position;
            
        }
    }
}
