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

// Classe para armazenar as configurações do jogo
public class ConfigFromSave
{
    // Idioma do áudio
    public Langs AudioLang { get; set; } = Langs.Portuguese;

    // Idioma da legenda
    public Langs LegendaLang { get; set; } = Langs.Portuguese;

    // Modo de janela
    public TiposDeJanela ModoDeJanela { get; set; } = TiposDeJanela.TelaCheia;

    // Resolução da tela
    public Resolution Resolucao { get; set; } = Screen.currentResolution;

    // Configurações de opções do jogo
    public bool Vsync { get; set; } = true;
    public bool Tutorial { get; set; } = true;
    public bool VibraControle { get; set; } = true;
    public bool VibraCamera { get; set; } = true;
    public bool Conquistas { get; set; } = true;

    // Configurações de volume do jogo
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
        // Define o caminho do arquivo de configuração
        string configPath = Application.persistentDataPath + "/config.json";

        // Verifica se o arquivo de configuração existe
        if (!System.IO.File.Exists(configPath))
        {
            // Se não existir, cria um novo objeto ConfigFromSave com as configurações padrão
            save = new ConfigFromSave();

            // Converte o objeto ConfigFromSave para JSON e grava no arquivo de configuração
            System.IO.File.WriteAllText(configPath, JsonUtility.ToJson(save));
        }
        else
        {
            // Se o arquivo de configuração existir, lê o conteúdo do arquivo
            string configContent = System.IO.File.ReadAllText(configPath);

            // Converte o conteúdo do arquivo de volta para um objeto ConfigFromSave
            save = JsonUtility.FromJson<ConfigFromSave>(configContent);
        }

