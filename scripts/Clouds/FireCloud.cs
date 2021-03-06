﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UniRx;
using UniRx.Triggers;

namespace MG.Clouds
{

    public class FireCloud : AttackCloud
    {

        private const int _usedCP = 10;
        public override int UsedCp { get { return _usedCP; } }
        private const int existTime = 10;
        private const int intervaltime = 1;
        private float speed = 2f;

        private void Start()
        {
            Observable.Timer(TimeSpan.FromSeconds(existTime))
                      .Subscribe(_ => base._destoryObservable.OnNext(Unit.Default));

            Move();

            CreateThunder();
        }

        private void Move()
        {
            transform.LookAt(transform.position + base.rotation);
            Observable.EveryUpdate()
                .Subscribe(_ => transform.position += transform.forward * speed * Time.deltaTime)
                .AddTo(this);
        }

        public void CreateThunder()
        {
            var weather = Instantiate(base.weatherPrehab);
            weather.transform.SetParent(transform, false);
            weather.transform.localPosition = Vector3.zero;
        }
    }
}