public class Item
{
    public string name{get;set;}
    public float value{get;set;}
    public float price{get;set;}
    public string description{get;set;}
    public Sprite icon{get;set;}

    public Category category{get;set;}
}
public class PlayerInventory{
    public List<Item> Items { get; set; }
    public float MaxWeight{get; set; }
    public float CurrentWeight{get; set; }

    public PlayerInventory(List<Item> Items, float MaxWeight, float CurrentWeight)
    {
        this.Items = Items;
        this.MaxWeight = MaxWeight;
        this.CurrentWeight = CurrentWeight;
    }
    public PlayerInventory()
    {
        this.Items = new List<Item>();
        this.MaxWeight = 0;
        this.CurrentWeight = 0;
    }

    public bool AddItem(Item item)
    {
        if((CurrentWeight + item.Weight)>MaxWeight)
        return false;

        Items.Add(item);
        CurrentWeight += item.Weight;
        return true;
    }

    public bool RemoveItem(Item item)
    {
        if(!Items.Contains(item))
            return false;
            Items.Remove(item);
            CurrentWeight -= item.Weight;
            return true;	
    }
}