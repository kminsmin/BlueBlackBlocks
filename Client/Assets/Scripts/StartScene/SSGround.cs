using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SSGround : MonoBehaviour
{

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.CompareTag("Area"))
            return;
        transform.Translate(Vector3.right * 40);
    }
}
