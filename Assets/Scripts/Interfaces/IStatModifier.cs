using Data;

namespace Interfaces
{
    public interface IStatModifier
    {
        void Apply(CharacterAttributes attributes);
        void Remove(CharacterAttributes attributes);
        
        string Description { get; }
    }
}