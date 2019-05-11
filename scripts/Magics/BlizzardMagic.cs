using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;


namespace MG.Magics{
    
    public class BlizzardMagic : BaseMagic {


        private void OnDestroy()
        {
            base.hascastMagic.Value = false;
            base.hascastMagic.Dispose();
        }
    }
}
