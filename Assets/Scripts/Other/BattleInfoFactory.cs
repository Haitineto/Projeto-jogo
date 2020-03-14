using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class BattleInfoFactory
{
    public static BattleInfo CreateBattleInfo(int battleId)
    {
        return new BattleInfo()
        {
            Id = battleId,
            BattleTime = 120,
            BattleMusic = null, //CustomResources.Load<AudioClip>("Sounds/Music/ThemeSong")
            BossInfo = new BossInfo()
            {
                Type = BattleBossType.Mystic,
                MaxPower = battleId * 2 > 49 ? 49 : battleId * 2,
                Name = "Water Wizard",
                AttributePoints = DistributeAttributePoints(battleId),
                Health = 100 * (1 + (battleId / 10))
            },
            Reward = new BattleReward() { CoinsToEarn = 100 * battleId, XPToEarn = 100 * battleId, HasSurprise = false }
        };
    }

    private static AttributePoints DistributeAttributePoints(int points)
    {
        var attributePoints = new AttributePoints
        {
            Air = 1, Earth = 1, Fire = 1, Health = 1, Light = 1, Water = 1
        };

        var pointsToDistribute = points;
        var enumAttributes = Enum.GetValues(typeof(AttributeType));
        var pointsPerAttribute = (int)Mathf.Ceil(points / enumAttributes.Length);
        pointsPerAttribute = pointsPerAttribute > 0 ? pointsPerAttribute : 1;        
        while (pointsToDistribute > 0)
        {
            var indexAttributeType = Random.Range(0, 5);
            var nameAttribute = enumAttributes.GetValue(indexAttributeType).ToString();
            var element = attributePoints.GetType().GetField(nameAttribute);
            if (element == null)
            {
                throw new Exception(string.Format("Não foi possivel localizar o elemento de nome {0} para atribuir os pontos do BOSS.", nameAttribute));
            }
            element.SetValue(attributePoints, pointsPerAttribute);
            pointsToDistribute -= pointsPerAttribute;
        }

        return attributePoints;
    }
}
