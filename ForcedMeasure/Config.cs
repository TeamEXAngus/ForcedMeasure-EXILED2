using Exiled.API.Enums;
using System.ComponentModel;
using Exiled.API.Interfaces;
using System.Collections.Generic;

namespace ForcedMeasure
{
    public sealed class Config : IConfig
    {
        [Description("Whether or not the plugin should be enabled on this server.")]
        public bool IsEnabled { get; set; } = true;

        [Description("A list of doors and how long they should be locked for.")]
        public Dictionary<DoorType, float> DoorsVsTimes { get; set; } = new Dictionary<DoorType, float>
        {
            {DoorType.GateA, 60f },
            {DoorType.GateB, 60f },
            {DoorType.CheckpointEntrance, 20f },
            {DoorType.CheckpointLczA, 40f },
            {DoorType.CheckpointLczB, 40f }
        };
    }
}