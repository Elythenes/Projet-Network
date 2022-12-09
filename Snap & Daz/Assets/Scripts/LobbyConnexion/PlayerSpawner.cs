using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
   public GameObject[] playerPrefabs;
   public List<Transform> spawnPoints;

   public GameObject camera;
   
   void Start()
   {
      int randomNumber = Random.Range(0, spawnPoints.Count);
      Transform spawnPoint = spawnPoints[randomNumber];

      spawnPoints.Remove(spawnPoint);
      
      GameObject playerToSpawn = playerPrefabs[(int)PhotonNetwork.LocalPlayer.CustomProperties["playerAvatar"]];
      GameObject player = PhotonNetwork.Instantiate(playerToSpawn.name, spawnPoint.position, Quaternion.identity);
      
      GameObject newCam = Instantiate(camera, Vector3.zero, Quaternion.identity);

      CameraManager camManager = newCam.GetComponent<CameraManager>();
      
      camManager.SetTarget(player.transform);
      camManager.cameraOffset = new Vector3(-6.5f, 13, 13);
      camManager.transform.rotation = Quaternion.Euler(50f, -215f, 0f);
      
      PhotonNetwork.AutomaticallySyncScene = true;
   }
}
