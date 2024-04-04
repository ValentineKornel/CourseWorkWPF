using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlumHub
{
    class Credentials
    {
        private long _id;
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get { return _id; } set { _id = value; } }

        private string _username;
        public string Username { get { return _username; } set { _username = value; } }

        private string _password;
        public string Password { get { return _password; } set { _password = value; } }

        [ForeignKey("UserId")]
        public long UserId { get; set; }

        public User User { get; set; }

        public Credentials() { }


        public Credentials(string username, string password, long userId)
        {
            UserId = userId;
            _username = username;
            _password = password;
        }

        public bool CheckPassword(string suggestedPassord)
        {
            return BCrypt.Net.BCrypt.Verify(suggestedPassord, _password);
        }



    }
}
