using UnityEngine;
using UnityEngine.UI;

namespace Tomino.View
{
    [ExecuteAlways]
    public class ImageView : MonoBehaviour
    {
        public ThemeProvider themeProvider;
        public Image image;
        public ThemeColorName colorName;

        private void Update()
        {
            image.color = themeProvider.currentTheme.GetColor(colorName);
        }
    }
}
