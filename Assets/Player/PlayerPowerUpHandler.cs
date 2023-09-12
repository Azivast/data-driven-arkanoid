using UnityEngine;

[RequireComponent(typeof(PlayerBehaviour))]
[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(AudioSource))]
public class PlayerPowerUpHandler : MonoBehaviour {
    [Tooltip("Currently active power-up")]
    public GameObject ActivePowerup;

    [Tooltip("Duration of camera shake.")] [SerializeField]
    private float camShakeDuration = 0.2f;

    [Tooltip("Intensity of camera shake.")] [SerializeField]
    private float camShakeIntensity = 0.3f;

    [Tooltip("Sound played when picking up a power up.")] [SerializeField]
    private AudioClip PickUpSound;
    
    private AudioSource audioSource;

    private void Start() {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter2D(Collision2D col) {
        if (col.gameObject.CompareTag("powerupCapsule")) {
            audioSource.PlayOneShot(PickUpSound);

            if (ActivePowerup is not null) Destroy(ActivePowerup);

            ActivePowerup = Instantiate(col.gameObject.GetComponent<PowerupCapsule>().Effect, transform);
            ActivePowerup.SetActive(true);
            CameraShake.Shake(camShakeDuration, camShakeIntensity);

            Destroy(col.gameObject);
        }
    }
}