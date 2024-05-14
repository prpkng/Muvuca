using UnityEngine;

namespace Muvuca.Effects
{
    public class CursorHover : MonoBehaviour
    {
        public void SetHover(bool hover) => CustomCursor.IsHovering = hover;
    }
}