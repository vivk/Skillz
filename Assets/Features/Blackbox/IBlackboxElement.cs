namespace Features.Blackbox
{
    public interface IBlackboxElement
    {
        public int ID { get; }
        
        public bool EqualType<T>() where T : IBlackboxElement;
    }
}
