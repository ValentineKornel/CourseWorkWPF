using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlumHub
{
    class User
    {
        private long _id;
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get { return _id; } set { _id = value; } }


        private string _username;
        public string Username { get { return _username; } set { _username = value; } }

        private byte[] _profileImage;
        public byte[] ProfileImage { get { return _profileImage; } set { _profileImage = value; } }

        private string _firstName;
        public string Firstname { get { return _firstName; } set { _firstName = value; } }

        private string _secondName;
        public string Secondname { get { return _secondName; } set { _secondName = value; } }

        private string _email;
        public string Email { get { return _email; } set { _email = value; } }

        private string _tel;
        public string Tel { get { return _tel; } set { _tel = value; } }

        [DefaultValue(0)]
        private ROLES _role;
        public ROLES Role { get { return _role; } set { _role = value; } }

        public ICollection<Booking> Bookings { get; set; }

        [AllowNull]
        public MasterInfo? MasterInfo { get; set; }


        public ICollection<UserRelation> Masters { get; set; }

        public ICollection<UserRelation> Followers { get; set; }

        public User() { }

        public User(string username, string firstname, string secondtname, string email, string tel)
        {
            Username = username;
            Firstname = firstname;
            Secondname = secondtname;
            Email = email;
            Tel = tel;
            Masters = new List<UserRelation>();
            Followers = new List<UserRelation>();
        }
    }
}
