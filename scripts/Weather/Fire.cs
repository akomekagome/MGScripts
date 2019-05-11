using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;
using MG.Damages;

namespace MG.Weathers
{

    public class Fire : BaseWeather
    {

        private const int damageValue = 10;

        protected override void Hit(GameObject hit)
        {
            var hitattacker = hit.GetComponent<IAttacker>();
            if (hitattacker == null || hitattacker.AttackerMobType == base.attacker.AttackerMobType) return;
            var damageable = hit.GetComponent<IDamageable>();
            if (damageable != null)
            {
                damageable?.ApplyDamage(new Damage(damageValue, base.attacker));
            }
        }
    }
}