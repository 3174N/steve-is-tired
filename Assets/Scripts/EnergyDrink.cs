using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyDrink : MonoBehaviour
{
    #region Variables
    public bool isActivatingTextBox;
    public GameObject textBoxToActivate;

    float lifetime = 0f;
    float Lifetime {get {return lifetime;}}
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        lifetime += Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerController player = collision.GetComponent<PlayerController>();
        if (player != null && (player.maxRewindTime > player.RewindTime || player.canDrink))
        {
            player.ResetRewindTime();
            FindObjectOfType<AudioManager>().Play("Energy Drink");
            Destroy(gameObject);
        }

        EnergyDrink drink = collision.GetComponent<EnergyDrink>();
        if (drink != null)
        {
            if (Lifetime > drink.Lifetime)
                Destroy(drink.gameObject);
        }
    }

    void ApplyTextBox()
    {
        if (isActivatingTextBox)
        {
            textBoxToActivate.transform.position = FindObjectOfType<PlayerController>().transform.position;
            textBoxToActivate.SetActive(true);
        }
    }
}
