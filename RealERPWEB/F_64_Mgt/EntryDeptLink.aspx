<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="EntryDeptLink.aspx.cs" Inherits="RealERPWEB.F_64_Mgt.EntryDeptLink" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">



    <script type="text/javascript" language="javascript">
        $(document).ready(function () {
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);


        });
        function pageLoaded() {

            $("input, select").bind("keydown", function (event) {
                var k1 = new KeyPress();
                k1.textBoxHandler(event);
            });


        }

    </script>



     
    
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>

            <div class="container moduleItemWrpper">
                <div class="contentPart">
                    <div class="row">
                        <fieldset class="scheduler-border fieldset_A">

                            <div class="col-md-3 pading5px asitCol3">
                                <asp:Label ID="lblDept" runat="server" CssClass="lblTxt lblName">Dept Name</asp:Label>
                                <asp:TextBox ID="txtsrchDept" runat="server" CssClass="  inputtextbox"></asp:TextBox>
                                <asp:LinkButton ID="ImgbtnFindDept" runat="server" CssClass="btn btn-primary srearchBtn colMdbtn" OnClick="ImgbtnFindDept_Click"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>

                            </div>
                            <div class="col-md-4 pading5px ">
                                <asp:DropDownList ID="ddldeptlist" runat="server" CssClass="form-control inputTxt">
                                </asp:DropDownList>
                            </div>

                            <div class="col-md-1 pading5px asitCol3">
                                <div class="colMdbtn pading5px">
                                    <asp:LinkButton ID="lbtnOk" runat="server" CssClass="btn btn-primary okBtn" OnClick="lbtnOk_Click">Ok</asp:LinkButton>
                                </div>

                            </div>
                            <div class="col-md-3 pading5px asitCol3">
                                <div class="colMdbtn pading5px">
                                    <asp:Label ID="lblmsg1" CssClass="btn-danger btn  primaryBtn" runat="server"></asp:Label>
                                </div>
                            </div>
                        </fieldset>
                        <asp:Panel ID="Panel2" runat="server" Visible="False">
                            <fieldset class="scheduler-border fieldset_A">
                                <div class="col-md-3 pading5px asitCol3">
                                    <asp:Label ID="lblhrdept" runat="server" CssClass="lblTxt lblName">Group</asp:Label>
                                    <asp:TextBox ID="txtsrchhrdept" runat="server" CssClass=" inputtextbox"></asp:TextBox>
                                    <asp:LinkButton ID="Imgbtnhrdept" runat="server" CssClass="btn btn-primary srearchBtn colMdbtn" OnClick="Imgbtnhrdept_Click"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>

                                </div>
                                <div class="col-md-4 pading5px ">
                                    <asp:DropDownList ID="ddlhrdept" runat="server" CssClass="form-control inputTxt" OnSelectedIndexChanged="ddlhrdept_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </div>



                                <div class="col-md-1 pading5px">

                                    <div class="colMdbtn pading5px">
                                        <asp:LinkButton ID="lbtnSelect" runat="server" CssClass="btn btn-primary primaryBtn" OnClick="lbtnSelect_Click">Select</asp:LinkButton>
                                    </div>
                                    </div>


                              
                                 <div class="col-md-1 pading5px">
                                     <div class="colMdbtn pading5px">
                                        <asp:LinkButton ID="lbtnSelectall" runat="server"  CssClass="btn btn-primary primaryBtn" OnClick="lbtnSelectall_Click">Select ALl</asp:LinkButton>
                                    </div>
                                     </div>

                            </fieldset>
                         </asp:Panel>
                            <asp:GridView ID="gvProLinkInfo" runat="server" CssClass="table-striped table-hover table-bordered grvContentarea"
                                AutoGenerateColumns="False" ShowFooter="True" Width="16px"
                                OnRowDeleting="gvProLinkInfo_RowDeleting">
                                <PagerSettings Visible="False" />
                                <RowStyle />
                                <Columns>
                                    <asp:TemplateField HeaderText="Sl.No.">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvSlNo0" runat="server" Height="16px"
                                                Style="text-align: right"
                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="35px"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:CommandField ShowDeleteButton="True" />

                                    <asp:TemplateField HeaderText="bactcode Code" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvBancCode" runat="server" Height="16px"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actcode")) %>'
                                                Width="80px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Group Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvSuplDesc1" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")) %>'
                                                Width="350px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Left" />
                                        <HeaderStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Remarks">
                                        <FooterTemplate>
                                            <asp:LinkButton ID="lbtnUpdate" runat="server" Font-Bold="True" CssClass=" btn  btn-danger primarygrdBtn" OnClick="lbtnUpdate_Click">Final Update</asp:LinkButton>
                                        </FooterTemplate>
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvSuplRemarks" runat="server" BorderColor="#99CCFF"
                                                BorderStyle="Solid" BorderWidth="0px" Font-Size="11px"
                                                Style="text-align: left; background-color: Transparent"
                                                Text='<%# DataBinder.Eval(Container.DataItem, "remarks").ToString() %>'
                                                Width="150px"></asp:TextBox>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="left" />
                                        <HeaderStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                </Columns>
                                <FooterStyle BackColor="#F5F5F5" />
                                <EditRowStyle />
                                <AlternatingRowStyle />
                                <PagerStyle CssClass="gvPagination" />
                                <HeaderStyle BackColor="#5F9467" ForeColor="#ffffff" />
                            </asp:GridView>
                       
                    </div>
                </div>
            </div>

        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

