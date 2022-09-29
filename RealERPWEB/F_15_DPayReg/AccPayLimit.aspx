
<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="AccPayLimit.aspx.cs" Inherits="RealERPWEB.F_15_DPayReg.AccPayLimit" %>

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
            $('.chzn-select').chosen({ search_contains: true });
           
        }

    </script>



    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>


            <div class="card mt-4 pb-4">
                <div class="card-body">
                    <div class="row">

                        
                                    <div class="col-md-3 pading5px asitCol3 d-none">
                                        <asp:Label ID="lblUser" runat="server" CssClass="lblTxt lblName">User Name</asp:Label>
                                        <asp:TextBox ID="txtUserSearch1" runat="server" CssClass=" inputtextbox"></asp:TextBox>
                                        <asp:LinkButton ID="ImgbtnFindUser1" runat="server" CssClass="btn btn-primary srearchBtn" OnClick="ImgbtnFindUser1_Click"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>

                                    </div>
                                    <div class="col-md-3">
                                        <asp:Label ID="Label1" runat="server" CssClass="form-label">User Name</asp:Label>
                                        <asp:DropDownList ID="ddlUserList" runat="server" CssClass="form-control form-control-sm chzn-select">
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-md-2 ml-4 mt-4">
                                        <asp:LinkButton ID="LinkButton1" runat="server" CssClass="btn btn-sm btn-primary" OnClick="lbtnSelectSupl1_Click">Select</asp:LinkButton>
                                        <asp:LinkButton ID="lbtnSelectAll" runat="server" CssClass="btn btn-sm btn-primary" OnClick="lbtnSelectAll_Click">Select All</asp:LinkButton>

                                        
                                    </div>


                                     <div class="col-md-3 pading5px pull-right">
                                            <asp:Label ID="lblmsg1" CssClass="btn-danger btn  primaryBtn" runat="server" Visible="false"></asp:Label>    
                                        
                                    </div>


                               </div>
                    </div>
                </div>
            <div class="card" style="min-height:480px;">
                <div class="card-body">
                    <div class="row">
                        <asp:GridView ID="gvPayLimit" runat="server" AutoGenerateColumns="False" ShowFooter="True"
                            Width="16px" OnRowDeleting="gvPayLimit_RowDeleting" CssClass="table table-striped table-hover table-bordered grvContentarea">
                            <PagerSettings Visible="False" />
                            <RowStyle />
                            <Columns>
                                <asp:TemplateField HeaderText="Sl.No.">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvSlNo0" runat="server" Height="16px" Style="text-align: right"
                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="35px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:CommandField ShowDeleteButton="True" DeleteText="" ControlStyle-CssClass="fa fa-trash text-red btn-xs"/>
                                <asp:TemplateField HeaderText="userid" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvUserid" runat="server" Height="16px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "userid")) %>'
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="User Name">

                                    <ItemTemplate>
                                        <asp:Label ID="lblgvSuplDesc1" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "userdesc")) %>'
                                            Width="180px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle HorizontalAlign="Left" />
                                    <HeaderStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Min Amout">
                                    <FooterTemplate>
                                        <asp:LinkButton ID="lbtnUpdate" runat="server" Font-Bold="True" Font-Size="13px"
                                            OnClick="lbtnUpdate_Click" Style="text-align: center; height: 15px;" Width="90px">Final Update</asp:LinkButton>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtgvMinamt" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                            BorderWidth="0px" Font-Size="11px" Style="text-align: right; background-color: Transparent"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "minamt")).ToString("#,##0;-#,##0; ") %>'
                                            Width="80px"></asp:TextBox>
                                    </ItemTemplate>
                                    <FooterStyle HorizontalAlign="left" />
                                    <HeaderStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Max Amout">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtgvMaxamt" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                            BorderWidth="0px" Font-Size="11px" Style="text-align: right; background-color: Transparent"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "maxamt")).ToString("#,##0;-#,##0; ") %>'
                                            Width="80px"></asp:TextBox>
                                    </ItemTemplate>
                                    <FooterStyle HorizontalAlign="left" />
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
                <!-- End of contentpart-->
            </div>
            <!-- End of Container-->

        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
