using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;

public class Config : MonoBehaviour
{
    private ConfigFromSave save;
    public AudioMixer audioMixer;
    public void Start(){
        if(!System.IO.File.Exists(Application.persistentDataPath + "/config.json"))
        {
            save = new ConfigFromSave();
            System.IO.File.WriteAllText(Application.persistentDataPath + "/config.json", JsonUtility.ToJson(save));
        }
        else
        {
            save = JsonUtility.FromJson<ConfigFromSave>(System.IO.File.ReadAllText(Application.persistentDataPath + "/config.json"));
        }
        Load();
    }
    public TMP_Text LegendaLabel;

    public void NextLegendaLang()
    {
        save.subTitleLang = (Langs)(((int)save.subTitleLang + 1) % 2);
        LegendaLabel.text = save.subTitleLang.ToString();
    }

    public void PreviosLegendaLang()
    {
        save.subTitleLang = (Langs)(((int)save.subTitleLang - 1) % 2);
        LegendaLabel.text = save.subTitleLang.ToString();
    }

    // Audio Lang
    public TMP_Text LangLabel;


    public void NextLangAudio()
    {
        save.audioLang = (Langs)(((int)save.audioLang + 1) % 2);
        LangLabel.text = save.audioLang.ToString();
    }
    public void PreviosLangAudio()
    {
        save.audioLang = (Langs)(((int)save.audioLang - 1) % 2);
        LangLabel.text = save.audioLang.ToString();
    }

    // Tutorial
    public GameObject TutorialButton;

    public void ToggleTutorial()
    {
        save.tutorial = !save.tutorial;
        if (save.tutorial)
            TutorialButton.GetComponent<Image>().color = Color.green;
        else
            TutorialButton.GetComponent<Image>().color = Color.red;
    }


    public GameObject VibraControle;

    public void ToggleVibraControle()
    {
        save.vibraControle = !save.vibraControle;
        if (save.vibraControle)
            VibraControle.GetComponent<Image>().color = Color.green;
        else
            VibraControle.GetComponent<Image>().color = Color.red;
    }

    public GameObject VibraCamera;

    public void ToggleVibraCamera()
    {
        save.vibraCamera = !save.vibraCamera;
        if (save.vibraCamera)
            VibraCamera.GetComponent<Image>().color = Color.green;
        else
            VibraCamera.GetComponent<Image>().color = Color.red;
    }

    public GameObject Vsync;
    
    public void ToggleVsync()
    {
        save.vsync = !save.vsync;
        if (save.vsync)
            Vsync.GetComponent<Image>().color = Color.green;
        else
            Vsync.GetComponent<Image>().color = Color.red;

        QualitySettings.vSyncCount = save.vsync ? 1 : 0;
    }

    public GameObject ModoDeJanela;
    public void NextTipoJanela()
    {
        save.modoDeJanela = (TiposDeJanela)(((int)save.modoDeJanela + 1) % 3);
        ModoDeJanela.GetComponent<TMP_Text>().text = save.modoDeJanela.ToString();

        switch (save.modoDeJanela)
        {
            case TiposDeJanela.TelaCheia:
                Screen.fullScreenMode = FullScreenMode.FullScreenWindow;
                break;
            case TiposDeJanela.Janela:
                Screen.fullScreenMode = FullScreenMode.Windowed;
                break;
            case TiposDeJanela.JanelaSemBorda:
                Screen.fullScreenMode = FullScreenMode.MaximizedWindow;
                break;
        }
    }
    
    public void PreviosTipoJanela()
    {
        save.modoDeJanela = (TiposDeJanela)(((int)save.modoDeJanela - 1) % 3);
        //ModoDeJanela.GetComponent<TMP_Text>().text = save.ModoDeJanela.ToString();

        switch (save.modoDeJanela)
        {
            case TiposDeJanela.TelaCheia:
                Screen.fullScreenMode = FullScreenMode.FullScreenWindow;
                ModoDeJanela.GetComponent<TMP_Text>().text = "Tela Cheia";
                break;
            case TiposDeJanela.Janela:
                Screen.fullScreenMode = FullScreenMode.Windowed;
                ModoDeJanela.GetComponent<TMP_Text>().text = "Janela";
                break;
            case TiposDeJanela.JanelaSemBorda:
                Screen.fullScreenMode = FullScreenMode.MaximizedWindow;
                ModoDeJanela.GetComponent<TMP_Text>().text = "Janela Sem Borda";
                break;
        }
    }

