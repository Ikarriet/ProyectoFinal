using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LanzarCana : MonoBehaviour
{
    public float angle = 45;
    public float force = 5;
    Rigidbody rb;
    public GameObject posInicio;
    public GameObject posPesca;
    public GameObject posAnzueloVuelta;
    public bool pescando = false;
    Transform posInicial;

    PuntoPesca punPesca;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();

        punPesca=posPesca.GetComponent<PuntoPesca>();

        pescando = false;

        
        
        rb.isKinematic = true;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            //print(posInicio.transform.forward);
            if (punPesca.posiblePesca == true && pescando == false)
            {
                rb.isKinematic = false;
                AddForceAtAngle();
                pescando = true;
            }
            else
            {
                
                RecogerCana();
                rb.isKinematic = false;
                pescando = false;
            }
        }
    }
    public void AddForceAtAngle()
    {
        
        rb.AddForce(posInicio.transform.forward * force);
    }    

    public void RecogerCana()
    {
        
        rb.AddForce(posAnzueloVuelta.transform.forward * (force*1.2f));
        Debug.Log("Puto");
        //Vector3 direccion = Direccion(-force, angle);
        //rb.AddForce(direccion, ForceMode.Acceleration);


        //Vector3 directionToMove = posLanzamiento - transform.position;
        //directionToMove = directionToMove.normalized * Time.deltaTime * force;
        //float maxDistance = Vector3.Distance(transform.position, posLanzamiento);
        //transform.position = transform.position + Vector3.ClampMagnitude(directionToMove, maxDistance);
    }

    /*private Vector3 Direccion(float force, float angle)
    {
        angle *= Mathf.Deg2Rad;
        float zComponent = Mathf.Cos(angle) * force;
        float xComponent = Mathf.Cos(angle) * force;
        float yComponent = Mathf.Sin(angle) * force;
        Vector3 forceApplied = new Vector3(xComponent, yComponent, zComponent);
        Vector3 cosa = Vector3.Scale(forceApplied, gameObject.transform.forward);

        return cosa;
    }

    /*private Vector3 FishingDirection()
    {
        
        Vector3 direction =posPesca - posInicio.position;
        return direction + new Vector3(0, 0, 0);
    }*/
    
}
