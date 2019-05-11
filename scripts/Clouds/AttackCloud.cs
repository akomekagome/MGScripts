using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;
using System;
using System.Linq;
using MG.Players;
using MG.Damages;
using MG.Weathers;
using MG.Utils;


namespace MG.Clouds{
    
    public abstract class AttackCloud : MonoBehaviour {

        protected Subject<Unit> _destoryObservable = new Subject<Unit>();
        public IObservable<Unit> OnDestroyObservable { get { return _destoryObservable; }}
        [SerializeField] private WeatherEnum weatherEnum;
        [SerializeField] private AttackCloudEnum attckCloudEnum;
        public AttackCloudEnum GetAttackCloudEnum { get { return attckCloudEnum; } }
        protected BaseWeather weatherPrehab;
        protected IAttacker attacker;
        protected Vector3 rotation;

        public void Init(IAttacker attacker, Vector3 vector3){
            this.rotation = vector3.SetY(0f);
            this.attacker = attacker;
            this.weatherPrehab = WeatherPrehabs.Instance.GetWeatherPrehabs(weatherEnum);
        }

        public abstract int UsedCp { get; }
    }
}
