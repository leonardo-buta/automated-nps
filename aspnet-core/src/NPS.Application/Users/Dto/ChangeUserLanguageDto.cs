using System.ComponentModel.DataAnnotations;

namespace NPS.Users.Dto
{
    public class ChangeUserLanguageDto
    {
        [Required]
        public string LanguageName { get; set; }
    }
}