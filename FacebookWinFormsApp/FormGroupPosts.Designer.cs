namespace BasicFacebookFeatures
{
    partial class FormGroupPosts
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.listBoxPostsByFilter = new System.Windows.Forms.ListBox();
            this.richTextBoxSelectedPost = new System.Windows.Forms.RichTextBox();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.SuspendLayout();
            // 
            // listBoxPostsByFilter
            // 
            this.listBoxPostsByFilter.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.listBoxPostsByFilter.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.listBoxPostsByFilter.Font = new System.Drawing.Font("Lucida Handwriting", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listBoxPostsByFilter.FormattingEnabled = true;
            this.listBoxPostsByFilter.HorizontalScrollbar = true;
            this.listBoxPostsByFilter.ItemHeight = 21;
            this.listBoxPostsByFilter.Location = new System.Drawing.Point(13, 36);
            this.listBoxPostsByFilter.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.listBoxPostsByFilter.Name = "listBoxPostsByFilter";
            this.listBoxPostsByFilter.Size = new System.Drawing.Size(952, 191);
            this.listBoxPostsByFilter.TabIndex = 0;
            this.listBoxPostsByFilter.SelectedIndexChanged += new System.EventHandler(this.listPostsByFilter_SelectedIndexChanged);
            // 
            // richTextBoxSelectedPost
            // 
            this.richTextBoxSelectedPost.BackColor = System.Drawing.SystemColors.Info;
            this.richTextBoxSelectedPost.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.richTextBoxSelectedPost.Font = new System.Drawing.Font("Lucida Handwriting", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.richTextBoxSelectedPost.Location = new System.Drawing.Point(13, 226);
            this.richTextBoxSelectedPost.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.richTextBoxSelectedPost.Name = "richTextBoxSelectedPost";
            this.richTextBoxSelectedPost.Size = new System.Drawing.Size(951, 241);
            this.richTextBoxSelectedPost.TabIndex = 1;
            this.richTextBoxSelectedPost.Text = "";
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // FormGroupPosts
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(994, 500);
            this.Controls.Add(this.richTextBoxSelectedPost);
            this.Controls.Add(this.listBoxPostsByFilter);
            this.Font = new System.Drawing.Font("Lucida Handwriting", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Name = "FormGroupPosts";
            this.Text = "FormGroupsByFilter";
            this.Load += new System.EventHandler(this.formGroupsByFilter_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox listBoxPostsByFilter;
        private System.Windows.Forms.RichTextBox richTextBoxSelectedPost;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
    }
}