using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score : MonoBehaviour
{

    public LevelInfo[] displayNumbers;

    public void DisplayNumber(int amount)
    {
        char[] chars = amount.ToString().ToCharArray();
        if(chars.Length > displayNumbers.Length)
        {
            throw new Exception("Unsupportet large Number " + amount + ": max " + displayNumbers.Length);
        }

        for(int i = 0; i < displayNumbers.Length; i++)
        {
            if(i < chars.Length)
            {
                displayNumbers[i].Setup(chars[i]);
            }
            else
            {
                displayNumbers[i].Hide();
            }
        }

    }
}
