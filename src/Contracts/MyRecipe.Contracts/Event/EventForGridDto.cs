namespace MyRecipe.Contracts.Event;

public class EventForGridDto
{
    /// <summary>
    /// Идентификатор события.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Наименование события.
    /// </summary>
    public string Name { get; set; }
}