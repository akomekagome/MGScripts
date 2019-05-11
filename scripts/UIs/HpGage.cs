using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;
using System;
using UnityEngine.UI;
using MG.Players;

namespace MG.UIs
{
    public class HpGage : MonoBehaviour
    {
        [SerializeField] private PlayerHealth health;
        [SerializeField] private Image hpGage;
        private const float fillProp = 0.75f;

        private void Start()
        {
            health.CurrentPlayerHealth
                .Subscribe(x => ChangeGage(x));
        }

        private void ChangeGage(int x)
        {
            hpGage.fillAmount = ((float)x / (float)health.PlayerMaxHealth) * fillProp;
        }
    }
}
