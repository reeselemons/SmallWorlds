using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class BlacksmithTasks : NPCTask
{
    // Start is called before the first frame update
    void Start()
    {
        manager.Character = new Blacksmith(gameObject);

    }

    // Update is called once per frame
    void Update()
    {
        PriorityChecks();
    }

    void PriorityChecks()
    {
        CheckPriorities(manager);
    }
}