using GJ.Obstacles.Base;
using System.Collections;
using UnityEngine;

namespace GJ.SpawningSystem
{
    public class ObjectSpawner : MonoBehaviour
    {
        [SerializeField] protected GameObject[] m_spawnPrefabs;
        [SerializeField] protected Transform m_spawnParent;
        [SerializeField] protected float m_spawnZoneWidth = 5f;
        [SerializeField] protected float m_spawnZoneLength = 5f;

        [SerializeField] protected float m_minimumSpawnTime = 1f;
        [SerializeField] protected float m_maximumSpawnTime = 5f;

        public bool SpawnItems = true;

        protected Coroutine m_spawningRoutine;
        protected GameObject m_lastSpawnedItem;

        protected void OnEnable()
        {
            StartCoroutine(SpawnLoop());
        }

        protected virtual IEnumerator SpawnLoop()
        {
            while(SpawnItems)
            {
                if (m_spawningRoutine == null)
                    m_spawningRoutine = StartCoroutine(SpawnItem());
                else
                    yield return new WaitForFixedUpdate();//Wait a frame
            }
        }

        protected virtual IEnumerator SpawnItem()
        {
            float waitTime = Random.Range(m_minimumSpawnTime, m_maximumSpawnTime);

            yield return new WaitForSeconds(waitTime);

            //Choose the prefab to spawn
            int chosenIndex = Random.Range(0, m_spawnPrefabs.Length);
            GameObject spawnedItem = Instantiate(m_spawnPrefabs[chosenIndex], m_spawnParent);
            m_lastSpawnedItem = spawnedItem;


            //figure out where to start it.
            Vector3 spawnPosition = m_spawnParent.position;

            float spawnMinX = -m_spawnZoneWidth;
            float spawnMaxX = m_spawnZoneWidth;
            spawnPosition.x = Random.Range(spawnMinX, spawnMaxX);

            float spawnMinZ = -m_spawnZoneLength;
            float spawnMaxZ = m_spawnZoneLength;
            spawnPosition.z = Random.Range(spawnMinZ, spawnMaxZ);

            spawnedItem.transform.position = spawnPosition;

            //As we may use this for stationary obstacle spawning
            if(spawnedItem.GetComponent<Obstacle>() != null)
            {
                spawnedItem.GetComponent<Obstacle>().Play();
            }

            m_spawningRoutine = null;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;

            Vector3 linePos = transform.position;
            linePos.z += m_spawnZoneLength;
            linePos.x += m_spawnZoneWidth;

            Vector3 line2pos = linePos;
            line2pos.x -= m_spawnZoneWidth * 2;
            Gizmos.DrawLine(linePos, line2pos);

            line2pos.x = linePos.x;
            line2pos.z -= m_spawnZoneLength * 2;
            Gizmos.DrawLine(linePos, line2pos);

            linePos.z = line2pos.z;
            linePos.x -= m_spawnZoneWidth * 2;
            Gizmos.DrawLine(linePos, line2pos);

            line2pos.z += m_spawnZoneLength * 2;
            line2pos.x = linePos.x;
            Gizmos.DrawLine(linePos, line2pos);
        }
    }
}