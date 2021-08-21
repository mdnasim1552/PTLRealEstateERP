<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="FxtAstGinf.aspx.cs" Inherits="RealERPWEB.F_29_Fxt.FxtAstGinf" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <link href="CSS/PageInformation.css" rel="stylesheet" type="text/css" />

    <link href="CSS/PageInformation.css" rel="stylesheet" type="text/css" />

    <link href="../CSS/PageInformation.css" rel="stylesheet" type="text/css" />
    <link href="../CSS/Style.css" rel="stylesheet" type="text/css" />


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">


    <%--<asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>--%>
    <div class="container moduleItemWrpper">
        <div class="contentPartSmall row">
            <fieldset class="scheduler-border fieldset_A">
                <div class="form-horizontal">
                    <asp:Panel ID="Panel1" runat="server" Visible="true">
                        <div class="form-group">
                            <div class="col-md-10  pading5px  asitCol10">
                                <asp:Label ID="Label4" runat="server" CssClass=" lblName lblTxt" Text="Fixed Asset:"></asp:Label>

                                <asp:TextBox ID="txtSrcFAst" runat="server" CssClass="inputtextbox"></asp:TextBox>


                                <asp:LinkButton ID="ibtnFindAst" runat="server" CssClass="btn btn-primary primaryBtn" OnClick="ibtnFindProject_Click"><span class="glyphicon glyphicon-search asitGlyp"></span></asp:LinkButton>

                                <asp:DropDownList ID="ddlFAstName" runat="server" CssClass="ddlPage" Width="300px"></asp:DropDownList>

                                <asp:LinkButton ID="lbtnOk" runat="server" CssClass="btn btn-primary okBtn" OnClick="lbtnOk_Click">Ok</asp:LinkButton>

                                <asp:Label ID="lblmsg" runat="server" CssClass="btn btn-danger primaryBtn"></asp:Label>

                                 <asp:Label ID="Label1" runat="server" CssClass=" lblName lblTxt" >Serial No :</asp:Label>
                                 <asp:Label ID="lblserial" runat="server" CssClass=" lblName" ></asp:Label>

                            </div>
                        </div>
                    </asp:Panel>
                </div>
            </fieldset>

        </div>

        <div class="form-group">
            <div class="col-sm-9 col-md-9 col-lg-9">
                 <div class="table table-responsive">
                      <asp:GridView ID="gvFAstInf" runat="server" AutoGenerateColumns="False" CssClass=" table-striped table-hover table-bordered grvContentarea"
                ShowFooter="True" Width="831px">

                <Columns>
                   <%--<asp:TemplateField HeaderText="Sl.No.">
                        <ItemTemplate>
                            <asp:Label ID="lblgvSlNo0" runat="server" Font-Bold="True" Height="16px"
                                Style="text-align: right"
                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="50px"></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                    </asp:TemplateField>--%>
                    <asp:TemplateField HeaderText="Code" Visible="False">
                        <ItemTemplate>
                            <asp:Label ID="lblgvItmCode" runat="server" Height="16px"
                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "fxtgcod")) %>'
                                Width="49px"></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Main Group">
                        <ItemTemplate>
                            <asp:Label ID="lgcResDesc1m" runat="server"
                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gdesc")) %>'
                                Width="200px"></asp:Label>
                        </ItemTemplate>
                        <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Details Group">
                        <ItemTemplate>
                            <asp:Label ID="lgcResDesc1" runat="server"
                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "fxtgdesc")) %>'
                                Width="200px"></asp:Label>
                        </ItemTemplate>
                        <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:Label ID="lgp" runat="server" Font-Bold="True" Font-Size="12px"
                                Height="16px"
                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gph")) %>'
                                Width="2px"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Type" Visible="False">
                        <ItemTemplate>
                            <asp:Label ID="lgvgval" runat="server"
                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "fxtgval")) %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <FooterTemplate>
                            <asp:LinkButton ID="lUpdatPerInfo" runat="server" OnClick="lUpdatPerInfo_Click" CssClass="btn btn-danger primaryBtn">Update Information</asp:LinkButton>
                        </FooterTemplate>
                        <ItemTemplate>
                            <asp:TextBox ID="txtgvVal" runat="server" BackColor="Transparent"
                                BorderColor="#660033" BorderStyle="None" BorderWidth="1px" Height="20px"
                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gdesc1")) %>'
                                Width="510px"></asp:TextBox>

                                <asp:TextBox ID="txtgvdVal" runat="server" BackColor="Transparent"
                                BorderColor="#660033" BorderStyle="None" BorderWidth="1px" Height="20px"
                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gdesc1")) %>'
                                Width="510px"></asp:TextBox>

                             <cc1:CalendarExtender ID="txtgvdVal_CalendarExtender" runat="server"
                              Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txtgvdVal">
                             </cc1:CalendarExtender>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                    </asp:TemplateField>
                </Columns>

                <FooterStyle CssClass="grvFooter" />
                <EditRowStyle />
                <AlternatingRowStyle />
                <PagerStyle CssClass="gvPagination" />
                <HeaderStyle CssClass="grvHeader" />
            </asp:GridView>


            <div class="form-group">
                <div class="col-md-8  pading5px  asitCol8">
                    <cc1:ListSearchExtender ID="ListSearchExtender2" runat="server"
                        QueryPattern="Contains" TargetControlID="ddlFAstName">
                    </cc1:ListSearchExtender>
                </div>
            </div>
                 </div>
            </div>
            <div class="col-sm-3 col-md-3 col-lg-3">
                <asp:Panel ID="imgpannel" Visible="false" runat="server">
                <fieldset class="form-group" style="margin-top:80px;">
                    <asp:Image ID="imgFix" runat="server"  Height="80" Width="150"/>
                    <legend style="font-size: 14px;">Image Upload</legend>

                    <cc2:AsyncFileUpload OnClientUploadError="uploadError"
                        OnClientUploadComplete="uploadComplete" runat="server"
                        ID="AsyncFileUpload1" UploaderStyle="Modern"
                        CompleteBackColor="White"
                        UploadingBackColor="#CCFFFF" ThrobberID="imgLoader"
                        OnUploadedComplete="FileUploadComplete" />

                    <asp:Image ID="imgLoader" runat="server" ImageUrl="~/images/Wait.gif" />
                    <br />
                    <asp:Button ID="btnShowimg" runat="server" Visible="false" CssClass="btn btn-success btn-sm" Text="Show Image" />
                    <asp:Label ID="lblMesg" runat="server" Text=""></asp:Label>
                </fieldset>
            </asp:Panel>
            </div>

        </div>

       

           

            
       
    </div>






    <%--         
        </ContentTemplate>
    </asp:UpdatePanel>--%>

    <script language="javascript" type="text/javascript">

        $(document).ready(function () {
            //For navigating using left and right arrow of the keyboard
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);
        });
        function pageLoaded() {

            $("input, select").bind("keydown", function (event) {
                var k1 = new KeyPress();
                k1.textBoxHandler(event);
            });
        }
    </script>
    <script type="text/javascript">
        function uploadComplete(sender) {
            $get("<%=lblMesg.ClientID%>").style.color = "green";
            $get("<%=lblMesg.ClientID%>").innerHTML = "File Uploaded Successfully";
        }

        function uploadError(sender) {
            $get("<%=lblMesg.ClientID%>").style.color = "red";
            $get("<%=lblMesg.ClientID%>").innerHTML = "File upload failed.";
        }


    </script>

</asp:Content>





