using Gridly.Virtualization;

namespace Gridly.Core;

public partial class GridlyView
{
    public void SetRangeVirtualProvider(IGridlyRangeRowProvider provider)
    {
        SetVirtualProvider(provider ?? throw new ArgumentNullException(nameof(provider)));
    }
}
