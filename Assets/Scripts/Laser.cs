﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    //speed variable of 8
    [SerializeField]
    private float _speed = 8f;
    [SerializeField]
    public bool destroyAtEdge = true;

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.up * _speed * Time.deltaTime);

        if (destroyAtEdge && transform.position.y > 8f)
        {
            Destroy(this.gameObject);
        }
    }
}