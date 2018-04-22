using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : MonoBehaviour
{
    [SerializeField] private float moveSpeed;

    private void Update()
    {
        transform.Translate(0f, moveSpeed * Time.deltaTime, 0f);

        if (transform.position.y > 35f)
        {
            Destroy(this.gameObject);
        }
    }
}
