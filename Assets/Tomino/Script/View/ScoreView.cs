using Constant;
using Tomino;
using UnityEngine;
using Text = UnityEngine.UI.Text;

public class ScoreView : MonoBehaviour
{
    public Text scoreText;
    public Game game;

    internal void Update()
    {
        var padLength = ScoreFormat.Length;
        var padCharacter = ScoreFormat.PadCharacter;
        scoreText.text = game.Score.Value.ToString().PadLeft(padLength, padCharacter);
    }
}
