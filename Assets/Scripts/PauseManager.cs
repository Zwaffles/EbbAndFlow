using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Audio;
using UnityEngine.UI;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class PauseManager : MonoBehaviour
{
    [Header("Pause Menu")]
    [SerializeField] private GameObject pauseMenuPanel;
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject optionsMenu;

    [Header("Game Settings")]
    [SerializeField] private TMP_Dropdown overallQuality;
    [SerializeField] private TMP_Dropdown resolution;
    [SerializeField] private Toggle fullscreen;

    [Header("Audio Settings")]
    [SerializeField] private Slider masterSlider;
    [SerializeField] private Slider sfxSlider;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider uiSlider;
    public AudioMixer audioMixer;

    [Header("Debug")]
    public bool debug;

    private Resolution[] resolutions;

    private bool isPaused;

    private void Start()
    {
        GetResolutions();
        if (!File.Exists(Application.persistentDataPath + "/Settings/VideoSettings.txt"))
        {
            DefaultVideoSettings();
        }
        else
        {
            LoadVideoSettings();
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseUnpause();
        }
    }

    public void PauseUnpause()
    {
        /* UnPause */
        if (isPaused)
        {
            /* Backtrack thing */
            if (optionsMenu.activeSelf)
            {
                ToggleOptionsMenu();
            }
            /* UnPause */
            else
            {
                Time.timeScale = 1;
                isPaused = false;
                pauseMenuPanel.SetActive(false);
            }
        }
        /* Pause */
        else
        {
            Time.timeScale = 0;
            isPaused = true;
            pauseMenuPanel.SetActive(true);
        }
    }

    public void TogglePauseMenu()
    {
        /* Deactivate */
        if (pauseMenu.activeSelf)
        {
            pauseMenu.SetActive(false);
            optionsMenu.SetActive(true);
        }
        /* Activate */
        else
        {
            optionsMenu.SetActive(false);
            pauseMenu.SetActive(true);
        }
    }

    public void ToggleOptionsMenu()
    {
        /* Deactivate */
        if (optionsMenu.activeSelf)
        {
            optionsMenu.SetActive(false);
            pauseMenu.SetActive(true);
        }
        /* Activate */
        else
        {
            pauseMenu.SetActive(false);
            optionsMenu.SetActive(true);
        }
    }

    #region Game Settings
    private void GetResolutions()
    {
        resolutions = Screen.resolutions;
        resolution.ClearOptions();

        List<string> options = new List<string>();
        int currentResolutionIndex = 0;

        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + "x" + resolutions[i].height;
            options.Add(option);

            if (resolutions[i].width == Screen.currentResolution.width &&
                resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }

        resolution.AddOptions(options);
        resolution.value = currentResolutionIndex;
        resolution.RefreshShownValue();
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    public void SetResolution(int width, int height)
    {
        for (int i = 0; i < resolutions.Length; i++)
        {
            if (resolutions[i].width == width &&
                resolutions[i].height == height)
            {
                Screen.SetResolution(width, height, Screen.fullScreen);
                resolution.value = i;
                resolution.RefreshShownValue();
            }
        }
    }

    private string ResolutionToString(Resolution resolution)
    {
        return resolution.width + "x" + resolution.height;
    }

    public void FullscreenToggle(bool enabled)
    {
        Screen.fullScreen = enabled;
        fullscreen.isOn = enabled;
    }

    public void SetQuality(int qualityIndex)
    {
        overallQuality.value = qualityIndex;
        overallQuality.RefreshShownValue();

        switch (qualityIndex)
        {
            case 0: /* Low */
                QualitySettings.SetQualityLevel(1);
                return;
            case 1: /* Medium */
                QualitySettings.SetQualityLevel(2);
                return;
            case 2: /* High */
                QualitySettings.SetQualityLevel(3);
                return;
            case 3: /* Ultra */
                QualitySettings.SetQualityLevel(5);
                return;
            default:
                Debug.Log("No QualityLevel of " + qualityIndex + " available");
                return;
        }
    }
    #endregion

    #region Audio Settings
    private float ConvertFMODVolume(float vol, bool fromSavefile)
    {
        if (fromSavefile)
        {
            return (vol * 100);
        }
        else
        {
            return (vol / 100);
        }
    }

    private float ConvertMixerVolume(float vol, bool fromSavefile)
    {
        if (fromSavefile)
        {
            return ((vol * 100) - 80);
        }
        else
        {
            return (vol - 80);
        }
    }

    public void SetMasterVolume(float value)
    {
        masterSlider.value = value;
        FMODUnity.RuntimeManager.GetVCA("vca:/Master").setVolume(ConvertFMODVolume(value, false));
        audioMixer.SetFloat("MasterVolume", ConvertMixerVolume(value, false));
    }

    private void LoadMasterVolume(float value)
    {
        masterSlider.value = ConvertFMODVolume(value, true);
        FMODUnity.RuntimeManager.GetVCA("vca:/Master").setVolume(value);
        audioMixer.SetFloat("MasterVolume", ConvertMixerVolume(value, true));
    }

    public void SetSFXVolume(float value)
    {
        sfxSlider.value = value;

        FMODUnity.RuntimeManager.GetVCA("vca:/SFX").setVolume(ConvertFMODVolume(value, false));
        audioMixer.SetFloat("SFXVolume", ConvertMixerVolume(value, false));
    }

    private void LoadSFXVolume(float value)
    {
        sfxSlider.value = ConvertFMODVolume(value, true);

        FMODUnity.RuntimeManager.GetVCA("vca:/SFX").setVolume(value);
        audioMixer.SetFloat("SFXVolume", ConvertMixerVolume(value, true));
    }

    public void SetMusicVolume(float value)
    {
        musicSlider.value = value;

        FMODUnity.RuntimeManager.GetVCA("vca:/Music").setVolume(ConvertFMODVolume(value, false));
        audioMixer.SetFloat("MusicVolume", ConvertMixerVolume(value, false));
    }

    private void LoadMusicVolume(float value)
    {
        musicSlider.value = ConvertFMODVolume(value, true);

        FMODUnity.RuntimeManager.GetVCA("vca:/Music").setVolume(value);
        audioMixer.SetFloat("MusicVolume", ConvertMixerVolume(value, true));
    }

    public void SetUIVolume(float value)
    {
        uiSlider.value = value;

        FMODUnity.RuntimeManager.GetVCA("vca:/UI").setVolume(ConvertFMODVolume(value, false));
        audioMixer.SetFloat("UIVolume", ConvertMixerVolume(value, false));
    }

    private void LoadUIVolume(float value)
    {
        uiSlider.value = ConvertFMODVolume(value, true);

        FMODUnity.RuntimeManager.GetVCA("vca:/UI").setVolume(value);
        audioMixer.SetFloat("UIVolume", ConvertMixerVolume(value, true));
    }
    #endregion

    public void SaveVideoSettings()
    {
        string path = Application.persistentDataPath + "/Settings/";
        if (debug)
        {
            Debug.Log("Saving data in file : '" + "VideoSettings.txt" + "' at '" + path + "'");
        }

        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = new FileStream(path + "VideoSettings.txt", FileMode.Create);
        VideoSettings data = new VideoSettings
        {
            fullscreen = fullscreen.isOn,
            resolutionWidth = Screen.currentResolution.width,
            resolutionHeight = Screen.currentResolution.height,
            qualityIndex = overallQuality.value,
            masterVolume = ConvertFMODVolume(masterSlider.value, false),
            sfxVolume = ConvertFMODVolume(sfxSlider.value, false),
            musicVolume = ConvertFMODVolume(musicSlider.value, false),
            uiVolume = ConvertFMODVolume(uiSlider.value, false)
        };

        bf.Serialize(file, data);
        file.Close();
    }

    public void LoadVideoSettings()
    {
        string path = Application.persistentDataPath + "/Settings/";
        if (File.Exists(path + "VideoSettings.txt"))
        {
            if (debug)
            {
                Debug.Log("Loading data from file : '" + "VideoSettings.txt" + "' at '" + path + "'");
            }

            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = new FileStream(path + "VideoSettings.txt", FileMode.Open);
            VideoSettings data = (VideoSettings)bf.Deserialize(file);

            FullscreenToggle(data.fullscreen);
            SetResolution(data.resolutionWidth, data.resolutionHeight);
            SetQuality(data.qualityIndex);
            LoadMasterVolume(data.masterVolume);
            LoadSFXVolume(data.sfxVolume);
            LoadMusicVolume(data.musicVolume);
            LoadUIVolume(data.uiVolume);

            bf.Serialize(file, data);
            file.Close();
        }
    }

    public void DefaultVideoSettings()
    {
        string path = Application.persistentDataPath + "/Settings/";
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
            if (debug)
            {
                Debug.Log("Creating directory at: " + path);
            }
        }
        if (debug)
        {
            Debug.Log("Creating file : '" + "VideoSettings.txt" + "' at '" + path + "'");
        }

        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = new FileStream(path + "VideoSettings.txt", FileMode.Create);
        VideoSettings data = new VideoSettings();

        FullscreenToggle(data.fullscreen);
        SetResolution(data.resolutionWidth, data.resolutionHeight);
        SetQuality(data.qualityIndex);
        LoadMasterVolume(data.masterVolume);
        LoadSFXVolume(data.sfxVolume);
        LoadMusicVolume(data.musicVolume);
        LoadUIVolume(data.uiVolume);

        bf.Serialize(file, data);
        file.Close();
    }
}