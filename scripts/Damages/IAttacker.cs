using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MobTypeEnum{
    Player,
    Enemy
}

namespace MG.Damages{
    
    public interface IAttacker{
        string ArrackerID { get; }
        MobTypeEnum AttackerMobType { get; }
    }
    
}