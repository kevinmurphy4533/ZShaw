using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Z_Shaw.Harmony
{
   /* public override void ExecuteAction(ItemActionData _actionData, bool _bReleased)
    {
        ItemActionDataVomit itemActionDataVomit = (ItemActionDataVomit)_actionData;
        if (_bReleased)
        {
            base.ExecuteAction(_actionData, _bReleased);
            resetAttack(itemActionDataVomit);
            return;
        }
        float time = Time.time;
        if (time - itemActionDataVomit.m_LastShotTime < Delay || (itemActionDataVomit.warningTime > 0f && time < itemActionDataVomit.warningTime))
        {
            return;
        }
        if (!itemActionDataVomit.bAttackStarted)
        {
            EntityAlive holdingEntity = _actionData.invData.holdingEntity;
            if (itemActionDataVomit.numWarningsPlayed < warningMax - 1 && holdingEntity.rand.RandomFloat < 0.5f)
            {
                itemActionDataVomit.numWarningsPlayed++;
                itemActionDataVomit.warningTime = time + warningDelay;
                holdingEntity.PlayOneShot(soundWarning);
                holdingEntity.Raging = true;
                return;
            }
            itemActionDataVomit.bAttackStarted = true;
            itemActionDataVomit.numVomits = 0;
            holdingEntity.StartSpecialAttack(animType);
            if (warningMax > 0)
            {
                holdingEntity.PlayOneShot(soundWarning);
                itemActionDataVomit.warningTime = time + warningDelay;
                return;
            }
        }
        if (itemActionDataVomit.numVomits >= GetMaxAmmoCount(itemActionDataVomit))
        {
            itemActionDataVomit.isDone = true;
            return;
        }
        itemActionDataVomit.curBurstCount = 0;
        base.ExecuteAction(_actionData, _bReleased);
    }*/
}
