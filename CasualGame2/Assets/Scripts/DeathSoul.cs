using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathSoul : MonoBehaviour
{
    public GameObject scythePrefab;
    private bool startFade = false;

	// Use this for initialization
	void Start ()
    {
        Vector2 scythe2D = new Vector2(transform.forward.x, transform.forward.z).normalized;
        Vector3 scythePosition = new Vector3(scythe2D.x * -10, 1, scythe2D.y * -10);
        GameObject scythe = Instantiate(scythePrefab, scythePosition + transform.position, Quaternion.Euler(90, 0, -transform.eulerAngles.y + 30));
        scythe.GetComponent<ScytheSwipe>().soulToCut = gameObject;
	}
	
	// Update is called once per frame
	void Update () 
	{
        if (startFade)
        {
            transform.localScale += new Vector3(0.004f, 0.004f, 0);
            transform.position += new Vector3(0, 0, 0.02f);
            Color color;
            color = transform.GetComponent<SpriteRenderer>().color;
            color.a -= .02f;
            transform.GetComponent<SpriteRenderer>().color = color;

            if (color.a <= 0)
            {
                Destroy(gameObject);
            }
        }
	}

    public void StartFade()
    {
        startFade = true;
    }
}
