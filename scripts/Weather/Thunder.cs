using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;
using MG.Damages;
using System;

namespace MG.Weathers{
    
    public class Thunder : BaseWeather {

        private const int damageValue = 10;
        private const float existTime = 0.3f;

        private void Start()
        {
            Observable.Timer(TimeSpan.FromSeconds(existTime))
                      .Subscribe(_ => Destroy(this));
        }

        protected override void Hit(GameObject hit)
        {
            var hitattacker = hit.GetComponent<IAttacker>();
            if (hitattacker == null || hitattacker.AttackerMobType == base.attacker.AttackerMobType) return;
            var damageable = hit.GetComponent<IDamageable>();
            if(damageable != null){
                damageable?.ApplyDamage(new Damage(damageValue, base.attacker));
            }
        }
    }
}