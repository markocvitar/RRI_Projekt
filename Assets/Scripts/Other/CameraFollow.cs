using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] CinemachineVirtualCamera cinemachineVirtualCamera;
    // Start is called before the first frame update
    void Awake()
    {
        GameManager.sharedInstance.Player.SetActive(true);
        cinemachineVirtualCamera.Follow = GameManager.sharedInstance.Player.transform;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
