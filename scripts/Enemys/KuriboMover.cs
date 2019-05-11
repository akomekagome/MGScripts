using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;
using System;
using System.Linq;
using MG.Utils;

namespace MG.Enemys{
    
    public class KuriboMover : MonoBehaviour {
        
        private bool IsMoving;
        private Vector3 moveVector3 = Vector3.zero;

        private float RandomValue{ get { return UnityEngine.Random.Range(-1f, 1f); }}
        private CapsuleCollider capsuleCollider;
        private const float canMoveHight = 5f;
        private const float walkSpeed = 2f;
        private const int walkInterval = 6;
        private const int walkTime = 30;

        private void Start()
        {
            var kuribo = GetComponent<Kuribo>();
            var rb = GetComponent<Rigidbody>();
            capsuleCollider = GetComponent<CapsuleCollider>();

            kuribo.EnemyMoveable
                  .Skip(1)
                  .Where(x => !x)
                  .Do(_ => IsMoving = true)
                  .Delay(TimeSpan.FromSeconds(walkInterval))
                  .Subscribe(_ => IsMoving = false);

            this.FixedUpdateAsObservable()
                .Where(_ => !IsMoving && kuribo.EnemyMoveable.Value)
                .Subscribe(_ =>
                {
                    IsMoving = true;

                    do { moveVector3 = new Vector3(RandomValue, 0f, RandomValue).normalized; } while (moveVector3.magnitude == 0f);

                    Observable.EveryFixedUpdate()
                              .TakeWhile(_2 => MoveJudge(moveVector3) && kuribo.EnemyMoveable.Value)
                              .TakeUntil(Observable.Timer(TimeSpan.FromSeconds(walkTime)))
                              .Subscribe(_2 =>
                              {
                                  rb.velocity = moveVector3.SetY(rb.velocity.y);
                              }, () => Observable.Timer(TimeSpan.FromSeconds(walkInterval))
                                                 .TakeUntil(kuribo.EnemyMoveable.Where(x => !x))
                                                 .Subscribe(_3 => { IsMoving = false; })).AddTo(this);
                });


            //Observable.Timer(TimeSpan.FromSeconds(10000)).TakeWhile(_ => flag).Subscribe(_ => Debug.Log("onnest"), () => Debug.Log("fiish"));
            //ReactiveProperty<bool> flag = new BoolReactiveProperty(true);
            //Observable.Return(Unit.Default).TakeWhile(_ => flag.Value).Delay(TimeSpan.FromSeconds(10000)).Subscribe(_ => Debug.Log("onnest"), () => Debug.Log("fiish")).AddTo(this);
            //Observable.EveryUpdate().TakeUntil(flag.Where(x => !x)).Subscribe(_ => Debug.Log("onnest"), () => Debug.Log("fiish")).AddTo(this);
            //Observable.Timer(TimeSpan.FromSeconds(1000)).TakeWhile(_ => flag.Value).Subscribe(_ => Debug.Log("onnest"), () => Debug.Log("fiish")).AddTo(this);

            //Observable.Timer(TimeSpan.FromSeconds(2)).Subscribe(_ => { flag.Value = false; Debug.Log("value change"); });


        }

        private bool MoveJudge(Vector3 moveDirection)
        {
            RaycastHit hitinfo;
            //return Physics.SphereCast(transform.position + capsuleCollider.center + (moveDirection * walkTime * Time.deltaTime),
                                      //capsuleCollider.radius,
                                      // Vector3.down,
                                      // out hitinfo,
                                      //(capsuleCollider.height / 2) - capsuleCollider.radius + canMoveHight);
            return true;
        }
    }
}
