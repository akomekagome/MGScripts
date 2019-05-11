using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx.Triggers;
using UniRx;

public class PlayerCamera : MonoBehaviour
{

    [SerializeField] private GameObject cameraObj;
    private static readonly Vector3 cameraPos = new Vector3(0f, 5f, -7f);

    private void Start()
    {
        
        this.LateUpdateAsObservable()
            .Subscribe(_ => MoveCamera());
    }

    private void MoveCamera()
    {
        cameraObj.transform.position = transform.position + cameraPos;
    }    
}

