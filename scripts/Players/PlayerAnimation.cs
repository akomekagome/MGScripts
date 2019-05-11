using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;

namespace MG.Players{

    public class PlayerAnimation : MonoBehaviour
    {
        private Animator animator;
        private bool IsRunning { set { animator?.SetBool("IsRunning", value); }}
        private float rotateSpeed = 12f;
        private PlayerJumper jumper;

        private IObservable<Unit> OnJumpEndObsrvable
        {
            get
            {
                return jumper.IsJumpingObservable
                    .Where(x => !x)
                    .AsUnitObservable();
            }
        }

        private void Start()
        {
            var rb = GetComponent<Rigidbody>();
            var core = GetComponent<PlayerCore>();
            var input = GetComponent<IPlayerInput>();
            animator = GetComponent<Animator>();
            jumper = GetComponent<PlayerJumper>();

            input.OnMoveDirectionObservable
                .TakeUntil(core.OnPlayerDeadAsObservable)
                .Subscribe(v =>
                {
                    IsRunning = v.magnitude != 0f;
                    transform.rotation = Quaternion.LookRotation(Vector3.RotateTowards(transform.forward,
                                                                                    v,
                                                                                    rotateSpeed * Time.deltaTime,
                                                                                    0f));
                });

            jumper.IsJumpingObservable
                .Where(x => x)
                .Subscribe(_ =>
                {
                    animator.SetBool("IsLanding", false);
                    Observable.NextFrame()
                    .Subscribe(_2 => {
                        if(rb.velocity.y > 0.01f)
                        {
                            animator.Play("JUMP_UP");
                            rb.ObserveEveryValueChanged(r => r.velocity)
                            .TakeUntil(OnJumpEndObsrvable)
                            .Where(v => v.y <= 0f)
                            .Take(1)
                            .Subscribe(_3 => animator.SetTrigger("IsJumpingDown"));
                        }
                        else
                        {
                            animator.Play("JUMP_DOWN");
                        }
                    });
                    OnJumpEndObsrvable.Subscribe(_2 => animator.SetBool("IsLanding", true));
                });
        }
    }
}