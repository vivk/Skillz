using Features.Health.Statuses;

namespace Features.Health.Core
{
    public interface IHitable
    {
        void Hit(HitableType type, int damage);
        void SetStatus(StatusType type, byte level);
    }
}
