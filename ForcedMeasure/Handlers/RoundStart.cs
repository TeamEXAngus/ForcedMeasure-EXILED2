using MEC;
using System.Linq;
using Exiled.API.Features;
using Exiled.API.Extensions;

namespace ForcedMeasure.Handlers
{
    internal class RoundStart
    {
        public void OnRoundStart()
        {
            foreach (var pair in ForcedMeasure.Instance.Config.DoorsVsTimes)
            {
                //Gets all existing doors of the current DoorType
                var theseDoors = Map.Doors.Where(door => door.Type() == pair.Key);

                foreach (var door in theseDoors)
                {
                    door.ActiveLocks += 1; //Lock the door
                    ForcedMeasure.Instance.LockedDoorTracker.Add(door); //Start tracking the door

                    ForcedMeasure.Instance.Coroutines.Add(
                    Timing.CallDelayed(pair.Value, () => //Await the configured time then unlock the door
                    {
                        door.ActiveLocks -= 1; //Unlock the door
                        ForcedMeasure.Instance.LockedDoorTracker.Remove(door); //Stop tracking the door
                    }));
                }
            }
        }
    }
}