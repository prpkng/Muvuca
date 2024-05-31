namespace Muvuca.Systems
{
    using UnityEngine;

    public class SetSpriteOpacity : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer spr;

        public void Set(float alpha) => spr.color = new Color(spr.color.r, spr.color.g, spr.color.b, alpha);
    }
}