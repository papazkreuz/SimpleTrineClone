using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    [SerializeField] private Color _finishedColor;
    private Text _text;
    private Character _character;
    private float _timeAfterStart;
    private bool _isRunning;

    private void Start()
    {
        _character = FindObjectOfType<Character>();
        _text = GetComponent<Text>();

        _character.OnCharacterFinishedEvent += StopTimer;

        _timeAfterStart = 0;
        _isRunning = true;
    }

    private void Update()
    {
        if (_isRunning)
        {
            _timeAfterStart += Time.deltaTime;
            _text.text = _timeAfterStart.ToString("00.00");
        }
    }

    private void StopTimer()
    {
        _text.color = _finishedColor;
        _isRunning = false;
    }
}
