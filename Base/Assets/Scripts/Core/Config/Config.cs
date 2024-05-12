using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

// Enum para representar os idiomas suportados
public enum Langs
{
    English,
    Portuguese
}

// Enum para representar os tipos de janela suportados
public enum TiposDeJanela
{
    TelaCheia,
    Janela,
    JanelaSemBorda
}

// Classe para armazenar as configura��es do jogo
public class ConfigFromSave
{
    // Idioma do �udio
    public Langs AudioLang { get; set; } = Langs.Portuguese;

    // Idioma da legenda
    public Langs LegendaLang { get; set; } = Langs.Portuguese;

    // Modo de janela
    public TiposDeJanela ModoDeJanela { get; set; } = TiposDeJanela.TelaCheia;

    // Resolu��o da tela
    public Resolution Resolucao { get; set; } = Screen.currentResolution;

    // Configura��es de op��es do jogo
    public bool Vsync { get; set; } = true;
    public bool Tutorial { get; set; } = true;
    public bool VibraControle { get; set; } = true;
    public bool VibraCamera { get; set; } = true;
    public bool Conquistas { get; set; } = true;

    // Configura��es de volume do jogo
    public float MainVolume { get; set; } = 1;
    public float MusicVolume { get; set; } = 1;
    public float SFXVolume { get; set; } = 1;
    public float InterfaceVolume { get; set; } = 1;
}

public class Config : MonoBehaviour
{
    private ConfigFromSave save;
    public AudioMixer audioMixer;
    public void Start()
    {
        // Define o caminho do arquivo de configura��o
        string configPath = Application.persistentDataPath + "/config.json";

        // Verifica se o arquivo de configura��o existe
        if (!System.IO.File.Exists(configPath))
        {
            // Se n�o existir, cria um novo objeto ConfigFromSave com as configura��es padr�o
            save = new ConfigFromSave();

            // Converte o objeto ConfigFromSave para JSON e grava no arquivo de configura��o
            System.IO.File.WriteAllText(configPath, JsonUtility.ToJson(save));
        }
        else
        {
            // Se o arquivo de configura��o existir, l� o conte�do do arquivo
            string configContent = System.IO.File.ReadAllText(configPath);

            // Converte o conte�do do arquivo de volta para um objeto ConfigFromSave
            save = JsonUtility.FromJson<ConfigFromSave>(configContent);
        }

        // Carrega as configura��es do jogo
        Load();
    }


    // Legenda Lang
    public TMP_Text LegendaLabel;

    // M�todo para alternar para o pr�ximo idioma de legenda
    public void NextLegendaLang()
    {
        // Incrementa o valor do enum e usa o operador m�dulo para garantir que o valor n�o exceda o n�mero de idiomas dispon�veis
        save.LegendaLang = (Langs)(((int)save.LegendaLang + 1) % System.Enum.GetValues(typeof(Langs)).Length);

        // Atualiza o texto do label com o novo idioma
        LegendaLabel.text = save.LegendaLang.ToString();
    }

    // M�todo para alternar para o idioma de legenda anterior
    public void PreviosLegendaLang()
    {
        // Decrementa o valor do enum e usa o operador m�dulo para garantir que o valor n�o seja negativo
        save.LegendaLang = (Langs)(((int)save.LegendaLang + System.Enum.GetValues(typeof(Langs)).Length - 1) % System.Enum.GetValues(typeof(Langs)).Length);

        // Atualiza o texto do label com o novo idioma
        LegendaLabel.text = save.LegendaLang.ToString();
    }

    // Label para exibir o idioma do �udio
    public TMP_Text LangLabel;

    // M�todo para alternar para o pr�ximo idioma do �udio
    public void NextLangAudio()
    {
        // Incrementa o valor do enum e usa o operador m�dulo para garantir que o valor n�o exceda o n�mero de idiomas dispon�veis
        save.AudioLang = (Langs)(((int)save.AudioLang + 1) % System.Enum.GetValues(typeof(Langs)).Length);

        // Atualiza o texto do label com o novo idioma
        LangLabel.text = save.AudioLang.ToString();
    }

    // M�todo para alternar para o idioma do �udio anterior
    public void PreviosLangAudio()
    {
        // Decrementa o valor do enum e usa o operador m�dulo para garantir que o valor n�o seja negativo
        save.AudioLang = (Langs)(((int)save.AudioLang + System.Enum.GetValues(typeof(Langs)).Length - 1) % System.Enum.GetValues(typeof(Langs)).Length);

        // Atualiza o texto do label com o novo idioma
        LangLabel.text = save.AudioLang.ToString();
    }


