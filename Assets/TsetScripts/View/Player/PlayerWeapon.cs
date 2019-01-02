using UnityEngine;

namespace Test
{
    public class PlayerWeapon : MonoBehaviour
    {
        public static bool IsRunFast { get; private set; }

        private GameObject archWeapon;
        private GameObject meleeWeapon;
        private GameObject currentWeapon;

        #region Unity lifecycle

        private void Awake()
        {
            archWeapon = Resources.Load<GameObject>("ArchWeapon");
            meleeWeapon = Resources.Load<GameObject>("MeleeWeapon");
            currentWeapon = null;

            SetArch();
            IsRunFast = true;
            ArchTorso.FastSpeedChanged += WeaponFactory_OnFastSpeedChanged;
            MeleeTorso.FastSpeedChanged += WeaponFactory_OnFastSpeedChanged;
        }

        private void OnDestroy()
        {
            ArchTorso.FastSpeedChanged += WeaponFactory_OnFastSpeedChanged;
            MeleeTorso.FastSpeedChanged += WeaponFactory_OnFastSpeedChanged;
        }

        private void Update()
        {
            if (Input.GetKeyUp(KeyCode.Q))
            {
                SetArch();
            }
            else if (Input.GetKeyUp(KeyCode.E))
            {
                SetMelee();
            }
        }

        #endregion

        #region Event handlers

        private void WeaponFactory_OnFastSpeedChanged(bool isRunFast)
        {
            IsRunFast = isRunFast;
        }

        #endregion

        private void SetArch()
        {
            if (currentWeapon == null || !currentWeapon.CompareTag("arch"))
            {
                DestroyWeapon();

                currentWeapon = Instantiate(archWeapon) as GameObject;

                if (!Player.instance.isLookingToTheRight)
                {
                    currentWeapon.transform.localScale = new Vector3(-5, 5, 1);
                }
                else
                {
                    currentWeapon.transform.localScale = new Vector3(5, 5, 1);
                }
                currentWeapon.transform.parent = transform;

                currentWeapon.transform.localPosition = Vector3.zero;
            }
        }

        public void SetMelee()
        {
            if (currentWeapon == null || !currentWeapon.CompareTag("melee"))
            {
                DestroyWeapon();

                currentWeapon = Instantiate(meleeWeapon) as GameObject;

                if (!Player.instance.isLookingToTheRight)
                {
                    currentWeapon.transform.localScale = new Vector3(-5, 5, 1);
                }
                else
                {
                    currentWeapon.transform.localScale = new Vector3(5, 5, 1);
                }
                currentWeapon.transform.parent = transform;

                currentWeapon.transform.localPosition = Vector3.zero;
            }
        }

        private void DestroyWeapon()
        {
            if (currentWeapon != null)
                Destroy(currentWeapon);
        }
    }
}
