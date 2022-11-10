using UnityEngine;
using UnityEngine.UI;

public class SettlementInterface : MonoBehaviour
{
    public GameObject settlement;
    public GyroscopicRotation gyro;
    public Text goins;
    
    private void OnEnable()
    {
        gyro.enabled = false;
        //goins.text = "获得金币：" + gyro.totationNumber;
    }

    public void ContinueGame()
    {
        gyro.enabled = true;
        settlement.SetActive(false);
    }

    
}
