using System;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

namespace Muvuca.Systems.DialogueSystem
{
    public class WaveEffect : MonoBehaviour
    {
        [SerializeField] private float wobbleForce;
        [SerializeField] private float wobbleSpeed;
        [SerializeField] private float wobbleCharacterDistance;

        [SerializeField] private TMP_Text tmp;
        
        private Vector3[] vertices;
        private Mesh mesh;

        private void Update()
        {
            tmp.ForceMeshUpdate();
            mesh = tmp.mesh;
            vertices = mesh.vertices;
            for (int i = 0; i < tmp.textInfo.characterCount; i++)
            {
                TMP_CharacterInfo c = tmp.textInfo.characterInfo[i];

                int index = c.vertexIndex;

                Vector3 offset = Wobble((Time.unscaledTime + i * wobbleCharacterDistance) * wobbleSpeed);
                vertices[index] += offset;
                vertices[index + 1] += offset;
                vertices[index + 2] += offset;
                vertices[index + 3] += offset;
            }

            mesh.vertices = vertices;
            tmp.canvasRenderer.SetMesh(mesh);
        }

        private Vector2 Wobble(float time)
        {
            return new Vector2(Mathf.Sin(time * 1.1f) * wobbleForce, Mathf.Cos(time * .8f) * wobbleForce);
        }
    }
}