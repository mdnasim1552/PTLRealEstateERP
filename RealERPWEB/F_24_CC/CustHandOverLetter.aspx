<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="CustHandOverLetter.aspx.cs" Inherits="RealERPWEB.F_24_CC.CustHandOverLetter" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="../CSS/Style.css" rel="stylesheet" type="text/css" />

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script src="../Scripts/jquery-1.4.1.js" type="text/javascript"></script>
    <script type="text/javascript" language="javascript" src="../Scripts/jquery-1.4.1.min.js"></script>
    <script src="../Scripts/jquery.keynavigation.js" type="text/javascript"></script>
    <script type="text/javascript" language="javascript" src="../Scripts/KeyPress.js"></script>
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

        };
    </script>



    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="RealProgressbar">
                <asp:UpdateProgress ID="UpdateProgress2" runat="server" AssociatedUpdatePanelID="UpdatePanel1" DisplayAfter="30">
                    <ProgressTemplate>
                        <div id="loader">
                            <div class="dot"></div>
                            <div class="dot"></div>
                            <div class="dot"></div>
                            <div class="dot"></div>
                            <div class="dot"></div>
                            <div class="dot"></div>
                            <div class="dot"></div>
                            <div class="dot"></div>
                            <div class="lading"></div>
                        </div>
                    </ProgressTemplate>
                </asp:UpdateProgress>
            </div>
            <div class="container moduleItemWrpper">
                <div class="contentPart">
                    <div class="row">
                        <fieldset class="scheduler-border fieldset_A">
                            <div class="form-horizontal">
                                <asp:Panel ID="Panel2" runat="server">

                                    <div class="form-group">
                                        <div class="col-md-8 pading5px asitCol8">
                                            <asp:Label ID="Label8" runat="server" CssClass="lblTxt lblName" Text="Date"></asp:Label>

                                            <asp:TextBox ID="txtCurDate" runat="server" CssClass=" inputtextbox" TabIndex="1"></asp:TextBox>
                                            <cc1:CalendarExtender ID="txtCurDate_CalendarExtender" runat="server" Format="dd-MMM-yyyy"
                                                TargetControlID="txtCurDate"></cc1:CalendarExtender>

                                            <asp:Label ID="Label9" runat="server" CssClass="smLbl_to" Text="No"></asp:Label>

                                            <asp:Label ID="lblCurNo1" runat="server" CssClass="inputtextbox"></asp:Label>

                                            <asp:Label ID="lblCurNo2" runat="server" CssClass="inputtextbox"></asp:Label>

                                            <asp:Label ID="Label11" runat="server" CssClass=" smLbl_to" Text="Ref No."></asp:Label>

                                            <asp:TextBox ID="txtRefNo" runat="server" CssClass="inputtextbox" TabIndex="1"></asp:TextBox>


                                        </div>

                                    </div>

                                    <div class="form-group">
                                        <div class="col-md-8 pading5px asitCol8">
                                            <asp:Label ID="Label4" runat="server" CssClass="lblTxt lblName" Text="Project Name:"></asp:Label>

                                            <asp:TextBox ID="txtSrcPro" runat="server" CssClass=" inputtextbox" TabIndex="1"></asp:TextBox>

                                            <asp:LinkButton ID="ibtnFindProject" CssClass="btn btn-primary srearchBtn" runat="server" OnClick="ibtnFindProject_Click" TabIndex="2"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>

                                            <asp:DropDownList ID="ddlProjectName" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlProjectName_SelectedIndexChanged" Width="300px" CssClass="ddlPage">
                                            </asp:DropDownList>

                                            <asp:LinkButton ID="lbtnOk" runat="server" CssClass="btn btn-primary okBtn" OnClick="lbtnOk_Click" TabIndex="4">Ok</asp:LinkButton>


                                        </div>

                                    </div>

                                    <div class="form-group">
                                        <div class="col-md-8 pading5px asitCol8">
                                            <asp:Label ID="Label6" runat="server" CssClass="lblTxt lblName" Text=" Flat No.:"></asp:Label>

                                            <asp:TextBox ID="txtsrchUnitName" runat="server" CssClass=" inputtextbox" TabIndex="1"></asp:TextBox>

                                            <asp:LinkButton ID="ibtnFindUnitName" CssClass="btn btn-primary srearchBtn" runat="server" OnClick="ibtnFindUnitName_Click" TabIndex="2"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>

                                            <asp:DropDownList ID="ddlUnitName" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlUnitName_SelectedIndexChanged" Width="300px" CssClass="ddlPage">
                                            </asp:DropDownList>

                                            <asp:Label ID="lmsg" runat="server" CssClass="btn btn-danger primaryBtn"></asp:Label>


                                        </div>

                                    </div>

                                    <div class="form-group">
                                        <div class="col-md-8 pading5px asitCol8">
                                            <asp:Label ID="lblPreList" runat="server" CssClass="lblTxt lblName" Text="Previous List:"></asp:Label>

                                            <asp:TextBox ID="txtSrchPreviousList" runat="server" CssClass=" inputtextbox" TabIndex="1"></asp:TextBox>

                                            <asp:LinkButton ID="ImgbtnFindPreviousList" CssClass="btn btn-primary srearchBtn" runat="server" OnClick="ImgbtnFindPreviousList_Click" TabIndex="2"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>

                                            <asp:DropDownList ID="ddlPrevList" runat="server" Width="300px" CssClass="ddlPage"></asp:DropDownList>

                                        </div>

                                    </div>

                                </asp:Panel>
                            </div>
                        </fieldset>
                    </div>
                    <div class="table table-responsive">
                           <asp:GridView ID="gvHLET" runat="server" AutoGenerateColumns="False" ShowFooter="True" CssClass=" table-striped table-hover table-bordered grvContentarea"
                Width="543px">

                <Columns>
                    <asp:TemplateField HeaderText="Sl.No.">
                        <ItemTemplate>
                            <asp:Label ID="lblgvSlNo3" runat="server" Font-Bold="True" ForeColor="Black" Height="16px"
                                Style="text-align: right" Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>'
                                Width="50px"></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Description of Item">
                        <FooterTemplate>
                            <%-- <asp:LinkButton ID="lFinalUpdateAdWork" runat="server" Font-Bold="True" 
                                            Font-Size="12px" ForeColor="#000" onclick="lFinalUpdateAdWork_Click"> Update Work</asp:LinkButton>--%>
                        </FooterTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lgdescw" runat="server" ForeColor="Black"
                                Text='<%# "<B>"+ Convert.ToString(DataBinder.Eval(Container.DataItem, "mgdesc")) + "</B>"+
                                                                         (DataBinder.Eval(Container.DataItem, "gdesc").ToString().Trim().Length>0 ? 
                                                                         (Convert.ToString(DataBinder.Eval(Container.DataItem, "mgdesc")).Trim().Length>0 ?  "<br>" : "")+                                                             
                                                                         "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"+ 
                                                                         Convert.ToString(DataBinder.Eval(Container.DataItem, "gdesc")).Trim(): "") 
                                                                         
                                                                    %>'
                                Width="200px">
                                            
                                            
                            </asp:Label>




                        </ItemTemplate>
                        <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Complete">
                        <ItemTemplate>
                            <asp:CheckBox ID="chkcomplete" runat="server" Checked='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "complete"))=="True" %>'
                                Width="20px" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Incomplete">
                        <ItemTemplate>
                            <asp:CheckBox ID="chkincomplete" runat="server" Checked='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "incomplete"))=="True" %>'
                                Width="20px" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Sign of Owner">

                        <FooterTemplate>
                            <asp:LinkButton ID="lbtnFinalUpdate" runat="server"  CssClass="btn btn-danger primaryBtn" OnClick="lbtnFinalUpdate_Click">Final Update</asp:LinkButton>
                        </FooterTemplate>

                        <ItemTemplate>
                            <asp:TextBox ID="txtgvsign" runat="server" ForeColor="Black" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "signowner")) %>'
                                Width="150px" BackColor="Transparent" BorderStyle="None" Style="font-size: 11px;"></asp:TextBox>
                        </ItemTemplate>
                        <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                    </asp:TemplateField>
                </Columns>
                <FooterStyle CssClass="grvFooter" />
                <EditRowStyle />
                <AlternatingRowStyle />
                <PagerStyle CssClass="gvPagination" />
                <HeaderStyle CssClass="grvHeader" />

            </asp:GridView>
                    </div>
                </div>
            </div>

        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>


