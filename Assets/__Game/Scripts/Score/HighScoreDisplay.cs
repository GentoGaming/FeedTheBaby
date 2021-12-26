using UnityEngine;
using UnityEngine.UI;

public class HighScoreDisplay : MonoBehaviour
{
	[SerializeField] private HighScore _highScore = default;

	private Text _text = default;
	private string _scoreText = default;
	
	private void Awake()
	{
		_text = GetComponent<Text>();

		foreach (var score in _highScore.highScores)
		{
			_scoreText += $"{score.score} {score.name}\n";
		}

		_text.text = _scoreText;
	}
}