using UnityEngine;
using System;
using UnityEngine.SceneManagement;

[Serializable]
public enum DriveType
{
    RearWheelDrive,
    FrontWheelDrive,
    AllWheelDrive
}

public class WheelDrive : MonoBehaviour
{
    [Tooltip("Maximum steering angle of the wheels")]
    public float maxAngle = 30f;
    [Tooltip("Maximum torque applied to the driving wheels")]
    public float maxTorque = 300f;
    [Tooltip("Maximum brake torque applied to the driving wheels")]
    public float brakeTorque = 30000f;
    [Tooltip("If you need the visual wheels to be attached automatically, drag the wheel shape here.")]
    public GameObject wheelShape;

    [Tooltip("The vehicle's speed when the physics engine can use different amount of sub-steps (in m/s).")]
    public float criticalSpeed = 5f;
    [Tooltip("Simulation sub-steps when the speed is above critical.")]
    public int stepsBelow = 5;
    [Tooltip("Simulation sub-steps when the speed is below critical.")]
    public int stepsAbove = 1;

    [Tooltip("The vehicle's drive type: rear-wheels drive, front-wheels drive or all-wheels drive.")]
    public DriveType driveType;

    private WheelCollider[] m_Wheels;
    public Transform leftController;
    public Transform rightController;
    public Rigidbody bike;
    private int rpm;



    public AudioSource audioData;

    OVRHapticsClip buzz;
    public AudioClip audioFile;

    public AudioSource audioData1;

    OVRHapticsClip buzz1;
    public AudioClip audioFile1;

    public AudioSource audioData2;
    public AudioSource audioData3;
    public AudioClip audioFile2;
    float elapsedTime = 0;
    private Vector3 mLastPosition;

    public AudioClip stall;
    public AudioClip pedal;


   




    // Find all the WheelColliders down in the hierarchy.
    void Start()
    {

        m_Wheels = GetComponentsInChildren<WheelCollider>();
        buzz = new OVRHapticsClip(audioFile);
        buzz1 = new OVRHapticsClip(audioFile1);

        for (int i = 0; i < m_Wheels.Length; ++i)
        {
            var wheel = m_Wheels[i];

            // Create wheel shapes only when needed.
            if (wheelShape != null)
            {
                var ws = Instantiate(wheelShape);
                ws.transform.parent = wheel.transform;
            }
        }
    }

    // This is a really simple approach to updating wheels.
    // We simulate a rear wheel drive car and assume that the car is perfectly symmetric at local zero.
    // This helps us to figure our which wheels are front ones and which are rear.
    void Update()
    {
        if (OVRInput.Get(OVRInput.Button.One))
        {
            SceneManager.LoadScene(sceneName: "City");
        }
        if (OVRInput.Get(OVRInput.Button.Three))
        {
            SceneManager.LoadScene(sceneName: "Track");
        }
        

        var vel = bike.velocity;      //to get a Vector3 representation of the velocity
        var speed = vel.magnitude;

        if (speed >1 )
        {
           

            if (rpm > 0 || maxTorque * Input.GetAxis("Vertical") > 0)
            {
                Debug.Log("pedaling");
                audioData3.Stop();   
                audioData2.clip =pedal;
                if (!audioData2.isPlaying)
                {
                    audioData2.Play();
                }             
                
            }
            else
            {
                audioData2.Stop();
                //audioData2.Stop();
                audioData3.clip = stall;
                if (!audioData3.isPlaying)
                {
                    audioData3.Play();
                }
                Debug.Log("stall not pedaling");
            }
        }
        else
        {
            audioData2.Stop();
            audioData3.Stop();
            Debug.Log("not moving");
        }
        m_Wheels[0].ConfigureVehicleSubsteps(criticalSpeed, stepsBelow, stepsAbove);

        if (OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger) > 0.8f)
        {
            audioData.Play();
            OVRHaptics.LeftChannel.Mix(buzz);
            OVRHaptics.RightChannel.Mix(buzz);
        }



        float angle = maxAngle * Input.GetAxis("Horizontal");
        float torque = maxTorque * Input.GetAxis("Vertical");
        OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick);
        float change = leftController.position.y - rightController.position.y;
        // float angle = change * 50;
        // float torque = rpm  ; //OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger);

        float handBrake = (OVRInput.Get(OVRInput.Axis1D.SecondaryIndexTrigger)) * brakeTorque;
        if ((OVRInput.Get(OVRInput.Axis1D.SecondaryIndexTrigger)) > 0.1f)
        {
            audioData1.Play();
            OVRHaptics.LeftChannel.Mix(buzz1);
            OVRHaptics.RightChannel.Mix(buzz1);
        }

        //float handBrake = (OVRInput.Get(OVRInput.Axis1D.SecondaryIndexTrigger) > 0) ? brakeTorque : 0;

        foreach (WheelCollider wheel in m_Wheels)
        {
            // A simple car where front wheels steer while rear ones drive.
            if (wheel.transform.localPosition.z > 0)
                wheel.steerAngle = angle;

            if (wheel.transform.localPosition.z < 0)
            {
                wheel.brakeTorque = handBrake;

            }

            if (wheel.transform.localPosition.z < 0 && driveType != DriveType.FrontWheelDrive)
            {
                wheel.motorTorque = torque;
               
            }

            if (wheel.transform.localPosition.z >= 0 && driveType != DriveType.RearWheelDrive)
            {
                wheel.motorTorque = torque;
            }

            // Update visual wheels if any.
            if (wheelShape)
            {
                Quaternion q;
                Vector3 p;
                wheel.GetWorldPose(out p, out q);

                // Assume that the only child of the wheelcollider is the wheel shape.
                Transform shapeTransform = wheel.transform.GetChild(0);
                shapeTransform.position = p;
                shapeTransform.rotation = q;
            }
        }
    }

    // Invoked when a line of data is received from the serial device.
    void OnMessageArrived(string msg)
    {
        rpm = Int32.Parse(msg);


    }

    // Invoked when a connect/disconnect event occurs. The parameter 'success'
    // will be 'true' upon connection, and 'false' upon disconnection or
    // failure to connect.
    void OnConnectionEvent(bool success)
    {

    }

}
