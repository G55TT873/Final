using System.Collections.Generic;
using UnityEngine;

public class HighScoreManager : MonoBehaviour
{
    public static HighScoreManager Instance;

    private List<float> highScores = new List<float>();
    private const int MaxHighScores = 5;
    private string saveKey = "HighScores";

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            LoadHighScores();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SaveHighScore(float time)
    {
        highScores.Add(time);

        highScores.Sort((a, b) => b.CompareTo(a));

        if (highScores.Count > MaxHighScores)
        {
            highScores.RemoveAt(highScores.Count - 1);
        }

        SaveHighScores();
    }

    private void SaveHighScores()
    {
        for (int i = 0; i < highScores.Count; i++)
        {
            PlayerPrefs.SetFloat($"{saveKey}_{i}", highScores[i]);
        }
        PlayerPrefs.Save();
    }

    private void LoadHighScores()
    {
        highScores.Clear();

        for (int i = 0; i < MaxHighScores; i++)
        {
            if (PlayerPrefs.HasKey($"{saveKey}_{i}"))
            {
                highScores.Add(PlayerPrefs.GetFloat($"{saveKey}_{i}"));
            }
        }
    }

    public List<float> GetHighScores()
    {
        return highScores;
    }
}