        // Carrega as configurações do jogo
        Load();
    }


    // Legenda Lang
    public TMP_Text LegendaLabel;

    // Método para alternar para o próximo idioma de legenda
    public void NextLegendaLang()
    {
        // Incrementa o valor do enum e usa o operador módulo para garantir que o valor não exceda o número de idiomas disponíveis
        save.LegendaLang = (Langs)(((int)save.LegendaLang + 1) % System.Enum.GetValues(typeof(Langs)).Length);

        // Atualiza o texto do label com o novo idioma
        LegendaLabel.text = save.LegendaLang.ToString();
    }

    // Método para alternar para o idioma de legenda anterior
    public void PreviosLegendaLang()
    {
        // Decrementa o valor do enum e usa o operador módulo para garantir que o valor não seja negativo
        save.LegendaLang = (Langs)(((int)save.LegendaLang + System.Enum.GetValues(typeof(Langs)).Length - 1) % System.Enum.GetValues(typeof(Langs)).Length);

        // Atualiza o texto do label com o novo idioma
        LegendaLabel.text = save.LegendaLang.ToString();
    }

    // Label para exibir o idioma do áudio
    public TMP_Text LangLabel;

    // Método para alternar para o próximo idioma do áudio
    public void NextLangAudio()
    {
        // Incrementa o valor do enum e usa o operador módulo para garantir que o valor não exceda o número de idiomas disponíveis
        save.AudioLang = (Langs)(((int)save.AudioLang + 1) % System.Enum.GetValues(typeof(Langs)).Length);

        // Atualiza o texto do label com o novo idioma
        LangLabel.text = save.AudioLang.ToString();
    }

    // Método para alternar para o idioma do áudio anterior
    public void PreviosLangAudio()
    {
        // Decrementa o valor do enum e usa o operador módulo para garantir que o valor não seja negativo
        save.AudioLang = (Langs)(((int)save.AudioLang + System.Enum.GetValues(typeof(Langs)).Length - 1) % System.Enum.GetValues(typeof(Langs)).Length);

        // Atualiza o texto do label com o novo idioma
        LangLabel.text = save.AudioLang.ToString();
    }


    // Referência ao botão do tutorial
    public GameObject TutorialButton;

    // Método para alternar a configuração do tutorial
    public void ToggleTutorial()
    {
        // Inverte o valor da configuração do tutorial
        save.Tutorial = !save.Tutorial;

        // Muda a cor do botão do tutorial com base na nova configuração
        TutorialButton.GetComponent<Image>().color = save.Tutorial ? Color.green : Color.red;
    }

    // Referência ao controle de vibração
    public GameObject VibraControle;

    // Método para alternar a configuração de vibração do controle
    public void ToggleVibraControle()
    {
        // Inverte o valor da configuração de vibração do controle
        save.VibraControle = !save.VibraControle;

        // Muda a cor do controle de vibração com base na nova configuração
        VibraControle.GetComponent<Image>().color = save.VibraControle ? Color.green : Color.red;
    }

    // Referência à vibração da câmera
    public GameObject VibraCamera;

    // Método para alternar a configuração de vibração da câmera
    public void ToggleVibraCamera()
    {
        // Inverte o valor da configuração de vibração da câmera
        save.VibraCamera = !save.VibraCamera;

        // Muda a cor da vibração da câmera com base na nova configuração
        VibraCamera.GetComponent<Image>().color = save.VibraCamera ? Color.green : Color.red;
    }

    // Referência ao Vsync
    public GameObject Vsync;

    // Método para alternar a configuração do Vsync
    public void ToggleVsync()
    {
        // Inverte o valor da configuração do Vsync
        save.Vsync = !save.Vsync;

        // Muda a cor do Vsync com base na nova configuração
        Vsync.GetComponent<Image>().color = save.Vsync ? Color.green : Color.red;

        // Atualiza a configuração do Vsync no QualitySettings
        QualitySettings.vSyncCount = save.Vsync ? 1 : 0;
    }


    // Referência ao modo de janela
    public GameObject ModoDeJanela;

    // Método para alternar para o próximo tipo de janela
    public void NextTipoJanela()
    {
        // Incrementa o valor do enum e usa o operador módulo para garantir que o valor não exceda o número de tipos de janela disponíveis
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

    // Método para alternar para o tipo de janela anterior
    public void PreviosTipoJanela()
    {
        // Decrementa o valor do enum e usa o operador módulo para garantir que o valor não seja negativo
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

    // Referência ao dropdown de resoluções
    public TMP_Dropdown Resolutions;

    // Método para carregar as resoluções disponíveis
    public void LoadResolutions()
    {
        // Adiciona as resoluções disponíveis ao dropdown
        Resolutions.AddOptions(new List<string>() { "800x600", "1024x768", "1280x720", "1366x768", "1600x900", "1920x1080" });
    }

    // Método para definir a resolução com base na opção selecionada no dropdown
    public void SetResolution()
    {
        // Define a resolução com base na opção selecionada no dropdown
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

        // Aplica a nova resolução
        Screen.SetResolution(width, height, Screen.fullScreenMode);

        // Atualiza a resolução salva
        save.Resolucao = Screen.currentResolution;
    }


    // Método para definir o volume principal do áudio
    public void SetMainAudio(Slider volume)
    {
        // Subtrai 1 do valor do slider e salva na configuração do volume principal
        save.MainVolume = volume.value - 1;

        // Define o volume principal no mixer de áudio
        audioMixer.SetFloat("MainVolume", save.MainVolume);
    }

    // Método para definir o volume da interface do áudio
    public void SetInterfaceAudio(Slider volume)
    {
        // Subtrai 1 do valor do slider e salva na configuração do volume da interface
        save.InterfaceVolume = volume.value - 1;

        // Define o volume da interface no mixer de áudio
        audioMixer.SetFloat("InterfaceVolume", save.InterfaceVolume);
    }

    // Método para definir o volume da música
    public void SetMusicAudio(Slider volume)
    {
        // Subtrai 1 do valor do slider e salva na configuração do volume da música
        save.MusicVolume = volume.value - 1;

        // Define o volume da música no mixer de áudio
        audioMixer.SetFloat("MusicVolume", save.MusicVolume);
    }

    // Método para definir o volume dos efeitos sonoros
    public void SetSFXAudio(Slider volume)
    {
        // Subtrai 1 do valor do slider e salva na configuração do volume dos efeitos sonoros
        save.SFXVolume = volume.value - 1;

        // Define o volume dos efeitos sonoros no mixer de áudio
        audioMixer.SetFloat("SFXVolume", save.SFXVolume);
    }

    // Método para salvar as configurações do jogo
    public void Save()
    {
        // Converte o objeto de configuração para JSON e grava no arquivo de configuração
        System.IO.File.WriteAllText(Application.persistentDataPath + "/config.json", JsonUtility.ToJson(save));

        // Atualiza as configurações do jogo no GameManager
        GameManager.config = save;
    }

    // Método para carregar as configurações do jogo
    public void Load()
    {
        // Atualiza as configurações do jogo no GameManager
        GameManager.config = save;

        // Define os volumes no mixer de áudio
        audioMixer.SetFloat("MainVolume", save.MainVolume);
        audioMixer.SetFloat("InterfaceVolume", save.InterfaceVolume);
        audioMixer.SetFloat("MusicVolume", save.MusicVolume);
        audioMixer.SetFloat("SFXVolume", save.SFXVolume);

        // Atualiza a cor dos botões com base nas configurações
        TutorialButton.GetComponent<Image>().color = save.Tutorial ? Color.green : Color.red;
        VibraControle.GetComponent<Image>().color = save.VibraControle ? Color.green : Color.red;
        VibraCamera.GetComponent<Image>().color = save.VibraCamera ? Color.green : Color.red;
        Vsync.GetComponent<Image>().color = save.Vsync ? Color.green : Color.red;

        // Atualiza os textos dos labels com os idiomas selecionados
        LegendaLabel.text = save.LegendaLang.ToString();
        LangLabel.text = save.AudioLang.ToString();

        // Carrega as resoluções disponíveis
        LoadResolutions();

        // Define a resolução selecionada no dropdown
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
