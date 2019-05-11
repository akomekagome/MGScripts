using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;
using System;
using MG.Utils;
using System.Linq;
using MG.Clouds;

namespace MG.Players{
    
    public class PlayerHealth : MonoBehaviour {

        public ReactiveProperty<int> CurrentPlayerHealth { get; private set; } = new ReactiveProperty<int>();
        private const int playerMaxHealth = 100;
        public int PlayerMaxHealth { get { return playerMaxHealth; } }
        private const float _invincibleTime = 4f;
        private const int recoveryValue = 2;
        public float InvincibleTime{ get { return _invincibleTime; }}
        private bool canSufferDamage = true;

        public IObservable<Unit> ReceiveDamageObservable
        {
            get
            {
                return CurrentPlayerHealth.Pairwise()
                                          .Where(x => x.Current < x.Previous)
                                          .AsUnitObservable();
            }
        }

        private void ChangeHealth(int changeValue)
        {
            CurrentPlayerHealth.Value = Mathf.Clamp(CurrentPlayerHealth.Value + changeValue, 0, playerMaxHealth);
        }


        private void Start()
        {
            var core = GetComponent<PlayerCore>();

            CurrentPlayerHealth.Value = playerMaxHealth;

            ReceiveDamageObservable.Do(_ => canSufferDamage = false)
                                   .Delay(TimeSpan.FromSeconds(InvincibleTime))
                                   .Subscribe(_ => canSufferDamage = true);

            //this.UpdateAsObservable()
                //.Subscribe(_ => Debug.Log(CurrentPlayerHealth.Value));
            
            core.DamageObservable
                .Where(_ => canSufferDamage)
                .Subscribe(x => ChangeHealth(-x.DamageValue));
            
            CurrentPlayerHealth.Where(x => x <= 0)
                               .FirstOrDefault()
                               .Subscribe(_ => core.SetPlayerAliveReactiveProperty = false);
        }
    }
}