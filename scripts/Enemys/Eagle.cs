using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;
using System.Linq;
using System;
using MG.Players;
using MG.GameManagers;

namespace MG.Enemys{
    
    public class Eagle : MonoBehaviour {
        
        private List<PlayerCore> currentPlayerList = new List<PlayerCore>();
        private const float moveDistance = 5f;
        private Subject<Transform> _onAttackSubject = new Subject<Transform>();
        public IObservable<Transform> OnAttackObservable { get { return _onAttackSubject; } }

        private void Start()
        {
            currentPlayerList = PlayerManager.Instance.GetAlivePlayers();
            
            foreach (var player in currentPlayerList)
            {
                player.gameObject.transform.ObserveEveryValueChanged(x => x.position)
                      .Where(v => (v - transform.position).magnitude <= moveDistance)
                      .Subscribe(_ => _onAttackSubject.OnNext(player.gameObject.transform));
            }
        }
    }
}