using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;
using System;
using System.Linq;
using MG.Utils;

namespace MG.Cameras
{
    public class CameraRotate : MonoBehaviour
    {
        [SerializeField] private bool reverse_Y = false;
        private Subject<Vector2> _mousePosSubject = new Subject<Vector2>();
        public IObservable<Vector2> MousePosObservable { get { return _mousePosSubject; } }
        [SerializeField] private Transform playerTf;
        private const float rotateSpeed_x = 100f;
        private static readonly Vector3 cameraPos = new Vector3(0f, 3f, -7f);
        private static readonly Vector3 cameraRotate = new Vector3(10f, 0f, 0f);
        private static readonly float rotateLimit = 30f;

        private float GetRotate(float rotate)
        {
            if (rotate > 180f) return rotate - 360f;
            else return rotate;
        }

        private float Reverse(bool reverse)
        {
            if (reverse) return -1f;
            else return 1f;
        }

        private void Start()
        {
            transform.position = playerTf.position + cameraPos;
            transform.eulerAngles = cameraRotate;
            var LimitVec2 = new Vector2(cameraRotate.x - rotateLimit, cameraRotate.x + rotateLimit);

            //this.UpdateAsObservable()
            //    .Select(_ => new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y")))
            //    .Subscribe(_mousePosSubject);

            this.UpdateAsObservable()
                .Select(_ => new Vector2(Input.GetAxis("Horizontal2"),
                                         Input.GetAxis("Vertical2") * Reverse(reverse_Y)))
                .Subscribe(_mousePosSubject);

            this.UpdateAsObservable()
                .Select(_ => new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y") * Reverse(!reverse_Y)))
                .Subscribe(_mousePosSubject);

            //MousePosObservable.Where(v => v.magnitude != 0f).Skip(1).Subscribe(v =>
            //{
            //    transform.rotation = (transform.rotation * Quaternion.Euler(v.y * -1f, v.x, 0f)).SetZ(0f);
            //});

            MousePosObservable.Where(v => v.magnitude != 0f).Skip(1).Subscribe(v =>
            {
                transform.RotateAround(playerTf.position, Vector3.up, rotateSpeed_x * Time.deltaTime * v.x);
                if (!(GetRotate(transform.eulerAngles.x) >= LimitVec2.y && v.y > 0f) && !(GetRotate(transform.eulerAngles.x) <= LimitVec2.x && v.y < 0f))
                    transform.rotation = transform.rotation * Quaternion.Euler(v.y, 0f, 0f);
                //transform.RotateAround(playerTf.position, Vector3.right, rotateSpeed_y * Time.deltaTime * v.y);
                transform.eulerAngles = new Vector3(Mathf.Clamp(GetRotate(transform.eulerAngles.x), LimitVec2.x, LimitVec2.y),
                    transform.eulerAngles.y, 0f);
                //            transform.eulerAngles = new Vetor3(Mathf.Clamp(transform.eulerAngles.x, cameraRotate.x - rotateLimit, cameraRotate.y + rotateLimit),
                //transform.eulerAngles.y, 0f);


                transform.eulerAngles = transform.eulerAngles.SetZ(0f);
                //Debug.Log(GetRotate(transform.eulerAngles.x));
            });

            playerTf.ObserveEveryValueChanged(x => x.position)
                .Pairwise()
                .Subscribe(v => transform.position = transform.position + (v.Current - v.Previous));
        }
    }

}