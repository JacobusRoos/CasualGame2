using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScytheSwipe : MonoBehaviour
{
    private float rotateAmount;
    public GameObject soulToCut;

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        float rotation = 2.5f;
        rotateAmount += rotation;
        transform.Rotate(0, 0, -rotation);
        if (rotateAmount >= 30)
        {
            soulToCut.GetComponent<DeathSoul>().StartFade();
        }
        if (rotateAmount >= 60)
        {
            Destroy(gameObject);
        }
	}
}
