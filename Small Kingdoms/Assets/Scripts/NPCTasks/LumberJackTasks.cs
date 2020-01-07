using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class LumberJackTasks : NPCTask
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
