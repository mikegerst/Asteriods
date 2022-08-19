using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{

    [SerializeField] private GameObject startButton;
    [SerializeField] private GameObject scoreObject;

    TextMeshProUGUI _score;

    
    void Awake()
    {
        _score = scoreObject.GetComponent<TextMeshProUGUI>();
    }
    public void DisableStartButton()
    {
        startButton.SetActive(false);
    }

    public void EnableStartButton()
    {
        startButton.SetActive(true);
    }

    public void UpdateScoreText(int value)
    {
        _score.text = value.ToString();
    }

    private void OnEnable()
    {
        Player.PlayerDied += EnableStartButton;
        GameManager.ScoreChanged += UpdateScoreText;
    }

    private void OnDisable()
    {
        Player.PlayerDied -= EnableStartButton;
        GameManager.ScoreChanged -= UpdateScoreText;
    }
}
