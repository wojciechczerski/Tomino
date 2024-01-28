using Tomino.Shared;
using UnityEngine;
using UnityEngine.UI;

namespace Tomino.View
{
    public class AlertButtonView: MonoBehaviour
    {
        public RectTransform RectTransform { get; private set; }
        public Button Button { get; private set; }
        public Text Text { get; private set; }
        public PointerHandler PointerHandler { get; private set; }

        private void Awake()
        {
            RectTransform = GetComponent<RectTransform>();
            Button = GetComponent<Button>();
            Text = GetComponentInChildren<Text>();
            PointerHandler = GetComponent<PointerHandler>();
        }
    }
}
