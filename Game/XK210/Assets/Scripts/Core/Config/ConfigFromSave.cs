using UnityEngine;

public class ConfigFromSave
{
    public Langs audioLang = Langs.Portuguese;
    public Langs subTitleLang = Langs.Portuguese;
    public TiposDeJanela modoDeJanela = TiposDeJanela.TelaCheia;

    public Resolution resolution = Screen.currentResolution;
    
    public bool vsync = false;
    public bool tutorial = false;
    public bool vibraControle = false;
    public bool vibraCamera = false;
    public bool conquistas = false;
    public float mainVolume = 1;
    public float musicVolume = 1;
    public float sfxVolume = 1;
    public float interfaceVolume = 1;
}
