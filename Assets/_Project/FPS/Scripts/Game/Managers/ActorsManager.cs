using System.Collections.Generic;
using UnityEngine;

namespace Unity.FPS.Game
{
    public class ActorsManager : MonoBehaviour
    {
        public List<Actor> Actors { get; private set; }
        public GameObject Player { get; private set; }
        public static ActorsManager instance;
        public void SetPlayer(GameObject player) => Player = player;

        void Awake()
        {
            instance = this;
            Actors = new List<Actor>();
        }
    }
}
