using System;
using System.Collections;
using UnityEngine;

namespace GJ.Environment.Impl
{
    public class SeaWaves : MonoBehaviour
    {
        [Range(0, 1)][SerializeField] private float m_waveSpeed = 0.7f;
        [Range(0, 1)][SerializeField] private float m_waveAmount = 0.3f;
        [Range(0, 1)][SerializeField] private float m_waveHeight = 0.4f;

        Vector3[] m_originalVertices;

        private void Start()
        {
            Vector3[] verts = GetComponent<MeshFilter>().mesh.vertices;
            m_originalVertices = new Vector3[verts.Length];

            Array.Copy(verts, m_originalVertices, verts.Length);

            StartCoroutine(MovementRoutine());
        }

        // Update is called once per frame
        IEnumerator MovementRoutine()
        {
            Mesh vMesh = GetComponent<MeshFilter>().mesh;

            Vector3[] vertices = new Vector3[vMesh.vertices.Length];

            Array.Copy(m_originalVertices, vertices, vertices.Length);

            yield return null;

            float dT = Time.deltaTime * 3;

            float waveT = m_waveSpeed * dT;

            for(int i = 0; i < vertices.Length - 1; i+=2 )
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

                vert = vertices[i+1];
                xz = vert.x * vert.z;
                wave = m_waveAmount * xz;

                wave += waveT;

                wave = Mathf.Sin(wave);

                wave *= m_waveHeight;

                wave += vert.y;

                vert = new Vector3(vert.x, wave, vert.z);

                vertices[i+1] = vert;

                yield return null;
            }

            vMesh.vertices = vertices;
        }
    }
}