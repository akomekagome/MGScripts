using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;

namespace MG.Enemys{
    
    public class EnemyHealth : MonoBehaviour {
        
        private ReactiveProperty<int> _enemyHealthObservable = new ReactiveProperty<int>();
        public IReadOnlyReactiveProperty<int> CurrentEnemyHealth { get { return _enemyHealthObservable; }}
        
        private void ChangeHealth(int value){
            _enemyHealthObservable.Value += value;
        }

        private void Start()
        {
            //var baseEnemy = GetComponent<BaseEnemy>();

            //baseEnemy.DamageObservable
                     //.Subscribe(x => ChangeHealth(-x.DamageValue));
        }
    }
}