﻿using UnityEngine;
using System.Collections;

public class Candle : MonoBehaviour
{
    [SerializeField]
    Sprite CandleLit;

    [SerializeField]
    Sprite CandleBlown;

    [SerializeField]
    float minCandleTime = 0f;

    [SerializeField]
    float maxCandleTime = 0f;

    SpriteRenderer spriteRenderer;
    bool playerInBounds;

    public bool isLit { get; private set; }

    void Start()
    {
        spriteRenderer = gameObject.GetComponentInParent<SpriteRenderer>();
        BlowCandle();
    }

    void LitCandle()
    {
        isLit = true;
        spriteRenderer.sprite = CandleLit;

        if(maxCandleTime > 0f)
            Invoke("BlowCandle", Random.Range(minCandleTime, maxCandleTime));
    }

    void BlowCandle()
    {
        isLit = false;
        spriteRenderer.sprite = CandleBlown;
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "Player")
        {
            playerInBounds = true;
        }
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.tag == "Player")
        {
            playerInBounds = false;
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && playerInBounds)
        {
            if(!isLit)
                LitCandle();
        }
    }
}
