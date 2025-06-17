using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
public class KiewietCOntrol : MonoBehaviour
{
 [SerializeField]
 private PlayerInput playerInput;
 private Vector3 inputDirection;
 private Vector3 movevector;
 public ArmFollower armFollowerleft;
 public ArmFollower armFollowerright;
 private ArmFollower armFollower;
 bool left = false;
 public controller_exam exam;
 public Rigidbody hips;
 public Layermanager lm;
 public Animator animator;
 
 

 private void Awake()
 {
  armFollower=armFollowerleft;
  Collider[] colliders = GetComponentsInChildren<Collider>();
  string layerName = lm.setlayer();
  int layer = LayerMask.NameToLayer(layerName);

  foreach (Collider col in colliders)
  {
   col.gameObject.layer = layer;
  }
 }


 public void OnArmMove(InputValue value)
 {
  Vector2 PlayerInput = value.Get<Vector2>();  //Gets the input and converts it to a Vector3
  inputDirection.x = PlayerInput.x;
  inputDirection.z = PlayerInput.y;
  //Debug.Log(inputDirection);

 }
 public void OnMove(InputValue value)
 {
  animator.SetBool("walking", true);
  Debug.Log("walking");
  Vector2 PlayerInput = value.Get<Vector2>();  //Gets the input and converts it to a Vector3
  movevector.x = PlayerInput.x;
  movevector.z = PlayerInput.y;
  Vector3 hi =new Vector3(hips.transform.position.x+movevector.x,hips.transform.position.y,hips.transform.position.z+movevector.z);
  hips.MovePosition((hi));
  
  
  //Debug.Log(movevector);

 }

 public void OnSwitchArmsDivan(InputValue value)
 {
  Debug.Log("switch");
  if (left)
  {
   armFollower = armFollowerleft;
   //left arm block 
   animator.SetBool("blockleft", false);
  }
  else
  {
   armFollower= armFollowerright;
   animator.SetBool("blockleft", true);
   //right arm block 
  }

  left = !left;
 }

 void OnResetDivan(InputValue value)
 {
  Debug.Log("reset");
  SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
 }

 private void FixedUpdate()
 {
  armFollower.movearm(inputDirection);
  
 }
}
