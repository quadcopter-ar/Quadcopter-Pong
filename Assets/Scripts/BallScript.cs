using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.XR;
using UnityEngine.UI;
using Photon.Pun;

[RequireComponent(typeof(Rigidbody))]
public class BallScript : MonoBehaviour
{
    Rigidbody rb;
    public float speed;
    [SerializeField]
    private XRNode controllerNode = XRNode.LeftHand;
    private List<InputDevice> devices = new List<InputDevice>();
    private InputDevice controller;
    public bool keyboardDebug = false;
    private Vector3 startPos;
    public string startBall;
    //public Text scoreText;
    //private int Score;
    //public CapsuleRight paddle1, paddle2;
    //public Text winText;

    private void GetDevice()
    {
        InputDevices.GetDevicesAtXRNode(controllerNode, devices);
        controller = devices.FirstOrDefault();
    }

    // Start is called before the first frame update
    void Start()
    {
        rb =  GetComponent<Rigidbody>();
        startPos = rb.position;
        //Score = 0;
        //SetScoreText();

        if(!keyboardDebug)
            GetDevice();
        
        if(keyboardDebug)
        {
            if (Input.GetKey(startBall))
            {
                rb.AddForce(new Vector3(8f, 0.1f, 8f), ForceMode.Impulse);
            }
        }
       
    }

    /*
    void OnTriggerEnter(Collider collider)
    {
        if(collider.gameObject.CompareTag("Reset"))
        {
            //increase the score here.
            //Score = Score + 1;
            //SetScoreText();
            //set player position and velocity to start

            if(paddle1.score >= 10)
            {
                winText.enabled = true;
                winText.text = "Player 1 wins!";
                Time.timeScale = 0;
            }
            else if (paddle2.score >= 10)
            {
                winText.enabled = true;
                winText.text = "Player 2 wins!";
                Time.timeScale = 0;
            }

            

            rb.position = startPos;
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }
    }
    */
    

    // Update is called once per frame
    void Update()
    {
            if(!PhotonNetwork.IsMasterClient)
                return;
            if(!keyboardDebug)
            {
                if (controller == null)
                {
                    GetDevice();
                }
                
                UpdateMovementofBall();
            }

            if(keyboardDebug)
            {
                Debug.Log("Keyboard button is pressed.");
                if (Input.GetKey(startBall))
                {
                    int velX = Random.Range(1,3) == 1 ? Random.Range(-4,-7) : Random.Range(4, 7);
                    int velZ = Random.Range(1,3) == 1 ? Random.Range(-4,-7) : Random.Range(4, 7);
                    int velY = Random.Range(1,3) == 1 ? Random.Range(-4,-7) : Random.Range(4, 7);

                    rb.velocity = new Vector3(velX, velY, velZ);
                    transform.position = new Vector3(0,0,0);

                    //rb.AddForce(new Vector3(0.5f, 0.1f, 0.5f), ForceMode.Impulse);
                    //rb.AddForce(new Vector3(1f, 1f, 1f) * speed * Time.deltaTime);
                }
            }


    }

    void UpdateMovementofBall()
    {
        bool primaryValue;
        Debug.Log("Trigger button is pressed.");
        if (controller.TryGetFeatureValue(UnityEngine.XR.CommonUsages.primaryButton, out primaryValue) && primaryValue)
        {
            Debug.Log("Trigger button is pressed.");

            int velX = Random.Range(1,3) == 1 ? Random.Range(-4,-7) : Random.Range(4, 7);
            int velZ = Random.Range(1,3) == 1 ? Random.Range(-4,-7) : Random.Range(4, 7);
            int velY = Random.Range(1,3) == 1 ? Random.Range(-4,-7) : Random.Range(4, 7);

            rb.velocity = new Vector3(velX, velY, velZ);
            transform.position = new Vector3(0,0,0);
            //rb =  GetComponent<Rigidbody>();
            //rb.AddForce(new Vector3(0.8f, 0, 0.8f), ForceMode.Impulse);
        }
    }

    //void SetScoreText()
    //{
    //    scoreText.text="Score: " + Score.ToString ();
   // }
}
