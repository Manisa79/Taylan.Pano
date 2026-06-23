using Gridly.Columns;

namespace Gridly.Undo;

public sealed class GridlyCellChange
{
    public object RowObject { get; init; } = null!;
    public GridlyColumn Column { get; init; } = null!;
    public object? OldValue { get; init; }
    public object? NewValue { get; init; }
}

public sealed class GridlyUndoService
{
    private readonly Stack<GridlyCellChange> _undo = new();
    private readonly Stack<GridlyCellChange> _redo = new();
    public bool CanUndo => _undo.Count > 0;
    public bool CanRedo => _redo.Count > 0;
    public void Push(GridlyCellChange change) { _undo.Push(change); _redo.Clear(); }
    public GridlyCellChange? Undo() { if (_undo.Count == 0) return null; var c = _undo.Pop(); c.Column.PutValue(c.RowObject, c.OldValue); _redo.Push(c); return c; }
    public GridlyCellChange? Redo() { if (_redo.Count == 0) return null; var c = _redo.Pop(); c.Column.PutValue(c.RowObject, c.NewValue); _undo.Push(c); return c; }
    public void Clear() { _undo.Clear(); _redo.Clear(); }
}
