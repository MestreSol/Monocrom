using System.Collections.Generic;
using UnityEngine;

// Default Entity Information script
public class EntityInfo : MonoBehaviour
{
    // Entity name
    public string Name;

    // Entity health
    public int Health;

    // Entity mana
    public int Mana;

    // Entity stats
    public Dictionary<string, int> Stats = new Dictionary<string, int>();

    // Add a stat to the entity
    public void AddStat(string statName, int value)
    {
        if (Stats.ContainsKey(statName))
        {
            Stats[statName] += value;
        }
        else
        {
            Stats.Add(statName, value);
        }
    }

    // Remove a stat from the entity
    public void RemoveStat(string statName)
    {
        if (Stats.ContainsKey(statName))
        {
            Stats.Remove(statName);
        }
    }
}
