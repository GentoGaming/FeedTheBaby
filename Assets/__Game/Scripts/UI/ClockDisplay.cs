using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClockDisplay : MonoBehaviour
{
    [SerializeField] private Image[] _timeDisply = new Image[4];

    [SerializeField] private Sprite[] numbers = new Sprite[10];
    [SerializeField,Range(0, 300)] int _currentTime = 300;

    private void Update()
    {

        int seconds = _currentTime % 60;
        int minutes = (_currentTime - seconds) / 60;
        char[] minChar = minutes.ToString().ToCharArray();
        Debug.Log("Minutes " + minChar.ToString());
        char[] secChar = seconds.ToString().ToCharArray();
        Debug.Log("seconds " + secChar.ToString());

        for(int i = 0; i < 2; i++)
        {
            try { 
            _timeDisply[i].sprite = numbers[(int)Char.GetNumericValue(minChar[i])];
            }
            catch (Exception e)
            {
                Debug.LogError("Unlucky " + minChar[i]);
            }
            try
            {
                _timeDisply[i+2].sprite = numbers[(int)Char.GetNumericValue(secChar[i])];
            }
            catch (Exception e)
            {
                Debug.LogError("Unlucky times 2 " + secChar[i]);
            }
        }
    }
}
