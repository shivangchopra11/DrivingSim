using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;

public class playerCollision : MonoBehaviour
{
    public GameObject player;
    // public ThirdPersonController controller;
    float smooth = 1.0f;
    float lerpDuration = 0.5f;
    bool rotating;
    void OnTriggerEnter(Collider collision)
    {
        
             Debug.Log("Collision Detected");
            Debug.Log(player.gameObject.transform.rotation);
            // var rotationVector = player.transform.rotation.eulerAngles;
            // Debug.Log(rotationVector);
            // rotationVector.y = rotationVector.y - 180.0f;
            // Debug.Log(rotationVector);
            // Quaternion target = Quaternion.Euler(rotationVector);


            // // Dampen towards the target rotation
            // player.transform.rotation = Quaternion.Slerp(player.transform.rotation, target,  Time.deltaTime * smooth);

            // Quaternion targetRotation = Quaternion.LookRotation(-player.transform.forward, Vector3.up);

            // player.transform.rotation = Quaternion.Slerp(player.transform.rotation, targetRotation, smooth * Time.deltaTime);


            rotating = true;
            // float timeElapsed = 0;
            Quaternion startRotation = player.gameObject.transform.rotation;
            Quaternion targetRotation = player.gameObject.transform.rotation * Quaternion.Euler(0, 180, 0);
            // while (timeElapsed < lerpDuration)
            // {
            //     player.gameObject.transform.rotation = Quaternion.Slerp(startRotation, targetRotation, timeElapsed / lerpDuration);
            //     timeElapsed += Time.deltaTime;
            //     yield return null;
            // }
            Vector3 posVector = player.gameObject.transform.position;
            posVector.x -= 0.2f;
            posVector.z += 0.2f;
            player.gameObject.transform.position = posVector;
            player.gameObject.transform.rotation = targetRotation;
            // rotating = false;

            // GameObject controller = GameObject.Find("ThirdPersonController");

            player.GetComponent<ThirdPersonController>().setVel();
       
    }
}
