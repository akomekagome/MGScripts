using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MG.Utils;

public enum MagicEnum{
    Blizzard,
    Fire,
}

namespace MG.Magics{
    
    public class MagicPrehabs : SingletonMonoBehaviour<MagicPrehabs>
    {
        
        [SerializeField] private BaseMagic[] magics;


        public BaseMagic GetMagic(MagicEnum magic){
            if (magic == MagicEnum.Blizzard) return magics[0];
            else if (magic == MagicEnum.Fire) return magics[1];
            return null;
        }
    }
}