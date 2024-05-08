using UnityEngine;

namespace Muvuca.UI
{
    public class MenuSelector : MonoBehaviour
    {
        public bool followX = false;
        public bool followY = true;
        [SerializeField] private ListMenuController controller;
        [SerializeField] private float moveSpeed;

        private void Update()
        {
            var current = controller.textButtons[controller.selection].transform.position;
            if (!followX) current.x = transform.position.x;
            if (!followY) current.y = transform.position.y;
            transform.position = Vector3.Lerp(transform.position, current, moveSpeed * Time.deltaTime);
        }
    }
}