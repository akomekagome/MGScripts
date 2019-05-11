using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;
using System;
using UnityEngine.UI;
using MG.Players;
using MG.Utils;

namespace MG.UIs
{
    public class CPGage : MonoBehaviour
    {
        [SerializeField] private PlayerCloudPoint cp;
        [SerializeField] private Slider cpGage;

        private void Start()
        {
            cp.CurrentCloudPoint
              .Subscribe(x => ChangeGage(x));
        }

        private void ChangeGage(int x)
        {
            cpGage.value = (float)x / (float)cp.MaxCloudPoint;
        }
    }
}
