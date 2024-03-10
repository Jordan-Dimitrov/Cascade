namespace Application.Shared.Abstractions
{
    public abstract record IntegragtionEvent
    {
        protected IntegragtionEvent()
        {
            Id = Guid.NewGuid();
            OccuredOnUtc = DateTime.UtcNow;
        }
        public Guid Id { get; protected init; }
        public DateTime OccuredOnUtc { get; protected init; }
    }
}
