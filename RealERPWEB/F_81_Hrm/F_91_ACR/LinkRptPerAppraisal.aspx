<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="LinkRptPerAppraisal.aspx.cs" Inherits="RealERPWEB.F_81_Hrm.F_91_ACR.LinkRptPerAppraisal" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<script runat="server">

   
  
   
</script>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

  
    <script type="text/javascript">
        $(document).ready(function () {
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);
        });
        function pageLoaded() {
        }

    </script>




        
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
    <div class="container moduleItemWrpper">
        <div class="contentPart">
            <div class="row">


            </div>

           <%-- <div class="row">--%>
             
                        <asp:GridView ID="gvEmpper" runat="server"
                            AutoGenerateColumns="False" CssClass="table-striped table-hover table-bordered grvContentarea" ShowFooter="True">
                            <RowStyle />
                            <Columns>
                                <asp:TemplateField HeaderText="Sl.No.">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvSlNo1" runat="server" Font-Bold="True"
                                            Style="text-align: right"
                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                  <ItemStyle HorizontalAlign="right" />

                                </asp:TemplateField>

                        <asp:TemplateField HeaderText="Employee Code" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvEmpName" runat="server"
                                            Text=' <%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "empid")) %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Month">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvSectionadesig" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "date1")) %>'
                                            Width="40px" Font-Bold="True" Font-Size="11px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>


                                <asp:TemplateField HeaderText="Narration">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvEmpName" runat="server"
                                            Text=' <%#Convert.ToString(DataBinder.Eval(Container.DataItem, "rmks")) %>'
                                            Width="200px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                  <ItemStyle HorizontalAlign="left" />

                                </asp:TemplateField>


                  

                            </Columns>
                            <FooterStyle CssClass="grvFooter" />
                            <EditRowStyle />
                            <AlternatingRowStyle />
                            <PagerStyle CssClass="gvPagination" />
                            <HeaderStyle CssClass="grvHeader" />
                        </asp:GridView>
          <%--         
            </div>--%>
        </div>
    </div>

        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

