using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;
using System;
using MG.Utils;
using System.Linq;

namespace MG.Players{
    
    public class PlayerEffect : MonoBehaviour
    {
        private List<SkinnedMeshRenderer> playerMeshList = new List<SkinnedMeshRenderer>();
        [SerializeField] private GameObject chargeEffect;
        private PlayerCloudPoint cloudPoint;

        void Start()
        {
            var health = GetComponent<PlayerHealth>();
            cloudPoint = GetComponent<PlayerCloudPoint>();

            playerMeshList = GetAllChildren.GetAll(this.gameObject)
                                           .Select(x => x.GetComponent<SkinnedMeshRenderer>())
                                           .Where(x => x != null)
                                           .ToList();

            health.ReceiveDamageObservable
                  .Subscribe(_ => Flash(health.InvincibleTime));

            cloudPoint.IsRecovering
                      .Where(x => x)
                      .Subscribe(_ => Charge());
        }

        private void Charge(){
            var charge = Instantiate(chargeEffect);
            charge.transform.SetParent(this.transform, false);
            charge.transform.position = this.transform.position;
            cloudPoint.IsRecovering
                      .Where(x => !x)
                      .Subscribe(_ => Destroy(charge));
        }

        private void Flash(float flashTime){
            Observable.EveryUpdate()
                      .TakeUntil(Observable.Timer(TimeSpan.FromSeconds(flashTime)))
                      .ThrottleFirst(TimeSpan.FromSeconds(0.1f))
                      .Subscribe(_ => { foreach (var mesh in playerMeshList) mesh.enabled = !mesh.enabled; },
                                 () => { foreach (var mesh in playerMeshList) mesh.enabled = true; });
        }
    }
}
