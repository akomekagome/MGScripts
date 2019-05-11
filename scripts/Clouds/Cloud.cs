using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;
using UnityEngine.UI;
using System;
using MG.Players;

namespace MG.Clouds{
    
    public class Cloud : MonoBehaviour {
        [SerializeField] private AudioClip sound;
        private Subject<Unit> _destroyObservable = new Subject<Unit>();
        public IObservable<Unit> OnDestroyObsevable{ get { return _destroyObservable; }}
        
        void Start () {
            Observable.Timer(TimeSpan.FromSeconds(10f))
                      .Subscribe(_ => _destroyObservable.OnNext(Unit.Default))
                      .AddTo(this);
           
            //this.OnCollisionEnterAsObservable()
                //.Select(x => x.gameObject.GetComponent<PlayerCore>())
                //.Where(x => x != null)
                //.Subscribe(_ => Debug.Log("hoge"));
        }
    }
    
}