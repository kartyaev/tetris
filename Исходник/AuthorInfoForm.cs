using System;
using System.Windows.Forms;

namespace Tetris
{
    public partial class AuthorInfoForm : Form
    {
        public AuthorInfoForm()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.lblAuthorInfo = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblAuthorInfo
            // 
            this.lblAuthorInfo.AutoSize = true;
            this.lblAuthorInfo.Location = new System.Drawing.Point(13, 13);
            this.lblAuthorInfo.Name = "lblAuthorInfo";
            this.lblAuthorInfo.Size = new System.Drawing.Size(200, 65);
            this.lblAuthorInfo.TabIndex = 0;
            this.lblAuthorInfo.Text = "ФИО: Картеяв Глеб Владимирович\n" +
                                    "Группа: 6106-090301D\n";
                               
            // 
            // AuthorInfoForm
            // 
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.lblAuthorInfo);
            this.Name = "AuthorInfoForm";
            this.Text = "Об авторе";
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.Label lblAuthorInfo;
    }
}
