using UnityEngine;
using UnityEngine.UI;

namespace Tomino.View
{
    [ExecuteAlways]
    public class TextView : MonoBehaviour
    {
        public ThemeProvider themeProvider;
        public Text text;
        public ThemeColorName colorName;

        private void Update()
        {
            text.color = themeProvider.currentTheme.GetColor(colorName);
        }
    }
}
