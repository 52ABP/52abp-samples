using Abp.Configuration.Startup;
using Abp.Localization.Dictionaries;
using Abp.Localization.Dictionaries.Xml;
using Abp.Reflection.Extensions;

namespace postgresqlDemo.Localization
{
    public static class postgresqlDemoLocalizationConfigurer
    {
        public static void Configure(ILocalizationConfiguration localizationConfiguration)
        {
            localizationConfiguration.Sources.Add(
                new DictionaryBasedLocalizationSource(postgresqlDemoConsts.LocalizationSourceName,
                    new XmlEmbeddedFileLocalizationDictionaryProvider(
                        typeof(postgresqlDemoLocalizationConfigurer).GetAssembly(),
                        "postgresqlDemo.Localization.SourceFiles"
                    )
                )
            );
        }
    }
}
