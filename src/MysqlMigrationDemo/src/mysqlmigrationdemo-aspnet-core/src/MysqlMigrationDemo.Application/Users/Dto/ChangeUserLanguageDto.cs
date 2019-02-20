using System.ComponentModel.DataAnnotations;

namespace MysqlMigrationDemo.Users.Dto
{
    public class ChangeUserLanguageDto
    {
        [Required]
        public string LanguageName { get; set; }
    }
}