using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate : MonoBehaviour
{
    bool isRaisingGate = false;
    bool isRaised = false;
    Vector3 startTransform { get; set; }
    Transform parentTransform { get; set; }
    int ColliderCount = 0;

    // Start is called before the first frame update
    void Start()
    {
        startTransform = GetComponentInParent<Transform>().position;
        parentTransform = GetComponentInParent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (ColliderCount > 0 && !isRaised)
        {
            RaiseGate();
        }
        else if (ColliderCount == 0 && isRaised)
        {
            LowerGate();
        }
    }


    private void OnCollisionExit(Collision collision)
    {
        if (collision.collider.tag == "NPC")
            ColliderCount--;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "NPC")
            ColliderCount++;
    }

    public void RaiseGate()
    {
        var newPositions = new Vector3(startTransform.x, startTransform.y + 14, startTransform.z);
        parentTransform.position = Vector3.MoveTowards(parentTransform.position, newPositions, (.5f * Time.deltaTime));
       // Debug.Log(parentTransform.position.y)
        if (parentTransform.position.y == startTransform.y)
            isRaised = true;
    }
    public void LowerGate()
    {
        parentTransform.position = Vector3.MoveTowards(parentTransform.position, startTransform, (.5f * Time.deltaTime));
        if (parentTransform.position.y == startTransform.y)
            isRaised = false;
    }
}
