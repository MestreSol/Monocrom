public class Weapon
{
    public string Name { get; set; }
    public int Damage { get; set; }
    public float Range { get; set; }
    public float FireRate { get; set; }
    public float CritChance { get; set; }
    // Add any other properties you need

    public Weapon(string name, int damage, float range, float fireRate)
    {
        Name = name;
        Damage = damage;
        Range = range;
        FireRate = fireRate;
    }

    // Add any other methods you need
}
