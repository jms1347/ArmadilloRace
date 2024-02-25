using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    public GameObject selectedObject;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.gameObject.tag.Contains("ClickObj"))
                {
                    if (hit.transform.GetComponent<Rigidbody>())
                    {
                        selectedObject = hit.transform.gameObject;
                    }
                }

            }
        }

        if (Input.GetMouseButton(0))
        {
            if (selectedObject)
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit))
                {
                    selectedObject.transform.position = new Vector3(hit.point.x, hit.point.y, selectedObject.transform.position.z);
                }
            }
        }


        if (Input.GetMouseButtonUp(0) && selectedObject)
        {
            selectedObject = null;
        }
    }
}
