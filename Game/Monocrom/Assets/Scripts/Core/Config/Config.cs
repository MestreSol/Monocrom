using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public enum Langs
{
    English,
    Portuguese
}
public enum TiposDeJanela
{
    TelaCheia,
    Janela,
    JanelaSemBorda
}
public class ConfigFromSave
{
    public Langs AudioLang = Langs.Portuguese;
    public Langs LegendaLang = Langs.Portuguese;
    public TiposDeJanela ModoDeJanela = TiposDeJanela.TelaCheia;
    public Resolution Resolucao = Screen.currentResolution;

    public bool Vsync = true;   
    public bool Tutorial = true;
    public bool VibraControle = true;
    public bool VibraCamera = true;
    public bool Conquistas = true;

    public float MainVolume = 1;
    public float MusicVolume = 1;
    public float SFXVolume = 1;
    public float InterfaceVolume = 1;
}
public class Config : MonoBehaviour
{
    private ConfigFromSave save;
    public AudioMixer audioMixer;
    public void Start()
    {
        // Se o arquivo de config não existir criar um novo
        if (!System.IO.File.Exists(Application.persistentDataPath + "/config.json"))
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

    // Legenda Lang
    public TMP_Text LegendaLabel;

    public void NextLegendaLang()
    {
        save.LegendaLang = (Langs)(((int)save.LegendaLang + 1) % 2);
        LegendaLabel.text = save.LegendaLang.ToString();
    }

    public void PreviosLegendaLang()
    {
        save.LegendaLang = (Langs)(((int)save.LegendaLang - 1) % 2);
        LegendaLabel.text = save.LegendaLang.ToString();
    }

    // Audio Lang
    public TMP_Text LangLabel;


    public void NextLangAudio()
    {
        save.AudioLang = (Langs)(((int)save.AudioLang + 1) % 2);
        LangLabel.text = save.AudioLang.ToString();
    }
    public void PreviosLangAudio()
    {
        save.AudioLang = (Langs)(((int)save.AudioLang - 1) % 2);
        LangLabel.text = save.AudioLang.ToString();
    }

    // Tutorial
    public GameObject TutorialButton;

    public void ToggleTutorial()
    {
        save.Tutorial = !save.Tutorial;
        if (save.Tutorial)
            TutorialButton.GetComponent<Image>().color = Color.green;
        else
            TutorialButton.GetComponent<Image>().color = Color.red;
    }


    public GameObject VibraControle;

    public void ToggleVibraControle()
    {
        save.VibraControle = !save.VibraControle;
        if (save.VibraControle)
            VibraControle.GetComponent<Image>().color = Color.green;
        else
            VibraControle.GetComponent<Image>().color = Color.red;
    }

    public GameObject VibraCamera;

    public void ToggleVibraCamera()
    {
        save.VibraCamera = !save.VibraCamera;
        if (save.VibraCamera)
            VibraCamera.GetComponent<Image>().color = Color.green;
        else
            VibraCamera.GetComponent<Image>().color = Color.red;
    }

    public GameObject Vsync;
    
    public void ToggleVsync()
    {
        save.Vsync = !save.Vsync;
        if (save.Vsync)
            Vsync.GetComponent<Image>().color = Color.green;
        else
            Vsync.GetComponent<Image>().color = Color.red;

        QualitySettings.vSyncCount = save.Vsync ? 1 : 0;
    }

    public GameObject ModoDeJanela;
    public void NextTipoJanela()
    {
        save.ModoDeJanela = (TiposDeJanela)(((int)save.ModoDeJanela + 1) % 3);
        ModoDeJanela.GetComponent<TMP_Text>().text = save.ModoDeJanela.ToString();

        switch (save.ModoDeJanela)
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
        save.ModoDeJanela = (TiposDeJanela)(((int)save.ModoDeJanela - 1) % 3);
        //ModoDeJanela.GetComponent<TMP_Text>().text = save.ModoDeJanela.ToString();

        switch (save.ModoDeJanela)
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
        save.Resolucao = Screen.currentResolution;
    }

    public void SetMainAudio(Slider volume)
    {
        save.MainVolume = volume.value-1;
        audioMixer.SetFloat("MainVolume", volume.value-1);
    }
    public void SetInterfaceAudio(Slider volume)
    {
        save.InterfaceVolume = volume.value-1;
        audioMixer.SetFloat("InterfaceVolume", volume.value-1);
    }
    public void SetMusicAudio(Slider volume)
    {
        save.MusicVolume = volume.value-1;
        audioMixer.SetFloat("MusicVolume", volume.value-1);
    }
    public void SetSFXAudio(Slider volume)
    {
        // 0 = -80
        // 1 = 0
        save.SFXVolume = volume.value-1;
        audioMixer.SetFloat("SFXVolume", volume.value-1);
    }
    public void Save()
    {
        System.IO.File.WriteAllText(Application.persistentDataPath + "/config.json", JsonUtility.ToJson(save));
        GameManager.config = save;
    }
    public void Load()
    {
        GameManager.config = save;
        audioMixer.SetFloat("MainVolume", save.MainVolume);
        audioMixer.SetFloat("InterfaceVolume", save.InterfaceVolume);
        audioMixer.SetFloat("MusicVolume", save.MusicVolume);
        audioMixer.SetFloat("SFXVolume", save.SFXVolume);

        if (save.Tutorial)
            TutorialButton.GetComponent<Image>().color = Color.green;
        else
            TutorialButton.GetComponent<Image>().color = Color.red;

        if (save.VibraControle)
            VibraControle.GetComponent<Image>().color = Color.green;
        else
            VibraControle.GetComponent<Image>().color = Color.red;

        if (save.VibraCamera)
            VibraCamera.GetComponent<Image>().color = Color.green;
        else
            VibraCamera.GetComponent<Image>().color = Color.red;

        if (save.Vsync)
            Vsync.GetComponent<Image>().color = Color.green;
        else
            Vsync.GetComponent<Image>().color = Color.red;

        LegendaLabel.text = save.LegendaLang.ToString();
        LangLabel.text = save.AudioLang.ToString();
        LoadResolutions();
        Resolutions.value = save.Resolucao.width;

        switch (save.ModoDeJanela)
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
    