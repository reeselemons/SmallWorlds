using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class WorldObjects
{
    public static GameObject Enviroment { get; set; }
    public static GameObject Settlements { get; set; }
    public static GameObject Resources { get; set; }
    public static GameObject Buildings { get; set; }

}

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        WorldObjects.Enviroment = GameObject.Find("Enviroment");
        WorldObjects.Settlements = GameObject.Find("Enviroment/Settlements");
        WorldObjects.Buildings = GameObject.Find("Enviroment/Settlements/Kingdom1/Buildings");
        WorldObjects.Resources = GameObject.Find("Enviroment/Resources");
    }

    // Update is called once per frame
    void Update()
    {
    }

    GameObject InstatiateNPC(NPCTypes type)
    {
        GameObject npc = Resources.Load("Prefabs/Unit_NPC") as GameObject;
        var peopleObj = GameObject.Find("Enviroment/Settlements/Kingdom1/People");
        var obj = Instantiate(npc, npc.transform.position, npc.transform.rotation);
        obj.transform.SetParent(peopleObj.transform);
        return obj;
    }

    void SetupNPC(GameObject obj, NPCTypes type)
    {
        NPCManager manager = obj.GetComponent<NPCManager>();
        NPCTask task = obj.GetComponent<NPCTask>();
        manager.SetCharacterType(obj, type, manager, task);
        //Destroy(obj.GetComponent<NPCTask>());
    }

    public void InstatiateNPC_LumberJack()
    {
        var type = NPCTypes.LumberJack;
        var obj = InstatiateNPC(type);
        if (obj == null)
            return;

        obj.AddComponent<LumberJackTasks>();
        SetupNPC(obj, type);
    }
    public void InstatiateNPC_Farmer()
    {
        var type = NPCTypes.Farmer;

        var obj = InstatiateNPC(type);
        if (obj == null)
            return;

        obj.AddComponent<FarmerTasks>();
        SetupNPC(obj, type);

    }
    public void InstatiateNPC_WaterGatherer()
    {
        var type = NPCTypes.WaterGatherer;

        var obj = InstatiateNPC(type);
        if (obj == null)
            return;

        obj.AddComponent<WaterGathererTasks>();
        SetupNPC(obj, type);
    }
    public void InstatiateNPC_CoalMiner()
    {
        var type = NPCTypes.CoalMiner;

        var obj = InstatiateNPC(type);
        if (obj == null)
            return;

        obj.AddComponent<CoalMinerTasks>();
        SetupNPC(obj, type);
    }
    public void InstatiateNPC_Cook()
    {
        var type = NPCTypes.Cook;

        var obj = InstatiateNPC(type);
        if (obj == null)
            return;

        obj.AddComponent<CookTasks>();
        SetupNPC(obj, type);
    }
    public void InstatiateNPC_Hunter()
    {
        var type = NPCTypes.Hunter;

        var obj = InstatiateNPC(type);
        if (obj == null)
            return;

        obj.AddComponent<HunterTasks>();
        SetupNPC(obj, type);
    }
}
