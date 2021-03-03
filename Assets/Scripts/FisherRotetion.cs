using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FisherRotetion : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //Vector3 direction = (mousePos - transform.position).normalized;
        //float angle = 360 - Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg;
        //transform.eulerAngles = new Vector3(0, 0, angle);

        Vector3 worldPos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -Camera.main.transform.position.z));
        Vector3 direction = worldPos - transform.position;
        direction.z = 0f;
        direction.Normalize();

        float targetAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, 0, targetAngle), 3f * Time.deltaTime);
    }
}
