using UnityEngine;

namespace Tomino.View
{
    [CreateAssetMenu(fileName = "Theme", menuName = "Tomino/ThemeProvider", order = 1)]
    public class ThemeProvider: ScriptableObject
    {
        public Theme currentTheme;
    }
}
