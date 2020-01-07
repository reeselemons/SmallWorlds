using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CoalMinerTasks : NPCTask
{
    // Start is called before the first frame update
    void Start()
    {
        if (manager == null)
            manager = GetComponent<NPCManager>();
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
