using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private TMP_Text highScoreText;
    [SerializeField] private TMP_Text energyText;
    [SerializeField] private Button playButton;

    [SerializeField] private AndroidNotificationHandler androidNotificationHandler;
    [SerializeField] private int maxEnergy;

    [SerializeField] private int evenryRechargeDuration;

    private int energy;

    private const string EvenrgyKey = "Energy";
    private const string EvenrgyReadyKey = "EnergyReady";

    private void Start()
    {
        OnApplicationFocus(true);
    }

    private void OnApplicationFocus(bool focus)
    {
        if(!focus)
            return;

        CancelInvoke();

        int highScore = PlayerPrefs.GetInt(ScoreSystem.HighScoreKey,0);
        highScoreText.text = $"High Score : {highScore:000}";

        energy = PlayerPrefs.GetInt(EvenrgyKey,maxEnergy);
        if(energy == 0)
        {
            string evenryReadyString = PlayerPrefs.GetString(EvenrgyReadyKey,string.Empty);
            if(evenryReadyString == string.Empty)
                return;

            DateTime enenrgyReady = DateTime.Parse(evenryReadyString);

            if(DateTime.Now > enenrgyReady)
            {
                energy = maxEnergy;
                PlayerPrefs.SetInt(EvenrgyKey,energy);
            }
            else
            {
                playButton.interactable = false;
                Invoke(nameof(EnergyRecharged),(enenrgyReady - DateTime.Now).Seconds);
            }
        }
        energyText.text = $"Play ({energy})";
    }

    private void EnergyRecharged()
    {
        playButton.interactable = true;
        energy = maxEnergy;
        PlayerPrefs.SetInt(EvenrgyKey,energy);
        energyText.text = $"Play ({energy})";
    }

    public void Play()
    {
        if(energy <= 0)
            return;

        energy--;
        PlayerPrefs.SetInt(EvenrgyKey,energy);

        if(energy == 0)
        {
            DateTime energyReady = DateTime.Now.AddMinutes(evenryRechargeDuration);
            PlayerPrefs.SetString(EvenrgyReadyKey,energyReady.ToString());
#if UNITY_ANDROID
            androidNotificationHandler.ScheduleNotification(energyReady);
#endif
        }

        SceneManager.LoadScene("Game");
    }
}