    // Refer�ncia ao bot�o do tutorial
    public GameObject TutorialButton;

    // M�todo para alternar a configura��o do tutorial
    public void ToggleTutorial()
    {
        // Inverte o valor da configura��o do tutorial
        save.Tutorial = !save.Tutorial;

        // Muda a cor do bot�o do tutorial com base na nova configura��o
        TutorialButton.GetComponent<Image>().color = save.Tutorial ? Color.green : Color.red;
    }

    // Refer�ncia ao controle de vibra��o
    public GameObject VibraControle;

    // M�todo para alternar a configura��o de vibra��o do controle
    public void ToggleVibraControle()
    {
        // Inverte o valor da configura��o de vibra��o do controle
        save.VibraControle = !save.VibraControle;

        // Muda a cor do controle de vibra��o com base na nova configura��o
        VibraControle.GetComponent<Image>().color = save.VibraControle ? Color.green : Color.red;
    }

    // Refer�ncia � vibra��o da c�mera
    public GameObject VibraCamera;

    // M�todo para alternar a configura��o de vibra��o da c�mera
    public void ToggleVibraCamera()
    {
        // Inverte o valor da configura��o de vibra��o da c�mera
        save.VibraCamera = !save.VibraCamera;

        // Muda a cor da vibra��o da c�mera com base na nova configura��o
        VibraCamera.GetComponent<Image>().color = save.VibraCamera ? Color.green : Color.red;
    }

    // Refer�ncia ao Vsync
    public GameObject Vsync;

    // M�todo para alternar a configura��o do Vsync
    public void ToggleVsync()
    {
        // Inverte o valor da configura��o do Vsync
        save.Vsync = !save.Vsync;

        // Muda a cor do Vsync com base na nova configura��o
        Vsync.GetComponent<Image>().color = save.Vsync ? Color.green : Color.red;

        // Atualiza a configura��o do Vsync no QualitySettings
        QualitySettings.vSyncCount = save.Vsync ? 1 : 0;
    }


    // Refer�ncia ao modo de janela
    public GameObject ModoDeJanela;

    // M�todo para alternar para o pr�ximo tipo de janela
    public void NextTipoJanela()
    {
        // Incrementa o valor do enum e usa o operador m�dulo para garantir que o valor n�o exceda o n�mero de tipos de janela dispon�veis
        save.ModoDeJanela = (TiposDeJanela)(((int)save.ModoDeJanela + 1) % System.Enum.GetValues(typeof(TiposDeJanela)).Length);

        // Atualiza o texto do label com o novo tipo de janela
        ModoDeJanela.GetComponent<TMP_Text>().text = save.ModoDeJanela.ToString();

        // Atualiza o modo de tela cheia com base no novo tipo de janela
        Screen.fullScreenMode = save.ModoDeJanela switch
        {
            TiposDeJanela.TelaCheia => FullScreenMode.FullScreenWindow,
            TiposDeJanela.Janela => FullScreenMode.Windowed,
            TiposDeJanela.JanelaSemBorda => FullScreenMode.MaximizedWindow,
            _ => Screen.fullScreenMode
        };
    }

    // M�todo para alternar para o tipo de janela anterior
    public void PreviosTipoJanela()
    {
        // Decrementa o valor do enum e usa o operador m�dulo para garantir que o valor n�o seja negativo
        save.ModoDeJanela = (TiposDeJanela)(((int)save.ModoDeJanela + System.Enum.GetValues(typeof(TiposDeJanela)).Length - 1) % System.Enum.GetValues(typeof(TiposDeJanela)).Length);

        // Atualiza o texto do label com o novo tipo de janela
        ModoDeJanela.GetComponent<TMP_Text>().text = save.ModoDeJanela.ToString();

        // Atualiza o modo de tela cheia com base no novo tipo de janela
        Screen.fullScreenMode = save.ModoDeJanela switch
        {
            TiposDeJanela.TelaCheia => FullScreenMode.FullScreenWindow,
            TiposDeJanela.Janela => FullScreenMode.Windowed,
            TiposDeJanela.JanelaSemBorda => FullScreenMode.MaximizedWindow,
            _ => Screen.fullScreenMode
        };
    }

    // Refer�ncia ao dropdown de resolu��es
    public TMP_Dropdown Resolutions;

