using System;
using System.Collections;
using System.Collections.Generic;
using Akali.Common;
using Akali.Scripts.Managers;
using Akali.Scripts.Managers.StateMachine;
using DG.Tweening;
using UnityEngine;

public class PlayerController : Singleton<PlayerController>
{
    public enum MovementType
    {
        run,climb,slide,throwHamemr
    }

    private Rigidbody rigidbody;
    public MovementType movementType;
    public Hammer hammer;
    public Vector3 rayOffset,rayOffsetClimb;
    public float rayLenght,rayLenghtClimb;
    [Header("Movement")]
    private float lastPosition;
    public float swerveSpeed, swerveClamp;
    public float moveSpeed;

    private CrowdManager crowdManager;
    private PlayerAnimationController playerAnimationController;
    Vector3 movement;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
        crowdManager = GetComponent<CrowdManager>();
        playerAnimationController = GetComponent<PlayerAnimationController>();
        GameStateManager.Instance.GameStateMainMenu.OnExecute += StartSwerve;
        GameStateManager.Instance.GameStatePlaying.OnExecute += Swerve;
        GameStateManager.Instance.GameStatePlaying.OnExecute += RayHammer;
        GameStateManager.Instance.GameStateMainMenu.OnExecute += RayHammer;
        GameStateManager.Instance.GameStatePlaying.OnExecute += RayClimb;
        GameStateManager.Instance.GameStateMainMenu.OnExecute += RayClimb;
        GameStateManager.Instance.GameStatePlaying.OnExecute += RayRun;
        GameStateManager.Instance.GameStateMainMenu.OnExecute += RayRun;
        GameStateManager.Instance.GameStatePlaying.OnExecute += RaySlider;
        GameStateManager.Instance.GameStateMainMenu.OnExecute += RaySlider;
        GameStateManager.Instance.GameStatePlaying.OnExecute += RayHammerFinal;
    }

    private void StartSwerve()
    {
        if (Input.GetMouseButtonDown(0))
        {
            movementType = MovementType.run;
            lastPosition = Input.mousePosition.x;
            AkaliLevelManager.Instance.LevelIsPlaying();
        }
    }

    private void Swerve()
    {
        
        switch (movementType)
        {
            case MovementType.climb:
                playerAnimationController.ClimbAnimation(true);
                movement = new Vector3(0,1,0.2f);
                rigidbody.useGravity = false;
                break;
            case MovementType.run:
                playerAnimationController.RunAnimation(true);
                movement = Vector3.forward;
                rigidbody.useGravity = true;
                break;
            case MovementType.slide:
                playerAnimationController.SlideAnimation(true);
                movement = Vector3.forward;
                rigidbody.useGravity = true;
                break;
        }
        
        transform.Translate(movement * (moveSpeed * Time.deltaTime));
        
        if (Input.GetMouseButtonDown(0)) lastPosition = Input.mousePosition.x;
            
        if (Input.GetMouseButton(0))
        {
            if (lastPosition == 0)
            {
                lastPosition = Input.mousePosition.x;
            }
            var currentPosition = Input.mousePosition.x;
            var deltaPosition = currentPosition - lastPosition;
            var targetPosition = deltaPosition * swerveSpeed * Time.deltaTime;
            var desiredPosition = transform.position + Vector3.right * (targetPosition);
            desiredPosition.x = Mathf.Clamp(desiredPosition.x, -swerveClamp, swerveClamp);
            transform.position = desiredPosition;
            lastPosition = Input.mousePosition.x;
        }
    }

    public void RayHammer()
    {
        int layermask = 1 << 6;
        
        Vector3 rayTransform = transform.position + rayOffset;
        RaycastHit raycastHit;
        if (Physics.Raycast(rayTransform, transform.TransformDirection(Vector3.forward),out raycastHit, rayLenght))
        {
            Debug.DrawRay(rayTransform,transform.TransformDirection(Vector3.forward) * raycastHit.distance,Color.red,layermask);
            if (raycastHit.collider.gameObject.layer == 6)
            {
                if (!hammer.canTarget)
                {
                    playerAnimationController.Throw();
                    hammer.target = raycastHit.transform;
                }
            }
        }
    }
    
    public void RayHammerFinal()
    {
        int layermask = 1 << 13;
        
        Vector3 rayTransform = transform.position + rayOffset;
        RaycastHit raycastHit;
        if (Physics.Raycast(rayTransform, transform.TransformDirection(Vector3.forward),out raycastHit, rayLenght))
        {
            Debug.DrawRay(rayTransform,transform.TransformDirection(Vector3.forward) * raycastHit.distance,Color.red,layermask);
            if (raycastHit.collider.gameObject.layer == 13)
            {
                if (!hammer.canTarget)
                {
                    playerAnimationController.Throw();
                    hammer.target = raycastHit.transform;
                }
            }
        }
    }

    public void RayRun()
    {
        int layermask = 1 << 10;
        
        Vector3 rayTransform = transform.position + rayOffsetClimb;
        RaycastHit raycastHit;
        if (Physics.Raycast(rayTransform, transform.TransformDirection(Vector3.forward),out raycastHit, rayLenghtClimb))
        {
            Debug.DrawRay(rayTransform,transform.TransformDirection(Vector3.forward) * raycastHit.distance,Color.yellow,layermask);

            if (raycastHit.collider.gameObject.layer == 10)
            {
                movementType = MovementType.run;
            }
            
        }
    }

    public void RaySlider()
    {
        int layermask = 1 << 12;
        
        Vector3 rayTransform = transform.position + rayOffsetClimb;
        RaycastHit raycastHit;
        if (Physics.Raycast(rayTransform, transform.TransformDirection(Vector3.down),out raycastHit, rayLenghtClimb))
        {
            Debug.DrawRay(rayTransform,transform.TransformDirection(Vector3.down) * raycastHit.distance,Color.blue,layermask);

            if (raycastHit.collider.gameObject.layer == 12)
            {
                movementType = MovementType.slide;
            }
            
        }
    }
    
    
    public void RayClimb()
    {
        int layermask = 1 << 9;
        
        Vector3 rayTransform = transform.position + rayOffsetClimb;
        RaycastHit raycastHit;
        if (Physics.Raycast(rayTransform, transform.TransformDirection(Vector3.forward),out raycastHit, rayLenghtClimb))
        {
            Debug.DrawRay(rayTransform,transform.TransformDirection(Vector3.forward) * raycastHit.distance,Color.yellow,layermask);
            if (raycastHit.collider.gameObject.layer == 9)
            {
                movementType = MovementType.climb;
            }

        }
    }

    public void ThrowHammerEvent()
    {
        hammer.canTarget = true;
        hammer.HammerThrow();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 6)
        {
            AkaliLevelManager.Instance.LevelIsFail();
            GetComponent<Animator>().SetTrigger("Dead");
            CrowdManager.Instance.DeadDummys();
        }

        if (collision.gameObject.layer == 4)
        {
            AkaliLevelManager.Instance.LevelIsFail();
            GetComponent<Animator>().SetTrigger("Dead");
            CrowdManager.Instance.DeadDummys();
        }
    }
    
}

