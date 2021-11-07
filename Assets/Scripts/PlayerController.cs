using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private GameObject heroPrefab;
    private int selectedHero = 0;

    public List<HeroController> heroes;
    
    void Start()
    {
        heroes = new List<HeroController>();
    }
    

    void Update()
    {
        if (heroes.Count > 0)
        {
            if (Input.GetKeyDown(KeyCode.Tab))
            {
                heroes[selectedHero].UnColorize();
                selectedHero = (selectedHero + 1) % heroes.Count;
                heroes[selectedHero].Colorize();
            }
        }
    }

    public GameObject SpawnHero()
    {
        GameObject newHero = Instantiate(heroPrefab, Vector3.zero, Quaternion.identity);
        heroes.Add(newHero.GetComponent<HeroController>());
        return newHero;
    }

    public void AddHero(HeroController newHero)
    {
        heroes.Add(newHero);
    }

    public void MoveHero(Vector2 direction)
    {
        Debug.Log(heroes.Count + " " + selectedHero);
        heroes[selectedHero].Move(direction);
    }

    public HeroController GetSelectedHero()
    {
        return heroes[selectedHero];
    }
}
