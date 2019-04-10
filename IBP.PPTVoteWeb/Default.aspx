<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="IBP.PPTVoteWeb._Default" %>

<%@ Register Src="UserControls/UcDescription.ascx" TagName="UcDescription" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        body
        {
            margin: 0;
            padding: 0;
            font: 12px/24px "宋体";
            color: #333;
        }
        .box
        {
            width: 960px;
            margin: 0 auto;
        }
        .header, .middle
        {
            margin-bottom: 15px;
        }
        .middle
        {
            width: 960px;
            overflow: hidden;
        }
        .middle_top, .middle_bottom
        {
            width: 960px;
            float: left;
        }
        .middle_top
        {
            background: url(imgage/ppt_05.jpg) no-repeat;
            height: 7px;
            overflow: hidden;
        }
        .middle_z
        {
            background: #efefef;
            width: 940px;
            padding: 10px 20px;
            overflow: hidden;
            float: left;
        }
        .middle_bottom
        {
            background: url(imgage/ppt_07.jpg) no-repeat;
            height: 6px;
        }
        .tab
        {
            width: 960px;
        }
        .black_overlay
        {
            display: none;
            position: absolute;
            top: 0%;
            left: 0%;
            width: 100%;
            height: 140%;
            background-color: black;
            z-index: 1001;
            -moz-opacity: 0.8;
            opacity: .80;
            filter: alpha(opacity=80);
        }
        .white_content
        {
            position: absolute;
            top: 25%;
            left: 30%;
            padding: 16px;
            width: 30%;
            border: 5px solid orange;
            background-color: white;
            z-index: 1002;
            overflow: auto;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <asp:Panel ID="fadePanel" runat="server" onclick="this.style.display='none';" CssClass="black_overlay">
                <asp:Panel CssClass="white_content" runat="server" ID="pnlMessage" Visible="false">
                    <asp:Literal runat="server" ID="litMessage"></asp:Literal>
                    <a href="javascript:void(0)" onclick="document.getElementById('<%=pnlMessage.ClientID %>').style.display='none';document.getElementById('<%=fadePanel.ClientID %>').style.display='none'">关闭</a>
                </asp:Panel>
            </asp:Panel>
            <div class="box">
                <div class="header">
                    <img src="imgage/ppt_02.jpg" width="960" height="108" />
                </div>
                <div class="middle">
                    <div class="middle_top">
                    </div>
                    <uc1:UcDescription ID="UcDescription1" runat="server" />
                    <div class="middle_bottom">
                    </div>
                </div>
                <div class="tab">
                    <asp:Literal ID="litWebCome" runat="server"></asp:Literal>
                    <asp:Button ID="btnLogout" runat="server" Text="退出" OnClick="btnLogout_Click" />
                    <asp:GridView ID="gvVoteItem" Width="100%" BorderWidth="0" CellPadding="0" AutoGenerateColumns="false"
                        BackColor="#efefef" CellSpacing="1" runat="server" OnRowEditing="gvVoteItem_RowEditing"
                        DataKeyNames="VoteItemId" OnRowCancelingEdit="gvVoteItem_RowCancelingEdit" OnRowUpdated="gvVoteItem_RowUpdated"
                        OnRowUpdating="gvVoteItem_RowUpdating" OnRowDataBound="gvVoteItem_RowDataBound">
                        <HeaderStyle BackColor="Violet" />
                        <Columns>
                            <asp:TemplateField HeaderText="序号">
                                <ItemStyle BackColor="White" HorizontalAlign="Center" Width="120px" />
                                <ItemTemplate>
                                    <%# Eval("SortOrder").ToString() %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="参赛作品">
                                <ItemStyle BackColor="White" HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <%# Eval("ItemTitle").ToString() %>
                                    <a href='<%# Eval("AttachmentPath").ToString() %>'>下载</a>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="当前已评分人数">
                                <ItemStyle BackColor="White" HorizontalAlign="Center" Width="120px" />
                                <ItemTemplate>
                                    <%# Eval("VoteTotal").ToString()%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="我的评分">
                                <ItemStyle BackColor="White" HorizontalAlign="Center" Width="180px" />
                                <ItemTemplate>
                                    <asp:Literal ID="litMyScore" runat="server"></asp:Literal>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtScore" runat="server"></asp:TextBox>
                                </EditItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="操作">
                                <ItemStyle BackColor="White" HorizontalAlign="Center" Width="180px" />
                                <ItemTemplate>
                                    <asp:LinkButton ID="btnScore" runat="server" Text="评分" CommandName="edit"></asp:LinkButton>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:HiddenField ID="hidVoteItemId" runat="server" />
                                    <asp:LinkButton ID="btnUpdate" runat="server" Text="提交" CommandName="update"></asp:LinkButton>
                                    <asp:LinkButton ID="btnCancel" runat="server" Text="取消" CommandName="cancel"></asp:LinkButton>
                                </EditItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                    <asp:Panel runat="server" ID="pnlLogin" Visible="false" CssClass="white_content">
                        <table>
                            <tr>
                                <td colspan="2">
                                    请登录：
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    工号：
                                </td>
                                <td>
                                    <asp:TextBox ID="txtWorkId" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    密码：
                                </td>
                                <td>
                                    <asp:TextBox ID="txtPwd" runat="server" TextMode="Password"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                </td>
                                <td>
                                    <asp:Button runat="server" ID="btnLogin" Text="登录" OnClick="btnLogin_Click" />
                                    <asp:Button runat="server" ID="btnCancelLogin" Text="取消" OnClick="btnCancelLogin_Click" />
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    </form>
</body>
</html>
