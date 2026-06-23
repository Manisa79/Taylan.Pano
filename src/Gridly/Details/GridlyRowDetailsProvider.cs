namespace Gridly.Details;

public sealed class GridlyRowDetailsProvider
{
    public Func<object, Control>? CreateDetailsControl { get; set; }
    public int PreferredHeight { get; set; } = 90;
}
