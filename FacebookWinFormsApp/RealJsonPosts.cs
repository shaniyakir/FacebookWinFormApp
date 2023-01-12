using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace BasicFacebookFeatures
{
    public sealed class RealJsonPosts : IJsonPosts
    {
        private static RealJsonPosts s_Instance;
        private static object s_LockObject = new object();
        private string m_CarPath = get_Relative_Path(@"..\\JsonFakeGroupsPosts\\carRental.json");
        private string m_TlvAptPath = get_Relative_Path(@"..\\JsonFakeGroupsPosts\\TLVapartmentRental.json");
        private JsonPosts m_CarGroupPosts = new JsonPosts();
        private JsonPosts m_TlvAptGroupPosts = new JsonPosts();

        private RealJsonPosts() 
        {
            LoadCarPostsFromJson();
            LoadTlvAptPostsFromJson();
        }
        public static RealJsonPosts Instance
        {
            get
            {
                if (s_Instance == null)
                {
                    lock (s_LockObject)
                    {
                        if (s_Instance == null)
                        {
                            s_Instance = new RealJsonPosts();
                        }
                    }
                }

                return s_Instance;
            }
        }

        private static string get_Relative_Path(string rel_path)
        {
            var projectFolder = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName;
            var file = Path.Combine(projectFolder, rel_path);
            string icon_path = Path.GetFullPath(file);
            icon_path = icon_path.Replace(@"\\", @"\");

            return icon_path;
        }

        public void LoadCarPostsFromJson()
        {
            JsonPosts jsonCarData = JsonConvert.DeserializeObject<JsonPosts>(File.ReadAllText(m_CarPath));
            m_CarGroupPosts.m_JsonPostsList = new List<JsonPost>();

            if (jsonCarData != null)
            {
                foreach (JsonPost carPost in jsonCarData.m_JsonPostsList)
                {
                    if (m_CarGroupPosts.m_JsonPostsList.Contains(carPost) == false)
                    {
                        m_CarGroupPosts.m_JsonPostsList.Add(carPost);
                    }
                }
            }
        }

        public void LoadTlvAptPostsFromJson()
        {
            JsonPosts jsonTlvData = JsonConvert.DeserializeObject<JsonPosts>(File.ReadAllText(m_TlvAptPath));
            m_TlvAptGroupPosts.m_JsonPostsList = new List<JsonPost>();

            if (jsonTlvData != null)
            {
                foreach (JsonPost tlvPost in jsonTlvData.m_JsonPostsList)
                {
                    if (m_TlvAptGroupPosts.m_JsonPostsList.Contains(tlvPost) == false)
                    {
                        m_TlvAptGroupPosts.m_JsonPostsList.Add(tlvPost);
                    }
                }
            }
        }

        public void GetCarPosts()
        {
            UserSingleton.Instance.CarPosts = m_CarGroupPosts.m_JsonPostsList;
        }

        public void GetTlvAptPosts()
        {
            UserSingleton.Instance.TlvAptPosts = m_TlvAptGroupPosts.m_JsonPostsList;
        }
    }

    public class JsonPosts
    {
        [JsonProperty("jsonPostsList")]
        public List<JsonPost> m_JsonPostsList { get; set; }
    }

    public class JsonPost
    {
        [JsonProperty("id")]
        public int m_Id { get; set; }

        [JsonProperty("userName")]
        public string m_UserName { get; set; }

        [JsonProperty("postText")]
        public string m_PostText { get; set; }

        [JsonProperty("nameOfGroup")]
        public string m_NameOfGroup { get; set; }

        [JsonProperty("postDate")]
        public string m_PostDate { get; set; }




        public string ToString()
        {
            string returnValue = "";
            string user_Name = this.m_UserName;
            string post_Text = this.m_PostText;
            string name_Of_Group = this.m_NameOfGroup;
            string post_Date = this.m_PostDate;
            returnValue = "User Name: " + user_Name + "\n" + "Post Text: " + post_Text + "\n" + "Name of group: " + name_Of_Group + "\n" + "Post Date: " + post_Date;

            return returnValue;
        }

        public static JsonPost GetItemByIndex(List<JsonPost> i_JsonPostsList, int i_Index)
        {
            return i_JsonPostsList[i_Index];
        }

        public static List<JsonPost> SortPostList(List<JsonPost> i_JsonPostsList)
        {
            return i_JsonPostsList.OrderBy(o => o.m_PostDate).ToList();
        }
    }
}