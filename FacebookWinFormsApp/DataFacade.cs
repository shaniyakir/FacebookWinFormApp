using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FacebookWrapper;
using FacebookWrapper.ObjectModel;
using Newtonsoft.Json;

namespace BasicFacebookFeatures
{
    public class DataFacade
    {
        private static UserSingleton m_User = UserSingleton.Instance;
        public static LoginResult LoginAndInit()
        {
            LoginResult loginResult = DataLogic.LoginAndInit();
            return loginResult;
        }
        public static FacebookObjectCollection<Post> FetchPosts()
        {
            return m_User.Posts;
        }

        public static FacebookObjectCollection<Post> FetchBestPosts()
        {
            FacebookObjectCollection<Post> bestPosts = DataLogic.FetchBestPosts();
            return bestPosts;
        }

        public static FacebookObjectCollection<Album> FetchAlbums()
        {
            return m_User.Albums;
        }

        public static FacebookObjectCollection<Event> FetchEvents()
        {
            return m_User.Events;
        }

        public static FacebookObjectCollection<Page> FetchLikedPages()
        {
            return m_User.LikedPages;
        }

        public static FacebookObjectCollection<Group> FetchGroups()
        {
            return m_User.Groups;
        }

        public static List<JsonPost> FetchCarGroupPosts()
        {
            ProxyJsonPosts carPosts = new ProxyJsonPosts();
            carPosts.GetCarPosts();
            return m_User.CarPosts;
        }
        
        public static List<JsonPost> FetchTlvAptGroupPosts()
        {
            ProxyJsonPosts tlvAptPosts = new ProxyJsonPosts();
            tlvAptPosts.GetTlvAptPosts();
            return m_User.TlvAptPosts;
        }
    }
}
