using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace MG.Players{
    
    public interface IPlayerInput{

        //IObservable<bool> OnOneButtonObservable { get; }
        //IObservable<bool> OnTwoButtonObservable { get; }
        //IObservable<bool> OnThreeButtonObservble { get; }
        //IObservable<bool> OnFourButtonObservable { get; }
        //IObservable<bool> OnFiveButtonObservable { get; }
        IObservable<Vector3> OnMoveDirectionObservable { get; }
        IObservable<Vector3> OnMoveCameraObservable { get; }
        IObservable<Vector2> OnSelectAttackCloudObsrvable { get; }
        IObservable<KeyCode> OnCreateAttackCloudKeybordObservable { get; }
        IObservable<bool> OnJumpButtonObseravable { get; }
        IObservable<bool> OnMagicButtonObserabable { get; }
        IObservable<bool> OnCreateCloudButtonObservable { get; }
        IObservable<bool> OnCreateAttackCloudObservable { get; }

        //IObservable<bool>[] OnNumKeysObserabable { get; }

    }
}