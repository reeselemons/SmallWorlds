using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public abstract class Building
{
    public string Name { get; set; }
    public double TotalInventoryAvailable = 200;
    public double InventorySpaceUsed = 0;
    public BuildingType BuildingType { get; set; }
    public List<ResourceType> AcceptableResourceTypes { get; set; }
    public Dictionary<ResourceType, ResourceInInventory> StoredResources { get; set; }

    public Building()
    {
        this.StoredResources = new Dictionary<ResourceType, ResourceInInventory>();
        this.AcceptableResourceTypes = new List<ResourceType>();
    }
}
public class StockPile : Building
{
    public StockPile()
    {
        #region AcceptableResourceTypes
        this.AcceptableResourceTypes.Add(ResourceType.Coal);
        this.AcceptableResourceTypes.Add(ResourceType.CoalOre);
        this.AcceptableResourceTypes.Add(ResourceType.Food);
        this.AcceptableResourceTypes.Add(ResourceType.Lumber);
        this.AcceptableResourceTypes.Add(ResourceType.Meat);
        this.AcceptableResourceTypes.Add(ResourceType.MetalOre);
        this.AcceptableResourceTypes.Add(ResourceType.WoodenBoard);
        this.AcceptableResourceTypes.Add(ResourceType.ContaminatedWater);
        #endregion
        #region ResourceTypes
        this.StoredResources.Add(ResourceType.ContaminatedWater, new ResourceInInventory(new ContaminatedWater()));
        this.StoredResources.Add(ResourceType.Coal, new ResourceInInventory(new Coal()));
        this.StoredResources.Add(ResourceType.CoalOre, new ResourceInInventory(new CoalOre()));
        this.StoredResources.Add(ResourceType.Food, new ResourceInInventory(new Food()));
        this.StoredResources.Add(ResourceType.Lumber, new ResourceInInventory(new Lumber()));
        this.StoredResources.Add(ResourceType.Meat, new ResourceInInventory(new Meat()));
        this.StoredResources.Add(ResourceType.WoodenBoard, new ResourceInInventory(new WoodenBoard()));
        this.StoredResources.Add(ResourceType.MetalOre, new ResourceInInventory(new MetalOre()));
        #endregion
        this.BuildingType = BuildingType.StockPile;
        this.Name = "StockPile";
        this.TotalInventoryAvailable = 20;
    }
}
public class WaterSilo : Building
{
    public WaterSilo()
    {
        #region AcceptableResourceTypes
        this.AcceptableResourceTypes.Add(ResourceType.Water);
        #endregion
        #region ResourceTypes
        this.StoredResources.Add(ResourceType.Water, new ResourceInInventory(new Water()));
        #endregion
        this.BuildingType = BuildingType.Purifier;
        this.Name = "Water Purifier";
        this.TotalInventoryAvailable = 500;

    }
}
public class Purifier : Building
{
    public Purifier()
    {
        #region AcceptableResourceTypes
        this.AcceptableResourceTypes.Add(ResourceType.Water);
        this.AcceptableResourceTypes.Add(ResourceType.ContaminatedWater);
        #endregion
        #region ResourceTypes
        this.StoredResources.Add(ResourceType.Water, new ResourceInInventory(new Water()));
        this.StoredResources.Add(ResourceType.ContaminatedWater, new ResourceInInventory(new ContaminatedWater()));
        #endregion
        this.BuildingType = BuildingType.Purifier;
        this.Name = "Purifier";
        this.TotalInventoryAvailable = 100;
    }
}
public class LumberMill : Building
{
    public LumberMill()
    {
        #region AcceptableResourceTypes
        this.AcceptableResourceTypes.Add(ResourceType.Lumber);
        this.AcceptableResourceTypes.Add(ResourceType.WoodenBoard);
        #endregion
        #region ResourceTypes
        this.StoredResources.Add(ResourceType.Lumber, new ResourceInInventory(new Lumber()));
        this.StoredResources.Add(ResourceType.WoodenBoard, new ResourceInInventory(new WoodenBoard()));
        #endregion
        this.BuildingType = BuildingType.LumberMill;
        this.Name = "Lumber Mill";
        this.TotalInventoryAvailable = 100;
    }
}
public class Kitchen : Building
{
    public Kitchen()
    {
        #region AcceptableResourceTypes
        this.AcceptableResourceTypes.Add(ResourceType.Food);
        this.AcceptableResourceTypes.Add(ResourceType.Meat);
        #endregion
        #region ResourceTypes
        this.StoredResources.Add(ResourceType.Meat, new ResourceInInventory(new Meat()));
        this.StoredResources.Add(ResourceType.Food, new ResourceInInventory(new Food()));
        #endregion
        this.BuildingType = BuildingType.Kitchen;
        this.Name = "Kitchen";
        this.TotalInventoryAvailable = 50;
    }
}
public class Armory : Building
{
    public Armory()
    {
        #region AcceptableResourceTypes

        #endregion
        #region ResourceTypes
        this.StoredResources.Add(ResourceType.MetalOre, new ResourceInInventory(new Metal()));
        #endregion
        this.BuildingType = BuildingType.Armory;
        this.Name = "Armory";
        this.TotalInventoryAvailable = 50;
    }
}
public class Smithy : Building
{
    public Smithy()
    {
        #region AcceptableResourceTypes
        this.AcceptableResourceTypes.Add(ResourceType.Metal);
        #endregion
        #region ResourceTypes
        this.StoredResources.Add(ResourceType.Metal, new ResourceInInventory(new Metal()));
        #endregion
        this.BuildingType = BuildingType.Smithy;
        this.Name = "Smithy";
        this.TotalInventoryAvailable = 100;

    }
}
public class Forge : Building
{
    public Forge()
    {
        #region AcceptableResourceTypes
        this.AcceptableResourceTypes.Add(ResourceType.Metal);
        this.AcceptableResourceTypes.Add(ResourceType.MetalOre);
        #endregion
        #region ResourceTypes
        this.StoredResources.Add(ResourceType.Metal, new ResourceInInventory(new Metal()));
        this.StoredResources.Add(ResourceType.MetalOre, new ResourceInInventory(new MetalOre()));
        #endregion
        this.BuildingType = BuildingType.Forge;
        this.Name = "Forge";
        this.TotalInventoryAvailable = 100;
    }
}
public class Stonery : Building
{
    public Stonery()
    {
        #region AcceptableResourceTypes
        this.AcceptableResourceTypes.Add(ResourceType.Coal);
        this.AcceptableResourceTypes.Add(ResourceType.CoalOre);
        #endregion
        #region ResourceTypes
        //this.StoredResources.Add(ResourceType.Coal, new ResourceInInventory(new Coal()));
        #endregion
        this.BuildingType = BuildingType.Stonery;
        this.Name = "Stonery";
        this.TotalInventoryAvailable = 100;
    }
}
public class House : Building
{
    public House()
    {
        #region AcceptableResourceTypes

        #endregion
        #region ResourceTypes
        this.StoredResources.Add(ResourceType.Food, new ResourceInInventory(new Food()));
        #endregion
        this.BuildingType = BuildingType.House;
        this.Name = "House";
        this.TotalInventoryAvailable = 100;
    }
}


public enum BuildingType
{
    Unassigned,
    StockPile,
    WaterSilo,
    Purifier,
    LumberMill,
    Kitchen,
    Armory,
    Smithy,
    Stonery,
    House,
    Forge

}