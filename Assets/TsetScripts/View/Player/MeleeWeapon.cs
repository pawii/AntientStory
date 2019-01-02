using UnityEngine;
using System;

namespace Test
{
    public class MeleeWeapon : MonoBehaviour
    {
        [SerializeField]
        private Animator anim;

        public event Action<bool> FastSpeedChanged;


        #region Unity lifecycle

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            { anim.SetTrigger("Attack"); }

            if (Input.GetMouseButtonDown(1))
            {
                anim.SetBool("Protect", true);
                FastSpeedChanged(false);
            }

            if (Input.GetMouseButtonUp(1))
            {
                anim.SetBool("Protect", false);
                FastSpeedChanged(true);
            }
        }

        #endregion
    }
}
