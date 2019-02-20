using System.ComponentModel.DataAnnotations;

namespace postgresqlDemo.Users.Dto
{
    public class ChangeUserLanguageDto
    {
        [Required]
        public string LanguageName { get; set; }
    }
}