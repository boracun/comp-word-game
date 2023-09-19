using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TimeManager : MonoBehaviour
{
    public static TimeManager Instance { get; private set; }
    
    [SerializeField] private float initialTime;     // in seconds
    [SerializeField] private GameObject gameOverPanelGO;
    
    private bool _paused = true;
    private float _time;
    private TextMeshProUGUI _timerText;
    private Image _timerBackground;

    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(this);
        else
            Instance = this;
    }

    private void Start()
    {
        _timerText = transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        _timerBackground = GetComponent<Image>();
    }

    private void Update()
    {
        if (_paused)
            return;

        _time -= Time.deltaTime;
        DisplayTime();

        if (_time > 0f)
            return;
        
        // Operations after the time runs out
        gameOverPanelGO.SetActive(true);
        ScoreManager.Instance.SaveScore();
        _paused = true;
    }

    public void StartTimer()
    {
        _time = initialTime;
        _paused = false;
    }

    private void DisplayTime()
    {
        int minutes = (int) _time / 60;
        int seconds = (int) _time % 60;

        if (seconds < 10)
            _timerText.text = minutes + ":0" + seconds;
        else
            _timerText.text = minutes + ":" + seconds;

        if (minutes > 0 || seconds > 20)
            return;

        _timerBackground.color = new Color(1f, 0f, 0f, 0.27f);
    }
}
