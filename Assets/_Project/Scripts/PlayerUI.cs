using System.Collections;
using TMPro;
using UnityEngine;

public class PlayerUI : MonoBehaviour
{
    public CarController car;

    [SerializeField] private TextMeshProUGUI _carPositionText;
    [SerializeField] private TextMeshProUGUI countdownText;
    [SerializeField] private TextMeshProUGUI gameOverText;

    private readonly WaitForSeconds _waitForOne = new(1);
    private readonly WaitForSeconds _waitForPointOne= new(0.1f);

    private void Update()
    {
        _carPositionText.text = car.racePosition.ToString() + " / " + GameManager.instance.cars.Count.ToString();
    }

    // TODO: Clean this code up.
    public void StartCountdownDisplay()
    {
        StartCoroutine(CountdownCoroutine());

        IEnumerator CountdownCoroutine()
        {
            countdownText.gameObject.SetActive(true);
            countdownText.text = "3";
            yield return _waitForOne;
            countdownText.text = "2";
            yield return _waitForOne;
            countdownText.text = "1";
            yield return _waitForOne;
            countdownText.text = "GO!";
            yield return _waitForPointOne;
            countdownText.alpha = 0.9f;
            yield return _waitForPointOne;
            countdownText.alpha = 0.8f;
            yield return _waitForPointOne;
            countdownText.alpha = 0.7f;
            yield return _waitForPointOne;
            countdownText.alpha = 0.6f;
            yield return _waitForPointOne;
            countdownText.alpha = 0.5f;
            yield return _waitForPointOne;
            countdownText.alpha = 0.4f;
            yield return _waitForPointOne;
            countdownText.alpha = 0.3f;
            yield return _waitForPointOne;
            countdownText.alpha = 0.2f;
            yield return _waitForPointOne;
            countdownText.alpha = 0.1f;
            yield return _waitForPointOne;
            countdownText.alpha = 0;
            yield return _waitForPointOne;
            countdownText.gameObject.SetActive(false);
        }
    }

    public void GameOver(bool winner)
    {
        gameOverText.gameObject.SetActive(true);
        gameOverText.color = winner == true ? Color.green : Color.red;
        gameOverText.text = winner == true ? "You Win!" : "You Lose!";
    }
}