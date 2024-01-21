using System;
using UnityEngine;

namespace Tomino.View
{
    public enum ThemeColorName
    {
        PanelBackgroundColor,
        PanelBorderColor,
        BoardBackgroundColor,
        BoardBorderColor,
        MenuBackgroundColor,
        MenuBorderColor,
        ButtonBorderColor,
        ButtonBackgroundColor,
        ButtonIconColor,
        ButtonTextColor,
        GameLabelTextColor,
        GameScoreTextColor,
        GameLevelTextColor,
        MenuTextColor,
        ScreenButtonColor,
        GameBackgroundColor
    }

    [CreateAssetMenu(fileName = "Theme", menuName = "Tomino/Theme", order = 1)]
    public class Theme: ScriptableObject
    {
        public Color blockColor1 = new(165 / 255.0f, 90 / 255.0f, 220 / 255.0f, 1);
        public Color blockColor2 = new(220 / 255.0f, 90 / 255.0f, 220 / 255.0f, 1);
        public Color blockColor3 = new(210 / 255.0f, 80 / 255.0f, 115 / 255.0f, 1);
        public Color blockColor4 = new(240 / 255.0f, 130 / 255.0f, 165 / 255.0f, 1);
        public Color blockColor5 = new(240 / 255.0f, 0 / 255.0f, 130 / 255.0f, 1);
        public Color blockColor6 = new(200 / 255.0f, 10 / 255.0f, 150 / 255.0f, 1);
        public Color blockColor7 = new(110 / 255.0f, 80 / 255.0f, 220 / 255.0f, 1);

        [Space(10)]
        public Color boardBackgroundColor = new(30 / 255.0f, 40 / 255.0f, 50 / 255.0f, 1);
        public Color boardBorderColor = new(55 / 255.0f, 135 / 255.0f, 225 / 255.0f, 1);
        public Color panelBackgroundColor = new(20 / 255.0f, 55 / 255.0f, 90 / 255.0f, 1);
        public Color panelBorderColor = new(55 / 255.0f, 135 / 255.0f, 225 / 255.0f, 1);
        public Color menuBackgroundColor = new(20 / 255.0f, 55 / 255.0f, 90 / 255.0f, 1);
        public Color menuBorderColor = new(55 / 255.0f, 135 / 255.0f, 225 / 255.0f, 1);
        public Color buttonBorderColor = Color.white;
        public Color buttonBackgroundColor = new(245 / 255.0f, 35 / 255.0f, 160 / 255.0f, 1);
        public Color buttonIconColor = Color.white;
        public Color buttonTextColor = Color.white;
        public Color gameLabelTextColor = new(245 / 255.0f, 35 / 255.0f, 160 / 255.0f, 1);
        public Color gameScoreTextColor = Color.white;
        public Color gameLevelTextColor = new(245 / 255.0f, 35 / 255.0f, 160 / 255.0f, 1);
        public Color toggleBorderColor = Color.white;
        public Color toggleBorderColorSelected = Color.white;
        public Color toggleIconColor = Color.white;
        public Color toggleIconColorSelected = Color.white;
        public Color toggleBackgroundColor = Color.clear;
        public Color toggleBackgroundColorSelected = new(245 / 255.0f, 35 / 255.0f, 160 / 255.0f, 1);
        public Color toggleTextColor = Color.white;
        public Color toggleTextColorSelected = Color.white;
        public Color menuTextColor = Color.white;
        public Color screenButtonColor = Color.white;
        public Color gameBackgroundColor = new(35 / 255.0f, 65 / 255.0f, 100 / 255.0f, 1);

        public Color[] BlockColors => new[]
        {
            blockColor1, blockColor2, blockColor3, blockColor4, blockColor5, blockColor6, blockColor7
        };

        public Color GetColor(ThemeColorName colorName)
        {
            return colorName switch
            {
                ThemeColorName.PanelBackgroundColor => panelBackgroundColor,
                ThemeColorName.PanelBorderColor => panelBorderColor,
                ThemeColorName.BoardBackgroundColor => boardBackgroundColor,
                ThemeColorName.BoardBorderColor => boardBorderColor,
                ThemeColorName.MenuBackgroundColor => menuBackgroundColor,
                ThemeColorName.MenuBorderColor => menuBorderColor,
                ThemeColorName.ButtonBorderColor => buttonBorderColor,
                ThemeColorName.ButtonBackgroundColor => buttonBackgroundColor,
                ThemeColorName.ButtonIconColor => buttonIconColor,
                ThemeColorName.ButtonTextColor => buttonTextColor,
                ThemeColorName.GameLabelTextColor => gameLabelTextColor,
                ThemeColorName.GameScoreTextColor => gameScoreTextColor,
                ThemeColorName.GameLevelTextColor => gameLevelTextColor,
                ThemeColorName.MenuTextColor => menuTextColor,
                ThemeColorName.ScreenButtonColor => screenButtonColor,
                ThemeColorName.GameBackgroundColor => gameBackgroundColor,
                _ => throw new ArgumentOutOfRangeException(nameof(colorName), colorName, null)
            };
        }
    }
}
