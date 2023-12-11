using UnityEngine;
using Steamworks;

public class SteamController : MonoBehaviour
{
    // Singleton para a classe SteamController
    public static SteamController Instance { get; private set; }

    // Método Awake é chamado quando o script é inicializado
    private void Awake()
    {
        // Se a instância for nula, esta instância se torna a instância Singleton
        if (Instance == null)
            Instance = this;
        // Se já existir uma instância, destruímos este objeto
        else
            Destroy(gameObject);
    }

    // Método para obter o ID do usuário Steam
    public string GetSteamID()
    {
        // Retorna o ID do usuário Steam como uma string
        return SteamUser.GetSteamID().ToString();
    }

    // Método para obter o nome do usuário Steam
    public string GetSteamName()
    {
        // Retorna o nome do usuário Steam
        return SteamFriends.GetPersonaName();
    }

    // Método para obter o idioma do jogo Steam
    public string GetSteamLanguage()
    {
        // Retorna o idioma atual do jogo
        return SteamApps.GetCurrentGameLanguage();
    }

    // Método para obter o caminho de instalação do jogo Steam
    public string GetSteamInstallPath()
    {
        // Obtém o caminho de instalação do jogo e retorna
        SteamApps.GetAppInstallDir(SteamUtils.GetAppID(), out string steamInstallPath, 256);
        return steamInstallPath;
    }

    // Método para obter o código do idioma do jogo Steam
    public string GetSteamLanguageCode()
    {
        // Retorna o código do idioma atual do jogo
        return SteamApps.GetCurrentGameLanguage();
    }
}