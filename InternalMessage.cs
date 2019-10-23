using Penguin.Cms.Entities;
using Penguin.Persistence.Abstractions.Attributes.Control;
using Penguin.Persistence.Abstractions.Attributes.Relations;
using Penguin.Persistence.Abstractions.Attributes.Validation;
using Penguin.Shared.Objects.Interfaces;
using System;
using System.Collections.Generic;

namespace Penguin.Cms.InternalMessaging
{
    [Table("InternalMessages")]
    public class InternalMessage : Entity, IRecursiveList<InternalMessage>
    {
        public string Body { get; set; } = string.Empty;

        [EagerLoad(1)]
        [DontAllow(DisplayContexts.Any)]
        public IList<InternalMessage> Children { get; set; }

        public string From { get; set; } = string.Empty;

        [NotMapped]
        [DontAllow(DisplayContexts.Any)]
        public bool HasSubject => !string.IsNullOrWhiteSpace(this.Subject) && this.Subject != "@";

        public Guid Origin { get; set; }

        [EagerLoad(1)]
        [OptionalToMany]
        [DontAllow(DisplayContexts.Any)]
#pragma warning disable CS8613 // Nullability of reference types in return type doesn't match implicitly implemented member.
        public InternalMessage Parent { get; set; }

#pragma warning restore CS8613 // Nullability of reference types in return type doesn't match implicitly implemented member.

        public bool Read { get; set; }

        public Guid Recipient { get; set; }

        [StringLength(80)]
        [Required]
        public string Subject { get; set; } = string.Empty;

        public string To { get; set; } = string.Empty;

        public InternalMessage()
        {
            this.Children = new List<InternalMessage>();
        }
    }
}