using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class Movement : MonoBehaviour
{
    Vector3 Offset;
    public GameObject GameCamera;
    private Rigidbody rb;
 
    // Start is called before the first frame update
    void Start()
    {
        Offset = GameCamera.transform.position - this.transform.position;
        rb = this.GetComponent<Rigidbody>();
    }
 
    // Update is called once per frame
    void Update()
    {
        GameCamera.transform.position = this.transform.position + Offset;
        if(Input.GetKey(KeyCode.W))
        {
            rb.AddForce(0f, 0f, 5f);
        }
        if (Input.GetKey(KeyCode.A))
        {
            rb.AddForce(-5f, 0f, 0f);
        }
        if (Input.GetKey(KeyCode.S))
        {
            rb.AddForce(0f, 0f, -5f);
        }
        if (Input.GetKey(KeyCode.D))
        {
            rb.AddForce(5f, 0f, 0f);
        }
    }
 
    private void OnCollisionStay(Collision collision)
    {
        GameObject Floor = collision.gameObject;
        Color OldColor = Floor.transform.GetComponent<MeshRenderer>().material.color;
 
        Vector3 ColorNormalized = new Vector3(
        OldColor.r + rb.velocity.normalized.x / 20,
        OldColor.g + rb.velocity.normalized.z / 20,
        OldColor.b + rb.velocity.normalized.x / 40 + rb.velocity.normalized.z / 40)
        .normalized;
 
        Color NewColor = new Color(Mathf.Abs(ColorNormalized.x), Mathf.Abs(ColorNormalized.y), Mathf.Abs(ColorNormalized.z));
        Floor.gameObject.transform.GetComponent<MeshRenderer>().material.color = NewColor;
    }
}