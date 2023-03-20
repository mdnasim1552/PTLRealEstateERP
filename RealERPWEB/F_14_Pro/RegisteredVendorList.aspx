<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="RegisteredVendorList.aspx.cs" Inherits="RealERPWEB.F_14_Pro.RegisteredVendorList" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
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

            <div class="card card-fluid">
                <div class="card-body">
                    <div class="row">
                        <div class="col-md-1">
                            <div class="form-group">
                                <asp:Label ID="Label1" runat="server">From</asp:Label>
                                <asp:TextBox ID="txtfrmdate" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                                <cc1:CalendarExtender runat="server" Format="dd-MMM-yyyy" TargetControlID="txtfrmdate"></cc1:CalendarExtender>
                            </div>
                        </div>
                        <div class="col-md-1">
                            <div class="form-group">
                                <asp:Label ID="Label2" runat="server">To</asp:Label>
                                <asp:TextBox ID="txttodate" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                                <cc1:CalendarExtender runat="server" Format="dd-MMM-yyyy" TargetControlID="txttodate"></cc1:CalendarExtender>
                            </div>
                        </div>
                        <div class="col-md-1 text-center" style="margin-top: 20px">
                            <asp:Button runat="server" ID="btnOk" CssClass="btn btn-sm btn-primary" Text="Ok" />
                        </div>
                    </div>
                </div>
            </div>

            <div class="card card-fluid">
                <div class="card-body" style="min-height: 450px;">
                    <div class="row">
                        <div class="table-responsive">
                            <asp:GridView ID="gvRegVendorList" runat="server" AllowPaging="True" CssClass=" table-striped table-hover table-bordered grvContentarea"
                                AutoGenerateColumns="False" ShowFooter="True" Width="501px"
                                OnRowDataBound="gvRegVendorList_RowDataBound">
                                <RowStyle />
                                <Columns>


                                    <asp:TemplateField HeaderText="SL">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvRVLSlNo" runat="server" Height="16px" Font-Size="X-Small"
                                                Style="text-align: right" Font-Bold="true"
                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="Vendor Id">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvRVLvendorid" runat="server" Font-Size="X-Small"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "vendorid")) %>'
                                                Width="50px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                        <FooterStyle HorizontalAlign="Center" />
                                        <HeaderStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="Vendor Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvRVLName" runat="server" Font-Size="X-Small"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "companyname")) %>'
                                                Width="150px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Center" />
                                        <HeaderStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="License No">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvRVLlicenseno" runat="server" Font-Size="X-Small"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "licenseno")) %>'
                                                Width="80px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                        <FooterStyle HorizontalAlign="Center" />
                                        <HeaderStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="Mobile">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvRVLmobile" runat="server" Font-Size="X-Small"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mobile")) %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                        <FooterStyle HorizontalAlign="Center" />
                                        <HeaderStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Email">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvRVLemail" runat="server" Font-Size="X-Small"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "email")) %>'
                                                Width="150px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Center" />
                                        <HeaderStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Concern Person">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvRVLConcernPerson" runat="server" Font-Size="X-Small"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "username")) %>'
                                                Width="150px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Center" />
                                        <HeaderStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Verify">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lbtngvRVLvarify" runat="server" Font-Size="X-Small" CssClass="btn btn-sm btn-primary"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "varify")) %>' 
                                                CommandArgument='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "id")) %>'
                                                OnClientClick="return confirm('Are you sure you want to change.');"
                                                OnClick="lbtngvRVLvarify_Click"></asp:LinkButton>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center"/> 
                                        <FooterStyle HorizontalAlign="Center" />
                                        <HeaderStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Business Type">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvRVLbusiness" runat="server" Font-Size="X-Small"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "businesstype")) %>'
                                                Width="100px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Center" />
                                        <HeaderStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Experience">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvRVLexperience" runat="server" Font-Size="X-Small"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "experience")) %>'
                                                Width="50px"></asp:Label>
                                        </ItemTemplate>
                                         <ItemStyle HorizontalAlign="Center" />
                                        <FooterStyle HorizontalAlign="Center" />
                                        <HeaderStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Owner Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvRVLownername" runat="server" Font-Size="X-Small" 
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "ownername")) %>'
                                                Width="150px"></asp:Label>
                                        </ItemTemplate>
                                         <ItemStyle HorizontalAlign="Center" />
                                        <FooterStyle HorizontalAlign="Center" />
                                        <HeaderStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Payment Schedule">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvRVLpaymntschdle" runat="server" Font-Size="X-Small" 
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "paymntschdle")) %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" />
                                        <HeaderStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>

                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:HyperLink ID="lbtngvRVLdetails" runat="server"  ToolTip="Details Info"
                                                NavigateUrl='<%# "~/F_14_Pro/RegisteredVendorDetails?id=" + Convert.ToString(DataBinder.Eval(Container.DataItem, "id")) %>'
                                                Target="_blank"
                                                ><i class="fa fa-id-card"></i></asp:HyperLink>
                                        </ItemTemplate>
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
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
