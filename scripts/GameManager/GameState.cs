using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using MG.Utils;

public enum GameStateEnum
{
    Standby,
    Countdown,
    GameUpdate,
    Judge,
    Exit
}

namespace MG.GameManagers{
    
    public class GameState : SingletonMonoBehaviour<GameState>
    {
        
        private ReactiveProperty<GameStateEnum> gameState = new ReactiveProperty<GameStateEnum>(GameStateEnum.Standby);
        
        public IReadOnlyReactiveProperty<GameStateEnum> GameStateReactiveProperty { get { return gameState.ToReadOnlyReactiveProperty(); } }
        
        private ReactiveProperty<int> gameCount = new ReactiveProperty<int>();
        
        public IReadOnlyReactiveProperty<int> CountDownReactiveProperty { get { return gameCount.ToReadOnlyReactiveProperty(); } }
        
        private void Start()
        {
            Application.targetFrameRate = 60;
            
            gameState.Where(x => x == GameStateEnum.Exit)
                     .First()
                     .Subscribe(_ =>
            {
                //FadeSceneTransition.instance.FadeChangeScene(SceneNameEnum.Title);
            });
            
            StartCoroutine(GameStart());
        }
        
        private IEnumerator GameStart()
        {
            ////生成待機
            //yield return m_StageSpawner.OnStageInitCompleteAsObservable().FirstOrDefault().StartAsCoroutine();
            
            ////フェードが終わるのを待機
            //yield return FadeSceneTransition.instance.IsFading.Where(x => x == false).FirstOrDefault().StartAsCoroutine();
            
            //// フェード終了後ちょっとだけ待つ
            //yield return new WaitForSeconds(0.5f);
            
            gameState.Value = GameStateEnum.Countdown;
            yield return StartCoroutine(CountStart(3));
            
            gameState.Value = GameStateEnum.GameUpdate;
            //yield return PlayerManager
            
            gameState.Value = GameStateEnum.Exit;
            yield break;
        }
        
        private IEnumerator CountStart(int startCount)
        {
            gameCount.Value = startCount;
            while (startCount > 0)
            {
                yield return new WaitForSeconds(1.0f);
                gameCount.Value -= 1;
            }
            yield return 0;
        }
    }
}