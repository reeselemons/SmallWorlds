using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public abstract class NPC
{
    public string Name { get; set; }
    public double TotalHealth = 100;
    public double CurrentHealth = 100;
    public float Speed = 1;
    public int CurrentInventory = 0;
    public int TotalInventory = 50;
    public int GatherResource_Amount = 5;
    public int GatherResource_Speed = 1;
    public double CurrentWeight = 0;
    public double WeightCanCarry = 100;
    public NPCTask Task { get; set; }
    public NPCPriorityTask CurrentPriorityTask = NPCPriorityTask.Idle;
    public NPCGatheringType NPCGatheringType = NPCGatheringType.resource;
    public List<int> LocateResource_Building_PreviouslyVisited { get; set; }
    public List<int> DepositResources_PreviouslyVisited { get; set; }
    public List<NPCPriorityTask> NPCPriorityTasks { get; set; }
    public List<ResourceType> GatherResources { get; set; }
    public List<ResourceType> ReturnResources { get; set; }
    public Dictionary<ResourceType, ResourceInInventory> ResourceInventory { get; set; }

    public NPC()
    {
        this.NPCPriorityTasks = new List<NPCPriorityTask>();
        this.GatherResources = new List<ResourceType>();
        this.ReturnResources = new List<ResourceType>();
        this.LocateResource_Building_PreviouslyVisited = new List<int>();
        this.DepositResources_PreviouslyVisited = new List<int>();
        this.ResourceInventory = new Dictionary<ResourceType, ResourceInInventory>();
    }
}
public class Guard : NPC
{

    public Guard(GameObject obj)
    {
        #region NPCPriorityTasks
        this.NPCPriorityTasks.Add(NPCPriorityTask.Idle);
        this.NPCPriorityTasks.Add(NPCPriorityTask.CheckHealth);
        this.NPCPriorityTasks.Add(NPCPriorityTask.GatherResource);
        #endregion


        this.Name = "Guard";
        obj.name = this.Name;
        this.TotalHealth += 200;
        this.CurrentHealth = this.TotalHealth;
        this.TotalInventory += 50;
        this.Speed -= .5f;
        this.WeightCanCarry += 150;
    }
}
public class CoalMiner : NPC
{

    public CoalMiner(GameObject obj)
    {
        #region NPCPriorityTasks
        this.NPCPriorityTasks.Add(NPCPriorityTask.Idle);
        this.NPCPriorityTasks.Add(NPCPriorityTask.CheckHealth);
        this.NPCPriorityTasks.Add(NPCPriorityTask.GatherResource);
        #endregion
        #region NPCResourceTypes
        this.GatherResources.Add(ResourceType.CoalOre);
        this.ReturnResources.Add(ResourceType.CoalOre);
        this.ResourceInventory.Add(ResourceType.CoalOre, new ResourceInInventory(new CoalOre()));
        #endregion
        this.Name = "Coal Miner";
        obj.name = this.Name;
        this.TotalHealth += 100;
        this.CurrentHealth = this.TotalHealth;
        this.TotalInventory += 20;
        this.Speed += .5f;
        this.WeightCanCarry += 75;
    }
}
public class Blacksmith : NPC
{

    public Blacksmith(GameObject obj)
    {
        #region NPCPriorityTasks
        this.NPCPriorityTasks.Add(NPCPriorityTask.Idle);
        this.NPCPriorityTasks.Add(NPCPriorityTask.CheckHealth);
        this.NPCPriorityTasks.Add(NPCPriorityTask.GatherResource);


        #endregion
        #region NPCResourceTypes
        this.GatherResources.Add(ResourceType.MetalOre);
        this.ReturnResources.Add(ResourceType.MetalOre);
        this.ReturnResources.Add(ResourceType.Metal);
        this.ResourceInventory.Add(ResourceType.MetalOre, new ResourceInInventory(new MetalOre()));
        this.ResourceInventory.Add(ResourceType.Metal, new ResourceInInventory(new Metal()));
        #endregion
        this.Name = "Blacksmith";
        obj.name = this.Name;
        this.TotalHealth += 100;
        this.CurrentHealth = this.TotalHealth;
        this.TotalInventory += 20;
        this.Speed -= .5f;
        this.WeightCanCarry += 75;

        this.NPCGatheringType = NPCGatheringType.structure;

    }
}
public class MetalMiner : NPC
{

