using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;
using System;
using MG.Damages;

namespace MG.Magics{
    
    public abstract class BaseMagic : MonoBehaviour {

        [SerializeField] private float usedMP = 3f;

        public float UsedMP{ get { return usedMP; } }

        protected IAttacker attacker;
        protected IObservable<bool> OnShootAsObservable;

        public void Init(IAttacker attacker, IObservable<bool> observable){
            this.attacker = attacker;
            this.OnShootAsObservable = observable;
        }

        protected ReactiveProperty<bool> hascastMagic = new ReactiveProperty<bool>(false);

        public IReadOnlyReactiveProperty<bool> HascastMagic{ get { return HascastMagic.ToReadOnlyReactiveProperty(); }}
    }
}
