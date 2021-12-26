using System;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[DefaultExecutionOrder(-90)]
public class InGameMenu : MonoBehaviour{
	private static InGameMenu Instance;
	
	[Header("Paused")] 
	[SerializeField] private bool _paused = false;
	
	[Header("Canvases")]
	public GameObject menuCanvas = default;
	public GameObject optionsCanvas = default;
	public GameObject gameOverScreen = default;

	[Header("Options")]
	[SerializeField] private Options _options = default;
	[SerializeField] private Slider _masterVolumeSlider = default;
	[SerializeField] private AudioMixer _audioMixer = default;

	[SerializeField] private Text _fruitField = null;
	[SerializeField] private Text _scoreField = null;

	[NonSerialized] public float _currentScore = 0;
	private float _currentFruit = 0;
	
	private void Awake()
	{
		if (Instance) Destroy(gameObject);
		else Instance = this;

		_currentFruit = 0;
		
		//DontDestroyOnLoad(gameObject);

		_masterVolumeSlider.value = _options.masterVolume;
		_audioMixer.SetFloat("MasterVolume", _options.masterVolume);
	}

	private void OnEnable()
	{
		EventManager.RegisterListener<AssignFruitEventInfo>(AddFruit);
		EventManager.RegisterListener<KidnappedFruitEventInfo>(LoseFruit);
		EventManager.RegisterListener<IncreasePlayerScoreEventInfo>(IncreaseScore);
		EventManager.RegisterListener<PauseEventInfo>(Pause);
		EventManager.RegisterListener<EndGameEventInfo>(EndGame);
	}

	private void OnDisable()
	{
		EventManager.UnregisterListener<PauseEventInfo>(Pause);
		EventManager.UnregisterListener<AssignFruitEventInfo>(AddFruit);
		EventManager.UnregisterListener<KidnappedFruitEventInfo>(LoseFruit);
		EventManager.UnregisterListener<IncreasePlayerScoreEventInfo>(IncreaseScore);
		EventManager.UnregisterListener<EndGameEventInfo>(EndGame);
	}

	public void GoToScene(int sceneIndex)
	{
		menuCanvas.SetActive(false);
		optionsCanvas.SetActive(false);

		Time.timeScale = 1;
		
		SceneManager.LoadScene(sceneIndex);
	}

	public void SetMasterVolume(float volume)
	{
		_audioMixer.SetFloat("MasterVolume", volume);
		_options.masterVolume = volume;
	}

	public void MenuCanvas()
	{
		optionsCanvas.SetActive(false);
		menuCanvas.SetActive(true);
	}

	public void OptionCanvas()
	{
		menuCanvas.SetActive(false);
		optionsCanvas.SetActive(true);
	}

	private void Pause(EventInfo ei)
	{
		int i = SceneManager.GetActiveScene().buildIndex;
		if(i == 0) return;
		
		if(_paused)
		{
			Resume();
			return;
		}

		_paused = true;
		Time.timeScale = 0;
		MenuCanvas();
	}

	public void Resume()
	{
		Time.timeScale = 1;
		_paused = false;
		
		menuCanvas.SetActive(false);
		optionsCanvas.SetActive(false);
	}

	void IncreaseScore(EventInfo ei)
    {
		IncreasePlayerScoreEventInfo Ipsei = (IncreasePlayerScoreEventInfo)ei;
		_currentScore += Ipsei.Score;

		_scoreField.text = $"{_currentScore}";
	}

	void LoseFruit(EventInfo ei)
	{
		_currentFruit --;
		_fruitField.text = $"{_currentFruit}";

		if (_currentFruit < 6)
		{
			if(SoundManager.SoundManagerInstance) 
				SoundManager.SoundManagerInstance.FastMusic();
		}
		
		if(_currentFruit < 1){
			if(SoundManager.SoundManagerInstance) 
				SoundManager.SoundManagerInstance.EndMusic();
			
			GameOver();
		}
	}

	void AddFruit(EventInfo ei)
	{
		_currentFruit++;
		_fruitField.text = $"{_currentFruit}";
	}

	private void EndGame(EventInfo ei)
	{
		GameOver();
	}
	
	private void GameOver()
	{
		_paused = true;

		_currentScore *= (_currentFruit * 0.01f + 1);
		_currentScore = (int) _currentScore;
		_scoreField.text = $"{_currentScore}";
		
		Time.timeScale = 0;
		
		menuCanvas.SetActive(false);
		optionsCanvas.SetActive(false);
		gameOverScreen.SetActive(true);
	}
}
