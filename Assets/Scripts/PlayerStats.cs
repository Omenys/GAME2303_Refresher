using System.Collections;
using UnityEngine;
public class PlayerStats : MonoBehaviour
{
    [SerializeField] Stat maxHealth;
    [SerializeField] Stat currentHealth;
    [SerializeField] Animator animator;

    float timeToDecrease = 0.5f;
    int healthToDecrease = 5;


    // Reference to coroutine
    private Coroutine drainHealthCoroutine;

    // Start is called before the first frame update
    void Start()
    {

        currentHealth.amount = maxHealth.amount;
    }

    private void Update()
    {
        /* if (decreaseHealth)
         {
             timeToDecrease -= Time.deltaTime;
             if (timeToDecrease <= 0)
             {
                 currentHealth.amount -= 1;
                 timeToDecrease = 2.0f;
             }
         }*/
    }

    public void StartDrainHealth()
    {
        drainHealthCoroutine = StartCoroutine(DrainHealth());
    }

    public void StopDrainHealth()
    {
        StopCoroutine(drainHealthCoroutine);
    }

    // Drain health from player
    IEnumerator DrainHealth()
    {
        while (currentHealth.amount > 0)
        {
            currentHealth.amount -= healthToDecrease;

            // Prevent negative values
            currentHealth.amount = Mathf.Max(currentHealth.amount, 0);

            if (currentHealth.amount <= 0)
            {
                animator.SetBool("Death", true);
                yield break;
            }

            yield return new WaitForSeconds(timeToDecrease);
        }
    }
}
