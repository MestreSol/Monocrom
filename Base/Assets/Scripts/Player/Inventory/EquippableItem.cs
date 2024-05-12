using UnityEngine;

public enum EquipmentType
{
	HELMET,
	CHEST,
	GLOVES,
	BOOTS,
	WEAPON1,
	WEAPON2,
	ACCESSORY1,
	ACCESSORY2,
}

[CreateAssetMenu]
public class EquippableItem : Item
{
	// Atributos Bonus
	public int StrengthBonus;
	public int DexterityBonus;
	public int ConstitutionBonus;
	public int IntelligenceBonus;
	public int CharismaBonus;
	public int LuckBonus;
	public int PerceptionBonus;

	// % 
	[Space]
	public float StrengthPercentBonus;
	public float DexterityPercentBonus;
	public float ConstitutionPercentBonus;
	public float IntelligencePercentBonus;
	public float CharismaPercentBonus;
	public float LuckPercentBonus;
	public float PerceptionPercentBonus;

	[Space]
	public EquipmentType EquipmentType;

	public void Equip(Entity entity)
	{
		if(StrengthBonus != 0)
		{
            entity.EntityStatus.GetStat(StatType.Strength).AddModifier(new StatModifier(StrengthBonus, StatModifierType.Flat, this));
        }
		if(DexterityBonus != 0)
		{
            entity.EntityStatus.GetStat(StatType.Dexterity).AddModifier(new StatModifier(DexterityBonus, StatModifierType.Flat, this));
        }
		if(ConstitutionBonus != 0)
		{
            entity.EntityStatus.GetStat(StatType.Constitution).AddModifier(new StatModifier(ConstitutionBonus, StatModifierType.Flat, this));
        }
		if(IntelligenceBonus != 0)
		{
            entity.EntityStatus.GetStat(StatType.Intelligence).AddModifier(new StatModifier(IntelligenceBonus, StatModifierType.Flat, this));
        }
		if(CharismaBonus != 0)
		{
            entity.EntityStatus.GetStat(StatType.Charisma).AddModifier(new StatModifier(CharismaBonus, StatModifierType.Flat, this));
        }
		if(LuckBonus != 0)
		{
            entity.EntityStatus.GetStat(StatType.Luck).AddModifier(new StatModifier(LuckBonus, StatModifierType.Flat, this));
        }
		if(PerceptionBonus != 0)
		{
            entity.EntityStatus.GetStat(StatType.Perception).AddModifier(new StatModifier(PerceptionBonus, StatModifierType.Flat, this));
        }

		if(StrengthPercentBonus != 0)
		{
            entity.EntityStatus.GetStat(StatType.Strength).AddModifier(new StatModifier(StrengthPercentBonus, StatModifierType.PercentMult, this));
        }
		if(DexterityPercentBonus != 0)
		{
            entity.EntityStatus.GetStat(StatType.Dexterity).AddModifier(new StatModifier(DexterityPercentBonus, StatModifierType.PercentMult, this));
        }
		if(ConstitutionPercentBonus != 0)
		{
            entity.EntityStatus.GetStat(StatType.Constitution).AddModifier(new StatModifier(ConstitutionPercentBonus, StatModifierType.PercentMult, this));
        }
		if(IntelligencePercentBonus != 0)
		{
            entity.EntityStatus.GetStat(StatType.Intelligence).AddModifier(new StatModifier(IntelligencePercentBonus, StatModifierType.PercentMult, this));
        }
		if(CharismaPercentBonus != 0)
		{
            entity.EntityStatus.GetStat(StatType.Charisma).AddModifier(new StatModifier(CharismaPercentBonus, StatModifierType.PercentMult, this));
        }
		if(LuckPercentBonus != 0)
		{
            entity.EntityStatus.GetStat(StatType.Luck).AddModifier(new StatModifier(LuckPercentBonus, StatModifierType.PercentMult, this));
        }
		if(PerceptionPercentBonus != 0)
		{
            entity.EntityStatus.GetStat(StatType.Perception).AddModifier(new StatModifier(PerceptionPercentBonus, StatModifierType.PercentMult, this));
        }


	}


}