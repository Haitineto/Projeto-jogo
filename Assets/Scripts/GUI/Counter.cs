using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Counter : MonoBehaviour
{
    public void StartCount(int number, int jumpFactor)
    {
        StartCoroutine(Count(number, jumpFactor));
    }

    IEnumerator Count(int number, int jumpFactor)
    {
        yield return new WaitForSeconds(0.5f);

        int initialNumber = 0;

        Text text = GetComponent<Text>();

        while (initialNumber<number)
        {
            initialNumber += jumpFactor;
            text.text = initialNumber.ToString();
            yield return null;
        }

        text.text = number.ToString();
    }
}
