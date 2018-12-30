using UnityEngine;
using UnityEngine.UI;
using Tomino;

public class ScoreView : MonoBehaviour
{
    public Text scoreText;
    public Game game;

    void Update()
    {
        scoreText.text = string.Format(Constant.Text.Score, game.Score);
    }
}
