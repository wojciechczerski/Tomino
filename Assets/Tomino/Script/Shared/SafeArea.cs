using UnityEngine;

namespace Tomino.Shared
{
    public class SafeArea : MonoBehaviour
    {
        public Camera mainCamera;

        private RectTransform _rectTransform;
        private RectTransform _parentTransform;

        private void Awake()
        {
            _rectTransform = GetComponent<RectTransform>();
            _parentTransform = _rectTransform.parent as RectTransform;
        }

        private void Update()
        {
            var safeArea = Screen.safeArea;
            var safeAreaBottomLeft = new Vector2(safeArea.xMin, safeArea.yMin);
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                _parentTransform,
                safeAreaBottomLeft,
                mainCamera,
                out var localBottomLeft
            );

            var safeAreaTopRight = new Vector2(safeArea.xMax, safeArea.yMax);
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                _parentTransform,
                safeAreaTopRight,
                mainCamera,
                out var localTopRight
            );

            var rect = _parentTransform.rect;
            _rectTransform.SetLeft(localBottomLeft.x + rect.width / 2);
            _rectTransform.SetRight(rect.width / 2 - localTopRight.x);
            _rectTransform.SetBottom(localBottomLeft.y + rect.height / 2);
            _rectTransform.SetTop(rect.height / 2 - localTopRight.y);
        }
    }
}
