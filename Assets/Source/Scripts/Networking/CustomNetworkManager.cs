using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class CustomNetworkManager : NetworkManager
{
    public GameObject[] SpawnedItems;

    private bool isStuffSpawned = false;

    public override void OnServerAddPlayer(NetworkConnection conn)
    {
        base.OnServerAddPlayer(conn);
        if (numPlayers == 1 && !isStuffSpawned)
        {
            for (int i = 0; i <= 4; i++)
            {
                GameObject collectableItem = spawnPrefabs.Find(prefab => prefab.name == "CollectableItem");
                GameObject randomItem = Instantiate(collectableItem, new Vector2(-11,6 - i * 1.5f), collectableItem.transform.rotation);
                NetworkServer.Spawn(randomItem);
                SpawnedItems[i] = randomItem;
                isStuffSpawned = true;
            }
        }
    }

}
