using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityBody : MonoBehaviour
{

    public GameObject planet;
    public GameObject planetParent;

    public float maxGravity;
    public float maxGravityDist;

    float lookAngle;
    Vector3 lookDir;

    public Rigidbody2D rb;

    private Vector3 mouseOffset;
    private float mouseZCoord = -1;

    private bool dragged = false;
    private bool pause = false;

    public float dragSpeed = -10f;

    private float dragV;
    public float dragAcceleration = 0.25f;

    public GameObject pauseText;
    private float pauseDelay = 0f;

    // Update is called once per frame
    void Update()
    {
        planet = getClosestPlanet();

        float dist = Vector2.Distance(planet.transform.position, transform.position);

        // Turns pause on and off depending on a delay so it cant be spammed 
        if (Input.GetKey(KeyCode.Space) && pauseDelay <= 0f) {
            pause = !pause;
            pauseText.active = pause;
            pauseDelay = 0.5f;
            freezeRB (pause);
        }
        if (pauseDelay >= 0)
            pauseDelay -= Time.deltaTime;

        dragV -= dragAcceleration * 0.5f;
        dragV = Mathf.Clamp(dragV, 0f, 5f);

        Vector2 vForce = new Vector2(dragV * -1, dragV * -1) * dist;

        rb.AddForce(vForce);

        if (isFrozen() || pause)
            return;

        Vector3 v = planet.transform.position - transform.position;
        rb.AddForce(v.normalized * (1.0f - dist / maxGravityDist) * maxGravity);

        lookDir = planet.transform.position - transform.position;
        lookAngle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(0f, 0f, lookAngle);
    }

    void OnMouseDown() {
        Debug.Log ("MEWOOWOWOW");
        if (Input.GetKey(KeyCode.LeftShift))
            return;
        // dragged = true;
        freezeRB(true);
        mouseOffset = gameObject.transform.position - GetMouseWorldPos();
        dragV = 0f;
    }

    void OnMouseUp() {
        if (Input.GetKey(KeyCode.LeftShift))
            return;

        // Turn off Pause when letting go of drag
        pause = false;
        pauseText.active = pause;
        pauseDelay = 0.5f;
        freezeRB (pause);
    }

    Vector3 GetMouseWorldPos() {
        Vector3 mousePoint = Input.mousePosition;

        mousePoint.z = mouseZCoord;

        return Camera.main.ScreenToWorldPoint(mousePoint) * dragSpeed;
    }

    void OnMouseDrag() {
        if (Input.GetKey(KeyCode.LeftShift))
            return;

        freezeRB(true);

        float dist = Vector2.Distance(planet.transform.position, transform.position);
        if (dist > 9.5f)
            return;
        if (pause && dist > 8.25f)
            return;

        Vector3 mousePoint = GetMouseWorldPos() + mouseOffset;

        transform.position = mousePoint;
        dragV += dragAcceleration;
    }

    // Gets the GO for the closest planet to the player
    GameObject getClosestPlanet () {
        float min_dist = 10000f;
        Transform min_dist_planet = planet.transform;
        foreach (Transform child in planetParent.transform ) {
            float dist = Vector2.Distance(child.transform.position, transform.position);
            if (dist < min_dist) {
                min_dist = dist;
                min_dist_planet = child;
            }
        }
        return min_dist_planet.gameObject;
    }

    void freezeRB(bool p){
        if (p)
            rb.bodyType = RigidbodyType2D.Static;
        else
            rb.bodyType = RigidbodyType2D.Dynamic;
    }
    bool isFrozen (){
        return rb.bodyType == RigidbodyType2D.Static;
    }

}
