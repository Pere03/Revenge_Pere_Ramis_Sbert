using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using System.Collections.Generic;

/* Note: animations are called via the controller for both the character and capsule using animator null checks
 */

namespace StarterAssets
{

    [RequireComponent(typeof(CharacterController))]
#if ENABLE_INPUT_SYSTEM && STARTER_ASSETS_PACKAGES_CHECKED
    [RequireComponent(typeof(PlayerInput))]
#endif
    public class ThirdPersonController : MonoBehaviour
    {
        [Header("Player")]
        public float MoveSpeed;
        public float SprintSpeed;
        public float RotationSmoothTime = 0.12f;
        public float SpeedChangeRate = 10.0f;
        public float JumpHeight = 1.2f;
        public float Gravity = -15.0f;
        public float JumpTimeout = 0.50f;
        public float FallTimeout = 0.15f;

        [Header("Player Grounded")]
        public bool Grounded = true;
        public float GroundedOffset = -0.14f;
        public float GroundedRadius = 0.28f;
        public LayerMask GroundLayers;

        //Player
        private float _speed;
        private float _animationBlend;
        private float _targetRotation = 0.0f;
        private float _rotationVelocity;
        private float _verticalVelocity;
        private float _terminalVelocity = 53.0f;
        private float _jumpTimeoutDelta;
        private float _fallTimeoutDelta;

        // animation IDs
        private int _animIDSpeed;
        private int _animIDGrounded;
        private int _animIDJump;
        private int _animIDFreeFall;
        private int _animIDMotionSpeed;

        private PlayerInput _playerInput;
        [SerializeField] private Animator _animator;
        private CharacterController _controller;
        private StarterAssetsInputs _input;
        private GameObject _mainCamera;

        private const float _threshold = 0.01f;
        private bool _hasAnimator;

        //Roll
        private bool _isRolling = false;
        [SerializeField] AnimationCurve dodgeCurve;
        private bool isDodging;
        private float dodgeTimer, dodge_coolDown;
        float velocityY;
        Vector3 direction;
        private Rigidbody _rig;
        private float ActCoolDown;
        [Header("Roll")]
        public float DodgeCoolDown = 1;
        public float PushAmt = 3;

        [Header("Abilities")]
        public bool Tornado_On;
        public Player_Abilities PAbilty;
        public float Normal_MoveSpeed = 2.5f;
        public float Normal_SprintSpeed = 6f;
        public float Tornado_MoveSpeed = 6f;
        public float Tornado_SprintSpeed = 10f;
        public CapsuleCollider Coll;

        [Header("Attack")]
        public Attack_Movement AM;
        public bool Attack_move;

        private void Awake()
        {
            if (_mainCamera == null)
            {
                _mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
            }
        }

        private void Start()
        {
            AM = GetComponent<Attack_Movement>();
            _animator = GetComponentInChildren<Animator>();
            _hasAnimator = TryGetComponent(out _animator);
            _controller = GetComponent<CharacterController>();
            _input = GetComponent<StarterAssetsInputs>();
            _playerInput = GetComponent<PlayerInput>();
            _rig = GetComponent<Rigidbody>();
            PAbilty = GetComponent<Player_Abilities>();
            Coll = GetComponent<CapsuleCollider>();

            AssignAnimationIDs();

            _jumpTimeoutDelta = JumpTimeout;
            _fallTimeoutDelta = FallTimeout;

            Keyframe dodge_lastFrame = dodgeCurve[dodgeCurve.length - 1];
            dodgeTimer = dodge_lastFrame.time;

            Coll.enabled = true;
        }

        private void Update()
        {
            Attack_move = AM.Attacking;
           _hasAnimator = TryGetComponent(out _animator);
            JumpAndGravity();
            GroundedCheck();
            Move();

            Tornado_On = PAbilty.Tornado_Abilty;

            if (Input.GetMouseButtonDown(1) && Tornado_On == false)
            {
                Dodge();
            }

            if(Tornado_On == false && Attack_move == false)
            {
                MoveSpeed = Normal_MoveSpeed;
                SprintSpeed = Normal_SprintSpeed;
                Coll.enabled = true;
            } 
            else if (Tornado_On == true)
            {
                MoveSpeed = Tornado_MoveSpeed;
                SprintSpeed = Tornado_SprintSpeed;
                Coll.enabled = false;
            }
        }
        

        void Dodge()
        {
           _rig.AddForce(transform.forward * PushAmt, ForceMode.Impulse);
           _animator.SetTrigger("Roll");
        }

        private void AssignAnimationIDs()
        {
            _animIDSpeed = Animator.StringToHash("Speed");
            _animIDGrounded = Animator.StringToHash("Grounded");
            _animIDJump = Animator.StringToHash("Jump");
            _animIDFreeFall = Animator.StringToHash("FreeFall");
            _animIDMotionSpeed = Animator.StringToHash("MotionSpeed");
        }

        private void GroundedCheck()
        {
            Vector3 spherePosition = new Vector3(transform.position.x, transform.position.y - GroundedOffset, transform.position.z);
            Grounded = Physics.CheckSphere(spherePosition, GroundedRadius, GroundLayers, QueryTriggerInteraction.Ignore);

            if (_hasAnimator)
            {
                _animator.SetBool(_animIDGrounded, Grounded);
            }
        }

