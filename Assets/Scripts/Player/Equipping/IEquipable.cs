public interface IEquipable : IItem
{
    public string HudInformation { get; }
    public void Use();
}