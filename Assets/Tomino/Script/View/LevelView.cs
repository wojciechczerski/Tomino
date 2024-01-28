using UnityEngine;
using UnityEngine.UI;

namespace Tomino.View
{
    public class LevelView : MonoBehaviour
    {
        public Text level;
        public Text lines;
        public Game game;

        internal void Update()
        {
            level.text = game.Level.Number.ToString();
            lines.text = game.Level.Lines.ToString();
        }
    }
}
