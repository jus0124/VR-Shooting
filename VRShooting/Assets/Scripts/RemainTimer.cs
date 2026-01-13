using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[RequireComponent(typeof(Text))]
public class RemainTimer : MonoBehaviour
{
    [SerializeField] float gameTime = 134.0f;
    Text uiText;
    float currentTime;

    // Start is called before the first frame update
    void Start()
    {
        uiText = GetComponent<Text>();
        currentTime = gameTime;
        
    }

    // Update is called once per frame
    void Update()
    {
        currentTime -= Time.deltaTime;
        if (currentTime < 0.0f) { 
            currentTime = 0.0f;
        }

        uiText.text = string.Format("남은 시간 : {0:F} 초", currentTime);

        
    }

    public bool IsCountingDown() 
    { 
        return currentTime > 0.0f;
    
    }
}
