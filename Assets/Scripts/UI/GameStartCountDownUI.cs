using System;
using TMPro;
using UnityEngine;

public class GameStartCountDownUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI countDownText;

    private void Start()
    {
        GameManager.Instance.OnStateChange += GameManager_OnStateChange;
    }

    private void Update()
    {
        countDownText.text = Mathf.Ceil(GameManager.Instance.CountDownToStartTimer).ToString();
    }

    private void GameManager_OnStateChange(object sender, EventArgs e)
    {
        if (GameManager.Instance.CurrentGameState == GameState.CountDownToStart)
        {
            Show();
        }
        else
        {
            Hide();
        }
    }

    private void Show()
    {
        Debug.Log("Show");
        gameObject.SetActive(true);
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }
}
