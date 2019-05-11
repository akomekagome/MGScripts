using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MG.Damages;
using UniRx;
using UniRx.Triggers;
using System;

public enum EnemyEnum
{
    Bird
}

namespace MG.Enemys
{

    public class EnemyCore : MonoBehaviour, IAttacker, IDamageable
    {

        [SerializeField] private int _enemyNumber = 1;
        public int EnemyNumber { get { return _enemyNumber; } }

        public string ArrackerID { get { return "Enemy_" + EnemyNumber; } }
        [SerializeField] private MobTypeEnum _mobType;
        public MobTypeEnum AttackerMobType { get { return _mobType; } }

        [SerializeField] private EnemyEnum _enemyeum;
        public EnemyEnum GetEnemyEnum { get { return _enemyeum; } }

        ReactiveProperty<bool> playerAliveReactiveProperty = new BoolReactiveProperty(true);

        private Subject<Unit> _deadObservable = new Subject<Unit>();
        public IObservable<Unit> OnDeadObservable { get { return _deadObservable; } }

        private Subject<Damage> _damageObservable = new Subject<Damage>();
        public IObservable<Damage> DamageObservable { get { return _damageObservable; } }

        public void ApplyDamage(Damage damage)
        {
            _damageObservable.OnNext(damage);
        }

    }
}