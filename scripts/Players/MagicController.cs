using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;
using System.Linq;
using System;
using MG.Magics;

namespace MG.Players{

    public class MagicController : MonoBehaviour
    {

        private IObservable<bool>[] attackObservable = new IObservable<bool>[5];
        private BaseMagic[] currentMagics = new BaseMagic[2];
        private Vector3 instantLocalPos = Vector3.zero;
        [SerializeField] private GameObject magics;
        private bool canDoMagic = true;
        private PlayerCore core;

        private void Start()
        {
            var input = GetComponent<IPlayerInput>();
            core = GetComponent<PlayerCore>();
            var select = FindObjectOfType<MagicSelect>();

            //for (int i = 0; i < input.OnNumKeysObserabable.Length; i++)
                //attackObservable[i] = input?.OnNumKeysObserabable[i]
                      //.TakeUntil(core.OnPlayerDeadAsObservable)
                      //.Where(x => x && canDoMagic);
            
            
            for(int i = 0; i < 2; i++)
            {
                int f = i;
                //select?.SelectedMagics[f]
                      //.Skip(1)
                      //.TakeUntil(core.OnPlayerDeadAsObservable)
                      //.Subscribe(x => InitMagic(MagicPrehabs.Instance.GetMagic(x), f));
            }
        }

        private void InitMagic(BaseMagic magicprehab, int index){
            //クラスを消すため依存に注意
            if (currentMagics[index] != null) Destroy(currentMagics[index]);
            var magic = Instantiate(magicprehab, Vector3.zero, Quaternion.identity);
            magic.Init(core, attackObservable[index]);
            magic.transform.SetParent(magics.transform, false);
            magic.transform.localPosition = instantLocalPos;
            magic.HascastMagic.Skip(1).Subscribe(x => canDoMagic = !x);
            currentMagics[index] = magic;
        }
    }
} 