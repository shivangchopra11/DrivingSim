using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class VignetteController : MonoBehaviour
{
    public float m_scale;
    public float m_smoothTime;
    public float m_maxIntensity;

    private PostProcessVolume   m_volume;
    private Vignette            m_vignetteLayer;
    private Quaternion          m_prevRotation;
    private float               m_currIntensityRate = 0.0F;

    void Start()
    {
        PostProcessVolume volume = gameObject.GetComponent<PostProcessVolume>();
        volume.profile.TryGetSettings(out m_vignetteLayer);
        m_prevRotation = Camera.main.transform.rotation;
    }

    void Update()
    {
        /*
        // Logs joystick button presses for input mapping
        for (int joystick = 1; joystick < 5; joystick++)
        {
            for (int button = 0; button < 20; button++)
            {
                if (Input.GetKey("joystick " + joystick + " button " + button))
                {

                    Debug.Log("joystick = " + joystick + "  button = " + button);
                }
            }
        }
        */
    }

    void FixedUpdate()
    {
        // Calculate angular velocity from difference in quatenion rotations
        Quaternion currRotation = Camera.main.transform.rotation;
        Quaternion angularDelta = m_prevRotation * Quaternion.Inverse(currRotation);
        float   angle;
        Vector3 axis;
        angularDelta.ToAngleAxis(out angle, out axis);
        Vector3 angularVelocity = axis * angle / Time.deltaTime;
        m_prevRotation = currRotation;

        // Caclulate intensity as a linear functon of pitch-yaw angular velocity
        Vector2 pitchYawAngularVelocity = angularVelocity;
        float intensity = Mathf.Clamp(Mathf.Abs((m_scale * pitchYawAngularVelocity).magnitude), 0, m_maxIntensity);

        // Smoothen intensity updates
        float currIntensity = m_vignetteLayer.intensity.value;
        float smoothIntensity = Mathf.SmoothDamp(currIntensity, intensity, ref m_currIntensityRate, m_smoothTime);
        m_vignetteLayer.intensity.value = smoothIntensity;
    }
}
