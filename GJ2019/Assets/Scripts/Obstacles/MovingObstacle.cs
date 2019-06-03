using GJ.HelperScripts;
using GJ.Obstacles.Base;
using System.Collections;
using UnityEngine;
using static GJ.HelperScripts.Easing;

namespace GJ.Obstacles.Impl
{
    public class MovingObstacle : MonoBehaviour, IObstacle
    {
        [SerializeField] private EaseType EasingType;
        [SerializeField] private float m_obstacleHeight = 3f;

        [SerializeField] private float m_startDelay = 2f;
        [SerializeField] private float m_moveDuration = 5f;

        [SerializeField] private float m_riseSinkDuration = 2f;

        private Coroutine m_movementRoutine;

        private Vector3 m_startingPosition;
        private Vector3 m_destinationPosition;

        private void OnEnable()
        {
            m_startingPosition = transform.position;
        }

        public void Play()
        {

        }

        public void Play(Transform[] a_positions, float a_width)
        {
            GetDestination(a_positions, a_width);

            if (m_movementRoutine != null)
                StopAllCoroutines();

            m_movementRoutine = StartCoroutine(MoveToDestination());
        }

        private void GetDestination(Transform[] a_positions, float a_width)
        {
            int randomIndex = Random.Range(0, a_positions.Length);
            Vector3 chosenPos = a_positions[randomIndex].position;

            chosenPos += a_positions[randomIndex].right * Random.Range(-a_width, a_width);

            m_destinationPosition = chosenPos;
        }

        public void Activate()
        {
            if (m_movementRoutine != null)
                StopAllCoroutines();

            m_movementRoutine = StartCoroutine(RiseMovement());
        }

        public void Deactivate()
        {
            if (m_movementRoutine != null)
                StopAllCoroutines();

            m_movementRoutine = StartCoroutine(SinkMovement());
        }

        private IEnumerator MoveToDestination()
        {
            float step = 0f;
            float currentLerpTime = 0f;

            Vector3 startingPos = transform.position;

            while (Vector3.Distance(transform.position, m_destinationPosition) > 0)
            {
                currentLerpTime += Time.deltaTime;
                step = Easing.GetLerpT(EasingType, currentLerpTime, m_riseSinkDuration);
                Vector3 newPos = Vector3.Lerp(startingPos, m_destinationPosition, step);
                transform.position = newPos;

                yield return new WaitForFixedUpdate();
            }

            //Testing
            StopAllCoroutines();
            Destroy(gameObject);
            //Deactivate();
        }

        private IEnumerator RiseMovement()
        {
            float step = 0f;
            float currentLerpTime = 0f;

            Vector3 startingPos = transform.position;
            Vector3 raisedPosition = startingPos;
            raisedPosition.y += m_obstacleHeight;

            while (Vector3.Distance(transform.position, raisedPosition) > 0)
            {
                currentLerpTime += Time.deltaTime;
                step = Easing.GetLerpT(EasingType, currentLerpTime, m_riseSinkDuration);
                Vector3 newPos = Vector3.Lerp(startingPos, raisedPosition, step);
                transform.position = newPos;

                yield return new WaitForFixedUpdate();
            }
        }

        private IEnumerator SinkMovement()
        {
            float step = 0f;
            float currentLerpTime = 0f;

            Vector3 startingPos = transform.position;
            Vector3 loweredPosition = startingPos;
            loweredPosition.y -= m_obstacleHeight;

            while (Vector3.Distance(transform.position, loweredPosition) > 0)
            {
                currentLerpTime += Time.deltaTime;
                step = Easing.GetLerpT(EasingType, currentLerpTime, m_riseSinkDuration);
                Vector3 newPos = Vector3.Lerp(startingPos, loweredPosition, step);
                transform.position = newPos;

                yield return new WaitForFixedUpdate();
            }
        }

        public EaseType GetEaseType()
        {
            return EasingType;
        }

    }
}