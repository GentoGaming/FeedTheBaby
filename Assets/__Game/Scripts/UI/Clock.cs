using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Clock : MonoBehaviour{
	[SerializeField] private Text _clockText;
	[SerializeField] private int _minutes;
	[SerializeField] private int _seconds;

	private string _minuteString;
	private string _secondString;

	private void Start()
	{
		SetClock();
		StartCoroutine(Timer());
	}

	private IEnumerator Timer()
	{
		var wait = new WaitForSecondsRealtime(1f);
		
		while (_minutes > 0 || _seconds > 0)
		{
			yield return wait;

			if (Time.timeScale != 0)
			{
				UpdateClock();
				_clockText.text = $"{_minuteString}:{_secondString}";	
			}
		}
		
		EndGameEventInfo egei = new EndGameEventInfo();
		EventManager.SendNewEvent(egei);
	}

	private void SetClock()
	{
		if (_seconds > 59)
		{
			while (_seconds > 59)
			{
				_minutes++;
				_seconds -= 60;
			}
		}
		
		_secondString = _seconds > 9 ? _seconds.ToString() : $"0{_seconds}";
		_minuteString = _minutes > 9 ? _minutes.ToString() : $"0{_minutes}";
		
		_clockText.text = $"{_minuteString}:{_secondString}";
	}
	
	private void UpdateClock()
	{
		_seconds--;
		if (_seconds < 0)
		{
			_seconds = 59;
			_minutes--;
		}

		_secondString = _seconds > 9 ? _seconds.ToString() : $"0{_seconds}";
		_minuteString = _minutes > 9 ? _minutes.ToString() : $"0{_minutes}";
	}
}
