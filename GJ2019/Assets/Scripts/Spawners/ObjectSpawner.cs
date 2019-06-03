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
            int chosenIndex = Random.Range(0, (m_spawnPrefabs.Length - 1));
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

            m_spawningRoutine = null;
        }
    }
}