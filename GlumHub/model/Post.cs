using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlumHub
{
    class Post
    {
        private long _id;
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get { return _id; } set { _id = value; } }

        [ForeignKey("UserId")]
        private long _masterId;
        public long Masterid { get { return _masterId; } set { _masterId = value; } }
        public User Master { get; set; }

        private byte[] _postImage;
        public byte[] PostImage { get { return _postImage; } set { _postImage = value; } }

        [AllowNull]
        private string? _description;
        public string? Description { get { return _description; } set { _description = value; } }


        public Post() { }


        public Post(long masterid, byte[] postImage, string? description)
        {
            Masterid = masterid;
            PostImage = postImage;
            Description = description;
        }
    }
}
