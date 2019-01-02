using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Test
{
    public class ArchWeapon : MonoBehaviour
    {
        public event Action<bool> OnFastSpeedChanged;
        
        [SerializeField]
        private float shootDelay = 0.5f;
        [SerializeField]
        private float shotPower = 7f;
        [SerializeField]
        private int damage = 1;
        [SerializeField]
        private Animator anim;

        private bool IsShoot
        {
            get { return anim.GetBool("shoot"); }
            set
            { anim.SetBool("shoot", value); }
        }

        private float time1;
        private float time2;
        private bool requireShoot = true;
        private bool power = false;

        #region Unity lifecycle

        void Update()
        {
            if (Input.GetMouseButtonDown(0) && requireShoot)
            {
                OnFastSpeedChanged(false);

                time1 = (float)DateTime.Now.Second + (float)DateTime.Now.Millisecond / (float)1000;
                power = true;

                IsShoot = true;
            }

            if (Input.GetMouseButtonUp(0) && power)
            {
                OnFastSpeedChanged(true);

                time2 = (float)DateTime.Now.Second + (float)DateTime.Now.Millisecond / (float)1000;
                if (time2 < time1)
                {
                    time2 += 60;
                }
                float charge = time2 - time1;
                charge = Mathf.Clamp(charge, 0.5f, 2); // ОГРАНИЧЕНИЕ ПО ВРЕМЕНИ ЗАРЯДА (СЕК)
                charge *= shotPower;

                Shoot(charge, damage);
                StartCoroutine(ShootDelay());

                power = false;
                IsShoot = false;
            }
        }

        #endregion

        private IEnumerator ShootDelay()
        {
            requireShoot = false;

            yield return new WaitForSeconds(shootDelay);

            requireShoot = true;
        }
    }
}