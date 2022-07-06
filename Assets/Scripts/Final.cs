using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Final : MonoBehaviour
{
    public List<TextMeshPro> texts;

    private void Awake()
    {
        TextX();
    }

    void TextX()
    {
        for (int i = 0; i < texts.Count; i++)
        {
            int x = i + 1;
            texts[i].text = x.ToString() + "X";
        }
    }
}
