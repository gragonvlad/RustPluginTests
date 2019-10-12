using UnityEngine;
using Oxide.Core.Libraries.Covalence;

namespace Oxide.Plugins
{
    [Info("CyberTest", "Quapi", "0.0.2")]
    class CyberTest : CovalencePlugin
    {
        [Command("cyber")] 
        void cmdCyberTest(IPlayer sender)
        {
            BasePlayer basePlayer = BasePlayer.FindByID(ulong.Parse(sender.Id));
            sender.Reply("1");
            BaseEntity entity = GetEntityLooking(basePlayer);
            sender.Reply("2");
            Interact(basePlayer, entity);
            sender.Reply("3");
        }

        void Interact(BasePlayer player, BaseEntity entity)
        {
            if(entity)
            {
                if(entity is Door)
                {
                    DoorUnlocking(entity as Door, player);
                    return;
                }
                player.ChatMessage("That is not a door.");
            }
        }

        void DoorUnlocking(Door door, BasePlayer player)
        {
            if(door != null)
            {
                var lockSlot = door.GetSlot(BaseEntity.Slot.Lock);
                if(lockSlot is CodeLock)
                {
                    door.SetOpen(true);
                    player.ChatMessage("You have unlocked the door.");
                }
            }
        }

        private BaseEntity GetEntityLooking(BasePlayer player)
        {
            RaycastHit hit;
            if(Physics.Raycast(player.eyes.HeadRay(), out hit, 100))
            {
                return hit.GetEntity();
            }
            return null;
        }
    }
}
