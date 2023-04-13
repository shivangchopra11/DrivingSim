using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class NPCPlayerController : MonoBehaviour
{
    public float movementSpeed = 5.0f;
 
     void Start () {
        // var player = GetComponent<GameObject>();
        // player.RegisterCallback<KeyDownEvent>(OnKeyUDown, TrickleDown.TrickleDown);
     }
     
    void Update () {

        Rigidbody rigidbody = GetComponent<Rigidbody>();
        


        if(Input.GetKey(KeyCode.W)) {
            transform.position += Vector3.forward * Time.deltaTime * movementSpeed;
        }
        else if(Input.GetKey(KeyCode.S)) {
            rigidbody.position += Vector3.back * Time.deltaTime * movementSpeed;
        }
        else if(Input.GetKey(KeyCode.A)) {
            rigidbody.position += Vector3.left * Time.deltaTime * movementSpeed;
            // transform.rotation = Quaternion.Euler(new Vector3(0, 0, rigidbody.rotation.z + angle));
            
        }
        else if(Input.GetKey(KeyCode.D)) {
            rigidbody.position += Vector3.right * Time.deltaTime * movementSpeed;
            // transform.rotation = Quaternion.Euler(new Vector3(0, 0, rigidbody.rotation.z + angle));
            // transform.Rotate(0, 0, 90);
        }

        if(Input.GetKeyDown(KeyCode.W)) {
            // transform.Rotate(0, 0, 0);
            // transform.eulerAngles.y = 0;
            transform.eulerAngles = new Vector3(
                transform.eulerAngles.x,
                0,
                transform.eulerAngles.z
            );

        } 
        else if(Input.GetKeyDown(KeyCode.A)) {
            // transform.Rotate(0, 90, 0);
            // transform.eulerAngles.y = 90;
            transform.eulerAngles = new Vector3(
                transform.eulerAngles.x,
                270,
                transform.eulerAngles.z
            );
        }
        if(Input.GetKeyDown(KeyCode.S)) {
            // transform.Rotate(0, 180, 0);
            // transform.eulerAngles.y = 180;
            transform.eulerAngles = new Vector3(
                transform.eulerAngles.x,
                180,
                transform.eulerAngles.z
            );
        } 
        else if(Input.GetKeyDown(KeyCode.D)) {
            // transform.Rotate(0, 2700, 0);
            // transform.eulerAngles.y = 270;
            transform.eulerAngles = new Vector3(
                transform.eulerAngles.x,
                90,
                transform.eulerAngles.z
            );
        }

        
        
    }

    void OnKeyDown(KeyDownEvent ev)
    {
        Debug.Log("KeyDown:" + ev.keyCode);
        Debug.Log("KeyDown:" + ev.character);
        Debug.Log("KeyDown:" + ev.modifiers);
    }
}
