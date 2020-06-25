using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIInfo : MonoBehaviour
{
    public static UIInfo instance;

    public GameObject textObject;
    public TMP_Text UIText;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    public void SetText(string text)
    {
        StartCoroutine(ShowText(text));
    }

    private IEnumerator ShowText(string text)
    {
        textObject.gameObject.SetActive(true);
        UIText.text = text;
        yield return new WaitForSeconds(2.5f);
        UIText.text = "";
        textObject.gameObject.SetActive(false);
    }
}
