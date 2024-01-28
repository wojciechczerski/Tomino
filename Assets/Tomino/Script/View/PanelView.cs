using UnityEngine;
using UnityEngine.UI;

namespace Tomino.View
{
    [ExecuteAlways]
    public class PanelView : MonoBehaviour
    {
        public ThemeProvider themeProvider;
        public Image borderImage;
        public Image backgroundImage;
        public ThemeColorName borderColorName;
        public ThemeColorName backgroundColorName;

        private void Update()
        {
            backgroundImage.color = themeProvider.currentTheme.GetColor(backgroundColorName);
            borderImage.color = themeProvider.currentTheme.GetColor(borderColorName);
        }
    }
}
