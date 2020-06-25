using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TownCenter : MonoBehaviour
{
    List<string> firstNames;
    List<string> surNames;
    public string[] lines;

    void Start()
    {
        TextAsset nameText = Resources.Load<TextAsset>("VillagerNames");

        lines = nameText.text.Split("\n"[0]);


    }


}
