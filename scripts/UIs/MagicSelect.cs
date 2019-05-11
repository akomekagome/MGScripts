using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;
using System;

namespace MG.Magics{

    public class MagicSelect : MonoBehaviour
    {

        //private ReactiveCollection<MagicEnum> UseMagic = new ReactiveCollection<MagicEnum>();

        //public IReadOnlyReactiveCollection<MagicEnum> UseMagic { get { return SelectedMagic; } }

        private ReactiveProperty<MagicEnum>[] selectedMagics = new ReactiveProperty<MagicEnum>[4];

        //public IObservable<MagicEnum>[] SelectedMagic { get { return selectedMagic; } }

        private void Awake()
        {
            for (int i = 0; i < selectedMagics.Length; i++)
                selectedMagics[i] = new ReactiveProperty<MagicEnum>();
        }

        private void Start()
        {
            selectedMagics[0].Value = MagicEnum.Blizzard;
        }
    }

}