<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="RptSupplierDueStatus.aspx.cs" Inherits="RealERPWEB.F_14_Pro.RptSupplierDueStatus" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="../CSS/PageInformation.css" rel="stylesheet" type="text/css" />

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script lang="javascript" type="text/javascript" src="../Scripts/jquery-1.4.1.min.js"></script>
    <script lang="javascript" type="text/javascript" src="../Scripts/ScrollableGridPlugin.js"></script>
    
      <script language="javascript" type="text/javascript">
        $(document).ready(function () {
            //For navigating using left and right arrow of the keyboard
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);
        });
        function pageLoaded() {

            $("input, select").bind("keydown", function (event) {
                var k1 = new KeyPress();
                k1.textBoxHandler(event);
            });


           <%-- var gridview = $('#<%=this.gvReqInfo.ClientID %>');
            $.keynavigation(gridview);--%>
            //$(".chzn-select").chosen(); $(".chzn-select-deselect").chosen({ allow_single_deselect: true });
            $('.chzn-select').chosen({ search_contains: true });




        };


        //function loadModal() {
        //    $('#detialsinfo').modal('toggle');
        //}

       
      </script>


   <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>

    <div class="container moduleItemWrpper">
        <div class="contentPart">
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
            <div class="row">
                <fieldset class="scheduler-border fieldset_A">

                    <div class="form-horizontal">             
                        <div class="row">

                  
                            <div class="form-group">
                                <div class="col-md-6">
                                    <asp:Label ID="Label8" runat="server" CssClass="lblTxt lblName" Text="Supplier Name:"></asp:Label>
                                    <asp:TextBox ID="txtSrcSupplier" runat="server" CssClass=" inputtextbox"></asp:TextBox>

                                    <asp:LinkButton ID="imgbtnFindSupplier" runat="server" CssClass="btn btn-primary srearchBtn" OnClick="imgbtnFindSupplier_Click"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>

                                    <asp:DropDownList ID="ddlSupplierName" runat="server" CssClass=" form-control chzn-select" Width="355px">
                                    </asp:DropDownList>
                                    </div>

                                    <div class="col-md-1">
                                    <asp:LinkButton ID="lnkbtnOk" runat="server" OnClick="lnkbtnOk_Click" CssClass="btn btn-primary okBtn">Ok</asp:LinkButton>

                                    </div>

                                </div>
                                                 
                            <div class="form-group">
                                <div class="col-md-3 pading5px asitCol3">
                                    <asp:Label ID="Label9" runat="server" CssClass="lblTxt lblName" Text="Date:"></asp:Label>
                                    <asp:TextBox ID="txttodate" runat="server" CssClass=" inputtextbox"></asp:TextBox>
                                    <cc1:CalendarExtender ID="txttodate_CalendarExtender" runat="server" Enabled="True"
                                        Format="dd-MMM-yyyy" TargetControlID="txttodate"></cc1:CalendarExtender>

                                    <asp:Label ID="lblPage" runat="server" Visible="False" Text="Page Size" CssClass="lblName lblTxt"></asp:Label>

                             

                                    <asp:DropDownList ID="ddlpagesize" runat="server" AutoPostBack="True" CssClass="ddlPage" OnSelectedIndexChanged="ddlpagesize_SelectedIndexChanged"
                                        Visible="False">
                                        <asp:ListItem>10</asp:ListItem>
                                        <asp:ListItem>20</asp:ListItem>
                                        <asp:ListItem>30</asp:ListItem>
                                        <asp:ListItem>50</asp:ListItem>
                                        <asp:ListItem>100</asp:ListItem>
                                        <asp:ListItem>150</asp:ListItem>
                                        <asp:ListItem>200</asp:ListItem>
                                        <asp:ListItem>300</asp:ListItem>
                                    </asp:DropDownList>

                                </div>
                            </div>
                    
                          </div>
                    </div>
                </fieldset>
            </div>
            <div class="table table-responsive">
  <asp:GridView ID="gvSupDueStatus" runat="server" AutoGenerateColumns="False" ShowFooter="True" CssClass=" table-striped table-hover table-bordered grvContentarea">
                <PagerSettings Position="Top" />

                <Columns>
                    <asp:TemplateField HeaderText="Sl">
                        <ItemTemplate>
                            <asp:Label ID="lblgvSlNo" runat="server" Font-Bold="True" Height="16px" Style="text-align: right"
                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="35px"></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Invoice Number">
                        <ItemTemplate>
                            <asp:Label ID="lblgvBillNo" runat="server" Height="16px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "billno")) %>'
                                Width="90px"></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="left" VerticalAlign="Top" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Invoice Date">
                        <ItemTemplate>
                            <asp:Label ID="lblgvBillDate" runat="server" Height="16px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "billdate")) %>'
                                Width="80px"></asp:Label>
                        </ItemTemplate>
                        <FooterTemplate>
                                    <asp:Label ID="lgvFInvAmtD" runat="server" Font-Bold="True" Font-Size="12px"
                                        ForeColor="Black" Style="text-align: right">Total :</asp:Label>
                                </FooterTemplate>
                        <HeaderStyle HorizontalAlign="left" VerticalAlign="Top" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Invoice Amount">
                        <ItemTemplate>
                            <asp:Label ID="lblgvInvAmt" runat="server" Height="16px" Text='<%#Convert.ToDouble(DataBinder.Eval(Container.DataItem, "billamt")).ToString("#,##0;(#,##0); ")%>'
                                Width="50px"></asp:Label>
                        </ItemTemplate>

                             <FooterTemplate>
                                    <asp:Label ID="lgvFgvInvAmt" runat="server" Font-Bold="True" Font-Size="12px"
                                        ForeColor="Black" Style="text-align: right"></asp:Label>
                                </FooterTemplate>
                        <HeaderStyle HorizontalAlign="left" VerticalAlign="Top" />
                        <ItemStyle HorizontalAlign="Right" />
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Paid Amount">
                        <ItemTemplate>
                            <asp:Label ID="lblgvInvAmtp" runat="server" Height="16px" Text='<%#Convert.ToDouble(DataBinder.Eval(Container.DataItem, "paidamt")).ToString("#,##0;(#,##0); ")%>'
                                Width="70px"></asp:Label>
                        </ItemTemplate>

                        <FooterTemplate>
                                    <asp:Label ID="lgvFInvAmtp" runat="server" Font-Bold="True" Font-Size="12px"
                                        ForeColor="Black" Style="text-align: right"></asp:Label>
                                </FooterTemplate>
                        <HeaderStyle HorizontalAlign="left" VerticalAlign="Top" />
                        <ItemStyle HorizontalAlign="Right" />
                    </asp:TemplateField>


                    <asp:TemplateField HeaderText="Dues Balance">
                        <ItemTemplate>
                            <asp:Label ID="lblgvUnpAmt" runat="server" Height="16px" Text='<%#Convert.ToDouble(DataBinder.Eval(Container.DataItem, "unpaid")).ToString("#,##0;(#,##0); ")%>'
                                Width="70px"></asp:Label>
                        </ItemTemplate>

                           <FooterTemplate>
                                    <asp:Label ID="lgvFUnpAmt" runat="server" Font-Bold="True" Font-Size="12px"
                                        ForeColor="Black" Style="text-align: right"></asp:Label>
                                </FooterTemplate>
                        <HeaderStyle HorizontalAlign="left" VerticalAlign="Top" />
                        <ItemStyle HorizontalAlign="Right" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Aging Days">
                        <ItemTemplate>
                            <asp:Label ID="lblgvAginDays" runat="server" Height="16px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "asingdays")) %>'
                                Width="50px"></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="left" VerticalAlign="Top" />
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
  
                        <%--<tr>
                            <td class="style63" style="text-align: left">
                                &nbsp;
                            </td>
                            <td class="style8" style="text-align: left">
                                <asp:Label ID="Label8" runat="server" Font-Bold="True" Font-Size="12px" Style="text-align: left;
                                    color: #FFFFFF;" Text="Supplier Name:" Width="100px"></asp:Label>
                            </td>
                            <td class="style9" style="text-align: left">
                                <asp:TextBox ID="txtSrcSupplier" runat="server" CssClass="txtboxformat" Font-Bold="True"
                                    Width="80px" BorderStyle="None"></asp:TextBox>
                            </td>
                            <td class="style10" style="text-align: left">
                                <asp:ImageButton ID="imgbtnFindSupplier" runat="server" Height="17px" ImageUrl="~/Image/find_images.jpg"
                                    OnClick="imgbtnFindSupplier_Click" Width="16px" />
                            </td>
                            <td class="style11" style="text-align: left">
                                <asp:DropDownList ID="ddlSupplierName" runat="server" Font-Bold="True" Font-Size="12px"
                                    Width="400px">
                                </asp:DropDownList>
                                <cc1:ListSearchExtender ID="ddlSupplierName_ListSearchExtender" runat="server" QueryPattern="Contains"
                                    TargetControlID="ddlSupplierName">
                                </cc1:ListSearchExtender>
                            </td>
                            <td class="style12" style="text-align: left">
                                <asp:LinkButton ID="lnkbtnOk" runat="server" BackColor="#000066" BorderColor="#000"
                                    BorderStyle="Solid" BorderWidth="1px" Font-Bold="True" Font-Size="12px" ForeColor="#000"
                                    OnClick="lnkbtnOk_Click" Style="text-align: center" Width="60px">Ok</asp:LinkButton>
                            </td>
                            
                            <td style="text-align: left">
                                &nbsp;
                            </td>
                        </tr>--%>
                        <tr>
                            <td class="style45" style="text-align: left"></td>
                            <td class="style45" colspan="6" style="text-align: left">
                                <table>
                                    <tr>
                                        <%--<td class="style45" style="text-align: left">
                                            <asp:Label ID="Label9" runat="server" Font-Bold="True" Font-Size="12px" Style="color: #FFFFFF;
                                                text-align: left;" Text="Date:" Width="70px"></asp:Label>
                                        </td>--%>
                                        <td class="style45" style="text-align: left">
                                            <%--<asp:TextBox ID="txtFDate" runat="server" CssClass="txtboxformat" Width="80px" 
                                                                    BorderStyle="None"></asp:TextBox>
                                                                <cc1:CalendarExtender ID="txtFDate_CalendarExtender" runat="server" 
                                                                    Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txtFDate">
                                                                </cc1:CalendarExtender>--%>
                                        </td>
                                        <td class="style13" style="text-align: left">
                                            <%--<asp:Label ID="Label10" runat="server" Font-Bold="True" Font-Size="12px" 
                                                                    style="text-align: right; color: #FFFFFF;" Text="To:"></asp:Label>--%>
                                        </td>
                                        <%--<td class="style45" style="text-align: left">
                                            <asp:TextBox ID="txttodate" runat="server" CssClass="txtboxformat" Font-Bold="False"
                                                Width="80px" BorderStyle="None"></asp:TextBox>
                                            <cc1:CalendarExtender ID="txttodate_CalendarExtender" runat="server" Enabled="True"
                                                Format="dd-MMM-yyyy" TargetControlID="txttodate">
                                            </cc1:CalendarExtender>
                                        </td>--%>
                                        <%--<td class="style45" style="text-align: left">
                                            <asp:Label ID="lblPage" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000"
                                                Style="color: #FFFFFF; text-align: right;" Text="Page Size" Visible="False" Width="60px"></asp:Label>
                                        </td>--%>
                                        <%--<td>
                                            <asp:DropDownList ID="ddlpagesize" runat="server" AutoPostBack="True" BackColor="#CCFFCC"
                                                Font-Bold="True" Font-Size="12px" ForeColor="#3366FF" OnSelectedIndexChanged="ddlpagesize_SelectedIndexChanged"
                                                Visible="False">
                                                <asp:ListItem>10</asp:ListItem>
                                                <asp:ListItem>20</asp:ListItem>
                                                <asp:ListItem>30</asp:ListItem>
                                                <asp:ListItem>50</asp:ListItem>
                                                <asp:ListItem>100</asp:ListItem>
                                                <asp:ListItem>150</asp:ListItem>
                                                <asp:ListItem>200</asp:ListItem>
                                                <asp:ListItem>300</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>--%>
                                    </tr>
                                </table>
                            </td>


                        </tr>
            
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
