using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private int pointsForAsteriod = 1;
    [SerializeField] private int pointsForAsteriodSecondGen = 5;


    private int _score;

    private bool _gameOn = false;

    public static event Action GameStarted;

    public static event Action<int> ScoreChanged;

    public bool GameOn
    {
        get => _gameOn;
    }



    public void StartGame()
    {
        Instantiate(player, Vector3.zero, Quaternion.identity);
        UpdateScore(0);
        GameStarted?.Invoke();

    }

    private void UpdateScore(GameObject asteriod)
    {
        if (asteriod.GetComponent<Asteriod>().IsSecondGen())
            _score += pointsForAsteriodSecondGen;
        else
            _score += pointsForAsteriod;
        ScoreChanged?.Invoke(_score);
    }

    private void UpdateScore(int score)
    {
        _score = score;
        ScoreChanged?.Invoke(_score);
    }


    private void OnEnable()
    {
        Asteriod.asteriodDestoryed += UpdateScore;
    }

}
