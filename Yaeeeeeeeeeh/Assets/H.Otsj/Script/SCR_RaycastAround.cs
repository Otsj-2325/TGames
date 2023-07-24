using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCR_RaycastAround : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hitData;

        for(float rot = 0; rot < 2 * Mathf.PI; rot += Mathf.PI / 180.0f){
            for (float rot2 = 0; rot2 < 2 * Mathf.PI; rot2 += Mathf.PI / 180.0f)
            {
                Vector3 dir;
                dir.x = Mathf.Cos(rot);
                dir.y = Mathf.Tan(rot);
                dir.z = Mathf.Sin(rot);

                Physics.Raycast(transform.position, dir * 10);
                Debug.DrawRay(transform.position, dir * 10, Color.blue, 0.01f, true);

            }
        }
    }
}
