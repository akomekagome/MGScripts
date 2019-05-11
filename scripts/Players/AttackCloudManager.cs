using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MG.Clouds;
using UniRx;
using UniRx.Triggers;
using System;
using System.Linq;
using MG.Weathers;
public enum AttackCloudEnum
{
    ThnderCloud,
    FireCloud,
    TornadoCloud,
    IcicleCloud
}

namespace MG.Players{
    
    public class AttackCloudManager : MonoBehaviour {

        private PlayerCloudPoint playerCP;
        private bool canCreateCloud = true;
        private ReactiveProperty<AttackCloudEnum> currentAttackCloud = new ReactiveProperty<AttackCloudEnum>();
        [SerializeField] private AttackCloud[] attackClouds;
        [SerializeField] private BaseWeather[] weathers;
        private Dictionary<AttackCloudEnum, AttackCloud> AttackCloudDictionary = new Dictionary<AttackCloudEnum, AttackCloud>();
        private Dictionary<WeatherEnum, BaseWeather> WeatherDictionary = new Dictionary<WeatherEnum, BaseWeather>();
        public ReadOnlyReactiveProperty<AttackCloudEnum> CurrentAttackCloud { get { return currentAttackCloud.ToReadOnlyReactiveProperty(); } }
        private Subject<AttackCloudEnum> onUseAttackCloudSubject = new Subject<AttackCloudEnum>();
        public IObservable<AttackCloudEnum> OnUseAttackCloudObsrvable { get { return onUseAttackCloudSubject; } }
        [SerializeField] private Transform cloudPlace;

        private PlayerCore core;

        private void Start()
        {
            var input = GetComponent<IPlayerInput>();
            playerCP = GetComponent<PlayerCloudPoint>();
            core = GetComponent<PlayerCore>();
            //foreach(var cloud in attackClouds)
            //{
            //    AttackCloudDictionary.Add(cloud.GetAttackCloudEnum);
            //}

            input.OnCreateAttackCloudKeybordObservable
                 .ThrottleFirstFrame(0)
                 .Subscribe(x => ChangeAttackCloudKeybord(x));

            input.OnSelectAttackCloudObsrvable
                .Where(v => v.magnitude != 0f)
                .Subscribe(v => ChangeAttackCloudGamePad(v));

            input.OnCreateAttackCloudObservable
                .Where(x => x && canCreateCloud)
                .Subscribe(_ => CreateAttackCloud(CloudPrehabs.Instance.AttackCloudDicitionary2[currentAttackCloud.Value]));
        }

        private void ChangeAttackCloudKeybord(KeyCode key)
        {
            if (key == KeyCode.Alpha1) currentAttackCloud.Value = AttackCloudEnum.ThnderCloud;
            else if (key == KeyCode.Alpha2) currentAttackCloud.Value = AttackCloudEnum.FireCloud;
            else if (key == KeyCode.Alpha3) currentAttackCloud.Value = AttackCloudEnum.TornadoCloud;
            else if (key == KeyCode.Alpha4) currentAttackCloud.Value = AttackCloudEnum.IcicleCloud;
        }

        private void ChangeAttackCloudGamePad(Vector2 vec2)
        {
            if(Mathf.Abs(vec2.x) >= Mathf.Abs(vec2.y))
            {
                if(vec2.x >= 0f) currentAttackCloud.Value = AttackCloudEnum.IcicleCloud;
                else currentAttackCloud.Value = AttackCloudEnum.FireCloud;
            }
            else
            {
                if(vec2.y >= 0f) currentAttackCloud.Value = AttackCloudEnum.ThnderCloud;
                else currentAttackCloud.Value = AttackCloudEnum.TornadoCloud;
            }
        }

        private void CreateAttackCloud(GameObject cloud){
            var cloudObj = Instantiate(cloud);
            var attackCloud = cloudObj.GetComponent<AttackCloud>();
            if (attackCloud.UsedCp > playerCP.CurrentCloudPoint.Value) { Destroy(cloudObj); return; }
            else playerCP.ChangeCP(-attackCloud.UsedCp);
            onUseAttackCloudSubject.OnNext(attackCloud.GetAttackCloudEnum);
            StartCoroutine(ChangeWeather(5f));
            cloudObj.transform.SetParent(cloudPlace, false);
            cloudObj.transform.position = this.transform.position + new Vector3(0f, 4f, 0f);
            attackCloud.Init(core, transform.forward);
            attackCloud.OnDestroyObservable
                .Subscribe(_ => Destroy(cloudObj));
        }

        private IEnumerator ChangeWeather(float time)
        {
            canCreateCloud = false;
            yield return new WaitForSeconds(time);
            canCreateCloud = true;
        }
    }
}