        //With this we make it so that we can move the Player towards the direction we want, and in addition to being able to rotate the Player
        private void Move()
        {
            float targetSpeed = _input.sprint ? SprintSpeed : MoveSpeed;

            if (_input.move == Vector2.zero) targetSpeed = 0.0f;

            float currentHorizontalSpeed = new Vector3(_controller.velocity.x, 0.0f, _controller.velocity.z).magnitude;

            float speedOffset = 0.1f;
            float inputMagnitude = _input.analogMovement ? _input.move.magnitude : 1f;

            if (currentHorizontalSpeed < targetSpeed - speedOffset ||
                currentHorizontalSpeed > targetSpeed + speedOffset)
            {
                _speed = Mathf.Lerp(currentHorizontalSpeed, targetSpeed * inputMagnitude,
                    Time.deltaTime * SpeedChangeRate);

                _speed = Mathf.Round(_speed * 1000f) / 1000f;
            }
            else
            {
                _speed = targetSpeed;
            }

            _animationBlend = Mathf.Lerp(_animationBlend, targetSpeed, Time.deltaTime * SpeedChangeRate);

            if (_animationBlend < 0.01f) _animationBlend = 0f;

            Vector3 inputDirection = new Vector3(_input.move.x, 0.0f, _input.move.y).normalized;

            if (_input.move != Vector2.zero)
            {
                _targetRotation = Mathf.Atan2(inputDirection.x, inputDirection.z) * Mathf.Rad2Deg + _mainCamera.transform.eulerAngles.y;
                float rotation = Mathf.SmoothDampAngle(transform.eulerAngles.y, _targetRotation, ref _rotationVelocity, RotationSmoothTime);

                transform.rotation = Quaternion.Euler(0.0f, rotation, 0.0f);
            }


            Vector3 targetDirection = Quaternion.Euler(0.0f, _targetRotation, 0.0f) * Vector3.forward;

            _controller.Move(targetDirection.normalized * (_speed * Time.deltaTime) + new Vector3(0.0f, _verticalVelocity, 0.0f) * Time.deltaTime);

            if (_hasAnimator)
            {
                _animator.SetFloat(_animIDSpeed, _animationBlend);
                _animator.SetFloat(_animIDMotionSpeed, inputMagnitude);
            }
        }


        private void JumpAndGravity()
        {
            if (Tornado_On == false)
            {
                if (Grounded && Tornado_On == false)
                {
                    _fallTimeoutDelta = FallTimeout;

                    if (_hasAnimator)
                    {
                        _animator.SetBool(_animIDJump, false);
                        _animator.SetBool(_animIDFreeFall, false);
                    }

                    if (_verticalVelocity < 0.0f)
                    {
                        _verticalVelocity = -2f;
                    }

                    // Jump
                    if (_input.jump && _jumpTimeoutDelta <= 0.0f)
                    {
                        _verticalVelocity = Mathf.Sqrt(JumpHeight * -2f * Gravity);

                        if (_hasAnimator)
                        {
                            _animator.SetBool(_animIDJump, true);
                        }
                    }

                    // jump timeout
                    if (_jumpTimeoutDelta >= 0.0f)
                    {
                        _jumpTimeoutDelta -= Time.deltaTime;
                    }
                }
                else
                {
                    // reset the jump timeout timer
                    _jumpTimeoutDelta = JumpTimeout;

                    // fall timeout
                    if (_fallTimeoutDelta >= 0.0f)
                    {
                        _fallTimeoutDelta -= Time.deltaTime;
                    }
                    else
                    {
                        // update animator if using character
                        if (_hasAnimator)
                        {
                            _animator.SetBool(_animIDFreeFall, true);
                        }
                    }

                    // if we are not grounded, do not jump
                    _input.jump = false;
                }

                if (_verticalVelocity < _terminalVelocity)
                {
                    _verticalVelocity += Gravity * Time.deltaTime;
                }
            }
            
        }

        private static float ClampAngle(float lfAngle, float lfMin, float lfMax)
        {
            if (lfAngle < -360f) lfAngle += 360f;
            if (lfAngle > 360f) lfAngle -= 360f;
            return Mathf.Clamp(lfAngle, lfMin, lfMax);
        }

        private void OnDrawGizmosSelected()
        {
            Color transparentGreen = new Color(0.0f, 1.0f, 0.0f, 0.35f);
            Color transparentRed = new Color(1.0f, 0.0f, 0.0f, 0.35f);

            if (Grounded) Gizmos.color = transparentGreen;
            else Gizmos.color = transparentRed;

            Gizmos.DrawSphere( new Vector3(transform.position.x, transform.position.y - GroundedOffset, transform.position.z), GroundedRadius);
        }

        private void OnFootstep(AnimationEvent animationEvent)
        {
            if (animationEvent.animatorClipInfo.weight > 0.5f)
            {
                
            }
        }
        private void OnLand(AnimationEvent animationEvent)
        {
            
        }
    }
}