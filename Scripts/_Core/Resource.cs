using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resource : MonoBehaviour
{
    public ResourceType resource;

    public ResourceCentre resourceCentre;
    public int age = 1;
    public int matureAge = 5;
    public int amountToGive = 5;
    public int maxAmount = 25;
    [Space]
    public int chargesLeft;
    public int gathered = 0;
    public float gatherTime;
    public string animTrigger;

    // Dragging objects
    Vector3 mOffset;
    float mouseZCoord;
    Camera cam;

    void OnEnable()
    {
        DayNightCycle.newDay += Age;
    }
    private void Awake()
    {
        cam = FindObjectOfType<Camera>();
        chargesLeft = maxAmount / amountToGive;
        gathered = chargesLeft;
    }

    private void Age()
    {
        if(age < matureAge)
        {
            age++;
        }
    }

    public void TakeResource()
    {
        chargesLeft--;
    }

    public virtual void Gather(Peon peon)
    {
        peon.itemInHand.resource = resource;
        peon.itemInHand.amount = amountToGive;

        gathered--;
        CheckResource();
    }

    private void CheckResource()
    {
        if (gathered == 0)
        {
            Debug.Log("Gathered");
            resourceCentre.RemoveNode(this);
            Destroy(gameObject);
        }
    }

    void OnDisable()
    {
        DayNightCycle.newDay -= Age;
    }
    
    void OnMouseDown()
    {
        Debug.Log(transform.name);
        mouseZCoord = cam.WorldToScreenPoint(gameObject.transform.position).z;

        mOffset = gameObject.transform.position - GetMouseWorldPos();

        Rigidbody rb = gameObject.AddComponent<Rigidbody>();
    }

    void OnMouseDrag()
    {        //mOffset = gameObject.transform.position - GetMouseWorldPos();

        transform.position = GetMouseWorldPos() + mOffset;
    }

    void OnMouseUp()
    {
        Debug.Log("tree dropped");
    }

    
    Vector3 GetMouseWorldPos()
    {
        Vector3 mousePoint = Input.mousePosition;

        mousePoint.z = mouseZCoord;

        return cam.ScreenToWorldPoint(mousePoint);
    }

    void OnTriggerEnter(Collider other)
    {
        Destroy(GetComponent<Rigidbody>());
    }

}
