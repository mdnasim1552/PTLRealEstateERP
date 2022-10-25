<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="Notice.aspx.cs" Inherits="RealERPWEB.Notices.Notice" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .page-title {
            font-size: 1.30rem;
        }
    </style>
    <script>

        function RedirNoticeCreate() {

            window.open('CreateNotice.aspx', '_blank');
        }

    </script>
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
            <div class="container-fluid mt-4">
   
                <div class="row mt-2">
                    <div class="card card-fluid" style="min-height: 550px; width:100%;">
                        <div class="card-header">
                                         <div class="row">
                    <div class="col-md-12">
                        <div class="page-title d-inline ">Notices</div>
                        <div class="float-right d-inline">
                            <asp:LinkButton ID="lbtnCreateNotice" runat="server" CssClass="btn btn-primary bt-xs" OnClick="lbtnCreateNotice_Click">Add Notice</asp:LinkButton>
                        </div>
                    </div>
                </div>
                        </div>
                        <div class="card-body">
                            <div class="table-responsive">
                                <asp:GridView ID="grvNotice" runat="server" AllowPaging="True" OnRowDataBound="grvNotice_RowDataBound"
                                    CssClass="table table-centered table-striped dt-responsive nowrap w-100 dataTable no-footer dtr-inline table-borderless border-0"
                                    AutoGenerateColumns="False" Font-Size="12px" OnPageIndexChanging="grvNotice_PageIndexChanging"
                                    PageSize="50" ShowFooter="True">
                                    <PagerSettings NextPageText="Next" PreviousPageText="Previous" />
                                    <FooterStyle Font-Bold="True" />

                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl" HeaderStyle-Width="30px">
                                            <ItemTemplate>
                                                <asp:Label ID="lblserialnoid" runat="server" Style="text-align: center"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle Font-Bold="True" Font-Size="13px" />
                                            <ItemStyle Font-Size="12px" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Notice Id">
                                            <ItemTemplate>
                                                <asp:Label ID="lblcompid" runat="server"
                                                    Text='<%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "nid"))%>'
                                                    Width="100px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Font-Size="12px" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Notice Title">
                                            <ItemTemplate>
                                                <asp:Label ID="lblcreatetask" runat="server" Font-Size="12px"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem,"ntitle")) %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle Font-Bold="True" Font-Size="13px" />
                                            <ItemStyle Font-Size="12px" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Details" HeaderStyle-VerticalAlign="Middle" HeaderStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                    <asp:Label ID="lbltaskdesc" runat="server" Font-Size="12px"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "ndetails")) %>'
                                                        Width="300px"></asp:Label></a>
                                            </ItemTemplate>
                                            <HeaderStyle Font-Bold="True" Font-Size="13px" />
                                            <ItemStyle Font-Size="12px" />
                                        </asp:TemplateField>
                                        
                                        <asp:TemplateField HeaderText="Attatchment">
                                            <ItemTemplate>
                                                 <asp:HyperLink runat="server" CssClass="text-info" NavigateUrl='<%#Eval("files")%>' Target="_blank"><i class="fa fa-paperclip"></i> </asp:HyperLink>
                                            </ItemTemplate>
                                      
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="User Name" HeaderStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:Label ID="lblTicketType" runat="server" Font-Size="12px"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "usrname")) %>'
                                                    Width="100px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle Font-Bold="True" Font-Size="13px" />
                                            <ItemStyle Font-Size="12px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Status" HeaderStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:Label ID="lblAssign" runat="server" Font-Size="12px"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "nstatus")) %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle Font-Bold="True" Font-Size="13px" />
                                            <ItemStyle Font-Size="12px" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Email">
                                            <ItemTemplate>
                                               <asp:CheckBox ID="chkEmail" runat="server" />
                                            </ItemTemplate>
                                            <HeaderStyle Font-Bold="True" Font-Size="13px" />
                                            <ItemStyle Font-Size="12px" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="SMS">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkSMS" runat="server" />
                                            </ItemTemplate>
                                            <HeaderStyle Font-Bold="True" Font-Size="13px" />
                                            <ItemStyle Font-Size="12px" />
                                        </asp:TemplateField>
                                    </Columns>
                                    <AlternatingRowStyle BackColor="" />
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
