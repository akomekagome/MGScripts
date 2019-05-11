using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;
using System;
using System.Linq;
using UnityEngine.UI;
using MG.Players;

namespace MG.UIs
{
    public class DamageGage : MonoBehaviour
    {
        [SerializeField] private PlayerHealth health;
        [SerializeField] private Image damageGage;
        private ReactiveProperty<int> _currentBeforeHP = new ReactiveProperty<int>();
        public IReactiveProperty<int> CurrentBeforeHp { get { return _currentBeforeHP; } }
        private Subject<float>  _damageBeforeHpSubject = new Subject<float>();
        public IObservable<float> DamageBeforeHpObservable { get { return _damageBeforeHpSubject; } }
        private const float fillProp = 0.75f;

        private void Start()
        {
            health.CurrentPlayerHealth
                .Pairwise()
                .Where(x => x.Current < x.Previous)
                .Subscribe(x => {
                    _damageBeforeHpSubject.OnNext((float)x.Previous);
                });

            DamageBeforeHpObservable
                .Subscribe(x =>
                {
                    var currentDamage = x;
                    var currentHealth = (float)health.CurrentPlayerHealth.Value;
                    ChangeDamageGage(x);
                    Observable.Interval(TimeSpan.FromSeconds(0.01f))
                    .TakeWhile(_ => currentDamage > currentHealth)
                    .Delay(TimeSpan.FromSeconds(0.2f))
                    .Subscribe(_ =>
                    {
                        currentDamage -= 0.5f;
                        ChangeDamageGage(currentDamage);
                    }).AddTo(this);
                });
        }

        private void ChangeDamageGage(float x)
        {
            damageGage.fillAmount = (x / (float)health.PlayerMaxHealth) * fillProp;
        }
    }
}
