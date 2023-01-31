using UnityEngine;

public class TrackZone : MonoBehaviour
{
    // Only the first Zone is the gate zone.
    [SerializeField] private bool isGate;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            CarController car = other.GetComponent<CarController>();
            car.currentTrackZone = this;
            car.zonesPassed++;

            if (isGate)
            {
                car.currentLap++;
                GameManager.instance.CheckIsWinner(car);
            }
        }
    }
}