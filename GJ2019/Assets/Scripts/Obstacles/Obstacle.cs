using UnityEngine;
using static GJ.HelperScripts.Easing;

namespace GJ.Obstacles.Base
{
    public abstract class Obstacle : MonoBehaviour
    {
        public EaseType EasingType = EaseType.Linear;

        public abstract void Play();
        public abstract void Activate();
        public abstract void Deactivate();
    }
}