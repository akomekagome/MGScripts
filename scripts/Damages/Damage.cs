using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MG.Damages{
    
    public struct Damage {

        public int DamageValue { get; private set; }
        public IAttacker Attacker { get; private set; }

        public Damage(int damageValue, IAttacker attacker){
            this.DamageValue = damageValue;
            this.Attacker = attacker;
        }
    }
}
