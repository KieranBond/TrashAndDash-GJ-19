using GJ.HelperScripts;
using GJ.Obstacles.Base;
using System.Collections;
using UnityEngine;
using static GJ.HelperScripts.Easing;

namespace GJ.Obstacles.Impl
{
    public class StationaryObstacle : MonoBehaviour, IObstacle
    {
        [SerializeField] private EaseType EasingType;
        [SerializeField] private float m_obstacleHeight = 5f;

        [SerializeField] private float m_riseMovementDuration = 3f;
        [SerializeField] private float m_sinkMovementDuration = 2f;

        [SerializeField] private float m_stayDuration = 5f;

        private Vector3 m_raisedPosition;
        private Vector3 m_loweredPosition;

        private Coroutine m_movementRoutine;

        private void OnEnable()
        {
            m_raisedPosition = transform.position;

            //Set it below the water
            Vector3 movePos = m_raisedPosition;
            movePos.y -= m_obstacleHeight;
            transform.position = movePos;

            m_loweredPosition = transform.position;
        }

        public void Play()
        {
            if (m_movementRoutine != null)
                StopAllCoroutines();

            m_movementRoutine = StartCoroutine(RiseAndSink());
        }

        private IEnumerator RiseAndSink()
        {
            m_movementRoutine = StartCoroutine(RiseMovement());
            yield return new WaitForSeconds(m_riseMovementDuration + m_stayDuration);
            m_movementRoutine = StartCoroutine(SinkMovement());
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


            //m_movementRoutine = StartCoroutine(SinkMovement());
        }

        private IEnumerator RiseMovement()
        {
            float step = 0f;
            float currentLerpTime = 0f;

            Vector3 startingPos = transform.position;

            while(Vector3.Distance(transform.position, m_raisedPosition) > 0)
            {
                currentLerpTime += Time.deltaTime;
                step = Easing.GetLerpT(EasingType, currentLerpTime, m_riseMovementDuration);
                //step += Time.deltaTime / m_riseMovementDuration;
                Vector3 newPos = Vector3.Lerp(startingPos, m_raisedPosition, step);
                transform.position = newPos;

                yield return new WaitForFixedUpdate();
            }
        }

        private IEnumerator SinkMovement()
        {
            float step = 0f;
            float currentLerpTime = 0f;

            Vector3 startingPos = transform.position;

            while (Vector3.Distance(transform.position, m_loweredPosition) > 0)
            {
                currentLerpTime += Time.deltaTime;
                step = Easing.GetLerpT(EasingType, currentLerpTime, m_sinkMovementDuration);
                //step += Time.deltaTime / m_sinkMovementDuration;
                Vector3 newPos = Vector3.Lerp(startingPos, m_loweredPosition, step);
                transform.position = newPos;

                yield return new WaitForFixedUpdate();
            }
            Debug.Log("Finsihed");
        }

        public EaseType GetEaseType()
        {
            return EasingType;
        }

        private void OnGUI()
        {
            if (GUILayout.Button("Play"))
            {
                Play();
            }
            if (GUILayout.Button("Activate"))
            {
                Activate();
            }
            if (GUILayout.Button("Deactivate"))
            {
                Deactivate();
            }
        }
    }
}