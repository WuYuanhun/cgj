using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet_Attraction : MonoBehaviour
{

    public float attraction = 1.0f;
    public float radius = 1.0f;

    public Vector2 position;

    // Start is called before the first frame update
    void Start()
    {
        position = transform.position;
        
        // add this planet to the list of planets
        GameManager.Instance.planet_Attractions.Add(this);

        transform.localScale = new Vector3(radius, radius, 1.0f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Vector2 getForce(Vector2 pos)
    {
        Vector2 direction = position - pos;
        float distance = direction.magnitude;
        float force = attraction / (distance * distance);
        direction.Normalize();
        return direction * force;
    }
}
