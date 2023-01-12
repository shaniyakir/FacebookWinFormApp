using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FacebookWrapper.ObjectModel;


namespace BasicFacebookFeatures
{
    public sealed class UserSingleton
    {
        private static UserSingleton s_Instance;
        private static object s_LockObject = new object();
        public User FacebookUser { get; set; }
        public List<JsonPost> CarPosts { get; set; }
        
        public List<JsonPost> TlvAptPosts { get; set; }

        public FacebookObjectCollection<Album> Albums => FacebookUser.Albums;
        public FacebookObjectCollection<Post> Posts => FacebookUser.Posts;
        public FacebookObjectCollection<Event> Events => FacebookUser.Events;
        public FacebookObjectCollection<Page> LikedPages => FacebookUser.LikedPages;
        public FacebookObjectCollection<Group> Groups => FacebookUser.Groups;



        private UserSingleton() { }

        public static UserSingleton Instance
        {
            get
            {
                if (s_Instance == null)
                {
                    lock (s_LockObject)
                    {
                        if (s_Instance == null)
                        {
                            s_Instance = new UserSingleton();
                        }
                    }
                }

                return s_Instance;
            }
        }
    }
}
