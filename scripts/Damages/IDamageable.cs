using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MG.Damages{
    
    public interface IDamageable{

        void ApplyDamage(Damage damage);
    }
}
