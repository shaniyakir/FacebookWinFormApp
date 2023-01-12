using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicFacebookFeatures
{
    public class ProxyJsonPosts
    {
        private RealJsonPosts m_RealJsonPosts;

        public ProxyJsonPosts() 
        {
            this.m_RealJsonPosts = RealJsonPosts.Instance;
        }

        public void GetCarPosts()
        {
            if (m_RealJsonPosts == null)
            {
                m_RealJsonPosts = RealJsonPosts.Instance;
            }

            m_RealJsonPosts.GetCarPosts();
        }

        public void GetTlvAptPosts()
        {
            if (m_RealJsonPosts == null)
            {
                m_RealJsonPosts = RealJsonPosts.Instance;
            }

            m_RealJsonPosts.GetTlvAptPosts();
        }
    }
}
