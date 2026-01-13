using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Example : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //처음에 한 번 메세지를 표시한다
        Debug.Log("[start]");
    }

    // Update is called once per frame
    void Update()
    {
        // space 키가 눌리고 있는 동안 메시지를 표시한다.
        if (Input.GetKey(KeyCode.Space)) {
            Debug.Log("[Update] Space Key Pressed");
        }
    }
}
