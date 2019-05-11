using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;
using System;
using MG.Damages;
using MG.Utils;

namespace MG.Enemys{
    
    public class KuriboAttacker : MonoBehaviour {
        
        private const float attackMoveSpeed = 4;
        private Vector3 moveVector3 = Vector3.zero;
        private const int attackValue = 25;
        private const int attackInteraval = 5;
        private IAttacker myAttacker;
        private Subject<Unit> _onSuccessfulAttackSubject = new Subject<Unit>();
        private IObservable<Unit> OnSuccessfulAttackObservable{ get { return _onSuccessfulAttackSubject; }}

        private void Start()
        {
            var kuribo = GetComponent<Kuribo>();
            var rb = GetComponent<Rigidbody>();
            myAttacker = GetComponent<IAttacker>();

            kuribo.OnAttackObservable
                  .Subscribe(t =>
                  {
                      Observable.EveryFixedUpdate()
                                .TakeUntil(Observable.Amb(
                                    Observable.Timer(TimeSpan.FromSeconds(4)).AsUnitObservable(),
                                    OnSuccessfulAttackObservable))
                                .Subscribe(_ =>
                                {
                                    moveVector3 = (t.position - transform.position).SetY(0f).normalized * attackMoveSpeed;
                                    rb.velocity = moveVector3.SetY(rb.velocity.y);
                                }, () =>
                                {
                                    kuribo.SetEnemyMoveable(false);
                                    Observable.Timer(TimeSpan.FromSeconds(attackInteraval))
                                              .Subscribe(_ => kuribo.SetEnemyMoveable(true));
                                });
                  });

            this.OnCollisionEnterAsObservable()
                .Subscribe(hit => Hit(hit.gameObject));
        }

        private void Hit(GameObject hit){
            var attacker = hit.GetComponent<IAttacker>();
            if (attacker == null || myAttacker.AttackerMobType == attacker.AttackerMobType) return;

            var damageable = hit.GetComponent<IDamageable>();
            if(damageable != null){
                damageable.ApplyDamage(new Damage(attackValue, myAttacker));
                _onSuccessfulAttackSubject.OnNext(Unit.Default);
            }
        }
    }
}