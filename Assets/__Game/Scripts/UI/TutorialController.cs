using UnityEngine;
using UnityEngine.UI;

public class TutorialController : MonoBehaviour
{
    [SerializeField] private GameObject _panel = null;
    [SerializeField] private MainMenu _mainMenu = null;

    [SerializeField] private Image _display = null;
    [SerializeField] private Sprite[] _chapters = new Sprite[0];

    int _currentIndex = 0;


    public void Previous()
    {
        _currentIndex--;
        if (_currentIndex <= 0)
        {
            _currentIndex = 0;
        }

        _display.sprite = _chapters[_currentIndex];
    }

    public void StartTutorial()
    {
        _panel.SetActive(true);
        _currentIndex = 0;
        _display.sprite = _chapters[_currentIndex];
    }

    public void Play()
    {
        _panel.SetActive(false);
        _currentIndex = 0;
        _display.sprite = _chapters[_currentIndex];
        _mainMenu.GoToScene(1);
    }

    public void Next()
    {
        _currentIndex++;
        if(_currentIndex >= _chapters.Length)
        {
            _currentIndex = _chapters.Length - 1;
        }
        _display.sprite = _chapters[_currentIndex];

    }
}
