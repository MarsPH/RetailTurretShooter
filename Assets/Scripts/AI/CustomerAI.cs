using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

//Revision History
// Mahan Poor Hamidian  2024/12/19  Created CustomerAI
public class CustomerAI : MonoBehaviour
{
   [SerializeField] private Transform[] waypoint;
   [SerializeField] float movementSpeed = 5f; // speed of the customer
   [SerializeField] float stateCooldown = 0.5f; //o control when states change (e.g., after being Idle for a few seconds, switch to Move).
   [Space] [Header("AI Random Settings")]
   [SerializeField] int MAX_RANDOM = 100; // max random number for behavior choosing
   [SerializeField] private int stealingRandomIndex = 20;
   [SerializeField] private int rudeRandomIndex = 30;
   
   private NavMeshAgent _agent; // refrenced agent
   private int _randomBehviorIndex;
   private CustomerState _currentState;
   private BehaviorType _currentBehaviorType;
   enum CustomerState
   {
      Idle, //The customer stands still, observing the environment or waiting before moving again.
      Moving,// The customer stands still, observing the environment or waiting before moving again.
      Acting, //The customer performs a behavior (e.g., stealing an item, being rude). For Day 4, you can keep this simple and just set them to “fidget” or “examine” their surroundings.
   }

   enum BehaviorType 
   {
      Stealing, //This customer might later try to pick up items or hide objects. For now, just mark them as “Stealing” behavior internally.
      Rude,//This customer might stand and “complain” or move around more aggressively. For Day 4, treat it the same as innocent, just a different label.
      Innocent // A normal shopper who just wanders and does nothing problematic.
   }

   private void Start()
   {
      _agent = GetComponent<NavMeshAgent>();
      SetRandomDestination();
      AssignRandomBehavior(); //Assigns a behavior type at spawn randomly.
      _currentState = CustomerState.Idle;
   }

   private void SetRandomDestination()
   {
      
   }

   private void Update()
   {
      if (_currentState == CustomerState.Idle)
      {
         //HandleIdleState(); // Do idle
      }

      if (_currentState == CustomerState.Moving)
      {
         //HandleMovingState(); //Do moving
      }

      if (_currentState == CustomerState.Acting)
      {
         //HandleActingState(); //Do acting
      }
   }

   
   private void AssignRandomBehavior()
   {
      _randomBehviorIndex = Random.Range(0, MAX_RANDOM);
      if (_randomBehviorIndex < stealingRandomIndex) // stealing
      {
         _currentBehaviorType = BehaviorType.Stealing; // assigning the behavior type to stealing
      }
      else if (_randomBehviorIndex < rudeRandomIndex) // rude
      {
         _currentBehaviorType = BehaviorType.Rude;// assigning the behavior type to rude
      }
      else //innocent
      {
         _currentBehaviorType = BehaviorType.Innocent;// assigning the behavior type to innocent
      }
      
      Debug.Log(_currentBehaviorType);
   }



   private void HandleActingState()
   {
      //throw new NotImplementedException();
   }

   private void HandleMovingState()
   {
      //throw new NotImplementedException();
   }

   private void HandleIdleState()
   {
      //throw new NotImplementedException();
   }
}
