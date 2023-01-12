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
    internal class DataLogic
    {
        public static LoginResult LoginAndInit()
        {
            LoginResult loginResult = FacebookService.Login(
                    "446018321062802",
                    /// requested permissions:
                    "email",
                    "public_profile",
                    "user_age_range",
                    "user_birthday",
                    "user_events",
                    "user_friends",
                    "user_gender",
                    "user_hometown",
                    "user_likes",
                    "user_link",
                    "user_location",
                    "user_photos",
                    "user_posts",
                    "user_videos");

            UserSingleton.Instance.FacebookUser = loginResult.LoggedInUser;

            return loginResult;
        }

        public static FacebookObjectCollection<Post> FetchBestPosts()
        {
            FacebookObjectCollection<Post> bestPosts = new FacebookObjectCollection<Post>();
            Post postWithMaxLikes = new Post();
            Post postWithMaxComments = new Post();

            foreach (Post post in UserSingleton.Instance.Posts)
            {
                if (post.LikedBy.Count() > postWithMaxLikes.LikedBy.Count())
                {
                    postWithMaxLikes = post;
                }

                if (post.Comments.Count() > postWithMaxComments.Comments.Count())
                {
                    postWithMaxComments = post;
                }
            }

            bestPosts.Add(postWithMaxLikes);
            bestPosts.Add(postWithMaxComments);
            return bestPosts;
        }
    }
}
