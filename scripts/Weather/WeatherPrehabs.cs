using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MG.Utils;
using System.Linq;

public enum WeatherEnum
{
    Thunder,
    Fire,
    Tornado,
    Icicle
}
namespace MG.Weathers{
    
    public class WeatherPrehabs : SingletonMonoBehaviour<WeatherPrehabs> {

        [SerializeField] List<BaseWeather> weatherPrehabs;
        private Dictionary<WeatherEnum, BaseWeather> weatherDictionary = new Dictionary<WeatherEnum, BaseWeather>();

        private void Start()
        {
            var weatherList = new WeatherEnum[] { WeatherEnum.Thunder, WeatherEnum.Fire, WeatherEnum.Tornado, WeatherEnum.Icicle};
            for (int i = 0; i < Mathf.Min(weatherPrehabs.Count(), weatherList.Count()); i++) weatherDictionary.Add(weatherList[i], weatherPrehabs[i]);
        }

        public BaseWeather GetWeatherPrehabs(WeatherEnum weatherEnum)
        {
            return weatherDictionary[weatherEnum];
        }
    }
}

