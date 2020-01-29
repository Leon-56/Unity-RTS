namespace RTS.GameAttr
{
    public class EnemyAttrStrategy : IAttrStrategy
    {
        public override void InitAttr(ICharacterAttr CharacterAttr) {}

        public override int GetAtkPlusValue(ICharacterAttr CharacterAttr)
        {
            EnemyAttr theEnemyAttr = CharacterAttr as EnemyAttr;
            if(theEnemyAttr == null)
                return 0;

            int RandValue = UnityEngine.Random.Range(0, 100);
            if (theEnemyAttr.GetCritRate() >= RandValue)
            {
                theEnemyAttr.CutdownCritRate();
                return theEnemyAttr.GetMaxHP() * 5;
            }

            return 0;
        }

        public override int GetDmgDescValue(ICharacterAttr CharacterAttr)
        {
            return 0;
        }
    }
}