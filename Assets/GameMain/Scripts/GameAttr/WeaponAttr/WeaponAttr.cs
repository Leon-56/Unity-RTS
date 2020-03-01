namespace RTS.GameAttr
{
    public class WeaponAttr
    {
        public int Atk { get; private set; } // 攻击力
        public float AtkRange { get; private set; } // 攻击距离
        public string AttrName { get; private set; } // 属性名称

        public WeaponAttr(int Atk, float AtkRange, string AttrName)
        {
            this.Atk = Atk;
            this.AtkRange = AtkRange;
            this.AttrName = AttrName;
        }
    }
}