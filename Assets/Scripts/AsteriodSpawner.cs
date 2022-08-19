using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteriodSpawner : MonoBehaviour
{
    [SerializeField] private List<GameObject> asteriodsToSpawn;
    [SerializeField] private int maxAsteriods = 3;
    List<GameObject> _currentAsteriods = new List<GameObject>();

    int side = 0;

    WaitForSeconds waitForSeconds = new WaitForSeconds(1f);
    Coroutine spawning;

    IEnumerator SpawnAsteriod()
    {
        while (this)
        {
            print(_currentAsteriods.Count);
            if (_currentAsteriods.Count - 1 > maxAsteriods)
            {
                yield return waitForSeconds;
                continue;
            }
                
            Vector3? asteriodStartPoint = null;
            print("Creating New asteriod");
            switch (side)
            {
                case 0:
                    var w = ScreenBorder.Width / 2;
                    asteriodStartPoint = new Vector3(Random.Range(-w, w), ScreenBorder.Height, 0);
                    break;
                case 1:
                    var h = ScreenBorder.Height / 2;
                    asteriodStartPoint = new Vector3(ScreenBorder.Width, Random.Range(-h, h), 0);
                    break;
                case 2:
                    w = ScreenBorder.Width / 2;
                    asteriodStartPoint = new Vector3(Random.Range(-w, w), -ScreenBorder.Height, 0);
                    break;
                case 3:
                    h = ScreenBorder.Height / 2;
                    asteriodStartPoint = new Vector3(-ScreenBorder.Width, Random.Range(-h, h), 0);
                    break;
                default:
                    asteriodStartPoint = Vector3.zero;
                    break;
            }
            var asteriod = Instantiate(asteriodsToSpawn[Random.Range(0, asteriodsToSpawn.Count - 1)], (Vector3)asteriodStartPoint, Quaternion.identity);
            asteriod.transform.parent = transform;
            _currentAsteriods.Add(asteriod);
            side++;
            if (side > 3)
                side = 0;
            yield return waitForSeconds;
        }

    }

    void AddToCurrentAsteriodsList(GameObject asteriod)
    {
        _currentAsteriods.Add(asteriod);
    }

    void RemoveFromCurrentAsteriodList(GameObject asterdiod)
    {
        _currentAsteriods.Remove(asterdiod);
    }

    void StartSpawning()
    {
        print("START SPAWNING ASTERIODS");
        spawning = StartCoroutine(SpawnAsteriod());
    }

    void StopSpawning()
    {
        print("STOP SPAWNING ASTERIODS");
        StopCoroutine(spawning);
    }

    private void OnEnable()
    {
        GameManager.GameStarted += StartSpawning;
        Player.PlayerDied += StopSpawning;
        Asteriod.asteriodSplit += AddToCurrentAsteriodsList;
        Asteriod.asteriodDestoryed += RemoveFromCurrentAsteriodList;
    }

    private void OnDisable()
    {
        GameManager.GameStarted -= StartSpawning;
        Player.PlayerDied -= StopSpawning;
        Asteriod.asteriodSplit -= AddToCurrentAsteriodsList;
        Asteriod.asteriodDestoryed -= RemoveFromCurrentAsteriodList;
    }


}
