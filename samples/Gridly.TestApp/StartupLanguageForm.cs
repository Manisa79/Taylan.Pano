using Gridly.Localization;
using System.Text.Json;

namespace Gridly.TestApp;

public sealed partial class StartupLanguageForm : Form
{
    public GridlyLanguage SelectedLanguage { get; private set; } = GridlyLanguage.Auto;
    public bool RememberSelection => chkRemember.Checked;

    public StartupLanguageForm(GridlyLanguage initialLanguage)
    {
        InitializeComponent();

        foreach (var lang in GridlyLocalization.SupportedLanguages)
            cmbLanguage.Items.Add(new LanguageChoice(lang));

        var selected = cmbLanguage.Items.Cast<LanguageChoice>().FirstOrDefault(x => x.Language == initialLanguage)
            ?? cmbLanguage.Items.Cast<LanguageChoice>().First();
        cmbLanguage.SelectedItem = selected;

        cmbLanguage.SelectedIndexChanged += (_, __) => RefreshTexts();
        btnContinue.Click += (_, __) => SelectedLanguage = ((LanguageChoice)cmbLanguage.SelectedItem!).Language;
        RefreshTexts();
    }

    private void RefreshTexts()
    {
        var lang = cmbLanguage.SelectedItem is LanguageChoice choice ? choice.Language : GridlyLanguage.Auto;
        GridlyLocalization.Use(lang);

        Text = lang switch
        {
            GridlyLanguage.Turkish => "Gridly TestApp - Dil Seçimi",
            GridlyLanguage.English => "Gridly TestApp - Language",
            _ => "Gridly TestApp - Language"
        };

        lblTitle.Text = lang == GridlyLanguage.Turkish ? "Gridly TestApp Dil Seçimi" : "Gridly TestApp Language";
        lblInfo.Text = lang == GridlyLanguage.Turkish
            ? "Gridly dahili menüleri, pencereleri ve örnek ekranları için kullanılacak dili seçin."
            : "Select the language used by Gridly built-in menus, dialogs and sample screens.";
        lblLanguage.Text = lang == GridlyLanguage.Turkish ? "Dil" : "Language";
        chkRemember.Text = lang == GridlyLanguage.Turkish ? "Seçimimi hatırla" : "Remember my selection";
        btnContinue.Text = lang == GridlyLanguage.Turkish ? "Devam" : "Continue";
        btnCancel.Text = lang == GridlyLanguage.Turkish ? "İptal" : "Cancel";
    }

    private sealed class LanguageChoice
    {
        public LanguageChoice(GridlyLanguage language) => Language = language;
        public GridlyLanguage Language { get; }
        public override string ToString() => GridlyLocalization.DisplayName(Language);
    }

    public static GridlyLanguage LoadSavedLanguage(string filePath)
    {
        try
        {
            if (!File.Exists(filePath)) return GridlyLanguage.Auto;
            var data = JsonSerializer.Deserialize<LanguageSettings>(File.ReadAllText(filePath));
            return GridlyLocalization.FromName(data?.Language, GridlyLanguage.Auto);
        }
        catch
        {
            return GridlyLanguage.Auto;
        }
    }

    public static void SaveLanguage(string filePath, GridlyLanguage language)
    {
        try
        {
            Directory.CreateDirectory(Path.GetDirectoryName(filePath)!);
            var json = JsonSerializer.Serialize(new LanguageSettings { Language = language.ToString() }, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(filePath, json);
        }
        catch
        {
            // TestApp should not fail just because settings cannot be written.
        }
    }

    private sealed class LanguageSettings
    {
        public string? Language { get; set; }
    }
}
