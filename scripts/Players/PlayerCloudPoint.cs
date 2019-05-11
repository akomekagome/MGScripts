using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;
using System;
using MG.Utils;
using MG.Clouds;
using System.Linq;

namespace MG.Players{
    
    public class PlayerCloudPoint : MonoBehaviour {

        public ReactiveProperty<int> CurrentCloudPoint { get; private set; } = new ReactiveProperty<int>();
        public ReactiveProperty<bool> IsRecovering { get; private set; } = new ReactiveProperty<bool>();
        private const int recoveryValue = 2;
        private List<RecoveryCloud> hitRecoveryClouds = new List<RecoveryCloud>();
        private const int maxCloudPoint = 100;
        public int MaxCloudPoint { get { return maxCloudPoint; } }

        private void Start()
        {
            CurrentCloudPoint.Value = maxCloudPoint;

            //this.UpdateAsObservable()
                //.Subscribe(_ => Debug.Log(CurrentCloudPoint.Value));

            this.UpdateAsObservable()
                .Subscribe(_ => IsRecovering.Value = hitRecoveryClouds.Count() > 0);

            this.UpdateAsObservable()
                .Where(_ => IsRecovering.Value)
                .ThrottleFirst(TimeSpan.FromSeconds(0.1f))
                .Subscribe(_ =>
                {
                    ChangeCP(recoveryValue);
                });

            this.OnTriggerEnterAsObservable()
                .Select(x => x.GetComponent<RecoveryCloud>())
                .Where(x => x != null)
                .Subscribe(x =>
                {
                    if (!hitRecoveryClouds.Contains(x)) hitRecoveryClouds.Add(x);
                });

            this.OnTriggerExitAsObservable()
                .Select(x => x.GetComponent<RecoveryCloud>())
                .Where(x => x != null)
                .Subscribe(x =>
                {
                    if (hitRecoveryClouds.Contains(x)) hitRecoveryClouds.Remove(x);
                });
        }

        public void ChangeCP(int value){
            CurrentCloudPoint.Value = Mathf.Clamp(CurrentCloudPoint.Value + value, 0, maxCloudPoint);
        }
    }
}
