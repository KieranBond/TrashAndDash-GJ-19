using System.Collections;
using UnityEngine;

namespace GJ.SpawningSystem
{
    public class ObstacleSpawner : MonoBehaviour
    {
        [SerializeField] protected GameObject[] m_spawnPrefabs;
        [SerializeField] protected Transform[] m_spawnPositions;
        [SerializeField] private float m_spawnZoneWidth;

        [SerializeField] protected float m_minimumSpawnTime = 1f;
        [SerializeField] protected float m_maximumSpawnTime = 5f;

        public bool SpawnObstacles = true;

        protected Coroutine m_spawningRoutine;
        protected GameObject m_lastSpawnedObstacle;

        protected void OnEnable()
        {
            StartCoroutine(SpawnLoop());
        }

        protected virtual IEnumerator SpawnLoop()
        {
            while (SpawnObstacles)
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

            int chosenIndex = Random.Range(0, m_spawnPositions.Length);
            Transform spawnParent = m_spawnPositions[chosenIndex];

            //Choose the prefab to spawn
            chosenIndex = Random.Range(0, m_spawnPrefabs.Length);
            GameObject spawnedItem = Instantiate(m_spawnPrefabs[chosenIndex], spawnParent);
            m_lastSpawnedObstacle = spawnedItem;


            //figure out where to start it.
            Vector3 spawnPosition = spawnParent.position;

            float spawnMinX = -m_spawnZoneWidth;
            float spawnMaxX = m_spawnZoneWidth;
            spawnPosition.x = Random.Range(spawnMinX, spawnMaxX);

            spawnedItem.transform.position = spawnPosition;

            spawnedItem.GetComponent<Obstacle>().Activate();

            m_spawningRoutine = null;
        }
    }
}