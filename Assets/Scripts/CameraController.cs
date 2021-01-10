using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    public float scroll_speed = 0.5f;
    private Vector3 move;

    public float dragSpeed;

    private Vector3 mouseOffset;
    private Vector3 dragOrigin;

    public GameObject player;

    private bool follow;
    private float followDelay;

    Vector3 GetMouseWorldPos() {
        Vector3 mousePoint = Input.mousePosition;

        return Camera.main.ScreenToWorldPoint(mousePoint) * dragSpeed;
    }

    // Update is called once per frame
    void Update()
    { 
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        // Debug.Log (scroll);
        // Debug.Log (Input.GetAxis("Mouse ScrollWheel"));
        if (scroll > 0f) // forward
        {
            if (transform.position.z < -2) {
                transform.position += new Vector3 (0f, 0f, scroll_speed);
            }
        }
        else if (scroll < 0f) {
            if (transform.position.z > -35) {
                transform.position += new Vector3 (0f, 0f, scroll_speed * -1);
            }
        }
        // if (scroll!= 0f) {
        //     Debug.Log (transform.position.z);
        //     if (transform.position.z < -2 && transform.position.z > -35){
        //         transform.position += move;
        //     }
        // }

        if (Input.GetKey(KeyCode.Backspace))
        {
            Debug.Log ("RECENTERING");
            transform.position = new Vector3 (player.transform.position.x, player.transform.position.y, transform.position.z);
        }

        if (Input.GetKey(KeyCode.Backspace) && followDelay <= 0){
            if (Input.GetKey(KeyCode.LeftShift)) {
                follow = !follow;
                followDelay = 0.5f;
            }
        }
        if (followDelay >= 0f) {
            followDelay -= Time.deltaTime;
        }

        if (follow) {
            Vector3 goal = new Vector3 (player.transform.position.x, player.transform.position.y, transform.position.z);
            transform.position = goal;
            return;
        }

        if (Input.GetMouseButtonDown(0))
        {
            if (Input.GetKey(KeyCode.LeftShift)) {
                dragOrigin = Input.mousePosition;
                return;
            }
        }

        if (!Input.GetMouseButton(0)) return;

        if (Input.GetKey(KeyCode.LeftShift)) {
            Vector3 pos = Camera.main.ScreenToViewportPoint(Input.mousePosition - dragOrigin);
            Vector3 cam_move = new Vector3(pos.x * transform.position.z * dragSpeed, pos.y * transform.position.z * dragSpeed, 0f);

            transform.Translate(cam_move, Space.World);  
        }

    }

}
