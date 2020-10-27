using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    //Player RigidBody
    public Rigidbody2D rb;

    //HealthBar stuff
    public HealthBar hpBar;
    public int maxHealth = 100;
    public int currentHealth;

    //ActionBar stuff
    public GameObject ActionBar;


    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        hpBar.SetMaxHealth(maxHealth);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //TakeDamage(10);
        }   
    }

    void TakeDamage(int damage)
    {
        currentHealth -= damage;
        hpBar.SetHealth(currentHealth);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Item"))
        {
            ActionBarScript abs = ActionBar.GetComponent<ActionBarScript>();
            Image[] frameArray = abs.FrameArray;
            Button ab1 = frameArray[0].transform.parent.gameObject.GetComponent<Button>();
            Button ab2 = frameArray[1].transform.parent.gameObject.GetComponent<Button>();
            Button ab3 = frameArray[2].transform.parent.gameObject.GetComponent<Button>();

            if (ab1.GetComponent<Image>().sprite == null)
            {
                ab1.GetComponent<Image>().sprite = other.GetComponent<SpriteRenderer>().sprite;
                Color color = ab1.GetComponent<Image>().color;
                ab1.GetComponent<Image>().color = new Color(color.r, color.g, color.b, 255);
                Destroy(other.gameObject);
            }
            else if (ab2.GetComponent<Image>().sprite == null)
            {
                ab2.GetComponent<Image>().sprite = other.GetComponent<SpriteRenderer>().sprite;
                Color color = ab2.GetComponent<Image>().color;
                ab2.GetComponent<Image>().color = new Color(color.r, color.g, color.b, 255);
                Destroy(other.gameObject);
            }
            else if (ab3.GetComponent<Image>().sprite == null)
            {
                ab3.GetComponent<Image>().sprite = other.GetComponent<SpriteRenderer>().sprite;
                Color color = ab3.GetComponent<Image>().color;
                ab3.GetComponent<Image>().color = new Color(color.r, color.g, color.b, 255);
                Destroy(other.gameObject);
            }
            
        }
    }
}
