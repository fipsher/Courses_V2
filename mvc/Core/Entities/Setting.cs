using System;
using System.ComponentModel;

namespace Core.Entities
{
    /// <summary>
    /// Used to save deadline dates
    /// </summary>
    public class Setting : Entity
    {
        public DateTime Value { get; set; }
    }
}
