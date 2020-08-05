using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Template : MonoBehaviour
{
    private void Awake()
    {
        transform.parent = null;
    }
}
