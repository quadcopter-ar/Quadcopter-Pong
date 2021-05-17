using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[RequireComponent(typeof(Text))]
public class Score : MonoBehaviour
{
    public CapsuleRight paddle;
    Text text;
    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<Text>();
        text.text = 0.ToString() ;
    }

    // Update is called once per frame
    void Update()
    {
        text.text = paddle.score.ToString();
    }
}
