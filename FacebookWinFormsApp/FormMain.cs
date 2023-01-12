using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FacebookWrapper.ObjectModel;
using FacebookWrapper;
using System.IO;
using Newtonsoft.Json;
using System.Threading;

namespace BasicFacebookFeatures
{
    public partial class FormMain : Form
    {
        LoginResult m_LoginResult;


        public FormMain()
        {
            InitializeComponent();
        }


        /// Login
        private void buttonLogin_Click(object sender, EventArgs e)
        {
            m_LoginResult = DataFacade.LoginAndInit();

            if (!string.IsNullOrEmpty(m_LoginResult.AccessToken))
            {
                new Thread(fetchUserInfo).Start();
            }
            else
            {
                MessageBox.Show(m_LoginResult.ErrorMessage, "Login Failed");
            }            
        }

        private void fetchUserInfo()
        {
            pictureBoxProfile.Invoke(new Action(() => pictureBoxProfile.LoadAsync(UserSingleton.Instance.FacebookUser.PictureNormalURL)));
            buttonLogin.Invoke(new Action (() => buttonLogin.Text = $"Logged in as {UserSingleton.Instance.FacebookUser.Name}"));
            labelStatus.Invoke(new Action(() => labelStatus.Text = $"What's on your mind, {UserSingleton.Instance.FacebookUser.FirstName}?"));
        }

        /// Logout
        private void buttonLogout_Click(object sender, EventArgs e)
        {
			FacebookService.LogoutWithUI();
			buttonLogin.Text = "Login";
            m_LoginResult = null;
        }

        /// Posts

        private void textBoxStatus_TextChanged(object sender, EventArgs e)
        {
            buttonSetStatus.Enabled = true;
        }

        private void buttonSetStatus_Click(object sender, EventArgs e)
        {
            try
            {
                Status postedStatus = UserSingleton.Instance.FacebookUser.PostStatus(textBoxStatus.Text);
                MessageBox.Show("Posted! ID: " + postedStatus.Id);
            }
            catch
            {
                MessageBox.Show("Please try again");
            }
        }

        private void displayPost(TextBox i_TextBox, Post i_Post)
        {
            if (i_Post.Message != null)
            {

                i_TextBox.Text = i_Post.Message;
            }
            else if (i_Post.Caption != null)
            {
                i_TextBox.Text = i_Post.Caption;
            }
            else
            {
                i_TextBox.Text = string.Format("[{0}]", i_Post.Type);
            }
        }

        private void linkPosts_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            postBindingSource.DataSource = DataFacade.FetchPosts();
            listBoxPosts.Visible = true;
            textBoxPost.Visible = true;
            panelEventDetails.Visible = true;
            buttonBestPosts.Visible = true;
        }
        
