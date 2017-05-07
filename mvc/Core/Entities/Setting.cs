using System;
using System.ComponentModel.DataAnnotations;

namespace Core.Entities
{
    /// <summary>
    /// Used to save deadline dates
    /// </summary>
    public class Setting : Entity
    {
        public string Name { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Поле обов'язкове до заповнення")]
        public DateTime Value { get; set; }
    }
}