    public MetalMiner(GameObject obj)
    {
        #region NPCPriorityTasks
        this.NPCPriorityTasks.Add(NPCPriorityTask.Idle);
        this.NPCPriorityTasks.Add(NPCPriorityTask.CheckHealth);
        this.NPCPriorityTasks.Add(NPCPriorityTask.GatherResource);
        #endregion
        #region NPCResourceTypes
        this.GatherResources.Add(ResourceType.MetalOre);
        this.ReturnResources.Add(ResourceType.MetalOre);
        this.ResourceInventory.Add(ResourceType.MetalOre, new ResourceInInventory(new MetalOre()));
        #endregion
        this.Name = "Metal Miner";
        obj.name = this.Name;
        this.TotalHealth += 100;
        this.CurrentHealth = this.TotalHealth;
        this.TotalInventory += 20;
        this.Speed -= .5f;
        this.WeightCanCarry += 75;
    }
}
public class StoneBuilder : NPC
{

    public StoneBuilder(GameObject obj)
    {
        #region NPCPriorityTasks
        this.NPCPriorityTasks.Add(NPCPriorityTask.Idle);
        this.NPCPriorityTasks.Add(NPCPriorityTask.CheckHealth);
        this.NPCPriorityTasks.Add(NPCPriorityTask.GatherResource);
        #endregion
        #region NPCResourceTypes
        //this.GatherResources.Add(ResourceType.Meat);
        //this.ReturnResources.Add(ResourceType.Meat);
        #endregion
        this.Name = "Stone Builder";
        obj.name = this.Name;
        this.TotalHealth += 100;
        this.CurrentHealth = this.TotalHealth;
        this.TotalInventory += 20;
        this.Speed -= .5f;
        this.WeightCanCarry += 75;

        this.NPCGatheringType = NPCGatheringType.structure;
    }
}
public class Farmer : NPC
{
    public Farmer(GameObject obj)
    {
        #region NPCPriorityTasks
        this.NPCPriorityTasks.Add(NPCPriorityTask.Idle);
        this.NPCPriorityTasks.Add(NPCPriorityTask.CheckHealth);
        this.NPCPriorityTasks.Add(NPCPriorityTask.GatherResource);
        #endregion
        #region NPCResourceTypes
        this.GatherResources.Add(ResourceType.Meat);
        this.ReturnResources.Add(ResourceType.Meat);
        this.ResourceInventory.Add(ResourceType.Meat, new ResourceInInventory(new Meat()));
        #endregion
        this.Name = "Farmer";
        obj.name = this.Name;
        this.TotalHealth += 10;
        this.CurrentHealth = this.TotalHealth;
        this.TotalInventory += 10;
        this.Speed += .5f;
        this.WeightCanCarry += 25;
    }
}
public class Cook : NPC
{
    public Cook(GameObject obj)
    {
        #region NPCPriorityTasks
        this.NPCPriorityTasks.Add(NPCPriorityTask.Idle);
        this.NPCPriorityTasks.Add(NPCPriorityTask.CheckHealth);
        this.NPCPriorityTasks.Add(NPCPriorityTask.GatherResource);
        #endregion
        #region NPCResourceTypes
        this.GatherResources.Add(ResourceType.Meat);
        this.ReturnResources.Add(ResourceType.Meat);
        this.ReturnResources.Add(ResourceType.Food);
        this.ResourceInventory.Add(ResourceType.Food, new ResourceInInventory(new Food()));
        this.ResourceInventory.Add(ResourceType.Meat, new ResourceInInventory(new Meat()));
        #endregion
        this.Name = "Cook";
        obj.name = this.Name;
        this.TotalHealth += 10;
        this.CurrentHealth = this.TotalHealth;
        this.TotalInventory += 10;
        this.Speed += .5f;
        this.WeightCanCarry += 25;

        this.NPCGatheringType = NPCGatheringType.structure;
    }
}
public class Hunter : NPC
{
    public Hunter(GameObject obj)
    {
        #region NPCPriorityTasks
        this.NPCPriorityTasks.Add(NPCPriorityTask.Idle);
        this.NPCPriorityTasks.Add(NPCPriorityTask.CheckHealth);
        this.NPCPriorityTasks.Add(NPCPriorityTask.GatherResource);
        #endregion
        #region NPCResourceTypes
        this.GatherResources.Add(ResourceType.Meat);
        this.ReturnResources.Add(ResourceType.Meat);
        this.ResourceInventory.Add(ResourceType.Meat, new ResourceInInventory(new Meat()));
        #endregion
        this.Name = "Hunter";
        obj.name = this.Name;
        this.TotalHealth += 10;
        this.CurrentHealth = this.TotalHealth;
        this.TotalInventory += 10;
        this.Speed += .5f;
        this.WeightCanCarry += 25;

        this.NPCGatheringType = NPCGatheringType.resource;
    }
}
public class WaterPurifier : NPC
{
    public WaterPurifier(GameObject obj)
    {
        #region NPCPriorityTasks
        this.NPCPriorityTasks.Add(NPCPriorityTask.Idle);
        this.NPCPriorityTasks.Add(NPCPriorityTask.CheckHealth);
        this.NPCPriorityTasks.Add(NPCPriorityTask.GatherResource);
        #endregion
        #region NPCResourceTypes
        this.GatherResources.Add(ResourceType.ContaminatedWater);
        this.ReturnResources.Add(ResourceType.ContaminatedWater);
        this.ReturnResources.Add(ResourceType.Water);
        this.ResourceInventory.Add(ResourceType.Water, new ResourceInInventory(new Water()));
        this.ResourceInventory.Add(ResourceType.ContaminatedWater, new ResourceInInventory(new ContaminatedWater()));
        #endregion
        this.Name = "Water Purifier";
        obj.name = this.Name;
        this.TotalHealth += 10;
        this.CurrentHealth = this.TotalHealth;
        this.TotalInventory += 10;
        this.Speed += .5f;
        this.WeightCanCarry += 25;

        this.NPCGatheringType = NPCGatheringType.structure;
    }
}
public class WaterGatherer : NPC
{
    public WaterGatherer(GameObject obj)
    {
        #region NPCPriorityTasks
        this.NPCPriorityTasks.Add(NPCPriorityTask.Idle);
        this.NPCPriorityTasks.Add(NPCPriorityTask.CheckHealth);
        this.NPCPriorityTasks.Add(NPCPriorityTask.GatherResource);
        #endregion

        #region NPCResourceTypes
        this.GatherResources.Add(ResourceType.ContaminatedWater);
        this.ReturnResources.Add(ResourceType.ContaminatedWater);
        this.ResourceInventory.Add(ResourceType.ContaminatedWater, new ResourceInInventory(new ContaminatedWater()));
        #endregion

        this.Name = "Water Gatherer";
        obj.name = this.Name;
        this.TotalHealth -= 15;
        this.CurrentHealth = this.TotalHealth;
        this.TotalInventory -= 10;
        this.Speed += 1;
        this.WeightCanCarry -= 20;
    }
}
public class LumberJack : NPC
{
    public LumberJack(GameObject obj)
    {
        #region NPCPriorityTasks
        this.NPCPriorityTasks.Add(NPCPriorityTask.Idle);
        this.NPCPriorityTasks.Add(NPCPriorityTask.CheckHealth);
        this.NPCPriorityTasks.Add(NPCPriorityTask.GatherResource);
        #endregion


        #region NPCResourceTypes
        this.GatherResources.Add(ResourceType.Lumber);
        this.ReturnResources.Add(ResourceType.Lumber);
        this.ResourceInventory.Add(ResourceType.Lumber, new ResourceInInventory(new Lumber()));
        #endregion

        this.Name = "Lumber Jack";
        obj.name = this.Name;
        this.TotalHealth += 50;
        this.Speed += 1;
        this.CurrentHealth = this.TotalHealth;
        this.TotalInventory += 20;
        this.WeightCanCarry += 50;
    }
}

public enum NPCGatheringType
{
    resource,
    structure
}
public enum NPCTypes
{
    Unassigned,
    MetalMiner,
    WaterPurifier,
    Cook,
    Hunter,
    StoneBuilder,
    Guard,
    Blacksmith,
    CoalMiner,
    Farmer,
    WaterGatherer,
    LumberJack
}
public enum NPCPriorityTask
{
    Idle,
    CheckHealth,
    GatherResource,
    SeekMedic,
    SeekGuard,
    MovingToResource,
    MovingToDeposit,
    MovingToWithdrawResource,
    DepositResource,
    LocateDepositBuilding,
    LocateDepositBuilding_Next,

    LocateDepositBuilding_ForGather,
    LocateDepositBuilding_Next_ForGather,

    GatherResource_CheckInventoryAndWeight,
    CheckOrder,

}
