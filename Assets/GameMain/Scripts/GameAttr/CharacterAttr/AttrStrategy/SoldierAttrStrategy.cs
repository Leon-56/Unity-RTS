namespace RTS.GameAttr
{
    public class SoldierAttrStrategy : IAttrStrategy
    {
        public override void InitAttr(ICharacterAttr CharacterAttr)
        {
            SoldierAttr theSoldierAttr = CharacterAttr as SoldierAttr;
            if(theSoldierAttr == null)
                return;

            int AddMaxHP = 0;
            int Lv = theSoldierAttr.GetSoldierLv();
            if (Lv > 0)
                AddMaxHP = (Lv - 1) * 2;
            
            theSoldierAttr.AddMaxHP(AddMaxHP);
        }

        public override int GetAtkPlusValue(ICharacterAttr CharacterAttr)
        {
            return 0;
        }

        public override int GetDmgDescValue(ICharacterAttr CharacterAttr)
        {
            SoldierAttr theSoldierAttr = CharacterAttr as SoldierAttr;
            if(theSoldierAttr == null)
                return 0;

            return (theSoldierAttr.GetSoldierLv() - 1) * 2;
        }
    }
}