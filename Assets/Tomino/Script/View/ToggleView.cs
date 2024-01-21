using Tomino.View;
using UnityEngine;
using UnityEngine.UI;

[ExecuteAlways]
public class ToggleView : MonoBehaviour
{
    public Toggle toggle;
    public Text label;
    public Image backgroundImage;
    public Image borderImage;
    public Image iconImage;
    public ThemeProvider themeProvider;

    private void Update()
    {
        var theme = themeProvider.currentTheme;
        backgroundImage.color = toggle.isOn ? theme.toggleBackgroundColorSelected : theme.toggleBackgroundColor;
        borderImage.color = toggle.isOn ? theme.toggleBorderColorSelected : theme.toggleBorderColor;
        iconImage.color = toggle.isOn ? theme.toggleIconColorSelected : theme.toggleIconColor;
        label.color = toggle.isOn ? theme.toggleTextColorSelected : theme.toggleTextColor;
    }
}
