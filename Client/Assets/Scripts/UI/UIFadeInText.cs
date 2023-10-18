using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIFadeInText : MonoBehaviour
{
    float time = 0;

    private void Update()
    {
        if(time < 2f)
        {
            GetComponent<TextMeshProUGUI>().alpha = time / 2;
        }
        else
        {
            time = 0;
        }
        time += Time.deltaTime;
    }
}
