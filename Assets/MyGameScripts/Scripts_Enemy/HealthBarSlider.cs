using UnityEngine;
using UnityEngine.UI;
public class HealthBarSlider : MonoBehaviour
{
    public Slider EnemyHPbar;
    public Color Low;
    public Color High;

    Vector3 offfset = new Vector3(0, -0.8f, 0);
    public void SetHealth(float health, float maxHealth) {
        EnemyHPbar.gameObject.SetActive(health < maxHealth);

        EnemyHPbar.value = health;
        EnemyHPbar.maxValue = maxHealth;

        EnemyHPbar.fillRect.GetComponentInChildren<Image>().color = Color.Lerp(Low, High, EnemyHPbar.normalizedValue);
    }
    void Start()
    {
    }
    void Update()
    {
        EnemyHPbar.transform.position = Camera.main.WorldToScreenPoint(transform.parent.position + offfset);
    }
}
