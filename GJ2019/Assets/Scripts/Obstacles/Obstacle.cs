using UnityEngine;
using static GJ.HelperScripts.Easing;

namespace GJ.Obstacles.Base
{
    public interface IObstacle
    {
        EaseType GetEaseType();

        void Play();
        void Activate();
        void Deactivate();
    }
}