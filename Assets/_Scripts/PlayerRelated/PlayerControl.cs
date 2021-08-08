using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
[RequireComponent(typeof(PlayerShoot))]
[RequireComponent(typeof(PlayerMove))]
[RequireComponent(typeof(WeaponControl))]

public class PlayerControl : MonoBehaviour
{
    public static bool soundmanagerLoaded;
    [Header("Set in Inspector")]
    public WeaponInfo weaponInf;
    public GameObject DeathText;
    public GameObject shootingEffect;
    public TrailRenderer playerTrail;
    public Animator animator;
    public PlayerMove moveBeh;
    public PlayerShoot shootingBeh;
    public WeaponControl weaponBeh;
    public PlayerAim playerAimingHandle;
    public PlayerInput playerInput;
    public PauseMenu pauseBeh;
    public MenuManager menuManagerHandle;
   
    private void Awake()
    {
        if(animator == null)
        {
            animator = GetComponent<Animator>();
        }


        if(SceneManager.GetActiveScene().name == "MainMenu")
        {
            gameObject.SetActive(false);
        }
        if(pauseBeh == null) pauseBeh = FindObjectOfType<PauseMenu>().GetComponent<PauseMenu>();
        if (moveBeh == null) moveBeh = GetComponent<PlayerMove>();
        if (shootingBeh == null) shootingBeh = GetComponent<PlayerShoot>();
        if (weaponBeh == null) weaponBeh = GetComponent<WeaponControl>();


        if (FindObjectOfType<SoundManager>() != null)
        {
            soundmanagerLoaded = true;
        }
        else
        {
            soundmanagerLoaded = false;
        }


        if (DeathText == null) DeathText = FindObjectOfType<DeathMenu>().gameObject;


        DeathText.SetActive(false);


        InitActions();

    }

    private void InitActions()
    {
        playerInput.actions["Jump"].started += ctx => OnJump(ctx);
        playerInput.actions["Run"].started += ctx => OnRun(ctx);
        playerInput.actions["Run"].canceled += ctx => OnRun(ctx);
        playerInput.actions["MoveDown"].started += ctx => OnMoveDown(ctx);
        playerInput.actions["Pause"].started += ctx => OnPause(ctx);
        playerInput.actions["NextWeapon"].started += ctx => OnSwitchWeaponForward(ctx);
        playerInput.actions["PreviousWeapon"].started += ctx => OnswitchWeaponBackward(ctx);
        playerInput.actions["MeleeAttack"].performed += ctx => OnMeleeAttack(ctx);
        playerInput.actions["Fire"].started += ctx => OnShoot(ctx);
        playerInput.actions["Fire"].canceled += ctx => OnShoot(ctx);
    }

   
    void Start()
    {
        if (soundmanagerLoaded)
        {
            SoundManager.playMusic(ref AudioSources.music);
        }
        FindObjectOfType<PlayerSpeaks>().SpecialPharase("There is Only One Mustache!!!", 5f);
        TextControl.PrintText("Only One Mustache will Survive!", 5f);
    }

    void Update()
    {
        playerInput.actions["MeleeAttack"].started += ctx => OnMeleeAttack(ctx);
        OnAim(playerInput.actions["Aim"].ReadValue<Vector2>());
        OnMove(playerInput.actions["Move"].ReadValue<float>());

        if (WeaponInfo.CurrentWeapon == null || WeaponInfo.CurrentWeapon.name == "Nothing")
        {
            animator.SetBool("SingleHandedEquipped", false);
            animator.SetBool("TwoHandedEquipped", false);
        } else if(WeaponInfo.CurrentWeapon.name.Contains("Grenade")==true || WeaponInfo.CurrentWeapon.name.Contains("Pistol") == true)
        {
            animator.SetBool("SingleHandedEquipped", true);
            animator.SetBool("TwoHandedEquipped", false);
        } else
        {
            animator.SetBool("SingleHandedEquipped", false);
            animator.SetBool("TwoHandedEquipped", true);
        }


    }

    public void OnMove(float move)
    {
        moveBeh.moveLeftRight(move);
        animator.SetFloat("Run", Mathf.Abs(move));
    }
    public void OnRun(InputAction.CallbackContext value)
    {
        bool spedUp = false;    
        if(value.action.triggered)
               spedUp = moveBeh.SetRunningSpeed();

        if (spedUp)
        {
            animator.SetBool("Accellerate", true);
            if(playerTrail != null)
            {
                playerTrail.startColor = Color.blue;
            }
        }
        else
        {
            animator.SetBool("Accellerate", false);
            playerTrail.startColor = Color.red;
        }

    }
    public void OnStopRun()
    {

    }

    public void OnJump(InputAction.CallbackContext value)
    {
        var p = value.ReadValue<float>();
        if (value.action.triggered)
        {
            moveBeh.Jump();
            animator.SetBool("Jump", true);
        }
        else
        {
            animator.SetBool("Jump", false);
        }
       

    }

    private AnimationClip findClip(Animator anim, string clipName)
    {
        AnimationClip an = null;
        for(int i=0; i < anim.runtimeAnimatorController.animationClips.Length; i++)
        {
            if(anim.runtimeAnimatorController.animationClips[i].name == clipName)
            {
                Debug.LogWarning("found clip");
                an = anim.runtimeAnimatorController.animationClips[i];
            }
        }
        
        
        return an;
    }

    public void OnMoveDown(InputAction.CallbackContext value)
    {
        if (value.action.triggered)
            moveBeh.MoveDown();
    }

    public void OnAim(Vector2 aim)  /////////////////////
    {
        playerAimingHandle.SetMousePosition(aim); 
    }
    public void OnShoot(InputAction.CallbackContext context)
    {
        float val = 0f;
        if (context.action.phase.ToString() == "Started") val = 1f;
        else if (context.action.phase.ToString() == "Cancelled") val = 0f;
        shootingBeh.Shoot(val);
    }

    public void OnSwitchWeaponForward(InputAction.CallbackContext value)
    {
        if(value.action.triggered)
            weaponBeh.SwitchForward();
    }

    public void OnswitchWeaponBackward(InputAction.CallbackContext value)
    {
        if (value.action.triggered)
            weaponBeh.SwitchBackwards();
    }

    public void OnMeleeAttack(InputAction.CallbackContext value)
    {
        var val = value.ReadValue<float>();
        value.action.started += _ => animator.SetBool("MeleeAttack", true);
        value.action.canceled += _ => animator.SetBool("MeleeAttack", false);

       
    }

    public void OnPause(InputAction.CallbackContext value)
    {
        menuManagerHandle.PauseGame();
    }



    public void SetGameplayActionMap()
    {
        playerInput.SwitchCurrentActionMap("Gameplay");
        CursorScript.SetGameCursor();
    }

    public  void SetMenuActionMap()
    {
        playerInput.SwitchCurrentActionMap("UI");
        CursorScript.SetMenuCursor();
        
    }




}
