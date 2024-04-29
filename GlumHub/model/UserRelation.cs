using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlumHub
{
    class UserRelation
    {
        public long FollowerId { get; set; }
        public User Follower { get; set; }

        public long MasterId { get; set; }
        public User Master { get; set; }

        UserRelation() { }

        public UserRelation(long followerId, User follower, long masterId, User master)
        {
            FollowerId = followerId;
            Follower = follower;
            MasterId = masterId;
            Master = master;
        }
    }
}
