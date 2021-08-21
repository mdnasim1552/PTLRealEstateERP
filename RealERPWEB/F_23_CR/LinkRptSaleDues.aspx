<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="LinkRptSaleDues.aspx.cs" Inherits="RealERPWEB.F_23_CR.LinkRptSaleDues" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <link href="../CSS/Style.css" rel="stylesheet" type="text/css" />
    <link href="../CSS/PageInformation.css" rel="stylesheet" type="text/css" />


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript">
      
        $(document).ready(function () {
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);

        });
        function pageLoaded() {
           <%-- var gv = $('#<%=this.dgvAccRec.ClientID %>');
            gv.Scrollable();--%>


            var dgvAccRec02 = $('#<%=this.dgvAccRec02.ClientID %>');

            dgvAccRec02.gridviewScroll({
                width: 1160,
                height: 420,             
                arrowsize: 30,
                railsize: 16,
                barsize: 8,
                varrowtopimg: "../Image/arrowvt.png",
                varrowbottomimg: "../Image/arrowvb.png",
                harrowleftimg: "../Image/arrowhl.png",
                harrowrightimg: "../Image/arrowhr.png",
                freezesize: 7
            });

        }

        function loadModal() {
            $('#custimage').modal('toggle');
        }

       

        
    </script>


    <%--<asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>--%>
    <div class="container moduleItemWrpper">
        <div class="contentPart">
            <div class="row">
                <fieldset class="scheduler-border fieldset_A">

                    <div class="form-horizontal">
                        <div class="form-group">
                            <div class=" col-md-12  pading5px">

                                <asp:Label ID="Label1" CssClass="lblTxt lblName" runat="server" Text="Project Name:"></asp:Label>
                                <asp:Label ID="lblProjectName" CssClass=" smLbl_to" runat="server" Text="Project Name:"></asp:Label>



                                <asp:Label ID="Label2" CssClass="lblTxt lblName" runat="server" Text="Date"></asp:Label>
                                <asp:Label ID="lbldaterange" CssClass="smLbl_to" runat="server" Text="Date"></asp:Label>


                                <asp:Label ID="lbljavascript" runat="server"></asp:Label>


                           
                                <asp:Label ID="lblPage" CssClass="lblTxt lblName" runat="server" Text="Page"></asp:Label>
                                <asp:DropDownList ID="ddlpagesize" runat="server" AutoPostBack="True" CssClass="ddlPage" OnSelectedIndexChanged="ddlpagesize_SelectedIndexChanged">
                                    <asp:ListItem>10</asp:ListItem>
                                    <asp:ListItem>15</asp:ListItem>
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
                </fieldset>
            </div>
            <asp:MultiView ID="MultiView1" runat="server">
                <asp:View ID="ViewRecList02" runat="server">

                    <div class="table row">
                        <asp:GridView ID="dgvAccRec02" runat="server" AllowPaging="True" CssClass=" table-striped table-hover table-bordered grvContentarea"
                            AutoGenerateColumns="False" OnPageIndexChanging="dgvAccRec02_PageIndexChanging"
                            ShowFooter="True" Style="text-align: left" Width="654px"
                            OnRowDataBound="dgvAccRec02_RowDataBound">
                            <RowStyle />
                            <Columns>
                                <asp:TemplateField HeaderText="Sl">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvSlNo0" runat="server"
                                            Font-Bold="True" Height="16px"
                                            Style="text-align: right"
                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    
                                    <ItemTemplate>
                                         <asp:LinkButton ID="lbtnsalesImg" runat="server" ToolTip="Customer & Nominee's Image" OnClick="lbtnsalesImg_OnClick"><span class="glyphicon glyphicon-picture"></span></asp:LinkButton>
                                    </ItemTemplate>
                                    
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Usircode" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lgusircode" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "usircode")) %>'
                                            Width="140px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Project Name">
                                           
                                       <HeaderTemplate>
                                        <table style="width: 47%;">
                                            <tr>
                                                <td class="style58">
                                                    <asp:Label ID="Label4" runat="server" Font-Bold="True"
                                                        Text="Project Name" Width="180px"></asp:Label>
                                                </td>
                                               <td>

                                                    <asp:HyperLink ID="hlbtntbCdataExel1" CssClass="btn brn-default btn-xs " runat="server"><span class="fa fa-file-excel"></span></asp:HyperLink>

                                                  
                                                </td>
                                            </tr>
                                        </table>
                                    </HeaderTemplate>
                                    
                                    
                                    
                                    <ItemTemplate>
                                        <asp:Label ID="lgactdesc02" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc")) %>'
                                            Width="140px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Cutomer Name">
                                    <ItemTemplate>
                                         

                                        <asp:LinkButton ID="lbtngacuname" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "custname"))
                                                                       %>'
                                            Width="150px"
                                            Style="color: Black; text-align: left; font-weight: bold; text-decoration: none;"
                                            OnClick="lbtngacuname_Click"></asp:LinkButton>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" />



                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Unit Desc.">
                                    <ItemTemplate>
                                        <asp:Label ID="lgudesc01" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "udesc")) %>'
                                            Width="90px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Flat Size">
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFunitsize" runat="server"
                                            Font-Bold="True" Font-Size="12px"
                                            ForeColor="#000" Style="text-align: right"></asp:Label>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lgvunitsize" runat="server" Style="text-align: right"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "usize")).ToString("#,##0;(#,##0); ") %>'
                                            Width="60px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Rate">
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFavgrate" runat="server"
                                            Font-Bold="True" Font-Size="12px"
                                            ForeColor="#000" Style="text-align: right"></asp:Label>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lgvrate" runat="server" Style="text-align: right"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "aptrate")).ToString("#,##0;(#,##0); ") %>'
                                            Width="60px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Apartment Price">
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFaptcost" runat="server"
                                            Font-Bold="True" Font-Size="12px"
                                            ForeColor="#000" Style="text-align: right"></asp:Label>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lgvaptcost" runat="server" Style="text-align: right"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "aptcost")).ToString("#,##0;(#,##0); ") %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Car Parking">
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFcpcost" runat="server"
                                            Font-Bold="True" Font-Size="12px"
                                            ForeColor="#000" Style="text-align: right"></asp:Label>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lgvcpcost" runat="server" Style="text-align: right"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "cpcost")).ToString("#,##0;(#,##0); ") %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Utility ">
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFutilitycost"
                                            runat="server" Font-Bold="True"
                                            Font-Size="12px" ForeColor="#000" Style="text-align: right"></asp:Label>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lgvutilitycost" runat="server" Style="text-align: right"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "utltycost")).ToString("#,##0;(#,##0); ") %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Others">
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFothcost" runat="server"
                                            Font-Bold="True" Font-Size="12px"
                                            ForeColor="#000" Style="text-align: right"></asp:Label>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lgvothescost" runat="server" Style="text-align: right"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "othcost")).ToString("#,##0;(#,##0); ") %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Total Value">
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFtocost" runat="server"
                                            Font-Bold="True" Font-Size="12px"
                                            ForeColor="#000" Style="text-align: right"></asp:Label>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lgvtocsot" runat="server"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "tocost")).ToString("#,##0;(#,##0); ") %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Encash">
                                    <FooterTemplate>
                                        <asp:Label ID="lgFEncash" runat="server"
                                            ForeColor="#000"></asp:Label>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lgvEncash" runat="server" BackColor="Transparent"
                                            BorderStyle="None" Font-Size="11px" Height="18px" Style="text-align: right"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "reconamt")).ToString("#,##0;(#,##0); ") %>'
                                            Width="60px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="right"
                                        Font-Size="11px" Font-Bold="True" />
                                </asp:TemplateField>


                                <asp:TemplateField HeaderText="Returned">
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFtretamt" runat="server" Font-Bold="True"
                                            Font-Size="12px" ForeColor="#000" Style="text-align: right" Width="80px"></asp:Label>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lgvtretamt" runat="server" Font-Size="11PX"
                                            Style="text-align: right"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "retcheque")).ToString("#,##0;(#,##0); ") %>'
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="right" />
                                </asp:TemplateField>


                                <asp:TemplateField HeaderText="Today's">
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFtframt" runat="server" Font-Bold="True"
                                            Font-Size="12px" ForeColor="#000" Style="text-align: right" Width="80px"></asp:Label>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lgvtframt" runat="server" Font-Size="11PX"
                                            Style="text-align: right"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "fcheque")).ToString("#,##0;(#,##0); ") %>'
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="right" />
                                </asp:TemplateField>



                                <asp:TemplateField HeaderText="Post Dated">
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFtpdamt" runat="server" Font-Bold="True"
                                            Font-Size="12px" ForeColor="#000" Style="text-align: right" Width="80px"></asp:Label>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lgvtpdamt" runat="server" Font-Size="11PX"
                                            Style="text-align: right"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "pcheque")).ToString("#,##0;(#,##0); ") %>'
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="right" />
                                </asp:TemplateField>













                                <asp:TemplateField HeaderText="Total Received">
                                    <FooterTemplate>
                                        <asp:HyperLink ID="hlnkgvFtoreceived"
                                            runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000" Target="_blank"
                                            Style="text-align: right" Width="100px"></asp:HyperLink>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lgvtotreceived" runat="server"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ramt")).ToString("#,##0;(#,##0); ") %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Total Dues">
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFatodues" runat="server"
                                            Font-Bold="True" Font-Size="12px"
                                            ForeColor="#000" Style="text-align: right"></asp:Label>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lgvatodues" runat="server"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "atodues")).ToString("#,##0;(#,##0); ") %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Dues Upto Dec">
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFtotaldues"
                                            runat="server" Font-Bold="True" Font-Size="12px"
                                            ForeColor="#000" Style="text-align: right"></asp:Label>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lgvtotaldues" runat="server"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "todues")).ToString("#,##0;(#,##0); ") %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Dues Balance">
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFtodues" runat="server"
                                            Font-Bold="True" Font-Size="12px"
                                            ForeColor="#000" Style="text-align: right"></asp:Label>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lgvtodues" runat="server"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "bamt")).ToString("#,##0;(#,##0); ") %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Previous Booking">
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFpbooking" runat="server"
                                            Font-Bold="True" Font-Size="12px"
                                            ForeColor="#000" Style="text-align: right"></asp:Label>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lgvtodues0" runat="server"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "pbookam")).ToString("#,##0;(#,##0); ") %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Previous Installment">
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFpinstallment"
                                            runat="server" Font-Bold="True"
                                            Font-Size="12px" ForeColor="#000" Style="text-align: right"></asp:Label>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lgvtodues1" runat="server"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "pinsam")).ToString("#,##0;(#,##0); ") %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Total Over Dues Amt">
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFpretodues"
                                            runat="server" Font-Bold="True" Font-Size="12px"
                                            ForeColor="#000" Style="text-align: right"></asp:Label>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lgvtodues2" runat="server"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ptodues")).ToString("#,##0;(#,##0); ") %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Current Booking ">
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFCbooking" runat="server"
                                            Font-Bold="True" Font-Size="12px"
                                            ForeColor="#000" Style="text-align: right"></asp:Label>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lgvCbooking" runat="server"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "cbookam")).ToString("#,##0;(#,##0); ") %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Current Installment ">
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFCinstallment"
                                            runat="server" Font-Bold="True"
                                            Font-Size="12px" ForeColor="#000" Style="text-align: right"></asp:Label>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lgvCinstallment" runat="server"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "cinsam")).ToString("#,##0;(#,##0); ") %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Total Current Dues ">
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFtoCInstalment"
                                            runat="server" Font-Bold="True"
                                            Font-Size="12px" ForeColor="#000" Style="text-align: right"></asp:Label>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lgvCoCInstalment" runat="server"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ctodues")).ToString("#,##0;(#,##0); ") %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>



                                <asp:TemplateField HeaderText="Total Due">
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFvtodues" runat="server"
                                            Font-Bold="True" Font-Size="12px"
                                            ForeColor="#000" Style="text-align: right"></asp:Label>
                                    </FooterTemplate>
                                    <ItemTemplate>




                                        <asp:Label ID="lblgvvtodues" runat="server"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "vtodues")).ToString("#,##0;(#,##0); ") %>'
                                            Width="70px"
                                            BorderStyle="None" Font-Size="11px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>


                                <asp:TemplateField HeaderText="Delay Charge">
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFdelcharge"
                                            runat="server" Font-Bold="True" Font-Size="12px"
                                            ForeColor="#000" Style="text-align: right"></asp:Label>
                                    </FooterTemplate>

                                    <ItemTemplate>
                                        <asp:Label ID="lblgvdelcharge" runat="server"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "cdelay")).ToString("#,##0;(#,##0); ") %>'
                                            Width="70px"
                                            BorderStyle="None" Font-Size="12px"></asp:Label>
                                    </ItemTemplate>




                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>



                                <asp:TemplateField HeaderText="Return Cheque">
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFdischarge"
                                            runat="server" Font-Bold="True" Font-Size="12px"
                                            ForeColor="#000" Style="text-align: right"></asp:Label>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lgvdischarge" runat="server"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "discharge")).ToString("#,##0;(#,##0); ") %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Net Total Dues">
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFnettodues"
                                            runat="server" Font-Bold="True" Font-Size="12px"
                                            ForeColor="#000" Style="text-align: right"></asp:Label>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lbtngvnettodues" runat="server"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ntodues")).ToString("#,##0;(#,##0); ") %>'
                                            Width="70px" Style="color: Black; text-align: right; font-weight: bold; text-decoration: none;"
                                            OnClick="lbtngvnettodues_Click"></asp:LinkButton>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
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
                    <asp:Panel ID="pnlIndPro" runat="server" Visible="False">
                        <asp:Label runat="server" CssClass="btn btn-success primaryBtn">Note</asp:Label>

                        <hr class=" hrline" />
                        <div class="table table-responsive">
                            <asp:GridView ID="gvinpro" runat="server" AutoGenerateColumns="False" CssClass=" table-striped table-hover table-bordered grvContentarea"
                                ShowFooter="True" Width="337px">
                                <PagerSettings Position="Top" />
                                <RowStyle />
                                <Columns>
                                    <asp:TemplateField HeaderText="Sl.No.">
                                        <ItemTemplate>
                                            <asp:Label ID="serialnoid0" runat="server"
                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="10px"></asp:Label>
                                        </ItemTemplate>

                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="Decription">
                                        <ItemTemplate>
                                            <asp:Label ID="lbldesinpro" runat="server" BackColor="Transparent"
                                                BorderColor="Transparent" BorderStyle="None" Font-Size="12px" ForeColor="Black"
                                                TabIndex="76"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "codedesc")) %>'
                                                Width="60px"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Unit">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvtounit" runat="server" BackColor="Transparent"
                                                BorderColor="Transparent" BorderStyle="None" Font-Size="12px" ForeColor="Black"
                                                TabIndex="76"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "unumber")).ToString("#,##0;(#,##0); ") %>'
                                                Width="80px"></asp:Label>
                                            <itemstyle horizontalalign="Right" />
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Total Unit Size">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvtounsize" runat="server" BackColor="Transparent"
                                                BorderColor="Transparent" BorderStyle="None" Font-Size="12px" ForeColor="Black"
                                                TabIndex="76"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "usize")).ToString("#,##0;(#,##0); ") %>'
                                                Width="80px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Rate">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvtourate" runat="server" BackColor="Transparent"
                                                BorderColor="Transparent" BorderStyle="None" Font-Size="12px" ForeColor="Black"
                                                TabIndex="76"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rate")).ToString("#,##0;(#,##0); ") %>'
                                                Width="80px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Amount">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvtouamt" runat="server" BackColor="Transparent"
                                                BorderColor="Transparent" BorderStyle="None" Font-Size="12px" ForeColor="Black"
                                                TabIndex="76"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amount")).ToString("#,##0;(#,##0); ") %>'
                                                Width="80px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="Precentate">
                                        <ItemStyle HorizontalAlign="Right" />
                                        <ItemTemplate>
                                            <asp:Label ID="lgvtoupercent" runat="server" BackColor="Transparent"
                                                BorderColor="Transparent" BorderStyle="None" Font-Size="12px" ForeColor="Black"
                                                TabIndex="76"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "percnt")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="80px"></asp:Label>
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
                    </asp:Panel>

                </asp:View>
                      <asp:View ID="Viewweeklycolleciton" runat="server">
                          <asp:GridView ID="grvWeekSales" runat="server" AutoGenerateColumns="False"
                                        Font-Size="10px" HorizontalAlign="Left" PageSize="5" ShowFooter="True"
                                        CssClass="table-striped table-hover table-bordered grvContentarea gvTopHeader" OnRowDataBound="grvWeekSales_RowDataBound">
                                        <RowStyle />
                                        <Columns>
                                            <asp:TemplateField HeaderText="Sl">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvSwlNo1" runat="server" Font-Bold="True"
                                                        Style="text-align: right"
                                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"
                                                        CssClass="gridtext"></asp:Label>
                                                </ItemTemplate>
                                                <ControlStyle Width="20px" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" Width="20px" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Days">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblprodNmId1" runat="server" Width="55px"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "wcode1")) %>'></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <p>
                                                        <asp:Label runat="server" ID="lbl1">Week Total</asp:Label>
                                                    </p>
                                                    <p>
                                                        <asp:Label runat="server" ID="lblyFT">-</asp:Label>
                                                    </p>

                                                </FooterTemplate>
                                                <HeaderStyle Font-Size="10px" />
                                                <ItemStyle HorizontalAlign="Left" />
                                                <FooterStyle HorizontalAlign="Right" Font-Size="10px" Font-Bold="true" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Sales">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblyAmt1" runat="server" Width="55px"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "wsamt1")).ToString("#,##0;-#,##0; ") %>'></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <p>
                                                        <asp:Label runat="server" ID="lblyFAmt1">-</asp:Label>
                                                    </p>
                                                    <p>
                                                        <asp:Label runat="server" ID="lblyFAmt1T">-</asp:Label>
                                                    </p>

                                                </FooterTemplate>
                                                <HeaderStyle Font-Size="10px" />
                                                <ItemStyle HorizontalAlign="Right" />
                                                <FooterStyle HorizontalAlign="Right" Font-Size="10px" Font-Bold="true" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Collection">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblyCollamt1" runat="server" Width="55px"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "wcamt1")).ToString("#,##0;-#,##0; ") %>'></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <p>
                                                        <asp:Label runat="server" ID="lblyFCollamt1">-</asp:Label>
                                                    </p>
                                                    <p>
                                                        <asp:Label runat="server" ID="lblyFCollamt1T">-</asp:Label>
                                                    </p>

                                                </FooterTemplate>
                                                <HeaderStyle Font-Size="10px" HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="Right" />
                                                <FooterStyle HorizontalAlign="Right" Font-Size="10px" Font-Bold="true" />
                                            </asp:TemplateField>


                                            <asp:TemplateField HeaderText="Days">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblprodNmId2" runat="server" Width="55px"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "wcode2")) %>'></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <p>
                                                        <asp:Label runat="server" ID="lbl2">Week Total</asp:Label>
                                                    </p>
                                                    <p>
                                                        <asp:Label runat="server" ID="lblyFT2">Sub-Total:</asp:Label>
                                                    </p>

                                                </FooterTemplate>
                                                <HeaderStyle Font-Size="10px" />
                                                <FooterStyle HorizontalAlign="Right" Font-Size="10px" Font-Bold="true" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Sales">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblyAmt2" runat="server" Width="55px"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "wsamt2")).ToString("#,##0;-#,##0; ") %>'></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <p>
                                                        <asp:Label runat="server" ID="lblyFAmt2">-</asp:Label>
                                                    </p>
                                                    <p>
                                                        <asp:Label runat="server" ID="lblyFAmt2T">-</asp:Label>
                                                    </p>

                                                </FooterTemplate>
                                                <HeaderStyle Font-Size="10px" />
                                                <ItemStyle HorizontalAlign="Right" />
                                                <FooterStyle HorizontalAlign="Right" Font-Size="10px" Font-Bold="true" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Collection">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblyCollamt2" runat="server" Width="55px"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "wcamt2")).ToString("#,##0;-#,##0; ") %>'></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <p>
                                                        <asp:Label runat="server" ID="lblyFCollamt2">-</asp:Label>
                                                    </p>
                                                    <p>
                                                        <asp:Label runat="server" ID="lblyFCollamt2T">-</asp:Label>
                                                    </p>

                                                </FooterTemplate>
                                                <HeaderStyle Font-Size="10px" HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="Right" />
                                                <FooterStyle HorizontalAlign="Right" Font-Size="10px" Font-Bold="true" />
                                            </asp:TemplateField>


                                            <asp:TemplateField HeaderText="Days">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblprodNmId3" runat="server" Width="55px"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "wcode3")) %>'></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <p>
                                                        <asp:Label runat="server" ID="lbl3">Week Total</asp:Label>
                                                    </p>
                                                    <p>
                                                        <asp:Label runat="server" ID="lblyFT3">Sub-Total:</asp:Label>
                                                    </p>

                                                </FooterTemplate>
                                                <HeaderStyle Font-Size="10px" />
                                                <FooterStyle HorizontalAlign="Right" Font-Size="10px" Font-Bold="true" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Sales">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblyAmt3" runat="server" Width="55px"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "wsamt3")).ToString("#,##0;-#,##0; ") %>'></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <p>
                                                        <asp:Label runat="server" ID="lblyFAmt3">-</asp:Label>
                                                    </p>
                                                    <p>
                                                        <asp:Label runat="server" ID="lblyFAmt3T">-</asp:Label>
                                                    </p>

                                                </FooterTemplate>
                                                <ItemStyle HorizontalAlign="Right" />
                                                <HeaderStyle Font-Size="10px" />
                                                <FooterStyle HorizontalAlign="Right" Font-Size="10px" Font-Bold="true" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Collection">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblyCollamt3" runat="server" Width="55px"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "wcamt3")).ToString("#,##0;-#,##0; ") %>'></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <p>
                                                        <asp:Label runat="server" ID="lblyFCollamt3">-</asp:Label>
                                                    </p>
                                                    <p>
                                                        <asp:Label runat="server" ID="lblyFCollamt3T">-</asp:Label>
                                                    </p>

                                                </FooterTemplate>
                                                <HeaderStyle Font-Size="10px" HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="Right" />
                                                <FooterStyle HorizontalAlign="Right" Font-Size="10px" Font-Bold="true" />
                                            </asp:TemplateField>


                                            <asp:TemplateField HeaderText="Days">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblprodNmId4" runat="server" Width="55px"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "wcode4")) %>'></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <p>
                                                        <asp:Label runat="server" ID="lbl4">Week Total</asp:Label>
                                                    </p>
                                                    <p>
                                                        <asp:Label runat="server" ID="lblyFT4">Gr Total:</asp:Label>
                                                    </p>

                                                </FooterTemplate>
                                                <HeaderStyle Font-Size="10px" />
                                                <FooterStyle HorizontalAlign="Right" Font-Size="10px" Font-Bold="true" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Sales">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblyAmt4" runat="server" Width="55px"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "wsamt4")).ToString("#,##0;-#,##0; ") %>'></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <p>
                                                        <asp:Label runat="server" ID="lblyFAmt4">-</asp:Label>
                                                    </p>
                                                    <p>
                                                        <asp:Label runat="server" ID="lblyFAmt4T">-</asp:Label>
                                                    </p>

                                                </FooterTemplate>
                                                <ItemStyle HorizontalAlign="Right" />
                                                <HeaderStyle Font-Size="10px" />
                                                <FooterStyle HorizontalAlign="Right" Font-Size="10px" Font-Bold="true" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Collection">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblyCollamt4" runat="server" Width="55px"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "wcamt4")).ToString("#,##0;-#,##0; ") %>'></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <p>
                                                        <asp:Label runat="server" ID="lblyFCollamt4">-</asp:Label>
                                                    </p>
                                                    <p>
                                                        <asp:Label runat="server" ID="lblyFCollamt4T">-</asp:Label>
                                                    </p>

                                                </FooterTemplate>
                                                <HeaderStyle Font-Size="10px" HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="Right" />
                                                <FooterStyle HorizontalAlign="Right" Font-Size="10px" Font-Bold="true" />
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

            <asp:Panel ID="PnlRmrks" runat="server" Visible="False">
            </asp:Panel>
        </div>
    </div>


     <div class="modal fade" id="custimage" role="dialog">
                <div class="modal-dialog modal-lg">
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                            <h4 class="modal-title text-center">Customer and Nominee's Photo</h4>
                        </div>
                        <div class="modal-body">
                            <div class="row">
                                <div class="col-md-6" style="border-right:1px #000000 solid">
                                     <h4 class="text-center">Customer Photo </h4>
                                    <asp:ListView ID="ListCustomer" runat="server" ItemPlaceholderID="itemplaceholder" OnItemDataBound="ListCustomer_OnItemDataBound">
                                    <LayoutTemplate>
                                        <asp:PlaceHolder ID="itemplaceholder" runat="server" />
                                    </LayoutTemplate>
                                    <ItemTemplate>
                                        <div class="col-xs-12 col-sm-4 col-md-4 listDiv" style="padding: 0 5px;">
                                            <div id="EmpAll" runat="server">
                                                <asp:Label ID="ImgLink" Visible="false" runat="server" Text='<%# Eval("imgpath") %>'></asp:Label>
                                                <asp:Label ID="pactcode" Visible="false" runat="server" Text='<%# Eval("pactcode") %>'></asp:Label>
                                                <asp:Label ID="usircode" Visible="false" runat="server" Text='<%# Eval("usircode") %>'></asp:Label>                                               
                                                <asp:Image ID="GetImg" runat="server" CssClass="custimg image img img-responsive img-thumbnail" Height="180px"/>
                                            </div>

                                        </div>
                                    </ItemTemplate>
                                </asp:ListView>
                                </div>
                                <div class="col-md-6">
                                    <h4 class="text-center">Nominee Photo </h4>
                                    <asp:ListView ID="ListNominee" runat="server" ItemPlaceholderID="itemplaceholder" OnItemDataBound="ListNominee_OnItemDataBound">
                                    <LayoutTemplate>
                                        <asp:PlaceHolder ID="itemplaceholder" runat="server" />
                                    </LayoutTemplate>
                                    <ItemTemplate>
                                        <div class="col-xs-12 col-sm-4 col-md-4 listDiv" style="padding: 0 5px;">
                                            <div id="EmpAll" runat="server">
                                                <asp:Label ID="ImgLink" Visible="false" runat="server" Text='<%# Eval("imgpath") %>'></asp:Label>
                                                <asp:Label ID="pactcode" Visible="false" runat="server" Text='<%# Eval("pactcode") %>'></asp:Label>
                                                <asp:Label ID="usircode" Visible="false" runat="server" Text='<%# Eval("usircode") %>'></asp:Label>
                                                <asp:Image ID="GetImg" runat="server" CssClass="custimg image img img-responsive img-thumbnail" Height="180px"/>
                                            </div>

                                        </div>
                                    </ItemTemplate>
                                </asp:ListView>
                                </div>
                                
                            </div>

                        </div>
                        <div class="modal-footer">
                        </div>
                    </div>

                    

                </div>
            </div>





    <%-- </ContentTemplate>
                </asp:UpdatePanel>--%>
</asp:Content>





