using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;
using TMPro;
public class GameManager : MonoBehaviour
{
    public static Action onGameStarted;
    public static Action onGameEnded;
    public static GameManager instance;
    public CameraFollower cameraFollower;
    public GameObject losePanel;
    public GameObject inGameUI;
    private bool isGameStarted;
    private int score;
    public TMP_Text scoreBar;
    public TMP_Text bestScoreBar;
    public TMP_Text moneyBar;
    private float currentTimeScale;
    private int money;
    public GameObject shield;
    public GameObject hat;
    public GameObject skirt;
    public GameObject topHat;
    public GameObject goldRing;
    private void Awake()
    {
        instance = this;
        if (!PlayerPrefs.HasKey("Money"))
        {
            PlayerPrefs.SetInt("Money", 0);
            PlayerPrefs.Save();
        }
        if (PlayerPrefs.GetInt("Shield")==1)
        {
            shield.SetActive(true);
        }
        UpdateMoney();
    }
    private void OnDisable()
    {
    }
    private void Start()
    {
        currentTimeScale = 1;
        Time.timeScale = 1;
        CheckBuy();
    }
    private void UpdateMoney()
    {
        money = PlayerPrefs.GetInt("Money");
        moneyBar.text = money.ToString();
        Debug.Log(PlayerPrefs.GetInt("Money"));
    }
    public void CheckBuy()
    {
        if (PlayerPrefs.HasKey("Buy1"))
        {
            hat.SetActive(true);
        }
        if (PlayerPrefs.HasKey("Buy2"))
        {
            skirt.SetActive(true);
        }
        if (PlayerPrefs.HasKey("Buy3"))
        {
            topHat.SetActive(true);
        }
        if (PlayerPrefs.HasKey("Buy4"))
        {
            goldRing.SetActive(true);
        }
    }
    private void FixedUpdate()
    {
        if (isGameStarted)
        {
            score++;
            scoreBar.text ="Score: "+ score.ToString();
            currentTimeScale += 0.0002f;
            Time.timeScale = currentTimeScale;
        }
        else
        {
            
        }
        if (Input.GetKeyDown(KeyCode.Z))
        {
            PlayerPrefs.SetInt("Money", 100);
            PlayerPrefs.Save();
            UpdateMoney();
        }
    }
    public void EndGame()
    {
        inGameUI.SetActive(false);
        losePanel.SetActive(true);
        onGameEnded?.Invoke();
        CheckBestScore();
        isGameStarted = false;
    }
    public void AddMoney()
    {
        PlayerPrefs.SetInt("Money", PlayerPrefs.GetInt("Money") + 1);
        PlayerPrefs.Save();
        Debug.Log("Money+1");
        UpdateMoney();
    }
    public void BuyShield()
    {
        if (PlayerPrefs.GetInt("Money")>=10 && PlayerPrefs.GetInt("Shield")==0)
        {
            PlayerPrefs.SetInt("Money", PlayerPrefs.GetInt("Money") - 10);
            PlayerPrefs.SetInt("Shield", 1);
            PlayerPrefs.Save();
            UpdateMoney();
            shield.SetActive(true);
        }
        else
        {
            Debug.Log("Shield brought");
        }
    }
    private void CheckBestScore()
    {
        if (PlayerPrefs.HasKey("BestScore"))
        {
            if (PlayerPrefs.GetInt("BestScore")<score)
            {
                bestScoreBar.text =score.ToString();
                PlayerPrefs.SetInt("BestScore", score);
            }
            else
            {
                bestScoreBar.text = PlayerPrefs.GetInt("BestScore").ToString();
            }
        }
        else
        {
            PlayerPrefs.SetInt("BestScore", score);
            bestScoreBar.text =  score.ToString();
        }
    }    
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public bool IsGameStarted()
    {
        return isGameStarted;
    }
    public void StartGame()
    {
        isGameStarted = true;
        onGameStarted?.Invoke();
        shield.transform.localPosition = new Vector3(shield.transform.localPosition.x, shield.transform.localPosition.y, -0.104f);
        cameraFollower.enabled = true;
        cameraFollower.StartShow();
    }
    public IEnumerator ShowLosePanel()
    {
        yield return new WaitForSeconds(1f);
        EndGame();
    }
    public void PauseGame()
    {
        Time.timeScale = 0;
    }
    public void UnPauseGame()
    {
        Time.timeScale = currentTimeScale;
    }
}
