using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MG.Players;

namespace MG.UIs
{
    public class WeatherRuseGage : MonoBehaviour
    {
        [SerializeField] private AttackCloudEnum attackCloudEnum;
        [SerializeField] private Image gage;
        public Image Gage { get { return gage; } }
        public AttackCloudEnum GetAttackCloudEnum { get { return attackCloudEnum; } }
    }
}
