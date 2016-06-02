using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Descriptors;

public class TurnOrderController : MonoBehaviour
{
    const int turnActivation = 1000;
    const int turnCost = 500;
    const int moveCost = 300;
    const int actionCost = 200;

    public const string RoundBeganNotification = "TurnOrderController.roundBegan";
    public const string TurnCheckNotification = "TurnOrderController.turnCheck";
    public const string TurnCompletedNotification = "TurnOrderController.turnCompleted";
    public const string RoundEndedNotification = "TurnOrderController.roundEnded";

    public CreatureDescriptor cStats;

    #region Public
    public IEnumerator Round()
    {
        BattleController bc = GetComponent<BattleController>(); ;
        while (true)
        {
            //this.PostNotification(RoundBeganNotification);

            List<Creature> creaturesJ1 = new List<Creature>(bc.creaturesJ1);
            List<Creature> creaturesJ2 = new List<Creature>(bc.creaturesJ2);


            for(int i=0; i<creaturesJ1.Count; ++i)
            {
                cStats = creaturesJ1[i].GetComponent<CreatureDescriptor>();
                if(cStats.HP.CurrentValue != 0)
                {
                    // TODO : Actions du J1
                }

            }

            for (int i = 0; i < creaturesJ2.Count; ++i)
            {
                cStats = creaturesJ2[i].GetComponent<CreatureDescriptor>();
                if (cStats.HP.CurrentValue != 0)
                {
                    // TODO : Actions du J2
                }
            }

        }
    }
    #endregion
}