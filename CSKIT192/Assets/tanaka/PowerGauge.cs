using UnityEngine;

using UnityEngine.UI;

public class PowerGauge : MonoBehaviour

{

    public Image powerBarFill;

    public float maxPower = 30f;

    public float changeSpeed = 30f;

    public float Power { get; private set; }

    private bool locked = false;

    private float direction = 1f;
   void Start()
    {
        ResetGauge(); 
    }
    void Update()

    {

        if (locked) return;

        Power += direction * changeSpeed * Time.deltaTime;

        if (Power >= maxPower)

        {

            Power = maxPower;

            direction = -1f;

        }

        else if (Power <= 0f)

        {

            Power = 0f;

            direction = 1f;

        }

        if (powerBarFill != null)

        {

            powerBarFill.fillAmount = Power / maxPower;

        }

    }

    public float LockPower()

    {

        locked = true;

        return Power;

    }
    public void ResetGauge()

    {

        Power = 0f;

        direction = 1f;

        locked = false;

        if (powerBarFill != null)

        {

            powerBarFill.fillAmount = 0f;

        }

    }

}
