using MEC;
using System;
using Exiled.API.Enums;
using Exiled.API.Features;
using System.Collections.Generic;
using Interactables.Interobjects.DoorUtils;
using ServerHandler = Exiled.Events.Handlers.Server;

namespace ForcedMeasure
{
    public class ForcedMeasure : Plugin<Config>
    {
        private static ForcedMeasure singleton = new ForcedMeasure();
        public static ForcedMeasure Instance => singleton;
        public override PluginPriority Priority { get; } = PluginPriority.Medium;

        public override Version RequiredExiledVersion { get; } = new Version(2, 10, 0);
        public override Version Version { get; } = new Version(1, 0, 0);

        private Handlers.RoundStart roundStart;

        public List<CoroutineHandle> Coroutines = new List<CoroutineHandle>();
        public List<DoorVariant> LockedDoorTracker = new List<DoorVariant>();

        private ForcedMeasure()
        {
        }

        //Run startup code when plugin is enabled
        public override void OnEnabled()
        {
            RegisterEvents();
        }

        //Run shutdown code when plugin is disabled
        public override void OnDisabled()
        {
            foreach (var coroutine in Coroutines)
            {
                Timing.KillCoroutines(coroutine);
            }

            foreach (var door in LockedDoorTracker)
            {
                door.ActiveLocks -= 1;
            }

            LockedDoorTracker.Clear();
            UnregisterEvents();
        }

        //Plugin startup code
        public void RegisterEvents()
        {
            roundStart = new Handlers.RoundStart();

            ServerHandler.RoundStarted += roundStart.OnRoundStart;
        }

        //Plugin shutdown code
        public void UnregisterEvents()
        {
            ServerHandler.RoundStarted -= roundStart.OnRoundStart;

            roundStart = null;
        }
    }
}