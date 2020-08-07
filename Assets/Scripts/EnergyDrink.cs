using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyDrink : MonoBehaviour
{
    #region Variables
    public bool isActivatingTextBox;
    public GameObject textBoxToActivate;

    float age = 0f;
    float Age {get {return age;}}
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        age += Time.deltaTime;
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
            if (Age > drink.Age)
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
