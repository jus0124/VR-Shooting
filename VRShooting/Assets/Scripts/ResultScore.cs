using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class ResultScore : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        var gameObj = GameObject.FindWithTag("Score");

        var score = gameObj.GetComponent<Score>();

        var uiText = GetComponent<Text>();

        uiText.text = string.Format("Á¡¼ö : {0:D3}Á¡", score.Points);
        
    }
}
