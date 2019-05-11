using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;
using System;

enum AxisType{
    Horizontal,
    Vertical
}

namespace MG.Players{
    
    public class PlayerInput : MonoBehaviour, IPlayerInput{
        
        private Subject<Vector3> moveDirectionObservable = new Subject<Vector3>();
        private Subject<Vector3> moveCameraSubject = new Subject<Vector3>();
        private Subject<Vector2> slectAttackCloudSubject = new Subject<Vector2>();
        private Subject<bool> jumpButtonObservable = new Subject<bool>();
        private Subject<bool> magicButtonObservable = new Subject<bool>();
        private Subject<bool> createCloudButtonObservable = new Subject<bool>();
        private Subject<bool> createAttackCloudSubject = new Subject<bool>();
        private Subject<KeyCode> createAttackCloudKeybordObservable = new Subject<KeyCode>();
        //private Subject<bool>[] numKeysObservable = new Subject<bool>[5];
        //private Subject<bool> oneButtonObservable = new Subject<bool>();
        //private Subject<bool> twoButtonObservable = new Subject<bool>();
        //private Subject<bool> threeButtonObservable = new Subject<bool>();
        //private Subject<bool> fourButtonObservable = new Subject<bool>();
        //private Subject<bool> fiveButtonObservable = new Subject<bool>();
        
        public IObservable<Vector3> OnMoveDirectionObservable { get { return moveDirectionObservable; }}
        public IObservable<Vector3> OnMoveCameraObservable { get { return moveCameraSubject; } }
        public IObservable<bool> OnJumpButtonObseravable { get { return jumpButtonObservable; } }
        public IObservable<bool> OnMagicButtonObserabable { get { return magicButtonObservable; }}
        public IObservable<bool> OnCreateCloudButtonObservable{ get { return createCloudButtonObservable; }}
        public IObservable<bool> OnCreateAttackCloudObservable { get { return createAttackCloudSubject; } }
        public IObservable<KeyCode> OnCreateAttackCloudKeybordObservable { get { return createAttackCloudKeybordObservable; }}
        public IObservable<Vector2> OnSelectAttackCloudObsrvable { get { return slectAttackCloudSubject; } }

        //public IObservable<bool>[] OnNumKeysObserabable { get { return numKeysObservable; }}
        //public IObservable<bool> OnOneButtonObservable { get { return oneButtonObservable; } }
        //public IObservable<bool> OnTwoButtonObservable { get { return twoButtonObservable; } }
        //public IObservable<bool> OnThreeButtonObservble { get { return threeButtonObservable; } }
        //public IObservable<bool> OnFourButtonObservable { get { return fourButtonObservable; } }
        //public IObservable<bool> OnFiveButtonObservable { get { return fiveButtonObservable; } }

        //private void Awake()
        //{
        //    for (int i = 0; i < numKeysObservable.Length; i++) numKeysObservable[i] = new Subject<bool>();
        //}

        private void Start()
        {
            //this.UpdateAsObservable()
            //    .Select(_ => Input.GetKeyDown(KeyCode.Alpha1))
            //    .Subscribe(oneButtonObservable);

            //this.UpdateAsObservable()
            //    .Select(_ => Input.GetKeyDown(KeyCode.Alpha2))
            //    .Subscribe(twoButtonObservable);

            //this.UpdateAsObservable()
            //    .Select(_ => Input.GetKeyDown(KeyCode.Alpha3))
            //    .Subscribe(threeButtonObservable);

            //this.UpdateAsObservable()
            //    .Select(_ => Input.GetKeyDown(KeyCode.Alpha4))
            //    .Subscribe(fourButtonObservable);

            //this.UpdateAsObservable()
            //.Select(_ => Input.GetKeyDown(KeyCode.Alpha5))
            //.Subscribe(fiveButtonObservable);

            //this.UpdateAsObservable()
            //    .Select(_ => Input.GetKeyDown(KeyCode.Alpha1))
            //    .Subscribe(numKeysObservable[0]);

            //this.UpdateAsObservable()
            //    .Select(_ => Input.GetKeyDown(KeyCode.Alpha2))
            //    .Subscribe(numKeysObservable[1]);

            //this.UpdateAsObservable()
            //    .Select(_ => Input.GetKeyDown(KeyCode.Alpha3))
            //    .Subscribe(numKeysObservable[2]);

            //this.UpdateAsObservable()
            //    .Select(_ => Input.GetKeyDown(KeyCode.Alpha4))
            //    .Subscribe(numKeysObservable[3]);

            //this.UpdateAsObservable()
            //.Select(_ => Input.GetKeyDown(KeyCode.Alpha5))
            //.Subscribe(numKeysObservable[4]); 

            var input1 = this.UpdateAsObservable()
                             .Where(_ => Input.GetKeyDown(KeyCode.Alpha1))
                             .Select(_ => KeyCode.Alpha1);
            
            var input2 = this.UpdateAsObservable()
                             .Where(_ => Input.GetKeyDown(KeyCode.Alpha2))
                             .Select(_ => KeyCode.Alpha2);
            
            var input3 = this.UpdateAsObservable()
                             .Where(_ => Input.GetKeyDown(KeyCode.Alpha3))
                             .Select(_ => KeyCode.Alpha3);
            
            var input4 = this.UpdateAsObservable()
                             .Where(_ => Input.GetKeyDown(KeyCode.Alpha4))
                             .Select(_ => KeyCode.Alpha4);

            Observable.Merge(input1, input2, input3, input4).Subscribe(createAttackCloudKeybordObservable);
            
            this.UpdateAsObservable()
                .Select(_ => Input.GetKeyDown(KeyCode.Space))
                .Subscribe(jumpButtonObservable);

            this.UpdateAsObservable()
                .Select(_ => Input.GetKeyDown("joystick button 0"))
                .Subscribe(jumpButtonObservable);

            this.UpdateAsObservable()
                .Select(_ => Input.GetKeyDown(KeyCode.R))
                .Subscribe(magicButtonObservable);

            this.UpdateAsObservable()
                .Select(_ => Input.GetKeyDown(KeyCode.R))
                .Subscribe(createCloudButtonObservable);

            this.UpdateAsObservable()
                .Select(_ => Input.GetKeyDown("joystick button 4"))
                .Subscribe(createCloudButtonObservable);

            this.UpdateAsObservable()
                .Select(_ => new Vector2(Input.GetAxis("Horizontal3"),
                                         Input.GetAxis("Vertical3"))
                        .normalized)
                .Subscribe(slectAttackCloudSubject);

            this.UpdateAsObservable()
                .Select(_ => Input.GetKeyDown("joystick button 1"))
                .Subscribe(createAttackCloudSubject);

            this.UpdateAsObservable()
                .Select(_ => Input.GetMouseButtonDown(0))
                .Subscribe(createAttackCloudSubject);

            this.UpdateAsObservable()
                .Select(_ => new Vector3(Input.GetAxis(AxisType.Horizontal.ToString()),
                                         0,
                                         Input.GetAxis(AxisType.Vertical.ToString()))
                        .normalized)
                .Subscribe(moveDirectionObservable);

        }
    }
    
}