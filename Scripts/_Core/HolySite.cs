using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HolySite : MonoBehaviour
{
    public Transform cam;
    public int beliefAmount = 0;
    Text beliefText;

    // private void Awake()
    // {
    //     beliefText = GetComponentInChildren<Text>();
    //     beliefText.text = beliefAmount.ToString();

    //     beliefText.enabled = false;
    // }

    public void ModifyBelief(int amount)
    {
        beliefAmount += amount;
        //beliefText.text = beliefAmount.ToString();
    }

    // private void OnMouseOver()
    // {
    //     beliefText.enabled = true;
    //     beliefText.transform.rotation = Quaternion.LookRotation(transform.position - cam.transform.position);

    // }

    // private void OnMouseExit()
    // {
    //     beliefText.enabled = false;
    // }

}
