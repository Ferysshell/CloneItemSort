using UnityEngine;
using UnityEngine.UI;

public class SettingMenu : MonoBehaviour
{

    [SerializeField] private Button _soundBtn, _musicBtn;
    [SerializeField] private Sprite _soundImageOn, _musicImageOn, _soundImageOff, _musicImageOff;
    [SerializeField] private bool _soundStatusBtn;
    [SerializeField] private bool _musicStatusBtn;

    void Start()
    {
        _soundStatusBtn = SoundManager.soundVolumeStatus;
        _musicStatusBtn = SoundManager.musicVolumeStatus;

        SetButtonStatus(_soundBtn, _soundStatusBtn, _soundImageOn, _soundImageOff);
        SetButtonStatus(_musicBtn, _musicStatusBtn, _musicImageOn, _musicImageOff);

        _soundBtn.onClick.AddListener(SwitchSound);
        _musicBtn.onClick.AddListener(SwitchMusic);
    }

    private void SwitchMusic()
    {
        SoundManager.instance.TapButton();
        _musicStatusBtn = !_musicStatusBtn;
        SoundManager.instance.SetVolumeMusic(_musicStatusBtn);
        SetButtonStatus(_musicBtn, _musicStatusBtn, _musicImageOn, _musicImageOff);
    }

    private void SwitchSound()
    {
        SoundManager.instance.TapButton();
        _soundStatusBtn = !_soundStatusBtn;
        SoundManager.instance.SetVolumeSound(_soundStatusBtn);
        SetButtonStatus(_soundBtn, _soundStatusBtn, _soundImageOn, _soundImageOff);
    }

    private void SetButtonStatus(Button button, bool status, Sprite imageOn, Sprite imageOff)
    {
        Image buttonImg = button.GetComponent<Image>();
        buttonImg.sprite = status ? imageOn : imageOff;
    }
}