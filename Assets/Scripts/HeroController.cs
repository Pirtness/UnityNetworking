using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroController : MonoBehaviour
{
    [SerializeField] private float stepLength = 0.3f;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Colorize()
    {
        gameObject.GetComponent<SpriteRenderer>().color = UnityEngine.Color.red;
    }
    
    public void UnColorize()
    {
        gameObject.GetComponent<SpriteRenderer>().color = UnityEngine.Color.white;
    }

    public void Move(Vector2 direction)
    {
        transform.position = new Vector2(transform.position.x, transform.position.y) + direction * stepLength;
    }
}
