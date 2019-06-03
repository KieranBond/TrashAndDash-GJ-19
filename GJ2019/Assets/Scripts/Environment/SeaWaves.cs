using UnityEngine;

namespace GJ.Environment.Impl
{
    public class SeaWaves : MonoBehaviour
    {
        [SerializeField] private float m_waveSpeed;
        [SerializeField] private float m_waveAmount;
        [SerializeField] private float m_waveHeight;

        // Update is called once per frame
        void LateUpdate()
        {
            Mesh vMesh = GetComponent<MeshFilter>().mesh;

            Vector3[] vertices = vMesh.vertices;

            float dT = Time.deltaTime * 3;

            float waveT = m_waveSpeed * dT;

            for(int i = 0; i < vertices.Length; i++)
            {
                Vector3 vert = vertices[i];

                float xz = vert.x * vert.z;
                float wave = m_waveAmount * xz;

                wave += waveT;

                wave = Mathf.Sin(wave);

                wave *= m_waveHeight;

                wave += vert.y;

                vert = new Vector3(vert.x, wave, vert.z);

                vertices[i] = vert;
            }

            vMesh.vertices = vertices;
        }
    }
}