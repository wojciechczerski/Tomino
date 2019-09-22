using UnityEngine;
using UnityEngine.UI;
using Tomino;

public class ScoreView : MonoBehaviour
{
    public Text scoreText;
    public Game game;

    void Update()
    {
        var padLength = Constant.ScoreFormat.Length;
        var padCharacter = Constant.ScoreFormat.PadCharacter;
        scoreText.text = game.Score.Value.ToString().PadLeft(padLength, padCharacter);
    }
}
