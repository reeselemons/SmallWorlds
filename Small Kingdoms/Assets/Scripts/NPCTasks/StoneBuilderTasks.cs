using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneBuilderTasks : NPCTask
{

    // Start is called before the first frame update
    void Start()
    {
        manager.Character = new StoneBuilder(gameObject);
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
