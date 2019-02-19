using Abp.Configuration.Startup;
using Abp.Localization.Dictionaries;
using Abp.Localization.Dictionaries.Xml;
using Abp.Reflection.Extensions;

namespace ExcelImportDemo.Localization
{
    public static class ExcelImportDemoLocalizationConfigurer
    {
        public static void Configure(ILocalizationConfiguration localizationConfiguration)
        {
            localizationConfiguration.Sources.Add(
                new DictionaryBasedLocalizationSource(ExcelImportDemoConsts.LocalizationSourceName,
                    new XmlEmbeddedFileLocalizationDictionaryProvider(
                        typeof(ExcelImportDemoLocalizationConfigurer).GetAssembly(),
                        "ExcelImportDemo.Localization.SourceFiles"
                    )
                )
            );
        }
    }
}
