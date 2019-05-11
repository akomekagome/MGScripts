using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

namespace MG.RuleSelect{

    public class GameMatchSetting{

        public enum PlayMode{
            SinglePlay,
            MultPlay
        }

        private static GameMatchSetting instance;

        public static GameMatchSetting Instance
        {
            get
            {
                if (instance == null)
                    instance = new GameMatchSetting();
                return instance;
            }
            private set { instance = value; }
        }

        public PlayMode playMode { get; private set; } = PlayMode.SinglePlay;

        public void PlayModeSet(PlayMode mode){
            playMode = mode;
        }

        public int PlayerNumberLimit()
        {
            if (playMode == PlayMode.SinglePlay) return 1;
            else if (playMode == PlayMode.MultPlay) return 2;
            return 0;
        }

    }
}
