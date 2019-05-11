using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;
using UnityEngine.UI;
using System;
using MG.Players;

namespace MG.UIs
{
    public class WeatherRuseManager : MonoBehaviour
    {
        [SerializeField] private AttackCloudManager attackCloudManager;
        [SerializeField] private WeatherRuseGage[] attackCloudUIs;
        private AttackCloudEnum currentAttackCloud;
        private Dictionary<AttackCloudEnum, WeatherRuseGage> WeatherRuseGageDictionary = new Dictionary<AttackCloudEnum, WeatherRuseGage>();
        private float totalTime;

        private void Start()
        {
            foreach(var gage in attackCloudUIs)
            {
                WeatherRuseGageDictionary.Add(gage.GetAttackCloudEnum, gage);
                gage.gameObject.SetActive(false);
            }

            totalTime = 5f;

            attackCloudManager.CurrentAttackCloud
                .Subscribe(x => ChangeGage(x));

            attackCloudManager.OnUseAttackCloudObsrvable
                .Subscribe(_ => StartCoroutine(ChangeGage(5f)));
        }

        private void ChangeGage(AttackCloudEnum attackCloud)
        {
            if (currentAttackCloud != attackCloud) WeatherRuseGageDictionary[currentAttackCloud].gameObject.SetActive(false);
            currentAttackCloud = attackCloud;
            WeatherRuseGageDictionary[currentAttackCloud].gameObject.SetActive(true);
            ChangeWeatherGage(5f);
        }

        private void ChangeWeatherGage(float max)
        {
            WeatherRuseGageDictionary[currentAttackCloud].Gage.fillAmount = (max - totalTime) / max;
        }

        private IEnumerator ChangeGage(float canRuseTime)
        {
            totalTime = 0f;
            do
            {
                yield return null;
                totalTime += Time.deltaTime;
                totalTime = Mathf.Min(totalTime, canRuseTime);
                ChangeWeatherGage(canRuseTime);
            } while (totalTime < canRuseTime);
        }
    }
}
