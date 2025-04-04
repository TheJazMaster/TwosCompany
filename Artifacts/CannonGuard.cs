﻿namespace TwosCompany.Artifacts {

    [ArtifactMeta(pools = new ArtifactPool[] { ArtifactPool.Common })]
    public class CannonGuard : Artifact {

        public int pos = 0;
        public bool markForGain = false;
        public override string Description() => "Whenever you end your turn in the same position you started, gain 1 <c=status>OVERDRIVE</c>.";
        public override void OnTurnStart(State state, Combat combat) {
            if (markForGain) {
                markForGain = false;
                combat.Queue(new AStatus() {
                    targetPlayer = true,
                    status = Status.overdrive,
                    statusAmount = 1,
                    dialogueSelector = ".mezz_cannonGuard",
                    artifactPulse = this.Key(),
                });
            }
            pos = state.ship.x;
        }
        public override void OnTurnEnd(State state, Combat combat) => markForGain = state.ship.x == pos;

        public override void OnCombatStart(State state, Combat combat) {
            pos = state.ship.x;
        }
		public override int? GetDisplayNumber(State s)
		{
			if (pos == s.ship.x) return null;
            return s.ship.x - pos;
		}
		
        public override List<Tooltip>? GetExtraTooltips() => new List<Tooltip>() { new TTGlossary("status.overdrive", 1) };
    }
}