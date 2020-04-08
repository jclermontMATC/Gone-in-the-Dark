using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Created by Mike Lecus
public class Torch : MonoBehaviour
{
    //public GameObject torch;        //torch gameobject
    public int fullHealth = 100;    //variable for full health
    public int fullOil = 100;       //variable  for full oil
    

    Player player = new Player();

    private Health health;
    private Lantern lantern;
    

    public GameObject playerObj;

    

    public void Start()
    {
        health = FindObjectOfType<Health>();    //Finding Script Health
        lantern = FindObjectOfType<Lantern>();  //Finding Script Lantern
       
    }
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            Load();
        }
    }

     void OnTriggerStay(Collider other) {    //when colliding with torch object
            if (Input.GetKeyDown(KeyCode.R) && other.gameObject.tag == "Player")
            {  //with  "R" pressed, while player is colliding with torch
                Debug.Log("Hit");
                Activation();       //Activates method
            }
        }


        void Activation() {
            health.currentHealth = fullHealth;      //health goes to full health
            lantern.currentOil = fullOil;           //oil goes to full oil
           

            player._health = health.currentHealth;
            player._oil = lantern.currentOil;
            

            player._XPos = playerObj.transform.position.x;      //getting position of player
            player._YPos = playerObj.transform.position.y;
            player._ZPos = playerObj.transform.position.z;

            XMLOp.Serialize(player, "player.xml");      //saving health, oil, and player position

        }

        public void Load() { 
        if (System.IO.File.Exists("player.xml"))
        {
            player = XMLOp.Deserialize<Player>("player.xml");
            Debug.Log(player._health);
        }
       /* else
        {
           player._health = health.currentHealth;
           player._oil = lantern.currentOil;
           player._XPos = playerObj.transform.position.x;
           player._YPos = playerObj.transform.position.y;
           player._ZPos = playerObj.transform.position.z;
           XMLOp.Serialize(player, "player.xml"); // for save button also
        }*/
    }

    }


