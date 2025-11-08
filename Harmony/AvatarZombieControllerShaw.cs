using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Z_Shaw
{
    public class ItemActionVomitOverride : ItemActionLauncher
    {
        public override ItemActionData CreateModifierData(ItemInventoryData _invData, int _indexInEntityOfAction)
        {
            return new ItemActionVomit.ItemActionDataVomit(_invData, _indexInEntityOfAction);
        }

        public override void ReadFrom(DynamicProperties _props)
        {
            base.ReadFrom(_props);
            _props.ParseInt("AnimType", ref this.animType);
            this.warningDelay = 1.2f;
            _props.ParseFloat("WarningDelay", ref this.warningDelay);
            this.warningMax = 3;
            _props.ParseInt("WarningMax", ref this.warningMax);
            _props.ParseString("Sound_warning", ref this.soundWarning);
        }

        private void resetAttack(ItemActionVomit.ItemActionDataVomit _actionData)
        {
            _actionData.numWarningsPlayed = 0;
            _actionData.warningTime = 0f;
            _actionData.bAttackStarted = false;
            _actionData.isDone = false;
        }

        public override void ItemActionEffects(GameManager _gameManager, ItemActionData _actionData, int _firingState, Vector3 _startPos, Vector3 _direction, int _userData = 0)
        {
            ItemActionVomit.ItemActionDataVomit itemActionDataVomit = (ItemActionVomit.ItemActionDataVomit)_actionData;
            if (itemActionDataVomit.muzzle == null)
            {
                itemActionDataVomit.muzzle = _actionData.invData.holdingEntity.emodel.GetRightHandTransform();
            }
            if (_firingState != 0)
            {
                itemActionDataVomit.numVomits++;
                Vector3 direction = itemActionDataVomit.invData.holdingEntity.GetLookRay().direction;
                int burstCount = this.GetBurstCount(_actionData);
                for (int i = 0; i < burstCount; i++)
                {
                    Vector3 directionRandomOffset = this.getDirectionRandomOffset(itemActionDataVomit, direction);
                    base.instantiateProjectile(_actionData, default(Vector3)).GetComponent<ProjectileMoveScript>().Fire(_startPos, directionRandomOffset, _actionData.invData.holdingEntity, this.hitmaskOverride, 0.2f);
                }
            }
            base.ItemActionEffects(_gameManager, _actionData, _firingState, _startPos, _direction, _userData);
        }

        public override void ExecuteAction(ItemActionData _actionData, bool _bReleased)
        {
            //Log.Out("Starting of the log");
            ItemActionVomit.ItemActionDataVomit itemActionDataVomit = (ItemActionVomit.ItemActionDataVomit)_actionData;
            if (_bReleased)
            {
                //Log.Out("Option 1 chosen");
                base.ExecuteAction(_actionData, _bReleased);
                this.resetAttack(itemActionDataVomit);
                return;
            }
            float time = Time.time;
            if (time - itemActionDataVomit.m_LastShotTime < this.Delay)
            {
                //Log.Out("Option 2 chosen");
                return;
            }
            if (itemActionDataVomit.warningTime > 0f && time < itemActionDataVomit.warningTime)
            {
                //Log.Out("Option 3 chosen");
                base.ExecuteAction(_actionData, _bReleased);
                this.resetAttack(itemActionDataVomit);
                return;
            }
            if (!itemActionDataVomit.bAttackStarted)
            {
                //Log.Out("Option 4 chosen");
                EntityAlive holdingEntity = _actionData.invData.holdingEntity;
                if (itemActionDataVomit.numWarningsPlayed < this.warningMax - 1 && holdingEntity.rand.RandomFloat < 0.5f)
                {
                    //Log.Out("Option 4A chosen");
                    /*//itemActionDataVomit.numWarningsPlayed++;
                    //itemActionDataVomit.warningTime = time + this.warningDelay;
                    //holdingEntity.PlayOneShot(this.soundWarning, false);
                    //holdingEntity.Raging = true;*/
                    base.ExecuteAction(_actionData, _bReleased);
                    this.resetAttack(itemActionDataVomit);
                    return;
                }
                itemActionDataVomit.bAttackStarted = true;
                itemActionDataVomit.numVomits = 0;
                holdingEntity.StartSpecialAttack(this.animType);
                if (this.warningMax > 0)
                {
                    //Log.Out("Option 4B chosen");
                    holdingEntity.PlayOneShot(this.soundWarning, false);
                    itemActionDataVomit.warningTime = time + this.warningDelay;
                    return;
                }
            }
            if (itemActionDataVomit.numVomits >= this.GetMaxAmmoCount(itemActionDataVomit))
            {
                //Log.Out("Option 5 chosen");
                itemActionDataVomit.isDone = true;
                return;
            }
            itemActionDataVomit.curBurstCount = 0;
            base.ExecuteAction(_actionData, _bReleased);
        }

        private const float cRadius = 0.2f;
        private int animType;
        private float warningDelay;
        private int warningMax;
        private string soundWarning;

        public class ItemActionDataVomit : ItemActionLauncher.ItemActionDataLauncher
        {
            public ItemActionDataVomit(ItemInventoryData _invData, int _indexInEntityOfAction) : base(_invData, _indexInEntityOfAction)
            {
            }

            public float warningTime;
            public int numWarningsPlayed;
            public int numVomits;
            public bool bAttackStarted;
            public bool isDone;
        }
    }
}