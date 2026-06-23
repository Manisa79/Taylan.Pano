namespace Gridly.TestApp;

public sealed partial class GridlyDesignerSmartTagSampleForm
{
    private System.ComponentModel.IContainer components = null;
    private System.Windows.Forms.Label lblInfo;
    private Gridly.Core.GridlyView gridly;

    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }

        base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
        this.components = new System.ComponentModel.Container();
        this.lblInfo = new System.Windows.Forms.Label();
        this.gridly = new Gridly.Core.GridlyView();
        this.SuspendLayout();
        // 
        // lblInfo
        // 
        this.lblInfo.Dock = System.Windows.Forms.DockStyle.Top;
        this.lblInfo.Height = 92;
        this.lblInfo.Padding = new System.Windows.Forms.Padding(12);
        this.lblInfo.Text = "Designer SmartTag örneği: Bu formu Visual Studio Designer'da aç, Gridly kontrolünü seç ve sağ üst hızlı görev okunu kullan.";
        this.lblInfo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
        // 
        // gridly
        // 
        this.gridly.AutoThemeFromParent = true;
        this.gridly.DesignTimeSampleData = true;
        this.gridly.DesignTimeThemePreview = Gridly.Theming.GridlyDesignTimeThemePreview.Auto;
        this.gridly.Dock = System.Windows.Forms.DockStyle.Fill;
        this.gridly.EmptyListMessage = "Designer SmartTag örneği";
        this.gridly.HeaderContextMenuBehavior = Gridly.Core.GridlyHeaderContextMenuBehavior.Full;
        this.gridly.Location = new System.Drawing.Point(0, 92);
        this.gridly.Name = "gridly";
        this.gridly.Size = new System.Drawing.Size(980, 528);
        this.gridly.TabIndex = 1;
        // 
        // GridlyDesignerSmartTagSampleForm
        // 
        this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.ClientSize = new System.Drawing.Size(980, 620);
        this.Controls.Add(this.gridly);
        this.Controls.Add(this.lblInfo);
        this.MinimumSize = new System.Drawing.Size(760, 460);
        this.Name = "GridlyDesignerSmartTagSampleForm";
        this.Text = "Gridly Designer SmartTag Örneği";
        this.ResumeLayout(false);
    }
}
