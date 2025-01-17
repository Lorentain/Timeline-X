using UnityEngine;

public class PowerUp : MonoBehaviour
{
    [SerializeField] private int ID;

    public void Execute()
    {
        switch (ID)
        {
            case 1:
                {
                    PowerUp1();
                    break;
                }
            case 2:
                {
                    PowerUp2();
                    break;
                }
            case 3:
                {
                    PowerUp3();
                    break;
                }
        }
    }

    private void PowerUp1()
    {
        Debug.Log("Power Up 1 funcionando");
    }
    private void PowerUp2()
    {
        Debug.Log("Power Up 2 funcionando");
    }
    private void PowerUp3()
    {
        Debug.Log("Power Up 3 funcionando");
    }
}
