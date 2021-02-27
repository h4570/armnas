using System;
using System.ComponentModel.DataAnnotations;

namespace WebApi.Models.Internal
{

    public enum AppHistoryType
    {
        Add,
        Update,
        Delete
    }

    public class AppHistory
    {

        /// <exception cref="T:System.ArgumentOutOfRangeException"></exception>
        public AppHistory(AppTable tableId, AppHistoryType type, int? elementId = null, string description = null)
        {
            TableId = tableId;
            Type = type;
            DateTime = DateTime.Now;
            ElementId = elementId;
            if (description == null) return;
            if (description.Length > 199) description = description.Substring(0, 199);
            Description = description;
        }

        [Key]
        public int Id { get; set; }
        [Required]
        public AppTable TableId { get; set; }
        public int? ElementId { get; set; }
        [Required]
        public AppHistoryType Type { get; set; }
        [Required]
        public DateTime DateTime { get; set; }
        [StringLength(200)]
        public string Description { get; set; }

    }

}
