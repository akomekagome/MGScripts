using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MG.Stages{
    
    public class StageCore : MonoBehaviour
    {

        [SerializeField] private Transform[] playerSpawnPosition;

        public Transform[] PlayerSpawnPosition{ get { return playerSpawnPosition; }}

        private void Start()
        {
            foreach (var spawn in playerSpawnPosition) spawn.gameObject.SetActive(false);
        }
    }
}
