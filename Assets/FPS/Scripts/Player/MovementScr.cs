using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utilites;

public class MovementScr : Singleton<MovementScr>
{
    #region Values
    public float gravity = -9.81f;
        float groundDistance = .4f, currentHeights,currentSpeed;

    Vector3 velocity;

    private KeyCode contrKey = KeyCode.LeftControl, buffActivator = KeyCode.V, shiftKey = KeyCode.LeftShift;

    public Transform groundCheck;
    public LayerMask groundMask;

   private CharacterController contFPPlayer;
    #region RangeVars
    [Range(0f,5f)]
    public float shiftHieghts = 2.5f;
    [Range(0f, 20f)]
    public float  speedForce = 10;
    [Range(0f, 10f)]
    public float jumpHeight = 2f;
    [Range(0, 100)]
    public int Health;
    [Range(0f, 30f)]
    public float buffsTime;
    #endregion
    public int currentHealth;

    bool isGrounded, isDoubleJump = false, isResist = false;

    byte buffNumber;

    public GameObject deathImage;
    #endregion
    void Start()
    {
        currentHealth = Health;
        currentSpeed = speedForce;

        contFPPlayer = GetComponent<CharacterController>();
        currentHeights = contFPPlayer.height;
        HealthManager.instance.SetMaxValue(currentHealth);

     
    }
    #region InputGetter
    void Update()
    {
       #region GroundTracker
        isGrounded = Physics.CheckSphere(groundCheck.position , groundDistance, groundMask);
        if(isGrounded && velocity.y < 0)
        {
            isDoubleJump = false;
            velocity.y = -2f;
        }
       #endregion

        Move();
        
        #region SquadInput   
        if (Input.GetKey(contrKey))
        {
            SquadSetter(shiftHieghts);
        }
        else if (!Input.GetKey(contrKey))
        {
            SquadSetter(currentHeights);
        }
        #endregion

        #region JumpInput
        if (Input.GetButton("Jump") && isGrounded)
        {         
            Jump();
        }
         else if (!isDoubleJump && Input.GetButtonDown("Jump") && !isGrounded)
        {
            Jump();
            isDoubleJump = true;
        }
        #endregion

        #region HealthTracker
        if (currentHealth <= 40)
        {
            deathImage.SetActive(true);
        }
        else
        {
            deathImage.SetActive(false);
        }
        #endregion

        #region buffTracker    
        if (Input.GetKeyDown(buffActivator))
        {
           switch (buffNumber)
            {
                case 1:
                StartCoroutine(JumpAdder());
                    break;
                case 2:
                    StartCoroutine(ResistGiver());
                    break;
                case 3:
                    StartCoroutine(DamageAdder());
                    break;
            }
        }
        #endregion

        #region SpeedUpInput
        if (Input.GetKey(shiftKey) && currentSpeed <= 20)
        {         
            SpeedUp(currentSpeed += 2);
        }else if (!Input.GetKey(shiftKey))
        {
            SpeedUp(speedForce);
        }
        #endregion

    }
    #endregion
    #region Movement
    void Move()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");

        Vector3 moveVector = transform.right * x + transform.forward * y;

        contFPPlayer.Move(moveVector * currentSpeed * Time.deltaTime);

        velocity.y += gravity * Time.deltaTime;
        contFPPlayer.Move(velocity * Time.deltaTime);

    }
    void SquadSetter(float heights)
    {
       contFPPlayer.height = heights;
    }
    void Jump()
    {
        velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
    }
    void SpeedUp(float speed)
    {
        currentSpeed = speed;
    }
    #endregion

    #region HealthManagement
   public void TakeDamage(int Damage)
    {
        if (isResist)
            return;

        currentHealth -= Damage;
        deathImage.SetActive(true);
        HealthManager.instance.SetValue(currentHealth);
        if(currentHealth < 80)
        {
            StartCoroutine(Regeneration());
        }
        else
        {
            StopCoroutine(Regeneration());
        }
    }
   IEnumerator Regeneration()
    {
        yield return new WaitForSeconds(10f);
            currentHealth += 10;
            HealthManager.instance.SetValue(currentHealth);     
    }
    #endregion

    #region Buffs
   IEnumerator JumpAdder()
    {
        jumpHeight *= 2f;
            yield return new WaitForSeconds(buffsTime);
        jumpHeight /= 2f;
        buffNumber = 0;
    }
    IEnumerator ResistGiver()
    {
        print("p");
        isResist = true;
        yield return new WaitForSeconds(buffsTime);
        isResist = false;
        buffNumber = 0;
    }
    IEnumerator DamageAdder()
    {
        float damage =   GunScr.Instance.damage;
        GunScr.Instance.damage += 50;
        yield return new WaitForSeconds(buffsTime);
        GunScr.Instance.damage = damage;
        buffNumber = 0;
    }
    #endregion
    void OnTriggerEnter(Collider other)
    {
      switch (other.gameObject.tag)
        {
            case "ammoAdd":
                GunScr.instance.currentAmmo += 10;
                break;
            case "jumpBuffAdder":
                buffNumber = 1;
                break;
            case "resistBuffAdder":
                buffNumber = 2;
                break;
            case "damageBuffAdder":
                buffNumber = 3;
                break;
        }
    }
}
