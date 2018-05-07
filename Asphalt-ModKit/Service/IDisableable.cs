namespace Asphalt.Service
{
    public interface IDisableable
    {
        void SetDisabled(bool disabled);
        bool IsDisabled();
    }
}
