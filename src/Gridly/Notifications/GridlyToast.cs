using Gridly.Theming;

namespace Gridly.Notifications;

public enum GridlyToastKind
{
    Info,
    Success,
    Warning,
    Error
}

/// <summary>
/// Lightweight themed toast notification used by Gridly samples/host apps.
/// It is intentionally dependency-free and safe for classic WinForms apps.
/// </summary>
public sealed class GridlyToast : Form
{
    private readonly System.Windows.Forms.Timer _timer = new() { Interval = 3500 };

    private GridlyToast(string message, GridlyToastKind kind, GridlyTheme theme)
    {
        FormBorderStyle = FormBorderStyle.None;
        ShowInTaskbar = false;
        StartPosition = FormStartPosition.Manual;
        Size = new Size(360, 86);
        Padding = new Padding(14);
        BackColor = theme.PanelBackColor;
        ForeColor = theme.ForeColor;
        DoubleBuffered = true;

        var title = new Label
        {
            Dock = DockStyle.Top,
            Height = 24,
            Text = kind.ToString(),
            Font = new Font("Segoe UI", 9F, FontStyle.Bold),
            ForeColor = kind == GridlyToastKind.Error ? Color.IndianRed : kind == GridlyToastKind.Warning ? Color.Goldenrod : theme.AccentColor
        };
        var body = new Label
        {
            Dock = DockStyle.Fill,
            Text = message,
            AutoEllipsis = true,
            ForeColor = theme.ForeColor
        };
        Controls.Add(body);
        Controls.Add(title);
        GridlyDialogChrome.ConfigureStandardDialog(this, theme, Size, sizeable: false, iconKind: ToDialogIconKind(kind));
        _timer.Tick += (_, _) => Close();
    }

    private static GridlyDialogIconKind ToDialogIconKind(GridlyToastKind kind) => kind switch
    {
        GridlyToastKind.Success => GridlyDialogIconKind.Success,
        GridlyToastKind.Warning => GridlyDialogIconKind.Warning,
        GridlyToastKind.Error => GridlyDialogIconKind.Error,
        _ => GridlyDialogIconKind.Info
    };

    public static void Show(Control owner, string message, GridlyToastKind kind = GridlyToastKind.Info, GridlyTheme? theme = null, int milliseconds = 3500)
    {
        var actualTheme = theme ?? WindowsThemeService.CurrentTheme();
        var toast = new GridlyToast(message, kind, actualTheme);
        toast._timer.Interval = Math.Max(800, milliseconds);
        var screen = owner?.FindForm() != null ? Screen.FromControl(owner) : Screen.PrimaryScreen;
        var area = screen?.WorkingArea ?? Screen.PrimaryScreen!.WorkingArea;
        toast.Location = new Point(area.Right - toast.Width - 18, area.Bottom - toast.Height - 18);
        toast.Shown += (_, _) => toast._timer.Start();
        toast.Show(owner?.FindForm());
    }
}
