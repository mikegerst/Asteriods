using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StayInBound : MonoBehaviour
{
    public void WrapEntityInBounds()
    {
        if(Mathf.Abs(transform.position.y) >= ScreenBorder.Height && Mathf.Abs(transform.position.x) >= ScreenBorder.Width)
        {
            transform.position = new Vector3(-transform.position.x, -transform.position.y);
        }
        else if (Mathf.Abs(transform.position.y) >= ScreenBorder.Height)
        {
            transform.position = new Vector3(transform.position.x, -transform.position.y);

        }
        else if (Mathf.Abs(transform.position.x) >= ScreenBorder.Width)
        {
            transform.position = new Vector3(-transform.position.x, transform.position.y);
        }
    }

    private void OnBecameInvisible()
    {
        WrapEntityInBounds();
    }
}
