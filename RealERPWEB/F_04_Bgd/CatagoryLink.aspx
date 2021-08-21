<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="CatagoryLink.aspx.cs" Inherits="RealERPWEB.F_04_Bgd.CatagoryLink" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <link href="../CSS/Style.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">


    <script type="text/javascript" language="javascript">
        $(document).ready(function () {
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);


        });
        function pageLoaded() {

            $('#<%=this.gvcat.ClientID%>').tblScrollable();


        }
    </script>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="container moduleItemWrpper">
                <div class="contentPart">
                    <div class="row">
                      

                        <asp:GridView ID="gvcat" runat="server" CssClass=" table-striped table-hover table-bordered grvContentarea"
                            AutoGenerateColumns="False" CellPadding="4" Font-Size="12px" PageSize="15"
                            ShowFooter="True" OnRowDataBound="gvcat_RowDataBound">
                            <PagerSettings NextPageText="Next" PreviousPageText="Previous" Position="Top" />
                            <FooterStyle BackColor="#5F9467" Font-Bold="True" ForeColor="#000" />
                            <Columns>
                                <asp:TemplateField HeaderText="Sl.No.">
                                    <ItemTemplate>
                                        <asp:Label ID="lblserialnoid" runat="server" Style="text-align: right"
                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:LinkButton ID="lbtnTotal" runat="server" OnClick="lbtnTotal_Click" CssClass="btn btn-primary btn-xs">Total</asp:LinkButton>
                                    </FooterTemplate>

                                    <HeaderStyle Font-Bold="True" Font-Size="16px" />
                                    <ItemStyle Font-Size="12px" />
                                </asp:TemplateField>


                                <asp:TemplateField HeaderText="Description">


                                    <ItemTemplate>
                                        <asp:Label ID="lblgvdesc" runat="server" Font-Size="12px" Style="font-size: 12px"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sirdesc")) %>'
                                            Width="300px"></asp:Label>
                                    </ItemTemplate>

                                   

                                    <FooterTemplate>
                                          <asp:LinkButton ID="lbtnSame" runat="server" OnClick="lbtnSame_Click" CssClass="btn btn-primary btn-xs">Same</asp:LinkButton>
                                        <asp:LinkButton ID="lbtnUpdateMat" runat="server" OnClick="lbtnUpdateMat_Click" CssClass="btn btn-danger btn-xs">Final Update</asp:LinkButton>
                                    </FooterTemplate>
                                    <HeaderStyle Font-Bold="True" Font-Size="16px" HorizontalAlign="Left" />
                                    <ItemStyle Font-Size="12px" HorizontalAlign="Left" />
                                </asp:TemplateField>




                                <asp:TemplateField HeaderText="Unit">

                                    <ItemTemplate>
                                        <asp:Label ID="lblgvunit" runat="server" BorderColor="#99CCFF"
                                            BorderStyle="Solid" BorderWidth="0px" Font-Size="11px"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem,  "sirunit")) %>'
                                            Width="60px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Font-Size="16px" HorizontalAlign="Center" />
                                    <ItemStyle Font-Size="12px" HorizontalAlign="left" />
                                </asp:TemplateField>




                                <asp:TemplateField HeaderText="Catagory">

                                    <ItemTemplate>
                                          <asp:DropDownList ID="ddlcatagory" runat="server" CssClass="  ddlPage">
                                                </asp:DropDownList>
                                    </ItemTemplate>
                                    <HeaderStyle Font-Size="16px" HorizontalAlign="Center" />
                                    <ItemStyle Font-Size="12px" HorizontalAlign="left" />
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


