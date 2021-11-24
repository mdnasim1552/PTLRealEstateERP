<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="RptLandInventory.aspx.cs" Inherits="RealERPWEB.F_01_LPA.RptLandInventory" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <script type="text/javascript">


        $(document).ready(function () {
            //alert("I m IN");
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);

        });



        function pageLoaded() {


            try
            {
                $('.chzn-select').chosen({ search_contains: true });
            }

            catch (e) {

                alert(e.message)
            }


        }




       
    </script>
    <style>
        .grvHeader, .grvFooter {
            font-size: 16px !important;
        }

        .grvRows {
            font-size: 12px !important;
        }
    </style>

    
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            


            <div class="card card-fluid">
                <div class="card-body">
                    <div class="row">

                         <div class="col-md-1">

                            <div class="form-group">
                                
                                        <span class=" control-label lblmargin-top9px lblleftwidth80">Project</span>
                                      
                                    </div>


                                </div>
                        <div class="col-md-4">

                            <div class="form-group">
                               

                                    <asp:DropDownList ID="ddlProject" runat="server" CssClass="form-control chzn-select"  ClientIDMode="Static">
                                    </asp:DropDownList>

                                </div>
                            </div>
                          <div class="col-md-1">

                            <div class="form-group">
                                <asp:LinkButton id="lbtnShow" class="btn btn-primary btn-xs"  OnClick="lbtnShow_Click" runat="server" Style="margin-left:10px;">Show</asp:LinkButton>
                                                         
                            </div>
                        </div>


                      
                    </div>
                    
                    <asp:GridView ID="gvlandinven" runat="server"
                            AutoGenerateColumns="False"
                          
                            ShowFooter="True" Width="1000px" CssClass="table-striped table-hover table-bordered grvContentarea" OnRowCreated="gvlandinven_RowCreated" OnRowDataBound="gvlandinven_RowDataBound">
                            <PagerSettings PageButtonCount="20" Mode="NumericFirstLast" />
                            <Columns>
                                <asp:TemplateField HeaderText="Sl.No.">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvSlNo" runat="server" Font-Bold="False" Font-Size="12px"
                                            Style="text-align: right"
                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                    </ItemTemplate>
                                    
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="CS">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvcsdhagno" runat="server" Font-Bold="False" Font-Size="12px"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "csdhagno"))   %>'
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lblgvFTotal" runat="server" BackColor="Transparent" BorderStyle="None" BorderWidth="1px" Font-Bold="True" Font-Size="11px"
                                            Style="text-align: left" Width="70px" Text="Total"></asp:Label>
                                    </FooterTemplate>
                               
                                        
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="SA">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvsadhagno" runat="server" Font-Bold="False" Font-Size="12px"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sadhagno"))   %>'
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="RS">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvrsdhagno" runat="server" Font-Bold="False" Font-Size="12px"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsdhagno"))   %>'
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="BS">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvbsdhagno" runat="server" Font-Bold="False" Font-Size="12px"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "bsdhagno"))   %>'
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>


                                
                                <asp:TemplateField HeaderText="CS">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvcslarea" runat="server" Font-Bold="False" Font-Size="12px"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "cslarea")).ToString("#,##0.0000;(#,##0.0000); ")   %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lblgvFcslarea" runat="server" BackColor="Transparent" BorderStyle="None" BorderWidth="1px" Font-Bold="True" Font-Size="11px"
                                            Style="text-align: right" Width="70px"></asp:Label>
                                    </FooterTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="Right" />
                                </asp:TemplateField>

                                 
                                <asp:TemplateField HeaderText="BS">
                                 
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvbslarea" runat="server" Font-Bold="False" Font-Size="12px"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "bslarea")).ToString("#,##0.0000;(#,##0.0000); ")   %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                       
                                     <FooterTemplate>
                                        <asp:Label ID="lblgvFbslarea" runat="server" BackColor="Transparent" BorderStyle="None" BorderWidth="1px" Font-Bold="True" Font-Size="11px"
                                            Style="text-align: right" Width="70px"></asp:Label>
                                    </FooterTemplate>
                                      <FooterStyle HorizontalAlign="Right" />
                                    <ItemStyle HorizontalAlign="Right" />

                                </asp:TemplateField>

                                
                                <asp:TemplateField HeaderText="Khotian No">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvbskhotianno" runat="server" Font-Bold="False" Font-Size="12px"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "bskhotianno"))   %>'
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                 <asp:TemplateField HeaderText="Acre of Land">
                                    
                                     
                                     
                                     <ItemTemplate>
                                        <asp:Label ID="lblgvbsklarea" runat="server" Font-Bold="False" Font-Size="12px"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "bsklarea")).ToString("#,##0.0000;(#,##0.0000); ")   %>'
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>
                                      <FooterTemplate>
                                        <asp:Label ID="lblgvFbsklarea" runat="server" BackColor="Transparent" BorderStyle="None" BorderWidth="1px" Font-Bold="True" Font-Size="11px"
                                            Style="text-align: right" Width="80px"></asp:Label>
                                    </FooterTemplate>
                                      <FooterStyle HorizontalAlign="Right" />
                                    <ItemStyle HorizontalAlign="Right" />

                                </asp:TemplateField>

                                
                                <asp:TemplateField HeaderText="JBL Ref. No">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvjblrefno" runat="server" Font-Bold="False" Font-Size="12px"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "jblrefno"))   %>'
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                 <asp:TemplateField HeaderText="JBL Purchase of Land">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvjblpurland" runat="server" Font-Bold="False" Font-Size="12px"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "budarea")).ToString("#,##0.0000;(#,##0.0000); ")   %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                     <FooterTemplate>
                                        <asp:Label ID="lblgvFjblpurland" runat="server" BackColor="Transparent" BorderStyle="None" BorderWidth="1px" Font-Bold="True" Font-Size="11px"
                                            Style="text-align: right" Width="70px"></asp:Label>
                                    </FooterTemplate>
                                      <FooterStyle HorizontalAlign="Right" />
                                    <ItemStyle HorizontalAlign="Right" />

                                </asp:TemplateField>

                                 <asp:TemplateField HeaderText="JBL Total Land">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvjbltland" runat="server" Font-Bold="False" Font-Size="12px"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "budarea")).ToString("#,##0.0000;(#,##0.0000); ")   %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>

                                      <FooterTemplate>
                                        <asp:Label ID="lblgvFjbltland" runat="server" BackColor="Transparent" BorderStyle="None" BorderWidth="1px" Font-Bold="True" Font-Size="11px"
                                            Style="text-align: right" Width="70px"></asp:Label>
                                    </FooterTemplate>
                                      <FooterStyle HorizontalAlign="Right" />
                                    <ItemStyle HorizontalAlign="Right" />



                                </asp:TemplateField>

                                 <asp:TemplateField HeaderText="Rest of Land">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvrestofland" runat="server" Font-Bold="False" Font-Size="12px"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "restlarea")).ToString("#,##0.0000;(#,##0.0000); ")    %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                      <FooterTemplate>
                                        <asp:Label ID="lblgvFrestofland" runat="server" BackColor="Transparent" BorderStyle="None" BorderWidth="1px" Font-Bold="True" Font-Size="11px"
                                            Style="text-align: right" Width="70px"></asp:Label>
                                    </FooterTemplate>
                                      <FooterStyle HorizontalAlign="Right" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>

                                   <asp:TemplateField HeaderText="Namzari">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvnamzri" runat="server" Font-Bold="False" Font-Size="12px"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "purarea")).ToString("#,##0.0000;(#,##0.0000); ")   %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>

                                        <FooterTemplate>
                                        <asp:Label ID="lblgvFnamzri" runat="server" BackColor="Transparent" BorderStyle="None" BorderWidth="1px" Font-Bold="True" Font-Size="11px"
                                            Style="text-align: right" Width="70px"></asp:Label>
                                    </FooterTemplate>
                                      <FooterStyle HorizontalAlign="Right" />
                                    <ItemStyle HorizontalAlign="Right" />
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
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
