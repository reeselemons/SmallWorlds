using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class NPCTask : MonoBehaviour
{
    public GameObject closestObjective { get; set; }
    public NPCManager manager { get; set; }

    public void CheckPriorities(NPCManager manager)
    {
        if (SeekMedic(manager))
        {
            manager.Character.CurrentPriorityTask = NPCPriorityTask.SeekMedic;
        }
        else if (manager.Character.CurrentPriorityTask == NPCPriorityTask.Idle)
        {
            manager.Character.CurrentPriorityTask = NPCPriorityTask.GatherResource;
        }
        PerformPriorityTask(manager, manager.Character.CurrentPriorityTask);
    }
    public static void PerformPriorityTask(NPCManager manager, NPCPriorityTask task)
    {
        switch (task)
        {
            case NPCPriorityTask.CheckHealth:

                break;
            case NPCPriorityTask.GatherResource_CheckInventoryAndWeight:
                NPCTypeHelper.CheckInventoryAndWeight(manager);
                break;
            case NPCPriorityTask.LocateDepositBuilding:
            case NPCPriorityTask.LocateDepositBuilding_Next:
                NPCTypeHelper.LocateDepositBuilding(manager);
                break;
            case NPCPriorityTask.LocateDepositBuilding_ForGather:
            case NPCPriorityTask.LocateDepositBuilding_Next_ForGather:
                NPCTypeHelper.LocateDepositBuilding_ForGather(manager);
                break;
            case NPCPriorityTask.GatherResource:
                if (manager.Character.NPCGatheringType == NPCGatheringType.resource)
                    NPCTypeHelper.LocateResource(manager);
                else if (manager.Character.NPCGatheringType == NPCGatheringType.structure)
                    NPCTypeHelper.LocateResource_Building(manager);
                break;
            case NPCPriorityTask.MovingToResource:
            case NPCPriorityTask.MovingToDeposit:
            case NPCPriorityTask.MovingToWithdrawResource:
                NPCTypeHelper.MoveCharacterToLocation(manager);
                break;
            case NPCPriorityTask.SeekGuard:

                break;
            case NPCPriorityTask.SeekMedic:

                break;
            default:
                manager.Character.CurrentPriorityTask = NPCPriorityTask.Idle;
                break;
        }
    }

    public void OnResource(NPCManager manager, GameObject resourceObj)
    {
        ResourceManager resource = resourceObj.GetComponent<ResourceManager>();
        if (resource != null)
        {
            if (manager.Character.GatherResources.Contains(resource.Type))
            {
                if (resource.HasResource(manager.Character.GatherResource_Amount))
                {
                    if (resource.DepleteResource(manager, manager.Character.GatherResource_Amount, resource.Type))
                    {
                        manager.Character.CurrentPriorityTask = NPCPriorityTask.GatherResource_CheckInventoryAndWeight;
                        //Debug.Log($"{manager.Character.Name} took {manager.Character.GatherResource_Amount} {manager.Character.ResourceInventory[resource.Type].ResourceTypes.Name}");
                        //Debug.Log($"{manager.Character.Name} {manager.Character.ResourceInventory[resource.Type].ResourceTypes.Name} Inventory: {manager.Character.ResourceInventory[resource.Type].Amount}");
                    }
                }
            }
        }
    }
    public void OnBuilding(NPCManager manager, GameObject resourceObj, BuildingManager ignore = null)
    {
        BuildingManager building = resourceObj.GetComponent<BuildingManager>();
        var LocateDepositBuilding_Next = false;
        if (building != null && !manager.Character.DepositResources_PreviouslyVisited.Contains(building.GetInstanceID()))
        {
            foreach (var type in building.Building.AcceptableResourceTypes)
            {
                if (manager.Character.ReturnResources.Contains(type))
                {
                    if (building.Building.DepositResource(manager, manager.Character.ResourceInventory[type].Amount, type))
                    {
                        manager.Character.CurrentPriorityTask = NPCPriorityTask.Idle;
                        //Debug.Log($"{manager.Character.Name} deposited {manager.Character.GatherResource_Amount} {manager.Character.ResourceInventory[type].ResourceTypes.Name}");
                        //Debug.Log($"{manager.Character.Name} {manager.Character.ResourceInventory[type].ResourceTypes.Name} Inventory: {manager.Character.ResourceInventory[type].Amount}");
                        Debug.Log($"{building.Building.Name} {building.Building.StoredResources[type].ResourceTypes.Name} Inventory: {building.Building.StoredResources[type].Amount}");
                    }
                    else
                    {
                        manager.Character.DepositResources_PreviouslyVisited.Add(building.gameObject.GetInstanceID());
                        LocateDepositBuilding_Next = true;
                    }
                }
            }
            if (LocateDepositBuilding_Next)
                manager.Character.CurrentPriorityTask = NPCPriorityTask.LocateDepositBuilding_Next;
        }
    }
    public void OnBuilding_RetrieveResource(NPCManager manager, GameObject resourceObj, BuildingManager ignore = null)
    {
        BuildingManager building = resourceObj.GetComponent<BuildingManager>();
        var LocateDepositBuilding_Next = false;
        if (building != null)
        {
            foreach (var type in building.Building.AcceptableResourceTypes)
            {
                if (manager.Character.GatherResources.Contains(type))
                {
                    if (building.Building.RetrieveResource(manager, manager.Character.GatherResource_Amount, type))
                    {
                        manager.Character.CurrentPriorityTask = NPCPriorityTask.LocateDepositBuilding_ForGather;
                        //Debug.Log($"{manager.Character.Name} deposited {manager.Character.GatherResource_Amount} {manager.Character.ResourceInventory[type].ResourceTypes.Name}");
                        //Debug.Log($"{manager.Character.Name} {manager.Character.ResourceInventory[type].ResourceTypes.Name} Inventory: {manager.Character.ResourceInventory[type].Amount}");
                        //Debug.Log($"{building.Building.Name} {building.Building.StoredResources[type].ResourceTypes.Name} Inventory: {building.Building.StoredResources[type].Amount}");
                    }
                    else
                    {
                        LocateDepositBuilding_Next = true;
                    }
                }
            }
            if (LocateDepositBuilding_Next)
                manager.Character.CurrentPriorityTask = NPCPriorityTask.LocateDepositBuilding_Next_ForGather;
        }
    }

    private bool SeekMedic(NPCManager manager)
    {
        if (manager.Character.CurrentHealth < (manager.Character.TotalHealth / 2))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
internal static class NPCTypeHelper
{
    internal static void CheckInventoryAndWeight(NPCManager manager)
    {
        double totalInventory = 0;
        double totalWeight = 0;
        foreach (var resource in manager.Character.ResourceInventory)
        {
            totalInventory += resource.Value.Amount * resource.Value.ResourceTypes.InventoryUsage;
            totalWeight += resource.Value.Amount * resource.Value.ResourceTypes.WeightUsage;
        }

        manager.Character.CurrentWeight = totalWeight;
        manager.Character.CurrentInventory = (int)totalInventory;

        if (totalInventory >= manager.Character.TotalInventory || totalWeight >= manager.Character.WeightCanCarry)
        {
            manager.Character.CurrentPriorityTask = NPCPriorityTask.LocateDepositBuilding;
            return;
        }

        manager.Character.CurrentPriorityTask = NPCPriorityTask.MovingToResource;

    }
    internal static void MoveCharacterToLocation(NPCManager manager)
    {
        if (manager.Character.Task.closestObjective != null)
            manager.transform.position = Vector3.MoveTowards(manager.transform.position, manager.Character.Task.closestObjective.transform.position, (manager.Character.Speed * Time.deltaTime));
    }
    internal static void LocateResource(NPCManager manager)
    {
        float lastDistance = 99999999;
        GameObject closestResource = null;
        foreach (ResourceManager resource in WorldObjects.Resources.transform.GetComponentsInChildren<ResourceManager>().Where(e => manager.Character.GatherResources.Contains(e.Resource.ResourceType)))
        {
            var distance = Vector3.Distance(manager.transform.position, resource.transform.position);
            if (distance < lastDistance)
            {
                lastDistance = distance;
                closestResource = resource.gameObject;
            }
        }
        if (closestResource != null)
        {
            manager.Character.Task.closestObjective = closestResource;
            manager.Character.CurrentPriorityTask = NPCPriorityTask.MovingToResource;
        }
    }
    internal static void LocateResource_Building(NPCManager manager)
    {
        float lastDistance = 99999999;
        GameObject closestBuilding = null;
        foreach (var inventoryItem in manager.Character.ResourceInventory.Where(e => ((int)e.Value.Amount) <= manager.Character.TotalInventory).Select(e => e.Value))
        {
            var found = false;
            foreach (BuildingManager building in WorldObjects.Buildings.transform.GetComponentsInChildren<BuildingManager>().Where(e => e.Building.AcceptableResourceTypes.Contains(inventoryItem.ResourceTypes.ResourceType)))
            {
                var distance = Vector3.Distance(manager.transform.position, building.transform.position);
                if (distance < lastDistance && !manager.Character.LocateResource_Building_PreviouslyVisited.Contains(building.gameObject.GetInstanceID()))
                {
                    closestBuilding = building.gameObject;
                    if (manager.Character.CurrentPriorityTask == NPCPriorityTask.LocateDepositBuilding_Next)
                    {
                        if (manager.Character.LocateResource_Building_PreviouslyVisited.Contains(closestBuilding.GetInstanceID()))
                            continue;
                        closestBuilding = building.gameObject;
                        found = true;
                    }
                    found = true;
                    lastDistance = distance;

                }
            }
            if (found)
                break;
            else
                DisposeResource(manager, inventoryItem.ResourceTypes.ResourceType);
        }
        if (closestBuilding != null)
        {
            manager.Character.LocateResource_Building_PreviouslyVisited.Add(closestBuilding.GetInstanceID());
            manager.Character.Task.closestObjective = closestBuilding;
            manager.Character.CurrentPriorityTask = NPCPriorityTask.MovingToWithdrawResource;
        }
        else
        {

        }
    }
    internal static void LocateDepositBuilding_ForGather(NPCManager manager)
    {
        float lastDistance = 99999999;
        GameObject closestBuilding = null;
        foreach (var inventoryItem in manager.Character.ResourceInventory.Select(e => e.Value))
        {
            var found = false;
            foreach (BuildingManager building in WorldObjects.Buildings.transform.GetComponentsInChildren<BuildingManager>().Where(e => e.Building.AcceptableResourceTypes.Contains(inventoryItem.ResourceTypes.ResourceType)))
            {
                var distance = Vector3.Distance(manager.transform.position, building.transform.position);
                if (distance < lastDistance && !manager.Character.DepositResources_PreviouslyVisited.Contains(building.gameObject.GetInstanceID()))
                {
                    closestBuilding = building.gameObject;
                    if (manager.Character.CurrentPriorityTask == NPCPriorityTask.LocateDepositBuilding_Next)
                    {
                        if (manager.Character.DepositResources_PreviouslyVisited.Contains(closestBuilding.GetInstanceID()))
                            continue;
                        closestBuilding = building.gameObject;
                        found = true;

                    }
                    found = true;
                    lastDistance = distance;
                }
            }
            if (found)
                break;
            else
                DisposeResource(manager, inventoryItem.ResourceTypes.ResourceType);
        }
        if (closestBuilding != null)
        {
            manager.Character.DepositResources_PreviouslyVisited.Add(closestBuilding.GetInstanceID());
            manager.Character.Task.closestObjective = closestBuilding;
            manager.Character.CurrentPriorityTask = NPCPriorityTask.MovingToDeposit;
        }
        else
        {

        }
    }
    internal static void LocateDepositBuilding(NPCManager manager)
    {
        float lastDistance = 99999999;
        GameObject closestBuilding = null;
        foreach (var inventoryItem in manager.Character.ResourceInventory.Where(e => e.Value.Amount > 0).Select(e => e.Value))
        {
            var found = false;
            foreach (BuildingManager building in WorldObjects.Buildings.transform.GetComponentsInChildren<BuildingManager>().Where(e => e.Building.AcceptableResourceTypes.Contains(inventoryItem.ResourceTypes.ResourceType)))
            {
                var distance = Vector3.Distance(manager.transform.position, building.transform.position);
                if (distance < lastDistance && !manager.Character.DepositResources_PreviouslyVisited.Contains(building.gameObject.GetInstanceID()))
                {
                    closestBuilding = building.gameObject;
                    if (manager.Character.CurrentPriorityTask == NPCPriorityTask.LocateDepositBuilding_Next)
                    {
                        if (manager.Character.DepositResources_PreviouslyVisited.Contains(closestBuilding.GetInstanceID()))
                            continue;
                        closestBuilding = building.gameObject;
                        found = true;

                    }
                    found = true;
                    lastDistance = distance;
                }
            }
            if (found)
                break;
            else
                DisposeResource(manager, inventoryItem.ResourceTypes.ResourceType);
        }
        if (closestBuilding != null)
        {
            manager.Character.DepositResources_PreviouslyVisited.Add(closestBuilding.GetInstanceID());
            manager.Character.Task.closestObjective = closestBuilding;
            manager.Character.CurrentPriorityTask = NPCPriorityTask.MovingToDeposit;
        }
        else
        {

        }
    }
    internal static bool DisposeResource(NPCManager manager, ResourceType resourceType)
    {
        try
        {
            manager.Character.ResourceInventory[resourceType].Amount = 0;
            manager.Character.DepositResources_PreviouslyVisited.Clear();
            manager.Character.LocateResource_Building_PreviouslyVisited.Clear();
            manager.Character.CurrentPriorityTask = NPCPriorityTask.Idle;
            return true;
        }
        catch (Exception ex)
        {
            return false;
        }
    }
    internal static bool CanCarry(this NPCManager manager, double amount)
    {
        if (manager.Character.CurrentInventory + amount <= manager.Character.TotalInventory)
            return true;
        else
            return false;
    }

}
internal static class BuildingTypeHelper
{
    internal static bool IsEnoughSpace(this Building buildingType, double space)
    {
        if (buildingType.InventorySpaceUsed + space >= buildingType.TotalInventoryAvailable)
            return false;
        else
            return true;
    }
    internal static bool HasResource(this Building buildingType, double amount, ResourceType resourceType)
    {
        if (buildingType.StoredResources[resourceType].Amount - amount > 0)
            return true;
        else
            return false;
    }
    internal static bool DepositResource(this Building buildingType, NPCManager manager, double amount, ResourceType resourceType)
    {
        if (buildingType.IsEnoughSpace(amount))
        {
            var availableSpace = buildingType.TotalInventoryAvailable - (buildingType.InventorySpaceUsed + amount);
            try
            {
                if (availableSpace > 0)
                {
                    manager.Character.ResourceInventory[resourceType].Amount = 0;

                    buildingType.StoredResources[resourceType].Amount += amount;
                    buildingType.InventorySpaceUsed += amount;
                }
                else
                {
                    var amountAvaiableToDeposit = amount - availableSpace;
                    manager.Character.ResourceInventory[resourceType].Amount -= amountAvaiableToDeposit;

                    buildingType.StoredResources[resourceType].Amount += amountAvaiableToDeposit;
                    buildingType.InventorySpaceUsed += amountAvaiableToDeposit;
                }
                manager.Character.DepositResources_PreviouslyVisited.Clear();
                manager.Character.LocateResource_Building_PreviouslyVisited.Clear();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        else
        {
            return false;
        }
    }

    internal static bool RetrieveResource(this Building buildingType, NPCManager manager, double takeAmount, ResourceType resourceType)
    {
        if (buildingType.HasResource(takeAmount, resourceType) && manager.CanCarry(takeAmount))
        {
            try
            {

                manager.Character.ResourceInventory[resourceType].Amount += (buildingType.StoredResources[resourceType].Amount - takeAmount < 0 ? buildingType.StoredResources[resourceType].Amount : takeAmount);

                buildingType.StoredResources[resourceType].Amount -= (buildingType.StoredResources[resourceType].Amount - takeAmount < 0 ? buildingType.StoredResources[resourceType].Amount : takeAmount);
                buildingType.InventorySpaceUsed -= (buildingType.InventorySpaceUsed - takeAmount < 0 ? buildingType.InventorySpaceUsed : takeAmount);


                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        else
        {
            return false;
        }
    }
}