<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="AccUserCash.aspx.cs" Inherits="RealERPWEB.F_34_Mgt.AccUserCash" %>

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
            <div class="card mt-4 pb-4">
                <div class="card-body">
                    <div class="row">
                        

                            <div class="col-md-3 d-none">
                               
                                <asp:TextBox ID="txtUserSearch1" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                                <asp:LinkButton ID="ImgbtnFindUser1" runat="server" CssClass="btn btn-primary srearchBtn colMdbtn" OnClick="ImgbtnFindUser1_Click"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>

                            </div>
                            <div class="col-md-3">
                                 <asp:Label ID="lblUser1" runat="server" CssClass="form-label">User Name</asp:Label>
                                <asp:DropDownList ID="ddlUserList" runat="server" CssClass="chzn-select form-control form-control-sm">
                                </asp:DropDownList>
                            </div>

                            <div class="col-md-2 ml-2" style="margin-top:25px;">
                               
                                    <asp:LinkButton ID="lbtnOk" runat="server" CssClass="btn btn-sm btn-primary" OnClick="lbtnOk_Click">Ok</asp:LinkButton>
                               
                            </div>
                            <div class="col-md-3 pading5px asitCol3 d-none">
                                <div class="colMdbtn pading5px">
                                    <asp:Label ID="lblmsg1" CssClass="btn-danger btn disabled primaryBtn" runat="server"></asp:Label>
                                </div>
                            </div>
                       </div>
                    </div>
                </div>
              <div class="card" style="min-height:480px;">
                <div class="card-body">
                    
                        <asp:Panel ID="Panel2" runat="server" Visible="False">
                          <div class="row">
                              <div class="col-md-3 d-none">
                                    
                                    <asp:TextBox ID="txtProSearch" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>

                                </div>
                                <div class="col-md-3">
                                    <asp:Label ID="lblConTrolCode" runat="server" CssClass="form-label">Control Code</asp:Label>
                                    <asp:LinkButton ID="ImgbtnFindProject" runat="server"  OnClick="ImgbtnFindProject_Click"><span class="fa fa-search"> </span></asp:LinkButton>

                                    <asp:DropDownList ID="ddlConTrolCode" runat="server" CssClass="form-control form-control-sm">
                                    </asp:DropDownList>
                                </div>



                                <div class="col-md-1" style="margin-top:20px;">

                                        <asp:LinkButton ID="lbtnSelectSupl1" runat="server" Style="margin: 0;" CssClass="btn btn-sm btn-primary" OnClick="lbtnSelectSupl1_Click">Select</asp:LinkButton>
                                   
                                </div>
                                <div class="col-md-1" style="margin-top:20px;">
                                    
                                        <asp:LinkButton ID="lbtnSelectAll" runat="server" Style="margin: 0;" CssClass="btn btn-sm btn-primary" OnClick="lbtnSelectAll_Click">Select All</asp:LinkButton>

                                </div>
                          </div>
                                
                            <div class="row mt-4" style="margin-left:10px;">
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
                                    <asp:CommandField ShowDeleteButton="True" ControlStyle-CssClass="fa fa-trash text-red btn-xs" DeleteText="" />

                                    <asp:TemplateField HeaderText="bactcode Code" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvBancCode" runat="server" Height="16px"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actcode")) %>'
                                                Width="80px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Bank Name">
                                        <FooterTemplate>
                                            <asp:LinkButton ID="lbtnDeleteAll" runat="server" Font-Bold="True"
                                                Font-Size="13px" Height="16px" OnClick="lbtnDeleteAll_Click"
                                                Style="text-align: center;" Width="90px">Delete All</asp:LinkButton>
                                        </FooterTemplate>
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
                                            <asp:LinkButton ID="lbtnUpdate" runat="server" Font-Bold="True"
                                                Font-Size="13px" OnClick="lbtnUpdate_Click"
                                                Style="text-align: center; height: 15px;" Width="90px">Final Update</asp:LinkButton>
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

                            
                        </asp:Panel>
                    
                </div>
            </div>

        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

