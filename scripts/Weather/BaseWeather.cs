using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MG.Damages;
using UniRx;
using UniRx.Triggers;
using System;
using UnityEngine.UI;

namespace MG.Weathers{
    
    public abstract class BaseWeather : MonoBehaviour {

        protected IAttacker attacker;
        [SerializeField] private AudioClip sound;

        public void InitAttacker(IAttacker attacker){
            this.attacker = attacker;
        }

        private void Start()
        {
            this.OnTriggerEnterAsObservable()
                .Subscribe(hit =>
                {
                    Hit(hit.gameObject);
                });
        }

        protected abstract void Hit(GameObject hit);
    }
}

