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
    private float MaxHealth = 100;
    private float currentHealth;
    private float MaxArmor = 100;
    private float currentArmor;

    private int Score  = 0;
    private int killCount = 0;
    public static bool soundmanagerLoaded;

    private bool isDead = false;
    private float timeToshoot = 0;
    private bool fire = false;

    [Header("Set in Inspector")]
    public WeaponInfo weaponInf;
    public TextMeshProUGUI ammoText;
    public TextMeshProUGUI healthText;
    public TextMeshProUGUI armorText;
    public TextMeshProUGUI killText;
    public TextMeshProUGUI GoldCount;

    public GameObject DeathText;
    public StatDisplay armorBar;
    public StatDisplay healthBar;
    public GameObject shootingEffect;
    public GameObject damageEffect;
    public TrailRenderer playerTrail;

    public Animator animator;
    public CameraFolllow camScript;
    public PlayerMove moveBeh;
    public PlayerShoot shootingBeh;
    public WeaponControl weaponBeh;
    public PlayerInput playerInput;
    public PauseMenu pauseBeh;

    public GameObject pauseMenuobj;
    

    public TextMeshProUGUI livesLeft;
    public static int NumberOfLifes = 4;



    private Vector2 screenPos;

    private static PlayerControl S;
    private void Awake()
    {
        if (S == null)
        {
            S = this;
        }

        if(animator == null)
        {
            animator = GetComponent<Animator>();
        }


        if(SceneManager.GetActiveScene().name == "MainMenu")
        {
            gameObject.SetActive(false);
        }


        if(camScript == null)
        {
            camScript = Camera.main.GetComponent<CameraFolllow>();
        }
      
        
       if(pauseBeh == null)
        {
            pauseBeh = FindObjectOfType<PauseMenu>().GetComponent<PauseMenu>();
        }

        if (moveBeh == null)
            moveBeh = GetComponent<PlayerMove>();

        if (shootingBeh == null)
            shootingBeh = GetComponent<PlayerShoot>();

        if (weaponBeh == null)
            weaponBeh = GetComponent<WeaponControl>();

        if (FindObjectOfType<SoundManager>() != null)
        {
            soundmanagerLoaded = true;
        }
        else
        {
            soundmanagerLoaded = false;
        }

        if(DeathText == null)
        {
            DeathText = FindObjectOfType<DeathMenu>().gameObject; 
        }

        PlayerMove.FacingRight = true;
        DeathText.SetActive(false);



        PlayerControl.EnableInGameInput();
    }


    // Start is called before the first frame update
    void Start()
    {
        if (soundmanagerLoaded)
        {
            SoundManager.playMusic(ref AudioSources.music);
        }


        currentHealth = MaxHealth;
        currentArmor = MaxArmor;
        Score = 0;
        killCount = 0;
        FindObjectOfType<PlayerSpeaks>().SpecialPharase("There is Only One Mustache!!!", 5f);
        TextControl.PrintText("Only One Mustache will Survive!", 5f);

        S.healthBar.setHealth(S.currentHealth / S.MaxHealth);
        S.armorBar.setHealth(S.currentArmor / S.MaxArmor);

        InputAction aim = playerInput.actions.FindAction("Aim");
        aim.performed += _ => shootingBeh.Aim(screenPos);

    }

    public static void Restart()
    {
        // armor at the start ?
        S.currentHealth = S.MaxHealth;
        S.Score = 0;
        S.killCount = 0;
        NumberOfLifes = 4;
        S.isDead = false;
        CursorScript.SetGameCursor();
        S.healthBar.setHealth(S.currentHealth / S.MaxHealth);
        S.armorBar.setHealth(S.currentArmor / S.MaxArmor);

        PlayerControl.EnableInGameInput();
    }

    public static  void Respawn()
    {
        S.currentHealth = S.MaxHealth;
        S.isDead = false;
        S.healthBar.setHealth(S.currentHealth / S.MaxHealth);
        S.armorBar.setHealth(S.currentArmor / S.MaxArmor);
        PlayerControl.EnableInGameInput();
    }

    // Update is called once per frame
    void Update()
    {
              

        if(livesLeft !=null)
            livesLeft.text = "Lives left: " + NumberOfLifes;



        if (currentHealth <= 0 && isDead == false)
        {
            Die();
        }

        if (PauseMenu.pauseMenuActive == false)
        {
            if (WeaponInfo.CurrentWeapon != null)
            {
                ammoText.text = "Current Ammo: " + WeaponInfo.CurrentWeapon.currentAmmo.ToString() + "\nCurrent Weapon: " + WeaponInfo.CurrentWeapon.NameToDisplay;
            }
            else
            {
                ammoText.text = "Current Weapon: empty" + "\nCurrent Ammo:";
            }
            healthText.text = "HP: " + (currentHealth * 100 / MaxHealth).ToString() + " %";
            armorText.text = "Armor: " + (currentArmor * 100 / MaxArmor).ToString() + " %";
            killText.text = "Enemies Killed: " + killCount.ToString();
            GoldCount.text = "Geld Collected: " + Score.ToString();
        }



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

        if (camScript == null)
        {
            camScript = FindObjectOfType<CameraFolllow>().GetComponent<CameraFolllow>();
        }

    }
    private void FixedUpdate()
    {
        
        moveBeh.moveLeftRight();
        if (fire && WeaponInfo.CurrentWeapon.fireRate>0)
        {
            if(Time.time >= timeToshoot)
            {
                bool shotwasfired = false;
                timeToshoot = Time.time + 1 / WeaponInfo.CurrentWeapon.fireRate;
                shotwasfired = shootingBeh.Shoot();

                if (shotwasfired == true)
                {
                    camScript.Shake();
                    shootingBeh.ShootingEffectPlay(shootingEffect);
                }
                if (soundmanagerLoaded == true && shotwasfired == true)
                {
                    SoundManager.PlaySound("Shoot", ref AudioSources.shoot);
                }
            }

        }
        
    }

    public void OnMove(InputAction.CallbackContext value)
    {
        float input = value.ReadValue<float>();
        if (input > 0)
            moveBeh.SetDefaultSpeed(1);
        else if (input < 0)
            moveBeh.SetDefaultSpeed(-1);
        else
            moveBeh.Stop();

        animator.SetFloat("Run", Mathf.Abs(input));
    }
    public void OnSetRunSpeed(InputAction.CallbackContext value)
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

    // // // AIM!!!!!!!!!!!!!!!!
    public void OnAim(InputAction.CallbackContext value)
    {

        screenPos = value.ReadValue<Vector2>();

    }
    public void OnShootOnce(InputAction.CallbackContext value)
    {
        if (value.performed)
        {
            fire = true;
        }
        else
        {
            fire = false;
        }


        bool shotwasfired = false;
        if(WeaponInfo.CurrentWeapon!=null &&  WeaponInfo.CurrentWeapon.fireRate == 0 && WeaponInfo.CurrentWeapon.name.Contains("Grenade") == false)
        {
            var val = value.ReadValue<float>();
            if (val == 1f)
                shotwasfired = shootingBeh.Shoot();
        } else if (WeaponInfo.CurrentWeapon != null &&  WeaponInfo.CurrentWeapon.name.Contains("Grenade"))
        {
            if (value.action.triggered)
                shootingBeh.throwGrenade();
        }

        if(shotwasfired == true )
        {
            camScript.Shake();
            shootingBeh.ShootingEffectPlay(shootingEffect);
        }
        if (soundmanagerLoaded==true && shotwasfired == true)
        {
            SoundManager.PlaySound("Shoot", ref AudioSources.shoot);
        }
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
       // value.action.started += _ => weaponBeh.DestroyCurrentWeapons();
       

       // value.action.canceled += _ => weaponBeh.InstantiateCurrentWeapon();
        value.action.canceled += _ => animator.SetBool("MeleeAttack", false);

       
    }

    public void OnPauseMenu(InputAction.CallbackContext value)
    {
        var p = value.ReadValue<float>();
        if (p == 1f)
        {
            PauseMenu.pauseMenuActive = true;
            pauseBeh.SetPause();
        }
        else if(p == 1f && PauseMenu.pauseMenuActive == true){
            pauseBeh.Resume();
        }
    }



  public static void DisableInGameInput()
    {
            CursorScript.SetMenuCursor();
           S.playerInput.SwitchCurrentActionMap("Menu");    
    }

    public static void EnableInGameInput()
    {       
            S.playerInput.SwitchCurrentActionMap("InGame");
            CursorScript.SetGameCursor();
        
    }

    public static void TakeDamage(float damage)
    {
        if (S.currentArmor > 0)
        {
            S.currentArmor -= damage;
        } else
        {
            S.currentHealth -= damage;
        }
        if (S.currentArmor < 0)
            S.currentArmor = 0;
        if (S.currentHealth < 0)
            S.currentHealth = 0;
        S.ShowDamageEffect(S.damageEffect);
        S.healthBar.setHealth(S.currentHealth / S.MaxHealth);
        S.armorBar.setHealth(S.currentArmor / S.MaxArmor);
    }

    public static void TakeArmor() {
        S.currentArmor = S.MaxArmor;
        S.armorBar.setHealth(S.currentArmor / S.MaxArmor);
    }


    public void ShowDamageEffect(GameObject effect)
    {
        effect.transform.position = gameObject.transform.position+new Vector3(0,1.5f,0);
        effect.transform.rotation = gameObject.transform.rotation;
        effect.GetComponent<ParticleSystem>().Play();
       
    }
    public static void AddKill()
    {
        S.killCount += 1;
    }


    public static void TakeGold(int amount)
    {
        S.Score += amount;
        if (soundmanagerLoaded)
        {
            SoundManager.PlaySound("TakeGold", ref AudioSources.pickups);
        }
    }
    public static void Heal(float health)
    {
        S.currentHealth += health;
        if(S.currentHealth > S.MaxHealth)
        {
            S.currentHealth = S.MaxHealth;
        }
        S.healthBar.setHealth(S.currentHealth / S.MaxHealth);
    }

    public void Die()
    {
        isDead = true;
        currentArmor = 1;
        Time.timeScale = 0f;
        if(DeathText!=null)
           DeathText.SetActive(true);

        NumberOfLifes = NumberOfLifes - 1;
    }




    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.gameObject.tag == "Gold")
        {
            TakeGold( UnityEngine.Random.Range(100, 1000));
        }
   
    }

}
