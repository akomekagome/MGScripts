using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UnityEngine.UI;
using System;
using MG.Utils;

namespace MG.UIs{
    
    public class FPSCounterPrehab : MonoBehaviour {
        
        private void Start()
        {
            FPSCounter.Current.Subscribe(x => Debug.Log(x));
        }
    }
}