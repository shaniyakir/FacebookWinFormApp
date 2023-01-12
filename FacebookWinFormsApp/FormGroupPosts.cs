using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FacebookWrapper.ObjectModel;
using FacebookWrapper;
using System.IO;
using Newtonsoft.Json;



namespace BasicFacebookFeatures
{
    public partial class FormGroupPosts : Form
    {
        List<JsonPost> m_GroupPosts = new List<JsonPost>();
        JsonPost m_GroupPostItem = new JsonPost();

        public FormGroupPosts(List<JsonPost> groupPosts)
        {
            InitializeComponent();
            FacebookWrapper.FacebookService.s_CollectionLimit = 200;
            m_GroupPosts = JsonPost.SortPostList(groupPosts);
        }


        private void listPostsByFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(listBoxPostsByFilter.SelectedIndex != -1)
            {
                var itemIndex = listBoxPostsByFilter.SelectedIndex;
                m_GroupPostItem = JsonPost.GetItemByIndex(m_GroupPosts,itemIndex);
                richTextBoxSelectedPost.Text = m_GroupPostItem.ToString();
            }
        }

        private void formGroupsByFilter_Load(object sender, EventArgs e)
        {
            listBoxPostsByFilter.Items.Clear();
            listBoxPostsByFilter.DisplayMember = "Post";
            foreach (JsonPost post in m_GroupPosts)
            {
                listBoxPostsByFilter.Items.Add(post.m_PostText);
            }
        }
    }
}