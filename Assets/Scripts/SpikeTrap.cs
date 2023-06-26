using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeTrap : MonoBehaviour
{
     // Start is called before the first frame update
    void Start()
    {
        GameManager.WaveUpEvent += ShouldSpawnSpikes;
    }

    void ShouldSpawnSpikes() {
        Transform spikes = gameObject.transform.GetChild(0);
        Debug.Log("transform: " + spikes);
        if (Random.value < .3f)
           spikes.gameObject.SetActive(true);
    }
}
