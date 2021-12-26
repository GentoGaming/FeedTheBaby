using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour{
	[Header("Canvases")]
	[SerializeField] private GameObject _mainMenuCanvas = default;
	[SerializeField] private GameObject _creditsCanvas = default;
	[SerializeField] private GameObject _optionsCanvas = default;

	[Header("Options")]
	[SerializeField] private Options _options = default;
	[SerializeField] private Slider _masterVolumeSlider = default;
	[SerializeField] private AudioMixer _audioMixer = default;

	[SerializeField] private TutorialController _tutorial = null;
	
	private void Awake(){
		_masterVolumeSlider.value = _options.masterVolume;
		_audioMixer.SetFloat("MasterVolume", _options.masterVolume);
	}

	public void ToggleTutorial()
    {
	    _creditsCanvas.SetActive(false);
	    _optionsCanvas.SetActive(false);
	    _mainMenuCanvas.SetActive(false);
	    
		_tutorial.StartTutorial();
	}

	public void GoToScene(int sceneIndex){
		SceneManager.LoadScene(sceneIndex);
	}

	public void ExitToDesktop(){
		Application.Quit();
	}

	public void MainMenuCanvas(){
		_creditsCanvas.SetActive(false);
		_optionsCanvas.SetActive(false);
		
		_mainMenuCanvas.SetActive(true);
	}
	
	public void CreditCanvas(){
		_mainMenuCanvas.SetActive(false);
		_optionsCanvas.SetActive(false);
		
		_creditsCanvas.SetActive(true);
	}

	public void OptionsCanvas(){
		_mainMenuCanvas.SetActive(false);
		_creditsCanvas.SetActive(false);
		
		_optionsCanvas.SetActive(true);
	}

	public void SetMasterVolume(float volume){
		_audioMixer.SetFloat("MasterVolume", volume);
		_options.masterVolume = volume;
	}
}
