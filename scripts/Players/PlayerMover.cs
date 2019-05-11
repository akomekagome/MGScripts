using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;
using DG.Tweening;
using UnityEngine.UI;
using MG.Utils;
using System.Linq;
using MG.Clouds;

namespace MG.Players{
    
    public class PlayerMover : MonoBehaviour {
        

        //private void Awake()
        //{
        //    rb = GetComponent<Rigidbody>();
        //    var core = GetComponent<PlayerCore>();

        //    GetComponent<IPlayerInput>()?.OnMoveDirectionObservable
        //                                 .TakeUntil(core.OnPlayerDeadAsObservable)
        //                                 .BatchFrame(0, FrameCountType.FixedUpdate)
        //                                 .Subscribe(v => Move(v[0]));
        //} 

        //private void Move(Vector3 v){
        //    totalTime += v.magnitude == 0f ? -totalTime : Time.deltaTime;
        //    v *= moveSpped * Mathf.Clamp01(Mathf.Pow(totalTime, (float)1 / 3));
        //    v.y = rb.velocity.y;
        //    rb.velocity = v;
        //}

        //private void Update()
        //{
        //    //Debug.Log(rb.velodcity);
        //}

        private float moveSpped = 5f;
        private Rigidbody rb;
        private float totalTime = 0f;
        private Vector3 moveVector3 = Vector3.zero;
        private const float accelerationGravity = 9.8f;
        private bool IsTouchingGround;

        private void Start()
        {
            var input = GetComponent<IPlayerInput>();
            var core = GetComponent<PlayerCore>();
            var jumper = GetComponent<PlayerJumper>();
            var hitList = new List<GameObject>();
            rb = GetComponent<Rigidbody>();

            //this.OnCollisionEnterAsObservable()
            //    .Where(x => x.collider.tag == EntityType.Ground.ToString() && !hitList.Contains(x))
            //    .Subscribe(x => hitList.Add(x));

            //this.OnCollisionExitAsObservable()
            //.Where(x => x.collider.tag == EntityType.Ground.ToString())
            //.Do(_ => Debug.Log("exit"))
            //.Subscribe(x => hitList.Remove(x));

            this.OnCollisionEnterAsObservable()
                .Where(x => x.collider.tag == EntityType.Ground.ToString() && !hitList.Contains(x.gameObject))
                .Subscribe(x => {
                    hitList.Add(x.gameObject);
                    var cloud = x.gameObject.GetComponent<Cloud>();
                    cloud?.OnDestroyObsevable
                    .FirstOrDefault()
                    .Subscribe(_ =>
                    {
                        if (hitList.Contains(x.gameObject))
                        {
                            hitList.Remove(x.gameObject);
                        }
                    });
                });

            this.OnCollisionExitAsObservable()
                .Where(x => x.collider.tag == EntityType.Ground.ToString())
                .Subscribe(x => hitList.Remove(x.gameObject));

            this.UpdateAsObservable()
            .Subscribe(_ => IsTouchingGround = hitList.Count > 0);

            //this.UpdateAsObservable()
            //    .Subscribe(_ => Debug.Log(jumper.IsJumpingObservable.Value));

            //this.UpdateAsObservable()
            //.Subscribe(_ => Debug.Log(IsTouchingGround));

            //this.UpdateAsObservable()
            //.Subscribe(_ => { foreach (var i in hitList) Debug.Log(i.gameObject.name); });

            //this.UpdateAsObservable()
            //    .Subscribe(_ => Debug.Log(hitList.Count()));

            //this.UpdateAsObservable()
            //.Subscribe(_ => Debug.Log(jumper.IsJumpingObservable.Value));

            //this.UpdateAsObservable()
            //    .Subscribe(_ => Debug.Log(IsTouchingGround));

            //this.OnCollisionEnterAsObservable()
            //    .Where(x => x.collider.tag == EntityType.Ground.ToString())
            //    .Subscribe(_ => IsTouchingGround = true);

            //this.OnCollisionExitAsObservable()
            //.Where(x => x.collider.tag == EntityType.Ground.ToString())
            //.Subscribe(_ => IsTouchingGround = false);

            //input.OnMoveDirectionObservable
            //.TakeUntil(core.OnPlayerDeadAsObservable)
            //.Where(_ =>!IsTouchingGround || !jumper.IsJumpingObservable.Value)
            //.Do(v =>
            //{
            //    totalTime += v.magnitude == 0f ? -totalTime : Time.deltaTime;
            //    moveVector3 = v * moveSpped * Mathf.Clamp01(Mathf.Pow(totalTime, (float)1 / 3));
            //})
            //.AsUnitObservable()
            //.BatchFrame(0, FrameCountType.FixedUpdate)
            //.Subscribe(_ => rb.velocity = moveVector3.AddSetY(rb.velocity.y));

            input.OnMoveDirectionObservable
                 .TakeUntil(core.OnPlayerDeadAsObservable)
                 .Do(v =>
                 {
                     totalTime += v.magnitude == 0f ? -totalTime : Time.deltaTime;
                     if (!IsTouchingGround || !jumper.IsJumpingObservable.Value)
                         moveVector3 = v * moveSpped * Mathf.Clamp01(Mathf.Pow(totalTime, (float)1 / 3));
                     else moveVector3 = Vector3.zero;
                 })
                 .AsUnitObservable()
                 .BatchFrame(0, FrameCountType.FixedUpdate)
                 .Subscribe(_ => rb.velocity = moveVector3.AddSetY(rb.velocity.y));

        }
    }
}
