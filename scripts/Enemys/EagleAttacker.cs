using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;
using MG.Damages;
using System;

namespace MG.Enemys{
    
    public class EagleAttacker : MonoBehaviour {

        public ReactiveProperty<bool> IsAttacking = new BoolReactiveProperty(false);
        private const int attackValue = 3;
        private IAttacker myAttacker;
        private Subject<Unit> _onSuccessfulAttackSubject = new Subject<Unit>();

        private void Start()
        {
            var eagle = GetComponent<Eagle>();
            myAttacker = GetComponent<IAttacker>();

            eagle.OnAttackObservable
                 .Where(_ => !IsAttacking.Value)
                 .Subscribe(_ =>
                 {
                     IsAttacking.Value = true;
                 });

            this.OnCollisionEnterAsObservable()
                .Subscribe(x => Hit(x.gameObject));
        }

        private void Hit(GameObject hit)
        {
            var attacker = hit.GetComponent<IAttacker>();
            if (attacker == null || myAttacker.AttackerMobType == attacker.AttackerMobType) return;

            var damageable = hit.GetComponent<IDamageable>();
            if (damageable != null)
            {
                damageable.ApplyDamage(new Damage(attackValue, myAttacker));
                _onSuccessfulAttackSubject.OnNext(Unit.Default);
            }
        }
    }
}
