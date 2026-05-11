using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHelper : MonoBehaviour
{
    [SerializeField] private List<AudioClip> uiSound;
    [SerializeField] private Slider volumeSlider;
    
    void Awake()
    {
        volumeSlider.value = AudioManager.Instance.GetVolume();
    }

    public void PlayUISound()
    {
        AudioManager.Instance.PlayOneShotAudio(
            uiSound[Random.Range(0, uiSound.Count)]);
    }

    public void SetVolume(float volume)
    {
        AudioManager.Instance.SetVolume(volume);
    }
}
