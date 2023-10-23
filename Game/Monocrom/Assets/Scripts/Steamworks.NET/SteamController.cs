using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteamController : MonoBehaviour
{
    public static SteamController Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public string GetSteamID()
    {
        return Steamworks.SteamUser.GetSteamID().ToString();
    }


    public string GetSteamName()
    {
        return Steamworks.SteamFriends.GetPersonaName();
    }

    public string GetSteamLanguage()
    {
        return Steamworks.SteamApps.GetCurrentGameLanguage();
    }

    public string GetSteamInstallPath()
    {
        string steamInstallPath = "";
        Steamworks.SteamApps.GetAppInstallDir(Steamworks.SteamUtils.GetAppID(), out steamInstallPath, 256);
        return steamInstallPath;
    }

    public string GetSetamLanguageCode()
    {
        return Steamworks.SteamApps.GetCurrentGameLanguage();
    }

    public bool IsSteamRunning()
    {
        return Steamworks.SteamAPI.IsSteamRunning();
    }

    public bool IsSteamUserLoggedIn()
    {
        return Steamworks.SteamUser.BLoggedOn();
    }

    public void UnlockAchievement(string achievementName)
    {
        Steamworks.SteamUserStats.SetAchievement(achievementName);
        Steamworks.SteamUserStats.StoreStats();
    }

    public void IncrementAchievement(string achievementName, int steps)
    {
        Steamworks.SteamUserStats.IndicateAchievementProgress(achievementName, (uint)steps, 100);
        Steamworks.SteamUserStats.StoreStats();
    }

    public void ResetAllAchievements()
    {
        Steamworks.SteamUserStats.ResetAllStats(true);
        Steamworks.SteamUserStats.StoreStats();
    }

    public bool IsAchievementUnlocked(string achievementName)
    {
        bool isUnlocked = false;
        Steamworks.SteamUserStats.GetAchievement(achievementName, out isUnlocked);
        return isUnlocked;
    }
}