    // M�todo para carregar as resolu��es dispon�veis
    public void LoadResolutions()
    {
        // Adiciona as resolu��es dispon�veis ao dropdown
        Resolutions.AddOptions(new List<string>() { "800x600", "1024x768", "1280x720", "1366x768", "1600x900", "1920x1080" });
    }

    // M�todo para definir a resolu��o com base na op��o selecionada no dropdown
    public void SetResolution()
    {
        // Define a resolu��o com base na op��o selecionada no dropdown
        (int width, int height) = Resolutions.value switch
        {
            0 => (800, 600),
            1 => (1024, 768),
            2 => (1280, 720),
            3 => (1366, 768),
            4 => (1600, 900),
            5 => (1920, 1080),
            _ => (Screen.currentResolution.width, Screen.currentResolution.height)
        };

        // Aplica a nova resolu��o
        Screen.SetResolution(width, height, Screen.fullScreenMode);

        // Atualiza a resolu��o salva
        save.Resolucao = Screen.currentResolution;
    }


    // M�todo para definir o volume principal do �udio
    public void SetMainAudio(Slider volume)
    {
        // Subtrai 1 do valor do slider e salva na configura��o do volume principal
        save.MainVolume = volume.value - 1;

        // Define o volume principal no mixer de �udio
        audioMixer.SetFloat("MainVolume", save.MainVolume);
    }

    // M�todo para definir o volume da interface do �udio
    public void SetInterfaceAudio(Slider volume)
    {
        // Subtrai 1 do valor do slider e salva na configura��o do volume da interface
        save.InterfaceVolume = volume.value - 1;

        // Define o volume da interface no mixer de �udio
        audioMixer.SetFloat("InterfaceVolume", save.InterfaceVolume);
    }

    // M�todo para definir o volume da m�sica
    public void SetMusicAudio(Slider volume)
    {
        // Subtrai 1 do valor do slider e salva na configura��o do volume da m�sica
        save.MusicVolume = volume.value - 1;

        // Define o volume da m�sica no mixer de �udio
        audioMixer.SetFloat("MusicVolume", save.MusicVolume);
    }

    // M�todo para definir o volume dos efeitos sonoros
    public void SetSFXAudio(Slider volume)
    {
        // Subtrai 1 do valor do slider e salva na configura��o do volume dos efeitos sonoros
        save.SFXVolume = volume.value - 1;

        // Define o volume dos efeitos sonoros no mixer de �udio
        audioMixer.SetFloat("SFXVolume", save.SFXVolume);
    }

    // M�todo para salvar as configura��es do jogo
    public void Save()
    {
        // Converte o objeto de configura��o para JSON e grava no arquivo de configura��o
        System.IO.File.WriteAllText(Application.persistentDataPath + "/config.json", JsonUtility.ToJson(save));

        // Atualiza as configura��es do jogo no GameManager
        GameManager.config = save;
    }

    // M�todo para carregar as configura��es do jogo
    public void Load()
    {
        // Atualiza as configura��es do jogo no GameManager
        GameManager.config = save;

        // Define os volumes no mixer de �udio
        audioMixer.SetFloat("MainVolume", save.MainVolume);
        audioMixer.SetFloat("InterfaceVolume", save.InterfaceVolume);
        audioMixer.SetFloat("MusicVolume", save.MusicVolume);
        audioMixer.SetFloat("SFXVolume", save.SFXVolume);

        // Atualiza a cor dos bot�es com base nas configura��es
        TutorialButton.GetComponent<Image>().color = save.Tutorial ? Color.green : Color.red;
        VibraControle.GetComponent<Image>().color = save.VibraControle ? Color.green : Color.red;
        VibraCamera.GetComponent<Image>().color = save.VibraCamera ? Color.green : Color.red;
        Vsync.GetComponent<Image>().color = save.Vsync ? Color.green : Color.red;

        // Atualiza os textos dos labels com os idiomas selecionados
        LegendaLabel.text = save.LegendaLang.ToString();
        LangLabel.text = save.AudioLang.ToString();

        // Carrega as resolu��es dispon�veis
        LoadResolutions();

        // Define a resolu��o selecionada no dropdown
        Resolutions.value = save.Resolucao.width;

        // Define o modo de tela cheia com base no tipo de janela selecionado
        Screen.fullScreenMode = save.ModoDeJanela switch
        {
            TiposDeJanela.TelaCheia => FullScreenMode.FullScreenWindow,
            TiposDeJanela.Janela => FullScreenMode.Windowed,
            TiposDeJanela.JanelaSemBorda => FullScreenMode.MaximizedWindow,
            _ => Screen.fullScreenMode
        };
    }


}
