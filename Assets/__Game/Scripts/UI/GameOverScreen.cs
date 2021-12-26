using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverScreen : MonoBehaviour
{
	[SerializeField] private InGameMenu _inGameMenu = default;
	[SerializeField] private InputField _inputField = default;
	[SerializeField] private HighScore _highScore = default;
	
	public void UpdateHighScore(){
		string text = ForeUpperCase(_inputField.text);

		_highScore.AddScore((int)_inGameMenu._currentScore, text);
		
		Time.timeScale = 1;
		_inGameMenu.menuCanvas.SetActive(false);
		_inGameMenu.optionsCanvas.SetActive(false);
		_inGameMenu.gameOverScreen.SetActive(false);
		SceneManager.LoadScene(0);
	}

	private string ForeUpperCase(string text)
	{
		text = text.ToUpper();
		return text;
	}
}
