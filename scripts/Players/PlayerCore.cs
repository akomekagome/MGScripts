using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MG.Damages;
using System;
using UniRx;
using MG.GameManagers;

namespace MG.Players{
    
    public class PlayerCore : MonoBehaviour, IDamageable, IAttacker{

        [SerializeField] private int _playerID;

        [SerializeField] private MobTypeEnum _mobType;
        public MobTypeEnum AttackerMobType{ get { return _mobType; }}

        /// <summary>
        /// プレイヤ識別子
        /// </summary>
        public int PlayerID{ get { return _playerID; }}

        private Rigidbody rb;
        private Subject<Damage> _damageObservable = new Subject<Damage>();
        public IObservable<Damage> DamageObservable{ get { return _damageObservable; }}

        /// <summary>
        /// プレイヤの名前
        /// </summary>
        public string PlayerName{ get { return PlayerID + "P"; }}

        public string ArrackerID { get { return "Player_" + PlayerID; } }

        public void SetPlayerID(int id){
            this._playerID = id;
        }

        ReactiveProperty<bool> playerAliveReactiveProperty = new BoolReactiveProperty(true);

        public bool SetPlayerAliveReactiveProperty{ set { playerAliveReactiveProperty.Value = value; }}


        /// <summary>
        /// プレイヤが死亡したことを通知する
        /// </summary>
        public IObservable<int> OnPlayerDeadAsObservable
        {
            get { return playerAliveReactiveProperty.Where(x => !x).Select(_ => PlayerID); }
        }

        private void Awake()
        {
            rb = this.GetComponent<Rigidbody>();
            PlayerManager.Instance.SetPlayer(this);
        }
        
        public void ApplyDamage(Damage damage){
            _damageObservable.OnNext(damage);
        }
    }
    
}