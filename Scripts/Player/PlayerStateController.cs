using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace PlayerCORE
{
    public enum PlayerState { Idle, Move, KnockDown, Attack, Dodge }
    public class PlayerStateController : MonoBehaviour
    {
        private Dictionary<PlayerState, Func<IEnumerator>> playerStates = new Dictionary<PlayerState, Func<IEnumerator>>();

        [SerializeField] private PlayerBasicData playerBasicData;
        [SerializeField] private Rigidbody rigidBody;
        [SerializeField] private Animator playerAnim;
        [SerializeField] private Transform groundChecker;
        [SerializeField, Range(0f, 10f)] private float smoothness;
        [SerializeField] private LayerMask groundLayerMask;

        private float controlledTime = 2.5f;
        private Camera mainCamera;

        private PlayerState playerState;
        private Coroutine coroutine;
        private void Awake()
        {
            playerStates.Add(PlayerState.Idle, Idle);
            playerStates.Add(PlayerState.Move, Move);
            playerStates.Add(PlayerState.KnockDown, KnockDown);
            playerStates.Add(PlayerState.Attack, Attack);
            playerStates.Add(PlayerState.Dodge, Dodge);

            ChangeState(PlayerState.Idle);
        }
        void Start()
        {
            StartCoroutine(InputMovement());
            StartCoroutine(CheckIsKnockedDown());
        }


        private void ChangeState(PlayerState newState)
        {
            if (coroutine != null)
            {
                StopCoroutine(coroutine);
                coroutine = null;
            }
            playerState = newState;

            if (coroutine == null)
                coroutine = StartCoroutine(playerStates[newState].Invoke());
        }
        private IEnumerator InputMovement()
        {
            while (true)
            {
                Vector2 moveInput = InputHelper.characterPosCoordinate;

                if (moveInput.magnitude != 0)
                {
                    if (IsReadyToMove())
                    {
                        ChangeState(PlayerState.Move);
                        yield return YieldCache.waitForEndOfFrame;
                    }
                }
                else if (moveInput.magnitude == 0 && IsReadyToMove())
                    ChangeState(PlayerState.Idle);

                yield return YieldCache.waitForEndOfFrame;
            }

            bool IsReadyToMove() => PlayerStateHelper.isAttacking == false && PlayerStateHelper.isKnockedDown == false && PlayerStateHelper.isDodging == false;
        }

        private IEnumerator CheckIsKnockedDown()
        {
            while (true)
            {
                if (PlayerStateHelper.isKnockedDown)
                {
                    ChangeState(PlayerState.KnockDown);
                    yield return new WaitUntil(() => PlayerStateHelper.isKnockedDown == false);
                }
                else
                    yield return YieldCache.waitForEndOfFrame;
            }
        }

        private IEnumerator Idle()
        {
            while (true)
            {
                if (PlayerStateHelper.isDodging)
                    ChangeState(PlayerState.Dodge);
                yield return null;
            }
        }
        private IEnumerator Move()
        {
            while (true)
            {
                if (PlayerStateHelper.isAttacking == true || PlayerStateHelper.isKnockedDown == true || PlayerStateHelper.isDodging == true)
                {
                    yield return YieldCache.waitForEndOfFrame;
                    continue;
                }

                Vector2 moveInput = InputHelper.characterPosCoordinate;
                bool isMove = moveInput.magnitude != 0;
                var animator = gameObject.GetComponent<Animator>();
                animator.SetFloat("moveSpeed", moveInput.magnitude);

                if (isMove)
                {
                    Vector3 lookForward = Normalization(mainCamera.transform.forward);
                    Vector3 lookRight = Normalization(mainCamera.transform.right);
                    Vector3 moveDir = lookForward * moveInput.normalized.y + lookRight * moveInput.normalized.x;

                    transform.forward = moveDir;
                    transform.position += moveDir * playerBasicData.runSpeed * Time.deltaTime;
                }

                if (moveInput.magnitude == 0)
                    ChangeState(PlayerState.Idle);

                yield return YieldCache.waitForEndOfFrame;
            }

            Vector3 Normalization(Vector3 transform) => Vector3.Scale(transform, new Vector3(1, 0, 1));
        }
        private IEnumerator KnockDown()
        {
            PlayerAnimationManager.Instance.RunPlayerKnockDownAnim();
            yield return YieldCache.GetCachedTimeInterval(controlledTime);
            PlayerStateHelper.isKnockedDown = false;
        }
    
        private IEnumerator Attack()
        {
            while (true)
            {
                yield return new WaitUntil(() => PlayerStateHelper.isAttacking == false);

                if(PlayerStateHelper.isAttacking == false)
                    ChangeState(PlayerState.Idle);
            }
        }

        private IEnumerator Dodge()
        {
            var curPosition = transform.position;
            var forward = transform.forward;
            var destination = forward * playerBasicData.dodgeDistance;
            float time = 0;
            while(time < 1)
            {
                time += Time.deltaTime * playerBasicData.dodgeSpeed;
                transform.position = Vector3.Lerp(curPosition, curPosition + destination, time);
                yield return YieldCache.waitForEndOfFrame;
            }
            PlayerStateHelper.isDodging = false;
            ChangeState(PlayerState.Idle);
        }


        public void SetMainCamera(Camera camera) => this.mainCamera = camera;
        public Camera GetMainCamera() => this.mainCamera;
        public void OnLeft() => SoundManager.Instance.PlayClip("Run_LeftF", transform);
        public void OnRight() => SoundManager.Instance.PlayClip("Run_RightF", transform);
    }
}
