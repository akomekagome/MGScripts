using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UniRx;
using UniRx.Triggers;
using MG.Stages;
using MG.Players;
using MG.RuleSelect;
using MG.Items;

namespace MG.GameManagers{
    
    public class AllSpowner : MonoBehaviour {
        [SerializeField] private PlayerCore[] playerPrehabs = null;
        [SerializeField] private StageCore[] stagePrehabs = null;
        [SerializeField] private IItemObject[] itemPrehabs;
        [SerializeField] private PlayerManager playerManager;
        private StageCore stageCore;
          
        //<summary>
        //生成したアイテム一覧
        //</summary>
        private Dictionary<int, IItemObject> spownedItemDictionary = new Dictionary<int, IItemObject>();

        private Subject<Unit> onStageInitComplete = new Subject<Unit>();

        /// <summary>
        /// ステージの初期化完了通知
        /// </summary>
        public IObservable<Unit> OnStageInitCompleteAsObservable{ get { return onStageInitComplete; }}

        private void Start()
        {
            GameState.Instance.GameStateReactiveProperty
                     .Where(x => x == GameStateEnum.Standby)
                     .FirstOrDefault()
                     .Subscribe(_ => InitGame());
        }

        private IEnumerator InitGame(){
            //yield return SpawnStage();
            yield return SpawnPlayer();
        }

        private IEnumerator SpawnStage(){
            stageCore = Instantiate(stagePrehabs[0]);
            yield break;
        }

        private IEnumerator SpawnPlayer(){

            var maxplayer = GameMatchSetting.Instance.PlayerNumberLimit();
            var playerInstantPosition = stageCore.PlayerSpawnPosition;
            for (var i = 0; i < maxplayer; i++){
                var corePlayer = Instantiate(playerPrehabs[0]);
                corePlayer.SetPlayerID(i + 1);
            }
            yield break;
        }
    }
}