using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TurretController : MonoBehaviour
{
    //Inspector variables
    [Header("Turret Objects for Movement")]
    [SerializeField] private GameObject turret; // total body of the turret
    [SerializeField] private GameObject turretHead; // only the head of turret for rotation // I have used this for mouse postiion as camera and worked
    [SerializeField] private Camera turretCamera;
    
    [Header("Turret Objects for Aiming")]
    [SerializeField] private GameObject turretBase;
    [SerializeField] private GameObject turretBarrel;
    
    [Space] [Header("Turret Settings")] 
    [SerializeField] private float turretSpeed; // the movement speed of the turret 
    [SerializeField] private float turretRotationSpeed; // the rotation speed of the pivot head of turret {for movement based on the position of mouse}
    [Space] [SerializeField] private float turretRotationSensitivity = 0.1f;
    [SerializeField] private float turretPitchLimit = 45f;
    //private variables
    private Vector2 _moveValue; // a variable to store the input
    private Vector2 _mousePosition2D; // mouse position
    private Vector2 _mouseDelta; // mouse delta for moving the aim

    private float _currentPitch = 0f;
    
    
    
    
    PlayerInputActions _inputActions; //the input action

    //Events
    private void Awake()
    {
        _inputActions = new PlayerInputActions(); // Instantiation the Object of PlayerInputActions
    }
    private void OnEnable()
    {
        _inputActions.Gameplay.Enable(); // Enabling input
        EnableMouseDeltaInput();
        EnableMovementInput();
        EnableMouseInput();
    }

    private void OnDisable()
    {
        DisableMovementInput();
        DisableMovementInput();
        DisableMouseInput();
        _inputActions.Gameplay.Disable();
    }

    private void Update()
    {
        MoveTurret(); // move the turret on the rail
        //TurretHeadRaycast(); this is based on the mouse Position
        RotateTurretOnDelta(); //moves turred based on the mouse delta
    }
    
    #region Mouse Delta Movement
   //Turret Rotation Based on the Mouse Delta

   private void RotateTurretOnDelta()
   {
       if (turretBase)
       {
           float yaw = _mouseDelta.x * turretRotationSensitivity; // saving the mouse movement on horizental axis * senstivity
           turretBase.transform.Rotate(0, yaw, 0); //rotation on y axis based on the movement of the mouse on x axis in 2D
       }

       if (turretBarrel)
       {
           float pitch = -_mouseDelta.y * turretRotationSensitivity; // saving the mouse movement on vertical axis * senstivty
           // -_mouseDelta.Y becasue the moveoment is inverted by default 
           _currentPitch = Mathf.Clamp(_currentPitch + pitch, -turretPitchLimit, turretPitchLimit);
           turretBarrel.transform.localRotation = Quaternion.Euler(0, 0, _currentPitch);// it will move the barrel on z axis
           
       }
       
       _mouseDelta = Vector2.zero; // to reset the delta avoiding the infinte movement
   }
   private void EnableMouseDeltaInput() //subscribe to the Look action
   {
       _inputActions.Gameplay.MouseLook.performed += OnMouseLookPerformed;
       _inputActions.Gameplay.MouseLook.canceled += OnMouseLookPerformed;
   }

   private void DisableMouseDeltaInput() //unsubscribe to the Look action
   {
       _inputActions.Gameplay.MouseLook.performed -= OnMouseLookPerformed;
       _inputActions.Gameplay.MouseLook.canceled -= OnMouseLookPerformed;
   }

   private void OnMouseLookPerformed(InputAction.CallbackContext context)
   {
       // read the mouse delta from the input system
       _mouseDelta = context.ReadValue<Vector2>();
   }
#endregion

    #region Turret Movement

   //Turret Movement on Z axis
   private void MoveTurretHead(Vector3 targetPosition)
   {
       Vector3 direction = (targetPosition - turretHead.transform.position).normalized;
        
       Quaternion lookRotation = Quaternion.LookRotation(direction);
        
       turretHead.transform.rotation = Quaternion.Slerp(turretHead.transform.rotation, lookRotation, turretRotationSpeed * Time.deltaTime);
   }
   private void OnMovePerformed(InputAction.CallbackContext ctx)
   {
       _moveValue = ctx.ReadValue<Vector2>(); // set the moveValue to the amount of the input
   }
   private void OnMoveCanceled(InputAction.CallbackContext ctx)
   {
       _moveValue = Vector2.zero; //setting it on 0 if the input cancelled
   }
   private void MoveTurret()
   {
       if (turret && _moveValue != Vector2.zero)
       {
           Vector3 movement = new Vector3(_moveValue.x, 0, _moveValue.y) * (Time.deltaTime * turretSpeed); // Translating 2D to 3D
            
           turret.transform.position += movement; //Moving the turret based on the Vector
       }
   }
   private void DisableMovementInput()
   {
       _inputActions.Gameplay.MoveAlongRails.performed -= OnMovePerformed; //unsubscribing
       _inputActions.Gameplay.MoveAlongRails.canceled -= OnMoveCanceled; //unsubscribing
        
       _inputActions.Gameplay.Disable(); //disabling input
   }
   private void EnableMovementInput()
   {

       _inputActions.Gameplay.MoveAlongRails.performed += OnMovePerformed; // subscribing
       _inputActions.Gameplay.MoveAlongRails.canceled += OnMoveCanceled; // subscribing
   }

   #endregion
   
    #region Turret Aiming On Position
    //Mouse Movement Based on the mouse Position. Not in use
    private void TurretHeadRaycast()
    {
        Ray ray = turretCamera.ScreenPointToRay(new Vector3(_mousePosition2D.x, _mousePosition2D.y, 0 ));
        Debug.DrawRay(ray.origin, ray.direction * 100, Color.yellow);
        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity)) // raycast with infinte length based on the position of  the mouse
        {
            Vector3 targetPosition = hit.point;
            
            MoveTurretHead(targetPosition);
            
        }
    }

    private void EnableMouseInput()
    {
        _inputActions.Gameplay.MoveMouse.performed += OnMousePositionChanged; 
    }

    private void DisableMouseInput()
    {
        _inputActions.Gameplay.MoveMouse.performed -= OnMousePositionChanged;
    }

    private void OnMousePositionChanged(InputAction.CallbackContext ctx)
    {
        _mousePosition2D = ctx.ReadValue<Vector2>();
    }
    #endregion
}
