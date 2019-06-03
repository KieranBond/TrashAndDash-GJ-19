using System.Collections;
using UnityEngine;

namespace GJ.SpawningSystem
{
    public class TrashSpawner : MonoBehaviour
    {
        [SerializeField] private GameObject m_spawnPrefab;
        [SerializeField] private Transform m_spawnParent;
        [SerializeField] private float m_spawnZoneWidth = 5f;
        [SerializeField] private float m_spawnZoneLength = 5f;

        [SerializeField] private float m_minimumSpawnTime = 1f;
        [SerializeField] private float m_maximumSpawnTime = 5f;

        public bool SpawnItems = true;

        private Coroutine m_spawningRoutine;

        void OnEnable()
        {
            StartCoroutine(SpawnLoop());
        }

        private IEnumerator SpawnLoop()
        {
            while(SpawnItems)
            {
                if (m_spawningRoutine == null)
                    m_spawningRoutine = StartCoroutine(SpawnItem());
                else
                    yield return new WaitForFixedUpdate();//Wait a frame
            }
        }

        private IEnumerator SpawnItem()
        {
            float waitTime = Random.Range(m_minimumSpawnTime, m_maximumSpawnTime);

            yield return new WaitForSeconds(waitTime);

            GameObject spawnedItem = Instantiate(m_spawnPrefab, m_spawnParent);

            Vector3 spawnPosition = m_spawnParent.position;

            float spawnMinX = -m_spawnZoneWidth;
            float spawnMaxX = m_spawnZoneWidth;
            spawnPosition.x = Random.Range(spawnMinX, spawnMaxX);

            float spawnMinZ = -m_spawnZoneLength;
            float spawnMaxZ = m_spawnZoneLength;
            spawnPosition.z = Random.Range(spawnMinZ, spawnMaxZ);

            spawnedItem.transform.position = spawnPosition;

            m_spawningRoutine = null;
        }
    }
}