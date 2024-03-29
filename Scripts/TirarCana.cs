using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class TirarCana : MonoBehaviour
{
    [SerializeField] float _initialVelocity;
    [SerializeField] float _angle;
    [SerializeField] LineRenderer _lineRenderer;
    [SerializeField] float _step;
    [SerializeField] Transform _throwPoint;
    
    private Camera _cam;

    private void Start()
    {
        _cam=Camera.main;
    }
    private void Update()
    {
        Ray ray=_cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if(Physics.Raycast(ray, out hit)) 
        {
            Vector3 direction = hit.point - _throwPoint.position;
            Vector3 groundDirection = new Vector3(direction.x, 0, direction.z);

            Vector3 targetPos = new Vector3(groundDirection.magnitude, direction.y, 0);
            targetPos.z = 0;
            float height = targetPos.y + targetPos.magnitude / 2f;
            height = Mathf.Max(0.01f, height);
            float angle;
            float v0;
            float time;
            CalculatePathWithHeight(targetPos, height, out v0, out angle, out time);
            DrawPath(groundDirection.normalized,_initialVelocity, time, angle, _step);
            if (Input.GetMouseButtonDown(0))
            {
                StopAllCoroutines();
                StartCoroutine(Coroutine_Throw(groundDirection.normalized,_initialVelocity, angle, time));
            }
        }       
       
    }
    private void DrawPath(Vector3 direction,float v0,float angle,float time,float step)
    {
        step=Mathf.Max(0.01f,step);
       
        _lineRenderer.positionCount = (int)(time / step) + 2;
        int count = 0;
        for(float i=0; i<time;i+=step)
        {
            float x=v0 * i * Mathf.Cos(angle);
            float y=v0 * i * Mathf.Sin(angle) - 0.5f * -Physics.gravity.y * Mathf.Pow(i,2);
            _lineRenderer.SetPosition(count,_throwPoint.position+ direction*x+Vector3.up*y);
            count++;
        }
        float xFinal= v0 * time * Mathf.Cos(angle);
        float yFinal = v0 * time * Mathf.Sin(angle) - 0.5f * -Physics.gravity.y * Mathf.Pow(time, 2);
        _lineRenderer.SetPosition(count,_throwPoint.position + direction * xFinal + Vector3.up * yFinal);
    }
    private float QuadraticEquation(float a, float b, float c, float sign) 
    {
        return (-b + sign * Mathf.Sqrt(b * b - 4 * a * c)) / (2 * a);
    }
    private void CalculatePathWithHeight(Vector3 targetPos,float h,out float v0,out float angle,out float time)
    {
        float xt=targetPos.x;
        float yt=targetPos.y;  
        float g=-Physics.gravity.y;

        float b = Mathf.Sqrt(2 * g * h);
        float a = (-0.5f * g);
        float c = -yt;

        float tplus = QuadraticEquation(a, b, c, 1);
        float tmin=QuadraticEquation(a, b, c, -1);
        time=tplus>tmin?tplus:tmin;

        angle = Mathf.Atan(b * time / xt);
        v0=b/Mathf.Sin(angle);
    }
    IEnumerator Coroutine_Throw(Vector3 direction, float v0, float angle, float time)
    {
        float t = 0;
        while(t<time)
        {
            float x=v0*t*Mathf.Cos(angle);
            float y=v0*t*Mathf.Sin(angle)-(1f/2f)*-Physics.gravity.y*Mathf.Pow(t,2);
            transform.position =_throwPoint.position + direction * x + Vector3.up * y;
            t += Time.deltaTime;
            yield return null;
        }
    }
    private void CalculatePath(Vector3 targetPos,float angle,out float v0,out float time)
    {
        float xt=targetPos.x;
        float yt=targetPos.y;
        float g=-Physics.gravity.y;

        float v1 = Mathf.Pow(xt, 2) * g;
        float v2=2*xt*Mathf.Sin(angle)*Mathf.Cos(angle);
        float v3=2*yt*Mathf.Pow(Mathf.Cos(angle),2);
        v0=Mathf.Sqrt(v1/(v2-v3));
        time =xt/(v0*Mathf.Cos(angle));
    }
    
}
