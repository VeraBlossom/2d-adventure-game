using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHealth : Singleton<PlayerHealth>
{
    public bool isDead { get; private set; }

    [SerializeField] private int maxHealth = 3;
    [SerializeField] private float knockBackThrustAmount = 10f;
    [SerializeField] private float damageRecoveryTime = 1f;

    private Slider healthSlider;
    private int currentHealth;
    private bool canTakeDamage = true;
    private Knockback knockback;
    private Flash flash;

    const string HEALTH_SLIDER_TEXT = "Health Slider";

    const string TOWN_TEXT = "Scene1";
    readonly int DEATH_HASH = Animator.StringToHash("Death");

    protected override void Awake()
    {
        base.Awake();

        flash = GetComponent<Flash>();
        knockback = GetComponent<Knockback>();
    }
    private void Start()
    {
        isDead = false;
        currentHealth = maxHealth;

        UpdateHealthSlider();
    }
    private void OnCollisionStay2D(Collision2D other)
    {
        EnemyAI enemy = other.gameObject.GetComponent<EnemyAI>();

        if(enemy)
        {
            TakeDamage(1, other.transform);
        }
    }

    public void HealthPlayer()
    {
        if (currentHealth < maxHealth)
        {
            currentHealth++;
            UpdateHealthSlider();
        }
    }

    public void TakeDamage(int damageAmount, Transform hitTrasform)
    {
        if(!canTakeDamage) return;

        ScreenShakeManager.Instance.ShakeScreen();

        AudioManager.instance.PLaySound(AudioManager.SoundFx.hit);

        knockback.GetKnockedBack(hitTrasform, knockBackThrustAmount);
        StartCoroutine(flash.FlashRoutine());

        canTakeDamage = false;
        currentHealth -= damageAmount;
        StartCoroutine(DamageRecoveryRoutine());

        UpdateHealthSlider();
        CheckIfPlayerDeath();
    }
    private void CheckIfPlayerDeath()
    {
        if (currentHealth <= 0 && !isDead)
        {
            isDead = true;
            Destroy(ActiveWeapon.Instance.gameObject);

            currentHealth = 0;
            
            GetComponent<Animator>().SetTrigger(DEATH_HASH);
            StartCoroutine(DeathLoadSceneRoutine());
        }
    }

    private IEnumerator DeathLoadSceneRoutine()
    {
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
        SceneManager.LoadScene(TOWN_TEXT);
    }

    private IEnumerator DamageRecoveryRoutine()
    {
        yield return new WaitForSeconds(damageRecoveryTime);
        canTakeDamage = true;
    }

    private void UpdateHealthSlider()
    {
        if (healthSlider == null)
        {
            healthSlider = GameObject.Find(HEALTH_SLIDER_TEXT).GetComponent<Slider>();
        }
        healthSlider.maxValue = maxHealth;
        healthSlider.value = currentHealth;
    }
}