        private void listBoxPosts_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBoxPosts.SelectedItems.Count == 1)
            {
                textBoxPost.Text = listBoxPosts.SelectedItem.ToString();
            }
        }

        private void buttonBestPosts_Click(object sender, EventArgs e)
        {
            hideAllBoxes();
            try
            {
                FacebookObjectCollection<Post> bestPosts = DataFacade.FetchBestPosts();
                Post mostLikedPost = bestPosts[0];
                Post mostCommentedPost = bestPosts[1];
                displayPost(textBoxMostLikedPost, mostLikedPost);
                displayPost(textBoxMostCommentedPost, mostCommentedPost);

                labelMostLikedPost.Visible = true;
                textBoxMostLikedPost.Visible = true;
                labelMostCommentedPost.Visible = true;
                textBoxMostCommentedPost.Visible = true;
            }
            catch
            {
                MessageBox.Show("No permission to see post likes/comments.. ):");
            }
        }

        /// Albums
        private void linkAlbums_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            hideAllBoxes();
            listBoxAlbums.Items.Clear();
            listBoxAlbums.DisplayMember = "Name";

            foreach (Album album in DataFacade.FetchAlbums())
            {
                listBoxAlbums.Items.Add(album);
            }
            if (listBoxAlbums.Items.Count == 0)
            {
                MessageBox.Show("No albums to show :(");
            }
            else
            {
                listBoxAlbums.Visible = true;
                pictureBoxAlbum.Visible = true;
            }   
        }

        private void listBoxAlbums_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBoxAlbums.SelectedItems.Count == 1)
            {
                Album selectedAlbum = listBoxAlbums.SelectedItem as Album;
                if (selectedAlbum.PictureAlbumURL != null)
                {
                    pictureBoxAlbum.Load(selectedAlbum.PictureAlbumURL);
                }
                else
                {
                    pictureBoxAlbum.Image = pictureBoxAlbum.ErrorImage;
                }
            }
        }

        /// Events
        private void linkEvents_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            hideAllBoxes();
            listBoxEvents.Items.Clear();
            listBoxEvents.DisplayMember = "Name";

            foreach(Event Event in DataFacade.FetchEvents())
            {
                listBoxEvents.Items.Add(Event);
            }
            if (listBoxEvents.Items.Count == 0)
            {
                MessageBox.Show("No events to show :(");
            }
            else
            {
                listBoxEvents.Visible = true;
                pictureBoxEvent.Visible = true;
            }
        }

        private void listBoxEvents_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBoxEvents.SelectedItems.Count == 1)
            {
                Event selectedEvent = listBoxEvents.SelectedItem as Event;
                if (selectedEvent.PictureNormalURL != null)
                {
                    pictureBoxEvent.Load(selectedEvent.PictureNormalURL);
                }
                else
                {
                    pictureBoxEvent.Image = pictureBoxEvent.ErrorImage;
                }
            }
        }

        /// Pages
        private void linkPages_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            hideAllBoxes();
            listBoxPages.Items.Clear();
            listBoxPages.DisplayMember = "Name";

            foreach (Page page in DataFacade.FetchLikedPages())
            {
                listBoxPages.Items.Add(page);
            }
            if (listBoxPages.Items.Count == 0)
            {
                MessageBox.Show("No pages to show :(");
            }
            else
            {
                listBoxPages.Visible = true;
                pictureBoxPage.Visible = true;
            }
        }

        private void listBoxPages_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBoxPages.SelectedItems.Count == 1)
            {
                Page selectedPage = listBoxPages.SelectedItem as Page;
                if (selectedPage.PictureNormalURL != null)
                {
                    pictureBoxPage.Load(selectedPage.PictureNormalURL);
                }
                else
                {
                    pictureBoxPage.Image = pictureBoxPage.ErrorImage;
                }
            }
        }

        /// Groups
        private void linkGroups_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            hideAllBoxes();
            listBoxGroups.Invoke(new Action(() =>
            {
                listBoxGroups.Items.Clear();
                listBoxGroups.DisplayMember = "Name";
            }));

            foreach (Group group in DataFacade.FetchGroups())
            {
                listBoxGroups.Invoke(new Action(() => listBoxGroups.Items.Add(group)));
            }
            if (listBoxGroups.Items.Count == 0)
            {
                MessageBox.Show("No groups to show :(");
            }
            else
            {
                listBoxGroups.Visible = true;
                pictureBoxGroup.Visible = true;
            }

            buttonGroupsByFilter.Visible = true;

        }

        private void listBoxGroups_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBoxGroups.SelectedItems.Count == 1)
            {
                Group selectedGroup = listBoxGroups.SelectedItem as Group;
                if (selectedGroup.PictureNormalURL != null)
                {
                    pictureBoxGroup.LoadAsync(selectedGroup.PictureNormalURL);
                }
                else
                {
                    pictureBoxGroup.Image = pictureBoxGroup.ErrorImage;
                }
            }
        }

        private void buttonGroupsByFilter_Click(object sender, EventArgs e)
        {
            hideAllBoxes();
            buttonTlvAppartment.Visible = true;
            buttonCarRent.Visible = true;
        }

        private void buttonCarRent_Click(object sender, EventArgs e)
        {
            try
            {
                DataFacade.FetchCarGroupPosts();
                viewAllCarGroupPostsSortedByDate();
            }
            catch
            {
                MessageBox.Show("It didnt work out. Please try again");
            }

        }

        private void buttonTlvAppartment_Click(object sender, EventArgs e)
        {
            try
            {
                DataFacade.FetchTlvAptGroupPosts();
                viewAllTLVApartmentsGroupPostsSortedByDate();
            }
            catch
            {
                MessageBox.Show("It didnt work out. Please try again");
            }
        }


        private void viewAllCarGroupPostsSortedByDate()
        {
            FormGroupPosts f2 = new FormGroupPosts(UserSingleton.Instance.CarPosts);
            f2.ShowDialog();

        }

        private void viewAllTLVApartmentsGroupPostsSortedByDate()
        {
            FormGroupPosts f2 = new FormGroupPosts(UserSingleton.Instance.TlvAptPosts);
            f2.ShowDialog();
        }

        private void hideAllBoxes()
        {
            // Posts
            listBoxPosts.Visible = false;
            textBoxPost.Visible = false;
            buttonBestPosts.Visible = false;
            labelMostLikedPost.Visible = false;
            textBoxMostLikedPost.Visible = false;
            labelMostCommentedPost.Visible = false;
            textBoxMostCommentedPost.Visible = false;
            panelEventDetails.Visible = false;

            // Albums
            listBoxAlbums.Visible = false;
            pictureBoxAlbum.Visible = false;
            // Events
            listBoxEvents.Visible = false;
            pictureBoxEvent.Visible = false;
            // Pages
            listBoxPages.Visible = false;
            pictureBoxPage.Visible = false;
            // Groups
            listBoxGroups.Visible = false;
            pictureBoxGroup.Visible = false;
            buttonGroupsByFilter.Visible = false;
            buttonTlvAppartment.Visible = false;
            buttonCarRent.Visible = false;
        }

        private void textBoxPost_TextChanged(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

    }
}
