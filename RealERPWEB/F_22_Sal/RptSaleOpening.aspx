<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="RptSaleOpening.aspx.cs" Inherits="RealERPWEB.F_22_Sal.RptSaleOpening" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
  
     

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">


      <script type="text/javascript" language="javascript">
          $(document).ready(function () {
              Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);
          });
          function pageLoaded() {


            
              //$('#tblrpSaleOpensum').Scrollable({

              //});


        }

      </script>


    
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
                </fieldset>
                <asp:MultiView ID="MultiView1" runat="server">
                    <asp:View ID="View1" runat="server">



                        <asp:Repeater ID="rpSaleOpensum" runat="server" OnItemDataBound="rpSaleOpensum_ItemDataBound">

                            <HeaderTemplate>
                                <table  id="tblrpSaleOpensum"   class=" table-striped table-hover table-bordered grvContentarea">
                                    <tr>
                                        <th>SL</th>
                                        <th>Project Name</th>
                                        <th>Opening Amt</th>
                                    </tr>
                            </HeaderTemplate>
                            <ItemTemplate>

                                <tr>
                                    <td>
                                        <asp:Label ID="lblgvSlNo" runat="server" Font-Bold="True" Height="16px"
                                            Style="text-align: right"
                                            Text='<%# Convert.ToString(Container.ItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="lgvProjectName" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc")) %>'
                                            Width="200px"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblgvopnamt" runat="server" BackColor="Transparent"
                                            BorderStyle="None" Style="text-align: right; font-size: 11px"
                                            Text='<%#  Convert.ToDouble(DataBinder.Eval(Container.DataItem, "opnamt")).ToString("#,##0;(#,##0); ") %>'
                                            Width="80px">
                                        </asp:Label>


                                    </td>
                                </tr>

                            </ItemTemplate>

                            <FooterTemplate>

                                 <tr>
                                    <th>
                                        
                                       

                                    </th>
                                    <th>
                                       <asp:Label ID="lblrpFTotal" runat="server" Font-Bold="True" Height="16px"
                                            Style="text-align: right"
                                            Text="Total" Width="200px"></asp:Label> 
                                    </th>
                                    <th>
                                        <asp:Label ID="lgvFToAmt" runat="server" Font-Bold="True" Font-Size="12px"
                                            ForeColor="Black" Style="text-align: right" Width="80px"  
                                            Text='<%#  Convert.ToDouble(DataBinder.Eval(Container.DataItem, "opnamt")).ToString("#,##0;(#,##0); ") %>'></asp:Label>
                                      


                                    </th>
                                </tr>


                                </table>
                            </FooterTemplate>





                        </asp:Repeater>



                        <asp:GridView ID="gvSaleOpensum" runat="server" AutoGenerateColumns="False" CssClass=" table-striped table-hover table-bordered grvContentarea"
                            ShowFooter="True">
                            <RowStyle />
                            <Columns>
                                <asp:TemplateField HeaderText="Sl.No.">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvSlNo" runat="server" Font-Bold="True" Height="16px"
                                            Style="text-align: right"
                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="50px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Project Name" FooterText="Total">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvProjectName" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc")) %>'
                                            Width="200px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle Font-Bold="True" Font-Size="12px" ForeColor="Black" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Opening Amt.">
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFToAmt" runat="server" Font-Bold="True" Font-Size="12px"
                                            ForeColor="Black" Style="text-align: right" Width="80px"></asp:Label>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvopnamt" runat="server" BackColor="Transparent"
                                            BorderStyle="None" Style="text-align: right; font-size: 11px"
                                            Text='<%#  Convert.ToDouble(DataBinder.Eval(Container.DataItem, "opnamt")).ToString("#,##0;(#,##0); ") %>'
                                            Width="80px">
                                        </asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                            </Columns>
                            <FooterStyle CssClass="grvFooter" />
                            <EditRowStyle />
                            <AlternatingRowStyle />
                            <PagerStyle CssClass="gvPagination" />
                            <HeaderStyle CssClass="grvHeader" />
                        </asp:GridView>
                    </asp:View>
                </asp:MultiView>
            </div>
        </div>
    </div>


</asp:Content>

