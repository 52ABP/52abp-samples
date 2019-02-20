using Abp.Configuration.Startup;
using Abp.Localization.Dictionaries;
using Abp.Localization.Dictionaries.Xml;
using Abp.Reflection.Extensions;

namespace MysqlMigrationDemo.Localization
{
    public static class MysqlMigrationDemoLocalizationConfigurer
    {
        public static void Configure(ILocalizationConfiguration localizationConfiguration)
        {
            localizationConfiguration.Sources.Add(
                new DictionaryBasedLocalizationSource(MysqlMigrationDemoConsts.LocalizationSourceName,
                    new XmlEmbeddedFileLocalizationDictionaryProvider(
                        typeof(MysqlMigrationDemoLocalizationConfigurer).GetAssembly(),
                        "MysqlMigrationDemo.Localization.SourceFiles"
                    )
                )
            );
        }
    }
}
