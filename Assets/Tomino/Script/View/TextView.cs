using Tomino.View;
using UnityEngine;
using UnityEngine.UI;

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
