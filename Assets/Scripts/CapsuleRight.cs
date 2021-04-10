using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.XR;
using Photon.Pun;

public class CapsuleRight : MonoBehaviour
{
    private PhotonView myPV; 

    public int score = 0;
    public float speed;
    public string left, right, up, down;
    [SerializeField]
    private XRNode controllerNode = XRNode.RightHand;
    private List<InputDevice> devices = new List<InputDevice>();
    private InputDevice controller;
    
    public bool keyboardDebug;
    Vector2 primary2dValueOld = new Vector2(0.0f, 1.0f);
    Vector2 primary2dValueNew;
    private float deadZoneAmt = 0.5f;
    // Start is called before the first frame update
    void Start()
    {
        myPV = GetComponent<PhotonView>();
        GetDevice();
    }
    private void GetDevice()
    {
        InputDevices.GetDevicesAtXRNode(controllerNode, devices);
        controller = devices.FirstOrDefault();
    }
    void Update()
    {
        if(myPV.IsMine)
        {
           // if (controller == null)
           // {
            //    GetDevice();
           // }

            UpdateMovement();
        }

    }

    // Update is called once per frame
    void UpdateMovement()
    {
        if (!keyboardDebug)
        {
            //Vector2 touchCoords = OVRInput.Get(OVRInput.Axis2D.PrimaryTouchpad);
            Debug.Log("Oculus mode");
            Vector2 touchCoords;

            InputFeatureUsage<Vector2> primary2DVector = CommonUsages.primary2DAxis;

            if (controller.TryGetFeatureValue(primary2DVector, out touchCoords) && touchCoords != Vector2.zero)
            {
                
                Debug.Log("primary2DAxisClick is pressed " + touchCoords);
 
                if (touchCoords.x < -deadZoneAmt) {
                    // touching left side, strafe left
                    transform.Translate(Vector3.down * touchCoords.x * Time.deltaTime * -speed);
                } else if (touchCoords.x > deadZoneAmt) {
                    // touching right side, strafe right
                    transform.Translate(Vector3.up * touchCoords.x * Time.deltaTime * speed);
                }
            
                if (touchCoords.y < -deadZoneAmt) {
                    // touching bottom side, move backwards
                    transform.Translate(Vector3.right * touchCoords.y * Time.deltaTime * speed);
                } else if (touchCoords.y > deadZoneAmt) {
                    // touching top side, move forward
                    transform.Translate(Vector3.left * touchCoords.y * Time.deltaTime * -speed);
                }
            }
            /*
            Vector2 primary2dValue;

            InputFeatureUsage<Vector2> primary2DVector = CommonUsages.primary2DAxis;

            if (controller.TryGetFeatureValue(primary2DVector, out primary2dValue) && primary2dValue != Vector2.zero)
            {
                
                Debug.Log("primary2DAxisClick is pressed " + primary2dValue);

                var xAxisRight = primary2dValue.x *-speed * Time.deltaTime;
                var xAxisLeft = primary2dValue.x * speed * Time.deltaTime;
                var yAxisUp = primary2dValue.y * speed * Time.deltaTime;
                var yAxisDown = primary2dValue.y * -speed * Time.deltaTime;

                Vector3 right = transform.TransformDirection(Vector3.right);
                Vector3 left = transform.TransformDirection(Vector3.left);
                Vector3 up = transform.TransformDirection(Vector3.up);
                Vector3 down = transform.TransformDirection(Vector3.down);


                transform.position += right * xAxisRight;
                transform.position += left * xAxisLeft;
                transform.position += up * yAxisUp;
                transform.position += down * yAxisDown;
            }
                
            */
        
            /*
            InputFeatureUsage<Vector2> primary2DVector = CommonUsages.primary2DAxis;
            if (controller.TryGetFeatureValue(primary2DVector, out primary2dValueNew) && primary2dValueNew != Vector2.zero)
            {

                Debug.Log("primary2DAxisClick is pressed " + primary2dValueNew);

                if (primary2dValueNew.y > primary2dValueOld.y )
                {
                    //Vector3 up = transform.TransformDirection(Vector3.up);
                    transform.Translate(new Vector3(speed * Time.deltaTime * primary2dValueNew.y ,0,0));
                }

                if (primary2dValueNew.y < primary2dValueOld.y)
                {
                    //Vector3 down = transform.TransformDirection(Vector3.down);
                    transform.Translate(new Vector3(-speed * Time.deltaTime * primary2dValueNew.y ,0,0));

                }
                
                if (primary2dValueNew.x > primary2dValueOld.x)
                {
                    //Vector3 left = transform.TransformDirection(Vector3.left);
                    transform.Translate(new Vector3(0, speed * Time.deltaTime * primary2dValueNew.x ,0));
                }

                if (primary2dValueNew.x < primary2dValueOld.x)
                {
                    //Vector3 right = transform.TransformDirection(Vector3.right);
                    transform.Translate(new Vector3(0, -speed * Time.deltaTime * primary2dValueNew.x ,0));
                }

                primary2dValueOld = primary2dValueNew;
                */

                
        }
        

        if (keyboardDebug)
        {
            Debug.Log("Keyboard mode");
            Debug.Log(down);
            if (Input.GetKey(left))
            {
                transform.Translate(new Vector3(0,speed * Time.deltaTime,0), Space.Self);
            }
            else if (Input.GetKey(right))
            {
                transform.Translate(new Vector3(0,-speed * Time.deltaTime,0), Space.Self);
            }
            else if (Input.GetKey(up))
            {
                transform.Translate(new Vector3(speed * Time.deltaTime,0,0),Space.Self);
            }
            else if (Input.GetKey(down))
            {
                transform.Translate(new Vector3(-speed * Time.deltaTime,0,0),Space.Self);
            }
        }

    }
}
