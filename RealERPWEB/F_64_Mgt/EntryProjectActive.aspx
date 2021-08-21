<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="EntryProjectActive.aspx.cs" Inherits="RealERPWEB.F_64_Mgt.EntryProjectActive" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">


 <script  type="text/javascript">

     $(document).ready(function () {
         Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);

     });
     function pageLoaded() {


         $("input, select").bind("keydown", function (event) {
             var k1 = new KeyPress();
             k1.textBoxHandler(event);

         });



         var gv = $('#<%=this.gvProLinkInfo.ClientID %>');
            gv.Scrollable();
        }
 </script>


    
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>

            <div class="container moduleItemWrpper">
                <div class="contentPart">
                    <div class="row">
                        <fieldset class="scheduler-border fieldset_A">

                            <div class="col-md-3 pading5px asitCol3">
                                

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


                         <asp:GridView ID="gvProLinkInfo" runat="server" CssClass="table-striped table-hover table-bordered grvContentarea"
                                AutoGenerateColumns="False" ShowFooter="True" Width="16px"
                               >
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
                                  

                                    <asp:TemplateField HeaderText=" Code" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvBancCode" runat="server" Height="16px"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactcode")) %>'
                                                Width="80px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Project Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvSuplDesc1" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc")) %>'
                                                Width="350px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:LinkButton ID="lbtnUpdate" runat="server" Font-Bold="True" CssClass=" btn  btn-danger primarygrdBtn" OnClick="lbtnUpdate_Click">Final Update</asp:LinkButton>
                                        </FooterTemplate>
                                        <FooterStyle HorizontalAlign="Left" />
                                        <HeaderStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>

                                     <asp:TemplateField HeaderText="Active">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkActive" runat="server"
                                            Checked='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "active"))=="True" %>' />
                                    </ItemTemplate>
                                    <%--<EditItemTemplate>
                                                    
                                                </EditItemTemplate>--%>
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
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

