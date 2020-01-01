using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControll : MonoBehaviour
{
    public Rigidbody rb;
    public bool Left = false;
    public bool Right = false;
    public bool Forward = false;
    public bool Back = false;
    public float x = 0;
    public float z = 0;
    public Vector3 LR;
    public Vector3 FB;
    public Rigidbody trap1;
    public GameObject playerone;
    public bool stun = false;
    public bool TimeShift = false;
    public bool ZaWarudo = false;
    private WorldControl WorldControlScript;
    public float[,] TimeCoordinates = new float[50, 3];
    int i;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Bolt")
        {
            if (WorldControlScript.ZaWarudo == false) { 
            OnHitDestroy bolt = collision.gameObject.GetComponent<OnHitDestroy>();
            stun = true;

            rb.velocity = bolt.rb.velocity;
        }
        }
        if (collision.gameObject.tag == "Wall")
        {
            if (stun == true)
            {
                stun = false;
                rb.velocity = new Vector3(0, 0, 0);
            }
        }
    }
    void TimeCapture()
    {
        for (i = 49; i > 0; i--)
        {
            TimeCoordinates[i, 0] = TimeCoordinates[i - 1, 0];
        }
   
        for (i = 49; i > 0; i--)
        {
            TimeCoordinates[i, 1] = TimeCoordinates[i - 1, 1];
        }
     
        for (i = 49; i > 0; i--)
        {
            TimeCoordinates[i, 2] = TimeCoordinates[i - 1, 2];
        }



    }
    // Start is called before the first frame update
    void Start()
    {
        for (i = 0; i < 50; i++)
        {
            TimeCoordinates[i, 0] = transform.position.x;
        }
        for (i = 0; i < 50; i++)
        {
            TimeCoordinates[i, 1] = transform.position.z;
        }
        for (i = 0; i < 50; i++)
        {
            TimeCoordinates[i, 2] = transform.rotation.y;
        }
        InvokeRepeating("TimeCapture", 0f, 0.1f);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void FixedUpdate()
    {
        WorldControlScript = GameObject.Find("WorldController").GetComponent<WorldControl>();
        TimeCoordinates[0, 0] = transform.position.x;
        TimeCoordinates[0, 1] = transform.position.z;
        TimeCoordinates[0, 2] = transform.rotation.y;
        if (stun == false)
        {
            rb.velocity = new Vector3(Input.GetAxis("Horizontal") * 2, 0, Input.GetAxis("Vertical") * 2);
            if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
            {
                transform.forward = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            }
            if (Input.GetKeyDown(KeyCode.Mouse0) == true)
        {

            Rigidbody clone;
            clone = Instantiate(trap1, transform.position, transform.rotation);


        }
    }


        
        TimeShift = Input.GetKey("e");
        if (TimeShift == true)
        {
            if (stun == true)
            {
                stun = false;
                rb.velocity = new Vector3(0, 0, 0);
            }
            rb.velocity = new Vector3(0, 0, 0);
            transform.position = new Vector3(TimeCoordinates[49, 0], transform.position.y, TimeCoordinates[49, 1]);
            transform.rotation = Quaternion.Euler(0, TimeCoordinates[49, 2], 0);
        }
        ZaWarudo = Input.GetKey("x");
        if (ZaWarudo == true)
        {
            if (stun == true)
            {
                stun = false;
                rb.velocity = new Vector3(0, 0, 0);
            }
        }

    }
}
