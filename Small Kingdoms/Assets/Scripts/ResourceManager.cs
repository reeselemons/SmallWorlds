using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    public Resource Resource { get; set; }
    public ResourceType Type;

    // Start is called before the first frame update
    void Start()
    {
        SetResourceType();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SetResourceType()
    {
        switch (Type)
        {
            case ResourceType.Coal:
                Resource = new Coal();
                break;
            case ResourceType.CoalOre:
                Resource = new CoalOre();
                break;
            case ResourceType.ContaminatedWater:
                Resource = new ContaminatedWater();
                break;
            case ResourceType.Water:
                Resource = new Water();
                break;
            case ResourceType.Lumber:
                Resource = new Lumber();
                break;
            case ResourceType.WoodenBoard:
                Resource = new WoodenBoard();
                break;
            case ResourceType.Food:
                Resource = new Food();
                break;
            case ResourceType.Meat:
                Resource = new Meat();
                break;
            case ResourceType.MetalOre:
                Resource = new MetalOre();
                break;
            case ResourceType.Metal:
                Resource = new Metal();
                break;
            default:
                Destroy(this);
                break;
        }
    }
    public bool HasResource(double takeAmount)
    {
        if (Resource.CurrentAvailable > 0)
            return true;
        else
            return false;
    }
    public bool DepleteResource(NPCManager manager, double takeAmount, ResourceType type)
    {
        try
        {
            if (Resource.CurrentAvailable <= takeAmount)
            {
                manager.Character.ResourceInventory[type].Amount += Resource.CurrentAvailable;
                Resource.CurrentAvailable = 0;
            }
            else
            {
                manager.Character.ResourceInventory[type].Amount += takeAmount;
                Resource.CurrentAvailable -= takeAmount;
            }
            return true;
        }
        catch
        {
            return false;
        }
    }
}
