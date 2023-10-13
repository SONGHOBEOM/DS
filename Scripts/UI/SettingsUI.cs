using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ResourceInfo("SettingsUI")]
public class SettingsUI : UIPopup
{
    [SerializeField] private Button saveButton;
    [SerializeField] private Text saveButtonText;
    [SerializeField] private Button loadButton;
    [SerializeField] private Text loadButtonText;
    [SerializeField] private Button resumeButton;
    [SerializeField] private Text resumeButtonText;
    [SerializeField] private Button exitButton;
    [SerializeField] private Text exitButtonText;

    private void OnEnable()
    {
        saveButtonText.text = STR.Get("Save");
        loadButtonText.text = STR.Get("Load");
        resumeButtonText.text = STR.Get("Resume");
        exitButtonText.text = STR.Get("Exit");
    }

    private void AddButtonFunc()
    {
        saveButton.onClick.AddListener(SaveGame);
        loadButton.onClick.AddListener(LoadGame);
        resumeButton.onClick.AddListener(Resume);
        exitButton.onClick.AddListener(ExitGame);
    }

    private void SubButtonFunc()
    {
        saveButton.onClick.RemoveAllListeners();
        loadButton.onClick.RemoveAllListeners();
        resumeButton.onClick.RemoveAllListeners();
        exitButton.onClick.RemoveAllListeners();
    }

    private void SaveGame()
    {
        SoundManager.Instance.PlayClip("UIClick");
        GameManager.Instance.GameSave();
    }
    private void LoadGame()
    {
        SoundManager.Instance.PlayClip("UIClick");
        GameManager.Instance.LoadGame();
    }

    private void Resume()
    {
        SoundManager.Instance.PlayClip("UIClick");
        Close();
    }

    private void ExitGame()
    {
        SoundManager.Instance.PlayClip("UIClick");
        Quit();
    }

    public void Quit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#elif UNITY_WEBPLAYER
        Application.OpenURL("http://google.com");
#else
        Application.Quit();
#endif
    }

    public override void Open(UIParam param)
    {
        base.Open(param);
        AddButtonFunc();
    }

    public override void Close()
    {
        SubButtonFunc();
        gameObject.SetActive(false);
    }

}
