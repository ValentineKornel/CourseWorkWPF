using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics.CodeAnalysis;
using System.Security.RightsManagement;

namespace GlumHub
{
    class Booking
    {
        private long _id;
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get { return _id; } set { _id = value; } }

        [ForeignKey("UserId")]
        [AllowNull]
        private long? _clientId;
        [AllowNull]
        public long? Clientid { get { return _clientId; } set { _clientId = value; } }
        public User? Client { get; set; }


        [ForeignKey("UserId")]
        private long _masterId;
        public long MasterId { get { return _masterId; } set { _masterId = value; } }
        public User Master { get; set; }


        


        private bool _booked;
        public bool Booked { get { return _booked; } set { _booked = value; } }

        
        private DateTime _dateTime;
        public DateTime Date_Time { get { return _dateTime; } set { _dateTime = value; } }

        private string _service;
        public string Service { get { return _service; } set { _service = value; } }

        [AllowNull]
        private string? _mastersComment;
        [AllowNull]
        public string? MastersComment { get { return _mastersComment; } set { _mastersComment = value;} }

        [AllowNull]
        private string? _clientsComment;
        [AllowNull]
        public string? ClientsComment { get { return _clientsComment; } set { _clientsComment = value; } }


        public Booking() { }


        public Booking(long masterId, DateTime dateTime,
            string service)
        {
            MasterId = masterId;
            Booked = false;
            Date_Time = dateTime;
            Service = service;
        }

    }
}
