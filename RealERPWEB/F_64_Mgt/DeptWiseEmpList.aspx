<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="DeptWiseEmpList.aspx.cs" Inherits="RealERPWEB.F_64_Mgt.DeptWiseEmpList" %>

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
                            <div class="form-group">
                                <div class="col-md-3 pading5px asitCol3">
                                    <asp:Label ID="lblDept" runat="server" CssClass="lblTxt lblName">Dept Name</asp:Label>
                                    <asp:TextBox ID="txtsrchDept" runat="server" CssClass="  inputtextbox"></asp:TextBox>
                                    <asp:LinkButton ID="ImgbtnFindDept" runat="server" CssClass="btn btn-primary srearchBtn" OnClick="ImgbtnFindDept_Click"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>

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
                                <div class="clearfix"></div>
                            </div>
                            <div class="form-group">
                                <div class="col-md-4 pading5px ">

                                    <asp:Label ID="lblfrmdate" runat="server" CssClass="lblTxt lblName" Text="Month:"></asp:Label>

                                    <asp:DropDownList ID="ddlyearmon" runat="server" AutoPostBack="True"
                                        TabIndex="11" CssClass="ddlPage" style="width:124px;">
                                    </asp:DropDownList>

                                    <asp:Label ID="lblPage" runat="server" CssClass=" smLbl_to" Text="Page Size" Visible="false"></asp:Label>

                                    <asp:DropDownList ID="ddlpagesize" runat="server" AutoPostBack="True" CssClass=" smDropDown"
                                        BackColor="#CCFFCC" Font-Bold="True" OnSelectedIndexChanged="ddlpagesize_SelectedIndexChanged" Visible="False"
                                        Width="70px">
                                        <asp:ListItem Value="10">10</asp:ListItem>
                                        <asp:ListItem Value="15">15</asp:ListItem>
                                        <asp:ListItem Value="20">20</asp:ListItem>
                                        <asp:ListItem Value="30">30</asp:ListItem>
                                        <asp:ListItem Value="50">50</asp:ListItem>
                                        <asp:ListItem Value="100">100</asp:ListItem>
                                        <asp:ListItem Value="150">150</asp:ListItem>
                                        <asp:ListItem Value="200">200</asp:ListItem>
                                        <asp:ListItem Value="300">300</asp:ListItem>
                                        <asp:ListItem Value="400">400</asp:ListItem>
                                    </asp:DropDownList>


                                </div>
                            </div>

                        </fieldset>

                        <asp:GridView ID="gvDepWiseEmp" runat="server" CssClass="table-striped table-hover table-bordered grvContentarea"
                            AutoGenerateColumns="False" ShowFooter="True" Width="16px" AllowPaging="True" OnPageIndexChanging="gvDepWiseEmp_PageIndexChanging" OnRowDataBound="gvDepWiseEmp_RowDataBound" OnDataBound="gvDepWiseEmp_DataBound">
                            <RowStyle />
                            <Columns>
                                <asp:TemplateField HeaderText="Sl">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvSlNo0" runat="server" Height="16px"
                                            Style="text-align: right"
                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="35px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>


                                <asp:TemplateField HeaderText="bactcode Code" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvempid" runat="server" Height="16px"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "empid")) %>'
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Employee Name">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvEmpname" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "empname")) %>'
                                            Width="250px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle HorizontalAlign="Left" />
                                    <HeaderStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Designation">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvDesg" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "desg")) %>'
                                            Width="150px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle HorizontalAlign="Left" />
                                    <HeaderStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Joining Date">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvJoindat" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "joindat")) %>'
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle HorizontalAlign="Left" />
                                    <HeaderStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Setup">
                                    <ItemTemplate>

                                        <asp:HyperLink ID="lblgvSetup" runat="server" Target="_blank"
                                            Text='Setup'
                                            Width="80px"></asp:HyperLink>
                                    </ItemTemplate>
                                    <FooterStyle HorizontalAlign="Left" />
                                    <HeaderStyle HorizontalAlign="Left" />
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

