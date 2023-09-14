using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CooldownDisplayer : MonoBehaviour
{
    private Image _cooldownTint;
    private float _time;
    private float _durationSeconds;
    private bool _inCooldown;

    private void Start()
    {
        _cooldownTint = GetComponent<Image>();
    }

    private void Update()
    {
        if (!_inCooldown)
            return;

        _time -= Time.deltaTime;
        
        if (_time <= 0f)
        {
            _time = 0f;
            _inCooldown = false;
        }
        
        _cooldownTint.fillAmount = _time / _durationSeconds;
    }

    public void StartCooldown(float durationSeconds)
    {
        _time = durationSeconds;
        _durationSeconds = durationSeconds;
        _inCooldown = true;
    }
}
