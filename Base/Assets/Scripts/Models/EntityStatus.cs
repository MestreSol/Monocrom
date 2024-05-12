using System;
using System.Collections.Generic;

[Serializable]
public class EntityStatus
{
    private Stat Strength;
    private Stat Dexterity;
    private Stat Luck;
    private Stat Perception;
    private Stat Constituition;
    private Stat Intelligence;
    private Stat Charisma;
    private Stat Energi;
    private Stat Constitution;

    public List<StatModifier> StatModifiers { get; private set; }

    public EntityStatus()
    {
        Strength = new Stat();
        Dexterity = new Stat();
        Luck = new Stat();
        Perception = new Stat();
        Constituition = new Stat();
        Intelligence = new Stat();
        Charisma = new Stat();
        Energi = new Stat();
        Constitution = new Stat();
    }

    public Stat GetStat(StatType statType)
    {
        switch (statType)
        {
            case StatType.Strength:
                return Strength;
            case StatType.Dexterity:
                return Dexterity;
            case StatType.Luck:
                return Luck;
            case StatType.Perception:
                return Perception;
            case StatType.Constituition:
                return Constituition;
            case StatType.Intelligence:
                return Intelligence;
            case StatType.Charisma:
                return Charisma;
            case StatType.Energi:
                return Energi;
            case StatType.Constitution:
                return Constitution;
            default:
                throw new ArgumentOutOfRangeException("statType", statType, null);
        }
    }

    public void SetStat(StatType statType, Stat stat)
    {
        switch (statType)
        {
            case StatType.Strength:
                Strength = stat;
                break;
            case StatType.Dexterity:
                Dexterity = stat;
                break;
            case StatType.Luck:
                Luck = stat;
                break;
            case StatType.Perception:
                Perception = stat;
                break;
            case StatType.Constituition:
                Constituition = stat;
                break;
            case StatType.Intelligence:
                Intelligence = stat;
                break;
            case StatType.Charisma:
                Charisma = stat;
                break;
            case StatType.Energi:
                Energi = stat;
                break;
            case StatType.Constitution:
                Constitution = stat;
                break;
            default:
                throw new ArgumentOutOfRangeException("statType", statType, null);
        }
    }

    public void SetStatValue(StatType statType, float value)
    {
        GetStat(statType).BaseValue = value;
    }

    public void AddModifier(StatType statType, StatModifier mod)
    {
        GetStat(statType).AddModifier(mod);
    }

    public bool RemoveModifier(StatType statType, StatModifier mod)
    {
        return GetStat(statType).RemoveModifier(mod);
    }

    public bool RemoveAllModifiersFromSource(StatType statType, object source)
    {
        return GetStat(statType).RemoveAllModifiersFromSource(source);
    }

    public float GetValue(StatType statType)
    {
        return GetStat(statType).Value;
    }

    public float GetBaseValue(StatType statType)
    {
        return GetStat(statType).BaseValue;
    }




    public void ClearModifiers(StatType statType)
    {
        List<StatModifier> modifiers = new List<StatModifier>();
        modifiers.AddRange(GetStat(statType).StatModifiers);
        foreach (StatModifier modifier in modifiers)
        {
            GetStat(statType).RemoveModifier(modifier);
        }
    }
    public void ClearAllModifiers()
    {
        List<StatModifier> modifiers = new List<StatModifier>();
        modifiers.AddRange(Strength.StatModifiers);
        modifiers.AddRange(Dexterity.StatModifiers);
        modifiers.AddRange(Luck.StatModifiers);
        modifiers.AddRange(Perception.StatModifiers);
        modifiers.AddRange(Constituition.StatModifiers);
        modifiers.AddRange(Intelligence.StatModifiers);
        modifiers.AddRange(Charisma.StatModifiers);
        modifiers.AddRange(Energi.StatModifiers);
        modifiers.AddRange(Constitution.StatModifiers);
        foreach (StatModifier modifier in modifiers)
        {
            Strength.RemoveModifier(modifier);
            Dexterity.RemoveModifier(modifier);
            Luck.RemoveModifier(modifier);
            Perception.RemoveModifier(modifier);
            Constituition.RemoveModifier(modifier);
            Intelligence.RemoveModifier(modifier);
            Charisma.RemoveModifier(modifier);
            Energi.RemoveModifier(modifier);
            Constitution.RemoveModifier(modifier);
        }
    }
}
public enum StatType
{
    Strength,
    Dexterity,
    Luck,
    Perception,
    Constituition,
    Intelligence,
    Charisma,
    Energi,
    Constitution
}