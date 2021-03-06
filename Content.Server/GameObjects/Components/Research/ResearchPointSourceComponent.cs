﻿using Content.Server.GameObjects.Components.Power.ApcNetComponents;
using Content.Shared.Interfaces.GameObjects.Components;
using Robust.Shared.GameObjects;
using Robust.Shared.Prototypes;
using Robust.Shared.Serialization;
using Robust.Shared.Serialization.Manager.Attributes;
using Robust.Shared.ViewVariables;

namespace Content.Server.GameObjects.Components.Research
{
    [RegisterComponent]
    [ComponentReference(typeof(IActivate))]
    public class ResearchPointSourceComponent : ResearchClientComponent
    {
        public override string Name => "ResearchPointSource";

        [DataField("pointspersecond")]
        private int _pointsPerSecond;
        [DataField("active")]
        private bool _active;
        private PowerReceiverComponent _powerReceiver;

        [ViewVariables(VVAccess.ReadWrite)]
        public int PointsPerSecond
        {
            get => _pointsPerSecond;
            set => _pointsPerSecond = value;
        }

        [ViewVariables(VVAccess.ReadWrite)]
        public bool Active
        {
            get => _active;
            set => _active = value;
        }

        /// <summary>
        /// Whether this can be used to produce research points.
        /// </summary>
        /// <remarks>If no <see cref="PowerReceiverComponent"/> is found, it's assumed power is not required.</remarks>
        [ViewVariables]
        public bool CanProduce => Active && (_powerReceiver is null || _powerReceiver.Powered);

        public override void Initialize()
        {
            base.Initialize();
            Owner.TryGetComponent(out _powerReceiver);
        }
    }
}
