using UnityEngine;

[ExecuteAlways]
public class BoardBackgroundView : MonoBehaviour
{
    public PanelView panel;

    private void Update()
    {
        panel.backgroundImage.color = panel.themeProvider.currentTheme.boardBackgroundColor;
        panel.borderImage.color = panel.themeProvider.currentTheme.boardBorderColor;
    }
}
