using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class Score : MonoBehaviour
{
    Text uiText;

    public int Points { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        uiText = GetComponent<Text>();
    }

    // Update is called once per frame
    public void AddScore(int addPoint)
    {
        Points += addPoint;

        uiText.text = string.Format("현재 점수 : {0 :D3} 점", Points);
    }
}
