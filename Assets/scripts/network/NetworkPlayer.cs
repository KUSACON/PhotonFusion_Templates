using Fusion;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

public class NetworkPlayer : NetworkBehaviour, IPlayerLeft
{ 
    public static NetworkPlayer Local { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public override void Spawned()
    {
        if (Object.HasInputAuthority)
        {
            Local = this;

            Camera.main.gameObject.SetActive(false);

            Debug.Log("Spawned local player");
        }
        else { 
            Debug.Log("Spawned remote player");

            //Disable the camera if we are not the local player
            Camera localCamera = GetComponentInChildren<Camera>();
            localCamera.enabled = false;

            //Only 1 audio listner is allowed in the scene so disable remote players audio listner
            AudioListener audioListener = GetComponentInChildren<AudioListener>();
            audioListener.enabled = false;
        }
    }

    public void PlayerLeft(PlayerRef player)
    {
        if (player == Object.InputAuthority)
            Runner.Despawn(Object);
    }
}
