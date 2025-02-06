using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlanetNavigation : MonoBehaviour
{
    public Camera[] planetCameras;
    public string[] planetNames = { "Earth", "Mars", "Neptune" };
    public TMP_Text planetNameText;
    public Button leftButton;
    public Button rightButton;
    public AudioSource bgmSource;
    public AudioSource sfxSource;
    public AudioClip bgmClip;
    public AudioClip buttonSFX;

    private int currentIndex = 0;

    private void Start()
    {
        bgmSource.clip = bgmClip;
        bgmSource.loop = true;
        bgmSource.Play();
        UpdateUI();
        leftButton.onClick.AddListener(() => ButtonClick(GoLeft));
        rightButton.onClick.AddListener(() => ButtonClick(GoRight));
    }

    private void GoLeft()
    {
        currentIndex = (currentIndex - 1 + planetCameras.Length) % planetCameras.Length;
        UpdateUI();
    }

    private void GoRight()
    {
        currentIndex = (currentIndex + 1) % planetCameras.Length;
        UpdateUI();
    }

    private void UpdateUI()
    {
        for (int i = 0; i < planetCameras.Length; i++)
        {
            planetCameras[i].gameObject.SetActive(i == currentIndex);
        }

        planetNameText.text = planetNames[currentIndex];
    }

    private void ButtonClick(System.Action action)
    {
        sfxSource.PlayOneShot(buttonSFX);
        action.Invoke();
    }
}
