using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using MG.Clouds;
using System.Linq;
using MG.Utils;

namespace MG.Players{
    
    public class CloudManager : MonoBehaviour{
        [SerializeField] private GameObject cloudPrehab;
        [SerializeField] private GameObject cloudPlace;
        private PlayerJumper jumper;
        [SerializeField] private float createJumpPower = 5.0f;

        //定数クラスにまとめる
        private int createCloudusedccp = 10;

        private CapsuleCollider cc;
        private PlayerCloudPoint playerCP;
        private List<Cloud> currentClouds = new List<Cloud>();
        private PlayerCore core;
        private Rigidbody rb;

        private void Start()
        {
            var input = GetComponent<IPlayerInput>();
            cc = GetComponent<CapsuleCollider>();
            core = GetComponent<PlayerCore>();
            playerCP = GetComponent<PlayerCloudPoint>();
            rb = GetComponent<Rigidbody>();
            jumper = GetComponent<PlayerJumper>();

            input?.OnCreateCloudButtonObservable
                  .Where(x => x)
                  .Subscribe(_ => CreateCloud());
        }

        private Vector3 GetInstatntPos(GameObject cloud){
            var cloudCollider = cloud.GetComponent<BoxCollider>();
            var currentPos = transform.position;
            currentPos.y -= (cc.height / 2) - cc.center.y + (cloud.transform.localScale.y * cloudCollider.size.y / 2);
            return currentPos;
        }
        
        private void CreateCloud(){
            if (playerCP.CurrentCloudPoint.Value < createCloudusedccp) return;
            else playerCP.ChangeCP(-createCloudusedccp);
            var cloudObj = Instantiate(cloudPrehab);
            var cloudInstance = cloudObj.GetComponent<Cloud>();
            while (currentClouds.Count() >= 3) { Destroy(currentClouds[0].gameObject); currentClouds.RemoveAt(0); }
            currentClouds.Add(cloudInstance);
            cloudObj.transform.SetParent(cloudPlace.transform, false);
            cloudObj.transform.position = GetInstatntPos(cloudObj) + rb.velocity.SetY(0f) * 0.2f;
            cloudInstance.OnDestroyObsevable
                         .FirstOrDefault()
                         .Subscribe(_ =>
                         {
                             currentClouds.Remove(cloudInstance);
                             Destroy(cloudObj);
                         });
        }

        private void CreateJump()
        {
            Observable.EveryFixedUpdate()
                .Take(1)
                .Subscribe(_ => rb.velocity = rb.velocity.AddSetY(createJumpPower));
        }
    }
}
