namespace Gridly.Core;

/// <summary>
/// GridlyView'in ana çalışma modunu belirtir.
/// ViewMode görsel sunumu (Details/Tile/List) yönetirken, Mode veri/çalışma yaklaşımını netleştirir.
/// </summary>
public enum GridlyMode
{
    /// <summary>POCO/model object listesi ile klasik Gridly benzeri kullanım.</summary>
    Object,

    /// <summary>DataTable/DataView/BindingSource tabanlı kullanım.</summary>
    DataTable,

    /// <summary>IRowProvider veya IQueryRowProvider ile sanal / çok büyük veri kullanımı.</summary>
    Virtual,

    /// <summary>TreeGridlyView veya hiyerarşik veri senaryosu.</summary>
    Tree,

    /// <summary>Kart/tile görünüm odaklı kullanım.</summary>
    Tile
}
