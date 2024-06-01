namespace Muvuca.Game.Player
{
    using UnityEngine;

    public class CenterMaterialsToPlayer : MonoBehaviour
    {
        public Material[] materials;

        private Camera cam;

        private void Awake()
        {
            cam = Camera.main;
        }

        private void Update()
        {
            var viewportPos = cam.WorldToViewportPoint(transform.position);
            viewportPos = new Vector2(Mathf.Clamp(viewportPos.x, 0, 1), Mathf.Clamp(viewportPos.y, 0, 1));
            foreach (var mat in materials)
            {
                mat.SetVector("_Offset", new Vector2(viewportPos.x, viewportPos.y));
            }
        }
    }
}