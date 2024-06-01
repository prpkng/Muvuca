namespace Muvuca.Effects
{
    using Muvuca.Game.Common;
    using UnityEngine;

    public class LineEnabler : MonoBehaviour
    {
        [SerializeField] private EnableUntilPlayerDeath enabler;

        [SerializeField] private LineRenderer lr;

        private void Awake()
        {
            if (lr == null) Destroy(this);
        }

        [SerializeField][ColorUsage(true, hdr: true)] private Color activeColor;
        [SerializeField][ColorUsage(true, hdr: true)] private Color inactiveColor;

        private void Update()
        {
            lr.material.SetColor("_Tint", enabler.didDisable ? activeColor : inactiveColor);
        }
    }
}