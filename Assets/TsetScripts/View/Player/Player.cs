using UnityEngine;

namespace Test
{
    public class Player : MonoBehaviour
    {
        public static Player instance = null;

        public Vector3 Position { get { return cashTransform.localPosition; } }
        public bool isLookingToTheRight
        {
            get { return cashTransform.localScale.x > 0; }
            private set
            {
                if (value != isLookingToTheRight)
                {
                    Vector3 newScale = transform.localScale;
                    newScale.x *= MathConstants.NEGATIVE_COEFFICIENT;
                    transform.localScale = newScale;
                }
            }
        }
        private int AnimatorState
        {
            get { return animator.GetInteger("state"); }
            set { animator.SetInteger("state", value); }
        }

        [SerializeField]
        private Transform cashTransform;
        [SerializeField]
        private Animator animator;
        [SerializeField]
        private Rigidbody2D rb;
        [SerializeField]
        private float radiusInteraction = 2f;
        [SerializeField]
        private float runSpeed = 5f;
        [SerializeField]
        private float walkSpeed = 2.5f;
        [SerializeField]
        private float jumpPower = 10f;
        [SerializeField]
        int getDamagePower = 5;

        private bool isGrounded = true;
        private AnimationState _animatorState = 0;
        private Vector3 getDamageForce = new Vector3(0.1f, 1, 0);


        #region Unity lifecycle methods

        private void Awake()
        {
            #region Singleton

            if (instance == null)
            {
                instance = this;
            }
            else
            {
                Destroy(gameObject);
            }

            #endregion

            gameObject.AddComponent<PlayerWeapon>();
        }

        private void Start()
        {
            MissionManager.OnLevelLoaded += Player_OnLevelLoaded;
        }

        private void OnDestroy()
        {
            MissionManager.OnLevelLoaded -= Player_OnLevelLoaded;
        }

        private void FixedUpdate()
        {
            /*if (Input.GetKeyUp(KeyCode.W))
            {
                Collider2D[] colliders = Physics2D.OverlapCircleAll(cashTransform.position, radiusInteraction);

                for (int i = 0; i < colliders.Length; i++)
                {
                    colliders[i].gameObject.SendMessage("Operate", SendMessageOptions.DontRequireReceiver);
                }
            }*/

            Vector3 globalMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3 mousePosition = globalMousePosition - cashTransform.position;

            if (mousePosition.x < 0)
            {
                isLookingToTheRight = false;
            }
            else
            {
                isLookingToTheRight = true;
            }


            // ДВИЖЕНИЕ И АНИМАЦИЯ

            SetGrounded();
            if (isGrounded)
            {
                _animatorState = AnimationState.Idle;
            }

            if (Input.GetButton("Horizontal"))
            {
                bool isRun = PlayerWeapon.IsRunFast || !isGrounded;
                Run(isRun);

                if (isGrounded)
                    //  if (!FlipX)
                    //      if (fastSpeed)
                    _animatorState = AnimationState.Run;
                //      else
                //      	AnimatorState = (int)AnimationState.Walk;
                //  else
                //      if (fastSpeed)
                //      	AnimatorState = (int)AnimationState.RunBack;
                //      else
                //	        AnimatorState = (int)AnimationState.WalkBack;
            }
            if (Input.GetButtonDown("Jump") && isGrounded)
            {
                Jump();
                _animatorState = AnimationState.Jump;
            }
            if (!isGrounded)
            {
                _animatorState = AnimationState.Jump;
            }

            AnimatorState = (int)_animatorState;
        }

        #endregion

        /*public void OnHit(MessageParameters parameters)
        {
            GameController.ChangeHealth(parameters.damage);

            GetDamage(parameters.direction);
        }*/

        #region Private methods

        private void SetGrounded()
        {
            isGrounded = false;

            Vector3 position = cashTransform.position;

            position.y -= 1f;
            Collider2D[] colliders = Physics2D.OverlapCircleAll(position, 0.1f);

            if (colliders.Length >= 1)
            {
                isGrounded = true;
            }
        }

        private void Run(bool isRun)
        {
            Vector3 currentPosition = cashTransform.position;
            Vector3 direction = cashTransform.right * Input.GetAxis("Horizontal");

            float speed = isRun ? runSpeed : walkSpeed;
            cashTransform.position = Vector2.MoveTowards(currentPosition, currentPosition + direction,
                                                     speed * Time.deltaTime);
        }

        private void Jump()
        {
            Vector3 force = cashTransform.up * jumpPower;
            rb.AddForce(force, ForceMode2D.Impulse);
        }

        /*private void GetDamage(int direction)
        {
            getDamageForce.x *= direction;
            if (rb != null)
            {
                rb.velocity = Vector3.zero;
                rb.AddForce(getDamagePower * getDamageForce, ForceMode2D.Impulse);
            }
        }*/

        #endregion

        #region Event Handlers

        private void Player_OnLevelLoaded()
        {
            if (!gameObject.activeSelf)
            {
                gameObject.SetActive(true);
            }

            cashTransform.localPosition = Vector3.up * 2;
        }

        #endregion
    }
}