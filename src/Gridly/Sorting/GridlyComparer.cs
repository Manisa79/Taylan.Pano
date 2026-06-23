namespace Gridly.Sorting;
public sealed class GridlyComparer : IComparer<object?>
{
    public int Compare(object? x, object? y)
    {
        if (x is IComparable cx) return cx.CompareTo(y);
        return string.Compare(Convert.ToString(x), Convert.ToString(y), StringComparison.CurrentCultureIgnoreCase);
    }
}
