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
        
        private Camera cam;
        public Image cursorImage;
        public Sprite defaultCursor;
        public Sprite hoverCursor;


        private void Awake()
        {
            DontDestroyOnLoad(transform.parent);
            Load();
        }

        private void Load()
        {
            IsHovering = false;
            rect = (RectTransform)transform;
            cam = Camera.main;
            Cursor.visible = false;
        }

        private void OnEnable()
        {
            SceneManager.sceneLoaded += Loaded;
        }

        private void OnDisable()
        {
            SceneManager.sceneLoaded -= Loaded;
        }

        private void Loaded(Scene _1, LoadSceneMode _2) => Load();


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