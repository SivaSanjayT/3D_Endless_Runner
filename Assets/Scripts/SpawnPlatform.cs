
using System.Collections;
using UnityEngine;

public class SpawnPlatform : MonoBehaviour
{ 
   private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("chest") && !Player.isplayerDied)
        {
            PlatformSpawner.Instance.SpawnPlatform();
            print("entered");
        }
    }


}
