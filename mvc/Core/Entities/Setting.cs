using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Core.Entities
{
    /// <summary>
    /// Used to save deadline dates
    /// </summary>
    public class Setting : Entity
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Поле обов'язкове до заповнення")]
        public DateTime Value { get; set; }
    }
}
