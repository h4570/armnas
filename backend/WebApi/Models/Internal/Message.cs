using System;
using System.ComponentModel.DataAnnotations;

namespace WebApi.Models.Internal
{

    public enum MessageType
    {
        Information,
        Warning,
        Error
    }

    public abstract class MessageBase
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string ShortName { get; set; }
        [StringLength(200)]
        public string Tooltip { get; set; }
        [Required]
        public MessageType Type { get; set; }
        public bool HasBeenRead { get; set; }
        [Required]
        [StringLength(50)]
        public string Author { get; set; }
        public DateTime? Date { get; set; }
    }


    public class Message : MessageBase
    {
        public Message() { }

        public Message(MessageODataHack hack)
        {
            Id = hack.Id;
            ShortName = hack.ShortName;
            Tooltip = hack.Tooltip;
            Type = hack.Type;
            HasBeenRead = hack.HasBeenRead;
            Author = hack.Author;
            Date = hack.Date;
        }

    }

    /// <summary>
    /// Sandro: You can think, WTF is that?
    /// Why just not use one Message class like Partition class?
    /// Reason: At the moment of writing this code (18.04.2021) oData had weird bug.
    /// I couldn't use a Message class as a [BodyParameter] in POST method. POST payload was always null.
    /// When I split Message class to two instances - first 'Message' for OData configuration,
    /// second 'MessageODataHack' for body param, everything started working.
    /// I think that you can remove these two implementations in future and
    /// rename MessageBase to Message. After this, please check if POST/PATCH message work!
    /// </summary>
    public class MessageODataHack : MessageBase { }

}
