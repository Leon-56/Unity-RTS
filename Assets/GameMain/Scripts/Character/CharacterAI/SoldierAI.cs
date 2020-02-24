namespace RTS.GameSystem.CharacterAI
{
    public class SoldierAI : ICharacterAI
    {
        public SoldierAI(ICharacter Character) : base(Character)
        {
            // 起始状态
            ChangeAIState(new IdleAIState());
        }

        // 是否可以攻击Heart
        public override bool CanAttackHeart()
        {
            return false;
        }
    }
}