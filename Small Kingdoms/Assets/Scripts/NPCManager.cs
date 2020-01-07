using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCManager : MonoBehaviour
{
    public NPC Character { get; set; }
    public NPCTypes Type;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        Character.Task.CheckPriorities(this);
    }
    private void OnCollisionStay(Collision collision)
    {
        if (Character.CurrentPriorityTask == NPCPriorityTask.MovingToResource)
            Character.Task.OnResource(this, collision.gameObject);

        if (Character.CurrentPriorityTask == NPCPriorityTask.MovingToWithdrawResource && Character.NPCGatheringType == NPCGatheringType.resource)
            Character.Task.OnBuilding(this, collision.gameObject);
        else if (Character.CurrentPriorityTask == NPCPriorityTask.MovingToWithdrawResource && Character.NPCGatheringType == NPCGatheringType.structure)
            Character.Task.OnBuilding_RetrieveResource(this, collision.gameObject);

        if (collision.collider.name == "InsideCastle")
            Debug.Log(collision.collider.name);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (Character.CurrentPriorityTask == NPCPriorityTask.MovingToDeposit && Character.NPCGatheringType == NPCGatheringType.resource)
            Character.Task.OnBuilding(this, collision.gameObject);
        else if (Character.CurrentPriorityTask == NPCPriorityTask.MovingToDeposit && Character.NPCGatheringType == NPCGatheringType.structure)
            Character.Task.OnBuilding_RetrieveResource(this, collision.gameObject);

        if (collision.collider.name == "InsideCastle")
            Debug.Log(collision.collider.name);
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.collider.name == "InsideCastle")
        Debug.Log(collision.collider.name);

    }
    public void SetCharacterType(GameObject npc, NPCTypes type, NPCManager manager, NPCTask task)
    {
        switch (type)
        {
            case NPCTypes.CoalMiner:
                Character = new CoalMiner(npc);
                break;
            case NPCTypes.Blacksmith:
                Character = new Blacksmith(npc);
                break;
            case NPCTypes.WaterGatherer:
                Character = new WaterGatherer(npc);
                break;
            case NPCTypes.WaterPurifier:
                Character = new WaterPurifier(npc);
                break;
            case NPCTypes.LumberJack:
                Character = new LumberJack(npc);
                break;
            case NPCTypes.StoneBuilder:
                Character = new StoneBuilder(npc);
                break;
            case NPCTypes.Cook:
                Character = new Cook(npc);
                break;
            case NPCTypes.Farmer:
                Character = new Farmer(npc);
                break;
            case NPCTypes.Hunter:
                Character = new Hunter(npc);
                break;
            default:
                Destroy(this);
                return;
        }
        Type = type;
        manager.Character.Task = task;
    }

}
