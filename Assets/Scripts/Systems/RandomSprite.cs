using UnityEngine;

namespace Muvuca.Systems
{
    public class RandomSprite : MonoBehaviour
    {
        [SerializeField] private Sprite[] sprites;

        [SerializeField] private SpriteRenderer spriteRenderer;

        private void Awake()
        {
            spriteRenderer.sprite = sprites[Random.Range(0, sprites.Length)];
            Destroy(this);
        }
    }
}