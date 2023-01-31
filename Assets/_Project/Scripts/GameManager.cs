using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public Transform[] spawnPoints;
    public List<CarController> cars = new List<CarController>();

    [SerializeField] private bool _gameStarted = false;
    [SerializeField] private int _playersToBegin;
    [SerializeField] private int _lapsToWin;
    [SerializeField] private float _positionUpdateRate;

    private float _lastPositionUpdateTime;

    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        if(Time.time - _lastPositionUpdateTime > _positionUpdateRate)
        {
            _lastPositionUpdateTime = Time.time;
            UpdateCarRacePositions();
        }

        if(!_gameStarted && cars.Count == _playersToBegin)
        {
            _gameStarted = true;
            StartCountdown();
        }
    }

    public void CheckIsWinner(CarController car)
    {
        if(car.currentLap == _lapsToWin + 1)
        {
            for (int i = 0; i < cars.Count; i++)
                cars[i].canControlCar = false;

            PlayerUI[] uiObjects = FindObjectsOfType<PlayerUI>();

            for (int i = 0; i < uiObjects.Length; i++)
                uiObjects[i].GameOver(uiObjects[i].car == car);
        }
    }

    private void StartCountdown()
    {
        PlayerUI[] uiObjects = FindObjectsOfType<PlayerUI>();

        for (int i = 0; i < uiObjects.Length; i++)
            uiObjects[i].StartCountdownDisplay();

        Invoke(nameof(BeginGame), 3);
    }

    private void BeginGame()
    {
        for (int i = 0; i < cars.Count; i++)
            cars[i].canControlCar = true;
    }

    private void UpdateCarRacePositions()
    {
        cars.Sort(SortPosition);

        for (int i = 0; i < cars.Count; i++)
            cars[i].racePosition = cars.Count - i;
    }

    private int SortPosition(CarController a, CarController b)
    {
        if (a.zonesPassed > b.zonesPassed)
            return 1;
        else if(b.zonesPassed > a.zonesPassed)
            return -1;

        float aDist = Vector3.Distance(a.transform.position, a.currentTrackZone.transform.position);
        float bDist = Vector3.Distance(b.transform.position, b.currentTrackZone.transform.position);

        return aDist > bDist ? 1 : -1;
    }
}