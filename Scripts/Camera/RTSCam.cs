using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RTSCam : MonoBehaviour {

    public Vector3 maxLimit, minLimit;
    public float borderDetect;
    public float panSpeed;

    public Transform player;
    public Transform cam;

    public CanvasGroup UIHint;
    bool hasMoved;

    //--CAM CONTROL BUTTONS
    bool movingUp;
    bool movingDown;
    bool movingLeft;
    bool movingRight;

    //--Pan Controls
    bool isPanning;
    public float sensitivity;

    //--Camera freelook vars
    float h;
    float v;

    //--Mobile switch
    public bool isMobile;
    public Joystick moveJoystick;
    public Joystick lookJoystick;
    public Vector3 lookDir, moveDir;

    private void Start()
    {
        hasMoved = false;
        UIHint.alpha = 0f;

        isPanning = false;

        //--CAM CONTROL BUTTONS
        movingUp = false;
        movingDown = false;
        movingLeft = false;
        movingRight = false;
    }

    void Update()
    {
        //UI hint
        if (!hasMoved)
        {
            UIHint.alpha += Time.deltaTime * 0.2f;
        }
        if (hasMoved && UIHint.alpha > 0f)
        {
            UIHint.alpha -= Time.deltaTime * 0.5f;
        }

        //--DESKTOP
        if (!isMobile)
        {
            //Pan Controls linked to right mouse button
            if (Input.GetKeyDown(KeyCode.Mouse1) || Input.GetKeyDown(KeyCode.Space))
            {
                isPanning = true;
                Cursor.lockState = CursorLockMode.Locked;
            }
            if (Input.GetKeyUp(KeyCode.Mouse1) || Input.GetKeyUp(KeyCode.Space))
            {
                isPanning = false;
                Cursor.lockState = CursorLockMode.None;
            }

            //Nav controls
            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow) || Input.mousePosition.y >= Screen.height - borderDetect)
            {
                player.transform.Translate(Vector3.forward * Time.deltaTime * panSpeed, Space.Self);
                if (!hasMoved)
                {
                    hasMoved = true;
                }
            }

            if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow) || Input.mousePosition.y <= borderDetect || movingDown)
            {
                player.transform.Translate(Vector3.back * Time.deltaTime * panSpeed, Space.Self);
                if (!hasMoved)
                {
                    hasMoved = true;
                }
            }

            if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow) || Input.mousePosition.x >= Screen.width - borderDetect || movingRight)
            {
                player.transform.Translate(Vector3.right * Time.deltaTime * panSpeed);
                if (!hasMoved)
                {
                    hasMoved = true;
                }
            }

            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow) || Input.mousePosition.x <= borderDetect || movingLeft)
            {
                player.transform.Translate(Vector3.left * Time.deltaTime * panSpeed);
                if (!hasMoved)
                {
                    hasMoved = true;
                }
            }

            //--Camera zoom
            float zoom = Input.GetAxis("Mouse ScrollWheel");
            if (player.transform.position.y <= 50f)
            {
                player.transform.Translate(Vector3.forward * panSpeed * zoom, Camera.main.transform);
            }
            if (player.transform.position.y > 50f && player.transform.position.y <= 100f)
            {
                player.transform.Translate(Vector3.forward * (panSpeed * 2f) * zoom, Camera.main.transform);
            }
            if (player.transform.position.y > 100f)
            {
                player.transform.Translate(Vector3.forward * (panSpeed * 4f) * zoom, Camera.main.transform);
            }
        }

        //--MOBILE
        if (isMobile)
        {
            //--Moving control
            //h = player.transform.eulerAngles.x;

            lookDir = (Vector3.right * lookJoystick.Horizontal + Vector3.back * lookJoystick.Vertical);
            moveDir = (Vector3.right * moveJoystick.Horizontal + Vector3.forward * moveJoystick.Vertical);
            player.transform.Translate(moveDir * panSpeed * Time.deltaTime, Space.Self);

            h = lookDir.x * sensitivity * 20f;
            v = lookDir.z * sensitivity * 20f;

            player.transform.Rotate(Vector3.up * Time.deltaTime * h);
            cam.transform.Rotate(Vector3.right * Time.deltaTime * v);
        }

        //--Freelook controls
        if (isPanning)
        {
            if (!isMobile)
            {
                h = player.transform.eulerAngles.y;
                v = cam.transform.eulerAngles.x;

                h += Input.GetAxis("Mouse X") * sensitivity;
                v -= Input.GetAxis("Mouse Y") * sensitivity;

                cam.transform.localEulerAngles = new Vector3(v, 0f, 0f);
                player.transform.eulerAngles = new Vector3(0f, h, 0f);
            }
        }
    }

    void LateUpdate ()
    {
        

        //--Clamp player to play area
        Vector3 playZone = player.position;
        playZone.x = Mathf.Clamp(player.position.x, minLimit.x, maxLimit.x);
        playZone.y = Mathf.Clamp(player.position.y, minLimit.y, maxLimit.y);
        playZone.z = Mathf.Clamp(player.position.z, minLimit.z, maxLimit.z);

        player.position = playZone;
	}

    //--CAM CONTROL BUTTONS
    public void MoveUp_down()
    {
        movingUp = true;
    }
    public void MoveUp_up()
    {
        movingUp = false;
    }

    public void MoveDown_down()
    {
        movingDown = true;
    }
    public void MoveDown_up()
    {
        movingDown = false;
    }

    public void MoveLeft_down()
    {
        movingLeft = true;
    }
    public void MoveLeft_up()
    {
        movingLeft = false;
    }

    public void MoveRight_down()
    {
        movingRight = true;
    }
    public void MoveRight_up()
    {
        movingRight = false;
    }

    //--ZOOM CONTROL
    public void ZoomIn()
    {

    }
    public void ZoomOut()
    {

    }
}
