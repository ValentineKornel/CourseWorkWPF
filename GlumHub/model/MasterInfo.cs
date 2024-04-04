using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlumHub
{
    class MasterInfo
    {
        private long _id;
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get { return _id; } set { _id = value; } }

        private string _bio;
        public string Bio { get { return _bio; } set { _bio = value; } }

        private string _businessAddress;
        public string BusinessAddress { get { return _businessAddress; } set { _businessAddress = value; } }

        [ForeignKey("UserId")]
        public long UserId { get; set; }

        public User User { get; set; }

        public MasterInfo() { }

        public MasterInfo(string bio, string businessAddress, long userId)
        {
            Bio = bio;
            BusinessAddress = businessAddress;
            UserId = userId;
        }
    }
}
