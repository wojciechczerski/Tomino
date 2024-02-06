using System.Collections.Generic;
using Tomino.Model;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Pool;
using Text = UnityEngine.UI.Text;

namespace Tomino.View
{
    public class AlertView : MonoBehaviour
    {
        public Text titleText;
        public RectTransform buttonsContainer;
        public GameObject buttonPrefab;
        public LocalizationProvider localizationProvider;

        private ObjectPool<AlertButtonView> _buttonPool;
        private readonly List<AlertButtonView> _buttons = new ();

        internal void Awake()
        {
            _buttonPool = new ObjectPool<AlertButtonView>(CreateAlertButton, OnGetAlertButton, OnReleaseAlertButton);
            Hide();
        }

        public void SetTitle(string textID)
        {
            titleText.text = localizationProvider.currentLocalization.GetLocalizedTextForID(textID);
        }

        public void AddButton(string textID, UnityAction onClickAction, UnityAction pointerDownAction)
        {
            var alertButton = _buttonPool.Get();
            alertButton.PointerHandler.onPointerDown.AddListener(pointerDownAction);
            alertButton.Button.onClick.AddListener(onClickAction);
            alertButton.Button.onClick.AddListener(Hide);
            alertButton.Text.text = localizationProvider.currentLocalization.GetLocalizedTextForID(textID);
            alertButton.RectTransform.SetSiblingIndex(buttonsContainer.childCount - 1);
        }

        public void Show()
        {
            gameObject.SetActive(true);
        }

        private void Hide()
        {
            _buttons.ForEach(_buttonPool.Release);
            _buttons.Clear();
            gameObject.SetActive(false);
        }

        private AlertButtonView CreateAlertButton()
        {
            var instance = Instantiate(buttonPrefab);
            instance.SetActive(false);

            var button = instance.GetComponent<AlertButtonView>();
            button.RectTransform.SetParent(buttonsContainer, false);

            return button;
        }

        private void OnGetAlertButton(AlertButtonView button)
        {
            button.gameObject.SetActive(true);
            _buttons.Add(button);
        }

        private static void OnReleaseAlertButton(AlertButtonView button)
        {
            button.Button.onClick.RemoveAllListeners();
            button.PointerHandler.onPointerDown.RemoveAllListeners();
            button.gameObject.SetActive(false);
        }
    }
}
