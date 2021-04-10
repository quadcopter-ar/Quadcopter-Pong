using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{
    public int score;
    public float speed;
    public string left, right, up, down;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(left))
        {
            transform.Translate(new Vector3(-speed * Time.deltaTime,0,0));
        }
        else if (Input.GetKey(right))
        {
            transform.Translate(new Vector3(speed * Time.deltaTime,0,0));
        }
        else if (Input.GetKey(up))
        {
            transform.Translate(new Vector3(0,speed * Time.deltaTime,0));
        }
        else if (Input.GetKey(down))
        {
            transform.Translate(new Vector3(0,-speed * Time.deltaTime,0));
        }

    }
}
