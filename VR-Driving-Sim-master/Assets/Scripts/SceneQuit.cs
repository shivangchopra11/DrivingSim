using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneQuit : MonoBehaviour
{
    public KeyCode m_hotKey = KeyCode.Q;

    void Start()
    {
    }

    void Update()
    {
        if (Input.GetKeyDown(m_hotKey)) Application.Quit();
    }
}
