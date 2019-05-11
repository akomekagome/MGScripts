using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;
using System.Linq;
using System;
using MG.Utils;

enum EntityType
{
    Ground
}

namespace MG.Players{
    
    public class PlayerJumper : MonoBehaviour
    {
        private static readonly Dictionary<int, float> jumpPower = new Dictionary<int, float>{
            {1, 6f}, {2, 6.5f}, {3, 7f}
        };
        private ReactiveProperty<bool> _isJumpingObservable = new ReactiveProperty<bool>();
        public ReactiveProperty<bool> IsJumpingObservable{ get { return _isJumpingObservable; }}
        private int _jumpLevel = 1;
        public int JumpLevel{ get { return _jumpLevel; } private set { _jumpLevel = Mathf.Clamp(value, 1, jumpMaxLevel); }}
        private const int jumpMaxLevel = 3;
        private bool canLevelUP;
        private const int levelUPGraceFrame = 4;

        private CapsuleCollider capsuleCollider;
        
        public void Start()
        {
            Rigidbody rb = GetComponent<Rigidbody>();
            capsuleCollider = GetComponent<CapsuleCollider>();
            var input = GetComponent<IPlayerInput>();
                             
            this.UpdateAsObservable().Subscribe(_ => _isJumpingObservable.Value = !JumpJudge());

            //n段ジャンプ
            input.OnJumpButtonObseravable
                .Where(x => x && !IsJumpingObservable.Value)
                .Do(_ =>
                {
                    IsJumpingObservable.Skip(1)
                                       .TakeUntil(input.OnJumpButtonObseravable.Skip(1).Where(x => x && IsJumpingObservable.Value))
                                       .Where(x => !x)
                                       .FirstOrDefault()
                                       .Do(_2 => canLevelUP = true)
                                       .DelayFrame(levelUPGraceFrame)
                                       .Subscribe(_2 => canLevelUP = false);

                    if (canLevelUP && JumpLevel < jumpMaxLevel) JumpLevel++;
                    else JumpLevel = 1;
                })
                .AsUnitObservable()
                .BatchFrame(0, FrameCountType.FixedUpdate)
                .Subscribe(_ => rb.velocity = rb.velocity.AddSetY(jumpPower[JumpLevel]));
        }

        //接地判定
        private bool JumpJudge(){
            RaycastHit hitinfo;
            if (Physics.SphereCast(transform.position + capsuleCollider.center, capsuleCollider.radius,
                                   Vector3.down,
                                   out hitinfo,
                                   (capsuleCollider.height / 2) - capsuleCollider.radius + 0.01f))
                return hitinfo.collider.tag == EntityType.Ground.ToString();
            else return false;
        }
    }
}
