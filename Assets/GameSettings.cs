
[System.Serializable]
public class VideoSettings
{
    /* Game */
    public int resolutionWidth = 1920;
    public int resolutionHeight = 1080;
    public int qualityIndex = 3;
    public bool fullscreen = true;

    /* Sound */
    public float masterVolume = 0.5f;
    public float sfxVolume = 1f;
    public float musicVolume = 1f;
    public float uiVolume = 1f;
}