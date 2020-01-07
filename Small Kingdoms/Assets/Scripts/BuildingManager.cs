using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingManager : MonoBehaviour
{
    public Building Building { get; set; }
    public BuildingType Type;

    // Start is called before the first frame update
    void Start()
    {
        SetBuildingType();
    }

    // Update is called once per frame
    void Update()
    {

    }
    void SetBuildingType()
    {
        switch (Type)
        {
            case BuildingType.StockPile:
                Building = new StockPile();
                break;
            case BuildingType.Armory:
                Building = new Armory();
                break;
            case BuildingType.House:
                Building = new House();
                break;
            case BuildingType.Kitchen:
                Building = new Kitchen();
                break;
            case BuildingType.LumberMill:
                Building = new LumberMill();
                break;
            case BuildingType.Purifier:
                Building = new Purifier();
                break;
            case BuildingType.Smithy:
                Building = new Smithy();
                break;
            case BuildingType.Stonery:
                Building = new Stonery();
                break;
            case BuildingType.WaterSilo:
                Building = new WaterSilo();
                break;
            default:
                Destroy(this);
                break;
        }
    }
    public bool CheckBuildingSpace_Take(double amount)
    {
        if (Building.InventorySpaceUsed - amount <= 0)
            return false;
        else
            return true;
    }
    public bool CheckBuildingSpace_Deposit(double amount)
    {
        if (Building.InventorySpaceUsed + amount >= Building.TotalInventoryAvailable)
            return false;
        else
            return true;
    }
}
