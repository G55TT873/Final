using UnityEngine;
using UnityEngine.UI;

public class VolumeManager : MonoBehaviour
{
    private const string VolumePrefKey = "GameVolume";
    private static bool instanceExists = false;
    private Slider volumeSlider;

    private void Awake()
    {
        if (!instanceExists)
        {
            DontDestroyOnLoad(gameObject);
            instanceExists = true;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        LoadVolume();
        FindSliderInScene();
    }

    private void Update()
    {
        if (volumeSlider == null)
        {
            FindSliderInScene();
        }
    }

    public void SetVolume(float volume)
    {
        AudioListener.volume = volume;
        SaveVolume(volume);

        if (volumeSlider != null)
        {
            volumeSlider.value = volume;
        }
    }

    private void SaveVolume(float volume)
    {
        PlayerPrefs.SetFloat(VolumePrefKey, volume);
        PlayerPrefs.Save();
    }

    private void LoadVolume()
    {
        if (PlayerPrefs.HasKey(VolumePrefKey))
        {
            float savedVolume = PlayerPrefs.GetFloat(VolumePrefKey);
            AudioListener.volume = savedVolume;
        }
        else
        {
            AudioListener.volume = 1f;
        }
    }

    private void FindSliderInScene()
    {
        GameObject sliderObject = GameObject.Find("SVolume");
        if (sliderObject != null)
        {
            volumeSlider = sliderObject.GetComponent<Slider>();
            if (volumeSlider != null)
            {
                volumeSlider.value = AudioListener.volume;
                volumeSlider.onValueChanged.AddListener(SetVolume);
            }
            else
            {
                Debug.LogError("GameObject 'SVolume' found but does not have a Slider component.");
            }
        }
    }
}
