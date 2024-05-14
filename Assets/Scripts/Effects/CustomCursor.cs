using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.AddressableAssets.ResourceLocators;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Muvuca.Effects
{
    public class CustomCursor : MonoBehaviour
    {
        public static bool IsHovering;

        public RectTransform canvasRect;
        private RectTransform rect;
        
        private UnityEngine.Camera cam;
        public Image cursorImage;
        public Sprite defaultCursor;
        public Sprite hoverCursor;


        private void Awake()
        {
            DontDestroyOnLoad(transform.parent);
            SceneManager.sceneLoaded += (_, _) => Loaded();
            Loaded();
        }

        private void Loaded()
        {
            IsHovering = false;
            rect = (RectTransform)transform;
            cam = UnityEngine.Camera.main;
            Cursor.visible = false;
        }


        private void Update()
        {
            if (!cam) return;
            cursorImage.sprite = IsHovering ? hoverCursor : defaultCursor;
            Vector3 pos;
            var mousePos = Mouse.current.position.ReadValue();
            var viewportPos = cam.ScreenToViewportPoint(mousePos);
            var screenPosition = new Vector2(
                viewportPos.x * canvasRect.sizeDelta.x - canvasRect.sizeDelta.x * 0.5f,
                viewportPos.y * canvasRect.sizeDelta.y - canvasRect.sizeDelta.y * 0.5f);
            rect.anchoredPosition = screenPosition;
        }
    }
}