using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics.CodeAnalysis;

namespace GlumHub
{
    internal class Request
    {

        private long _id;
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get { return _id; } set { _id = value; } }

        [ForeignKey("UserId")]
        private long _clientId;
        public long Clientid { get { return _clientId; } set { _clientId = value; } }
        public User Client { get; set; }

        private byte[] _attachmentImage;
        public byte[] AttachmentImage { get { return _attachmentImage; } set { _attachmentImage = value; } }

        [AllowNull]
        private string? _attachmentLetter;
        public string? AttachmentLetter { get { return _attachmentLetter; } set { _attachmentLetter = value; } }

        public Request() { }

        public Request(long clientid, byte[] attachmentImage, string? attachmentLetter)
        {
            Clientid = clientid;
            AttachmentImage = attachmentImage;
            AttachmentLetter = attachmentLetter;
        }
    }
}
