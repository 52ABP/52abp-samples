using System.ComponentModel.DataAnnotations;

namespace ExcelImportDemo.Users.Dto
{
    public class ChangeUserLanguageDto
    {
        [Required]
        public string LanguageName { get; set; }
    }
}