using System.Collections;
using System.Collections.Generic;

public abstract class Resource
{
    public string Name { get; set; }
    public double TotalAvailable = 10000;
    public double CurrentAvailable = 10000;
    public int InventoryUsage { get; set; }
    public double WeightUsage { get; set; }
    public ResourceType ResourceType { get; set; }
}
public class ResourceInInventory
{
    public ResourceInInventory(Resource type)
    {
        ResourceTypes = type;
    }

    public double Amount { get; set; }
    public Resource ResourceTypes { get; set; }
}
public class MetalOre : Resource
{
    public MetalOre()
    {
        this.ResourceType = ResourceType.MetalOre;
        this.Name = "Metal Ore";
        this.InventoryUsage = 10;
        this.WeightUsage = 20;
    }
}
public class Metal : Resource
{
    public Metal()
    {
        this.ResourceType = ResourceType.Metal;
        this.Name = "Metal";
        this.InventoryUsage = 10;
        this.WeightUsage = 20;
    }
}
public class Coal : Resource
{
    public Coal()
    {
        this.ResourceType = ResourceType.Coal;
        this.Name = "Coal";
        this.InventoryUsage = 10;
        this.WeightUsage = 20;
    }
}
public class CoalOre : Resource
{
    public CoalOre()
    {
        this.ResourceType = ResourceType.CoalOre;
        this.Name = "Coal Ore";
        this.InventoryUsage = 10;
        this.WeightUsage = 20;
    }
}
public class Lumber : Resource
{
    public Lumber()
    {
        this.ResourceType = ResourceType.Lumber;
        this.Name = "Lumber";
        this.InventoryUsage = 10;
        this.WeightUsage = 15;

    }
}
public class WoodenBoard : Resource
{
    public WoodenBoard()
    {
        this.ResourceType = ResourceType.WoodenBoard;
        this.Name = "Wooden Board";
        this.InventoryUsage = 10;
        this.WeightUsage = 15;

    }
}
public class ContaminatedWater : Resource
{
    public ContaminatedWater()
    {
        this.ResourceType = ResourceType.ContaminatedWater;
        this.Name = "Contaminated Water";
        this.InventoryUsage = 1;
        this.WeightUsage = 5;
    }
}
public class Water : Resource
{
    public Water()
    {
        this.ResourceType = ResourceType.Water;
        this.Name = "Water";
        this.InventoryUsage = 1;
        this.WeightUsage = 5;
    }
}
public class Food : Resource
{
    public Food()
    {
        this.ResourceType = ResourceType.Food;
        this.Name = "Food";
        this.InventoryUsage = 3;
        this.WeightUsage = 7;
    }
}
public class Meat : Resource
{
    public Meat()
    {
        this.ResourceType = ResourceType.Meat;
        this.Name = "Meat";
        this.InventoryUsage = 3;
        this.WeightUsage = 7;
    }
}

public enum ResourceType
{
    Unassigned,
    CoalOre,
    Coal,
    Lumber,
    Water,
    Meat,
    MetalOre,
    Metal,
    ContaminatedWater,
    Food,
    WoodenBoard,

}