    public TMP_Dropdown Resolutions;

    public void LoadResolutions()
    {
        Resolutions.AddOptions(new List<string>() { "800x600", "1024x768", "1280x720", "1366x768", "1600x900", "1920x1080" });
    }

    public void SetResolution()
    {
        switch (Resolutions.value)
        {
            case 0:
                Screen.SetResolution(800, 600, Screen.fullScreenMode);
                break;
            case 1:
                Screen.SetResolution(1024, 768, Screen.fullScreenMode);
                break;
            case 2:
                Screen.SetResolution(1280, 720, Screen.fullScreenMode);
                break;
            case 3:
                Screen.SetResolution(1366, 768, Screen.fullScreenMode);
                break;
            case 4:
                Screen.SetResolution(1600, 900, Screen.fullScreenMode);
                break;
            case 5:
                Screen.SetResolution(1920, 1080, Screen.fullScreenMode);
                break;
        }
        save.resolution = Screen.currentResolution;
    }

    public void SetMainAudio(Slider volume)
    {
        save.mainVolume = volume.value-1;
        audioMixer.SetFloat("MainVolume", volume.value-1);
    }
    public void SetInterfaceAudio(Slider volume)
    {
        save.interfaceVolume = volume.value-1;
        audioMixer.SetFloat("InterfaceVolume", volume.value-1);
    }
    public void SetMusicAudio(Slider volume)
    {
        save.musicVolume = volume.value-1;
        audioMixer.SetFloat("MusicVolume", volume.value-1);
    }
    public void SetSFXAudio(Slider volume)
    {
        // 0 = -80
        // 1 = 0
        save.sfxVolume = volume.value-1;
        audioMixer.SetFloat("SFXVolume", volume.value-1);
    }
    public void Save()
    {
        System.IO.File.WriteAllText(Application.persistentDataPath + "/config.json", JsonUtility.ToJson(save));
        GameManager.instance.configure = save;
    }
    public void Load()
    {
        GameManager.instance.configure = save;
        audioMixer.SetFloat("MainVolume", save.mainVolume);
        audioMixer.SetFloat("InterfaceVolume", save.interfaceVolume);
        audioMixer.SetFloat("MusicVolume", save.musicVolume);
        audioMixer.SetFloat("SFXVolume", save.sfxVolume);

        if (save.tutorial)
            TutorialButton.GetComponent<Image>().color = Color.green;
        else
            TutorialButton.GetComponent<Image>().color = Color.red;

        if (save.vibraControle)
            VibraControle.GetComponent<Image>().color = Color.green;
        else
            VibraControle.GetComponent<Image>().color = Color.red;

        if (save.vibraCamera)
            VibraCamera.GetComponent<Image>().color = Color.green;
        else
            VibraCamera.GetComponent<Image>().color = Color.red;

        if (save.vsync)
            Vsync.GetComponent<Image>().color = Color.green;
        else
            Vsync.GetComponent<Image>().color = Color.red;

        LegendaLabel.text = save.subTitleLang.ToString();
        LangLabel.text = save.audioLang.ToString();
        LoadResolutions();
        Resolutions.value = save.resolution.width;

        switch (save.modoDeJanela)
        {
            case TiposDeJanela.TelaCheia:
                Screen.fullScreenMode = FullScreenMode.FullScreenWindow;
                break;
            case TiposDeJanela.Janela:
                Screen.fullScreenMode = FullScreenMode.Windowed;
                break;
            case TiposDeJanela.JanelaSemBorda:
                Screen.fullScreenMode = FullScreenMode.MaximizedWindow;
                break;
        }
    }
}