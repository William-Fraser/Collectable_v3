using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    //variables
    public GameObject pickUp;
    public ParticleSystem emitter;
    private Rigidbody p_Rigidbody;

    Vector3 posOffset = new Vector3();
    Vector3 tempPos = new Vector3();

    // Start is called before the first frame update
    void Start()
    {
        p_Rigidbody = pickUp.GetComponent<Rigidbody>();
        posOffset = transform.position;
    }
    private void Update()
    {
        Hover();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        p_Rigidbody.transform.Rotate(new Vector3(0f, Time.deltaTime * 10, 0f), Space.World); // rotate around in a circle

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            StartCoroutine("Absorb");
        }
    }

    private void Hover()
    {
        tempPos = posOffset;
        tempPos.y += Mathf.Sin(Time.fixedTime * Mathf.PI * .3f) * 0.5f;

        transform.position = tempPos;
    }

    IEnumerator Absorb() 
    {
        emitter.Emit(20);
        new WaitForSeconds(3);
        pickUp.GetComponent<MeshRenderer>().enabled = false;
        pickUp.GetComponent<MeshCollider>().enabled = false;
        yield return StartCoroutine("Respawn");
    }
    IEnumerator Respawn()
    {
        Debug.Log("startin coroutine");
        yield return new WaitForSeconds(3);
        pickUp.GetComponent<MeshRenderer>().enabled = true;
        pickUp.GetComponent<MeshCollider>().enabled = true;
        Debug.Log("Respawned");
    }
}
