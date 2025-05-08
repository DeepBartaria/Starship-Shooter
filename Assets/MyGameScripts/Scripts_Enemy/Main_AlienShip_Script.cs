using UnityEngine;

public class Main_AlienShip_Script : All_Objects_Behaviour
{
    public GameObject main_AlienShip_Prefab;
    public static bool Main_AlienShip_destroyed = false;
    public AudioClip arrivalSound;
    private AudioSource audioSource;
    public AudioClip Nahngom_Sound;
    private AudioSource audioSource1;

    public GameObject MAbullet1Prefab;
    public GameObject MAbullet2Prefab;
    public GameObject MAbullet3Prefab;

    public HealthBarSlider HealthBarSlider;
    void Start()
    {
        Hitpoints = 100;
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = arrivalSound;
        audioSource.playOnAwake = false;

        audioSource1 = gameObject.AddComponent<AudioSource>();

        audioSource1.clip = Nahngom_Sound;
        audioSource1.playOnAwake = false;

        Invoke("Respawn", 60f);

        HealthBarSlider.SetHealth(Hitpoints, 100f);

    }
    void Update()
    {
        Move();
        Attack();
    }

    protected override void Move()
    {
        max_speed = 0.2f;
        transform.Translate(Vector2.up * max_speed * Time.deltaTime);
    }

    float coolDown = 0;
    float delayBullet = 1;
    protected override void Attack()
    {
        Vector3 offset1 = new Vector3(0, -2.4f, 0);
        Vector3 offset2 = new Vector3(-1.6f, -1, 0);
        Vector3 offset3 = new Vector3(1.6f, -1, 0);
        coolDown -= Time.deltaTime;
        if (coolDown < 0) {
            coolDown = delayBullet;

            Instantiate(MAbullet1Prefab, transform.position + offset1, transform.rotation);
            Instantiate(MAbullet2Prefab, transform.position + offset2, transform.rotation);
            Instantiate(MAbullet3Prefab, transform.position + offset3, transform.rotation);
        }
    }

    protected override void Respawn()
    {
        Vector3 spawnPosition = new Vector3(0, 6f, -1f);
        Instantiate(main_AlienShip_Prefab, spawnPosition, transform.rotation);
        if (audioSource != null && arrivalSound != null) {
            StartCoroutine(PlaySoundTwice());
        }
    }

    private System.Collections.IEnumerator PlaySoundTwice()
    {
        audioSource.Play();
        yield return new WaitForSeconds(arrivalSound.length);
        audioSource.Play();
        audioSource1.Play();
    }

    protected override void Hitpoints_Handler()
    {

    }

    public GameMenuController GameMenuController;
    private void OnTriggerEnter2D()
    {
        Hitpoints--;
        HealthBarSlider.SetHealth(Hitpoints, 100f);
        if (Hitpoints <= 0) {
            Main_AlienShip_destroyed = true;
            Destroy(gameObject);
            GameMenuController.onMainAlienShipDestroyed();
        }
    }

}