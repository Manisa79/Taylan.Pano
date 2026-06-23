namespace Gridly.Core;

/// <summary>
/// Host projelerde aynı v27 ayarlarını tekrar tekrar yazmamak için hızlı başlangıç yardımcıları.
/// MasterData, AOI Support Desk ve üretim araçlarında drop-in kullanım hedeflenmiştir.
/// </summary>
public static class GridlyUserFeatureBootstrapper
{
    public static GridlyView UseMasterDataDefaults(this GridlyView grid, string stateFilePath, string stateKeyAspectName = "Id", GridlyViewScenario scenario = GridlyViewScenario.DataTable)
    {
        if (grid == null) throw new ArgumentNullException(nameof(grid));
        grid.StateKeyAspectName = stateKeyAspectName;
        grid.AutoStateFilePath = stateFilePath ?? string.Empty;
        grid.AutoLoadStateOnCreate = true;
        grid.AutoSaveStateOnDispose = true;
        grid.ShowStateMenuItems = true;
        grid.ShowScenarioMenuItems = true;
        grid.ActiveScenario = scenario;
        grid.ApplyActiveScenario();
        return grid;
    }

    public static GridlyView UseBomPositionDefaults(this GridlyView grid, string stateFilePath, string stateKeyAspectName = "MaterialCode")
        => grid.UseMasterDataDefaults(stateFilePath, stateKeyAspectName, GridlyViewScenario.BomPositions);

    public static GridlyView UseTicketBoardDefaults(this GridlyView grid, string stateFilePath, string stateKeyAspectName = "Id")
    {
        if (grid == null) throw new ArgumentNullException(nameof(grid));
        grid.StateKeyAspectName = stateKeyAspectName;
        grid.AutoStateFilePath = stateFilePath ?? string.Empty;
        grid.AutoLoadStateOnCreate = true;
        grid.AutoSaveStateOnDispose = true;
        grid.ShowStateMenuItems = true;
        grid.ShowScenarioMenuItems = true;
        grid.ActiveScenario = GridlyViewScenario.TicketBoard;
        grid.ApplyActiveScenario();
        return grid;
    }
}
