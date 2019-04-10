using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IBP.Models;
using IBP.Services;
using IBP.Common;
using Framework.Utilities;

namespace IBP.PPTVoteWeb
{
    public partial class _Default : System.Web.UI.Page
    {
        private Dictionary<string, RelUserVoteitemModel> userVoteReuslt = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindVoteItemData();
            }
        }

        protected void ShowMessageBox(string message)
        {
            fadePanel.Visible = true;
            fadePanel.Style[HtmlTextWriterStyle.Display] = "block";
            pnlLogin.Visible = false;

            pnlMessage.Visible = true;
            litMessage.Text = message;
        }

        protected void ShowLoginBox()
        {
            fadePanel.Visible = true;
            fadePanel.Style[HtmlTextWriterStyle.Display] = "block";
            pnlLogin.Visible = true;
            pnlMessage.Visible = false;
            litMessage.Text = "";
        }

        protected void HideLoginBox()
        {
            fadePanel.Visible = false;
            fadePanel.Style[HtmlTextWriterStyle.Display] = "none";
            pnlLogin.Visible = false;
            pnlMessage.Visible = false;
            litMessage.Text = "";
        }


        protected void HideMessageBox()
        {
            fadePanel.Visible = false;
            fadePanel.Style[HtmlTextWriterStyle.Display] = "none";
            pnlMessage.Visible = false;
            litMessage.Text = "";
        }

        protected void gvVoteItem_RowEditing(object sender, GridViewEditEventArgs e)
        {
            if (SessionUtil.Current == null)
            {
                ShowLoginBox();
                return;
            }

            if (SessionUtil.Current.IsLogin == false)
            {
                ShowLoginBox();
                return;
            }

            HideLoginBox();
            HideMessageBox();
            gvVoteItem.EditIndex = e.NewEditIndex;
            BindVoteItemData();

        }

        private void BindVoteItemData()
        {
            List<VoteProjectItemInfoModel> list = VoteProjectItemInfoService.Instance.GetVoteItemListByProjectId("A0B4B4C5-B196-48E2-B00D-7E50921E0675");
            if (list != null)
            {
                userVoteReuslt = VoteProjectItemInfoService.Instance.GetUserVoteList("A0B4B4C5-B196-48E2-B00D-7E50921E0675", (SessionUtil.Current == null) ? null : SessionUtil.Current.UserId);

                gvVoteItem.DataSource = list;
                gvVoteItem.DataBind();

            }

            if (SessionUtil.Current != null)
            {
                litWebCome.Text = string.Format("{0}, 欢迎光临！", SessionUtil.Current.CnName);
                btnLogout.Visible = true;
            }
            else
            {
                litWebCome.Text = "欢迎光临！请登录后进行投票评分，谢谢！";
                btnLogout.Visible = false;
            }
        }

        protected void gvVoteItem_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvVoteItem.EditIndex = -1;
            BindVoteItemData();
        }

        protected void gvVoteItem_RowUpdated(object sender, GridViewUpdatedEventArgs e)
        {
            
        }

        protected void gvVoteItem_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            if (SessionUtil.Current == null)
            {
                ShowLoginBox();
                return;
            }

            if (SessionUtil.Current.IsLogin == false)
            {
                ShowLoginBox();
                return;
            }
            
            GridViewRow row = gvVoteItem.Rows[e.RowIndex];
            TextBox txtScore = (TextBox)row.Cells[3].FindControl("txtScore");

            if(CharacterUtil.isNumber(txtScore.Text.Trim()) == false)
            {
                ShowMessageBox("请填写一个0至100的数字分数值");
                return;
            }

            int score = Convert.ToInt32(txtScore.Text.Trim());
            if (score < 0 || score> 100)
            {
                ShowMessageBox("请填写一个0至100的数字分数值");
                return;
            }

            string message = "操作失败，请与管理员联系";
            //HiddenField hidVoteItemId = (HiddenField)row.Cells[5].FindControl("hidVoteItemId");

            RelUserVoteitemModel voteInfo = new RelUserVoteitemModel();
            voteInfo.UserId = SessionUtil.Current.UserId;
            voteInfo.VoteProjectId = "A0B4B4C5-B196-48E2-B00D-7E50921E0675";
            voteInfo.VoteItemId = gvVoteItem.DataKeys[e.RowIndex].Value.ToString();
            voteInfo.Score = txtScore.Text;
            voteInfo.Status = 0;

            if (VoteProjectItemInfoService.Instance.ProjectItemVote(voteInfo, out message))
            {
                ShowMessageBox(message);
                gvVoteItem.EditIndex = -1;
                BindVoteItemData();
            }
            else
            {
                ShowMessageBox(message);
            }
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            LoginStatusEnum loginStatus = UserInfoService.Instance.UserLogin(txtWorkId.Text.Trim(), txtPwd.Text.Trim());

            switch (loginStatus)
            {
                case LoginStatusEnum.Success:
                    HideLoginBox();
                    BindVoteItemData();
                    break;

                default:
                    ShowMessageBox("登录失败，请检查工号及密码是否正确，或与管理员联系");
                    break;
            }
        }

        protected void btnCancelLogin_Click(object sender, EventArgs e)
        {
            HideLoginBox();
        }

        protected void btnLogout_Click(object sender, EventArgs e)
        {
            SessionUtil.Current = null;
            BindVoteItemData();
        }

        protected void gvVoteItem_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string VoteItemId = (string)DataBinder.GetPropertyValue(e.Row.DataItem, "VoteItemId");
                Literal litMyScore = (Literal)e.Row.FindControl("litMyScore");
                TextBox txtScore = (TextBox)e.Row.FindControl("txtScore");
                HiddenField hidVoteItemId = (HiddenField)e.Row.FindControl("hidVoteItemId");

                if (e.Row.RowState == DataControlRowState.Normal || e.Row.RowState == DataControlRowState.Alternate || e.Row.RowState == DataControlRowState.Selected)
                {
                    if (SessionUtil.Current == null)
                    {
                        litMyScore.Text = "-";
                    }
                    else
                    {
                        string VoteScore = userVoteReuslt.ContainsKey(VoteItemId) ? userVoteReuslt[VoteItemId].Score : "请评分";
                        litMyScore.Text = VoteScore;
                    }
                }
                else if (e.Row.RowState == DataControlRowState.Edit)
                {
                    if (hidVoteItemId != null)
                    {
                        hidVoteItemId.Value = VoteItemId;
                    }

                    string VoteScore = userVoteReuslt.ContainsKey(VoteItemId) ? userVoteReuslt[VoteItemId].Score : "";
                    txtScore.Text = VoteScore;
                }
            }
        }

        
    }
}