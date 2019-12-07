using UnityEngine;
using UnityEngine.UI;
using Tomino;

public class LevelView : MonoBehaviour
{
    public Text level;
    public Text lines;
    public Game game;

    void Update()
    {
        level.text = game.Level.Number.ToString();
        lines.text = game.Level.Lines.ToString();
    }
}
