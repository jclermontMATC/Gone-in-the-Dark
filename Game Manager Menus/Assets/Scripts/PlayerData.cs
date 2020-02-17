using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData {
    int hp;
    int stam;
    int oil;
    float[] position;
    string[] inventory;
    int[] itemCount;
    public PlayerData(Player player)
    {
        hp = player.hp;
        stam = player.stam;
        oil = player.oil;

        position = new float[3];
        position[0] = player.transform.position.x;
        position[1] = player.transform.position.y;
        position[2] = player.transform.position.z;

        //parallel arrays for inventory items and count
    }
}

