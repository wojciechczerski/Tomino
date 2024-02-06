using Tomino.Model;
using UnityEngine;
using UnityEngine.UI;
using Text = UnityEngine.UI.Text;

namespace Tomino.View
{
    [ExecuteAlways]
    public class ToggleView : MonoBehaviour
    {
        public Toggle toggle;
        public Text label;
        public string textID;
        public Image backgroundImage;
        public Image borderImage;
        public Image iconImage;
        public ThemeProvider themeProvider;
        public LocalizationProvider localizationProvider;

        private void Update()
        {
            var theme = themeProvider.currentTheme;
            backgroundImage.color = toggle.isOn ? theme.toggleBackgroundColorSelected : theme.toggleBackgroundColor;
            borderImage.color = toggle.isOn ? theme.toggleBorderColorSelected : theme.toggleBorderColor;
            iconImage.color = toggle.isOn ? theme.toggleIconColorSelected : theme.toggleIconColor;
            label.color = toggle.isOn ? theme.toggleTextColorSelected : theme.toggleTextColor;

            if (!string.IsNullOrEmpty(textID))
            {
                label.text = localizationProvider.currentLocalization.GetLocalizedTextForID(textID);
            }
        }
    }
}
