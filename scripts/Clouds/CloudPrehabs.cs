using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using MG.Utils;

namespace MG.Clouds{
    
    public class CloudPrehabs : SingletonMonoBehaviour<CloudPrehabs> {
        
        [SerializeField] private GameObject cloudPrehab;
        [SerializeField] private GameObject[] attackCloudPrehabs;
        private Dictionary<KeyCode, GameObject> _attackCloudDictionary = new Dictionary<KeyCode, GameObject>();
        public Dictionary<KeyCode, GameObject> AttackCloudDicitionary{ get { return _attackCloudDictionary; }}
        private Dictionary<AttackCloudEnum, GameObject> _attackCloudDictionary2 = new Dictionary<AttackCloudEnum, GameObject>();
        public Dictionary<AttackCloudEnum, GameObject> AttackCloudDicitionary2 { get { return _attackCloudDictionary2; } }


        private void Start()
        {
            var keyList = new KeyCode[] { KeyCode.Alpha1, KeyCode.Alpha2, KeyCode.Alpha3, KeyCode.Alpha4 };
            var attackCloudList = new AttackCloudEnum[] { AttackCloudEnum.ThnderCloud, AttackCloudEnum.FireCloud, AttackCloudEnum.TornadoCloud, AttackCloudEnum.IcicleCloud};

            for (var i = 0; i < Mathf.Min(attackCloudPrehabs.Length, keyList.Length); i++){
                _attackCloudDictionary.Add(keyList[i], attackCloudPrehabs[i]);
            }

            for (var i = 0; i < Mathf.Min(attackCloudPrehabs.Length, attackCloudList.Length); i++)
            {
                _attackCloudDictionary2.Add(attackCloudList[i], attackCloudPrehabs[i]);
            }
        }
    }
}